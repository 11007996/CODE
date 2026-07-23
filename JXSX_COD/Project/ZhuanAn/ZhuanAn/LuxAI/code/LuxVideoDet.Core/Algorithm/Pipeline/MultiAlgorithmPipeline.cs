using System.Collections.Concurrent;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Algorithm.Results;
using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Inference.Results;
using LuxVideoDet.Core.VideoSource;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Algorithm.Pipeline;

/// <summary>
/// 多算法流水线 — 从单一视频源读取帧，并行分发给多个 AlgorithmWorker。
/// <para>
/// 设计要点：
/// <list type="bullet">
///   <item>视频流只拉取一次，避免带宽浪费</item>
///   <item>每个算法拥有独立队列和处理线程，互不阻塞</item>
///   <item>队列满时丢弃最旧帧（实时预览 / RTSP 式跳帧），不阻塞取帧解码</item>
///   <item>单算法配置时零开销（无额外克隆）</item>
/// </list>
/// </para>
/// </summary>
public sealed class MultiAlgorithmPipeline : IDisposable
{
    private readonly IVideoSource _videoSource;
    private readonly List<AlgorithmWorker> _workers;
    private readonly ILogger _logger;
    private readonly int _maxConsecutiveReadFailures;

    private Task? _broadcastTask;
    private CancellationTokenSource? _cts;
    private bool _disposed;

    // ── 多算法结果合成（无锁设计，各 Worker 完全异步） ──
    private readonly ConcurrentDictionary<string, DetectionResult> _latestResults = new();
    private const int StateInfoYSpacing = 60;

    /// <summary>任意算法产出结果时触发</summary>
    public event Action<string, DetectionResult>? ResultAvailable;

    /// <summary>流水线级别错误（如视频源持续读取失败）</summary>
    public event Action<Exception>? PipelineError;

    /// <summary>单个算法处理错误</summary>
    public event Action<string, Exception>? AlgorithmError;

    /// <summary>流水线是否正在运行</summary>
    public bool IsRunning { get; private set; }

    /// <summary>广播器已读取的总帧数</summary>
    public long TotalFramesRead { get; private set; }

    /// <summary>所有工作者的只读视图</summary>
    public IReadOnlyList<AlgorithmWorker> Workers => _workers;

    /// <summary>
    /// 向指定算法工作者派发 UI 操作（与配置中 <c>algorithmType</c> 一致，如 <c>ucs</c>、<c>tearofftab</c>）。
    /// </summary>
    public bool TryInvokeUiAction(string algorithmType, string actionId, out string? errorMessage)
    {
        errorMessage = null;
        foreach (var w in _workers)
        {
            if (!string.Equals(w.AlgorithmType, algorithmType, StringComparison.OrdinalIgnoreCase))
                continue;

            if (w.Algorithm is IAlgorithmUiCommandHandler handler)
                return handler.TryInvokeUiAction(actionId, out errorMessage);

            errorMessage = "该算法未实现 UI 操作";
            return false;
        }

        errorMessage = $"未找到算法类型 {algorithmType} 的工作者";
        return false;
    }

    /// <summary>
    /// 创建多算法流水线。
    /// </summary>
    /// <param name="videoSource">已打开的视频源</param>
    /// <param name="algorithmPairs">算法实例与其配置的配对列表</param>
    /// <param name="logger">日志</param>
    /// <param name="queueCapacity">每个工作者的帧队列容量（默认 3）</param>
    /// <param name="maxConsecutiveReadFailures">连续读帧失败上限（默认 100）</param>
    public MultiAlgorithmPipeline(
        IVideoSource videoSource,
        List<(IDetectionAlgorithm Algorithm, AlgorithmConfig Config)> algorithmPairs,
        ILogger logger,
        int queueCapacity = 3,
        int maxConsecutiveReadFailures = 100)
    {
        _videoSource = videoSource ?? throw new ArgumentNullException(nameof(videoSource));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _maxConsecutiveReadFailures = maxConsecutiveReadFailures;

        if (algorithmPairs == null || algorithmPairs.Count == 0)
            throw new ArgumentException("至少需要一个算法", nameof(algorithmPairs));

        _workers = algorithmPairs
            .Select(pair => new AlgorithmWorker(pair.Algorithm, pair.Config, queueCapacity, logger))
            .ToList();

        foreach (var worker in _workers)
        {
            worker.ResultAvailable += OnWorkerResult;
            worker.Error += OnWorkerError;
        }

        var nominalFps = videoSource.GetFps();
        if (nominalFps > 0.01)
        {
            foreach (var w in _workers)
            {
                if (w.Algorithm is DetectionAlgorithmBase b)
                    b.SetSourceVideoFpsForDisplay(nominalFps);
            }
        }
    }

    /// <summary>
    /// 启动流水线：先启动所有 Worker，再启动帧广播循环。
    /// </summary>
    public void Start()
    {
        if (IsRunning) return;

        _cts = new CancellationTokenSource();
        IsRunning = true;

        foreach (var worker in _workers)
            worker.Start(_cts.Token);

        _broadcastTask = Task.Run(() => BroadcastLoopAsync(_cts.Token), _cts.Token);

        _logger.LogInformation(
            "[流水线·运行] 已启动 | 视频源={SourceType} | 并行工作者={WorkerCount}",
            _videoSource.SourceType, _workers.Count);
    }

    /// <summary>
    /// 安全停止流水线：取消广播、停止工作者、等待退出。
    /// </summary>
    public async Task StopAsync(TimeSpan? timeout = null)
    {
        if (!IsRunning) return;

        var stopTimeout = timeout ?? TimeSpan.FromSeconds(5);
        _logger.LogInformation("[流水线·运行] 正在停止…");

        _cts?.Cancel();

        // 等待广播循环退出
        if (_broadcastTask != null)
        {
            try
            {
                await _broadcastTask.WaitAsync(stopTimeout);
            }
            catch (TimeoutException)
            {
                _logger.LogWarning("[流水线·运行] 等待取帧广播任务结束超时");
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "[流水线·运行] 等待取帧广播任务结束异常");
            }
        }

        // 停止并等待所有工作者
        foreach (var worker in _workers)
            worker.Stop();

        var workerWaitTasks = _workers
            .Select(w => w.WaitForCompletionAsync(stopTimeout));
        await Task.WhenAll(workerWaitTasks);

        IsRunning = false;

        _logger.LogInformation(
            "[流水线·运行] 已停止 | 视频源共读取 {TotalFrames} 帧", TotalFramesRead);

        foreach (var w in _workers)
        {
            _logger.LogInformation(
                "[流水线·运行]   [{AlgorithmType}] 已处理={Processed} | 丢旧帧(实时预览)={Dropped}",
                w.AlgorithmType, w.ProcessedFrameCount, w.DroppedFrameCount);
        }
    }

    private async Task BroadcastLoopAsync(CancellationToken ct)
    {
        var firstFrame = true;
        var consecutiveFailures = 0;

        // 本地视频文件需要帧率节流，否则会以最大速度解码导致播放加速
        var needsThrottle = _videoSource.SourceType == "LocalVideo";
        var fps = _videoSource.GetFps();
        var frameIntervalMs = fps > 0 ? 1000.0 / fps : 0;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        long frameIndex = 0;

        _logger.LogDebug("[流水线] 进入取帧广播循环");

        while (!ct.IsCancellationRequested)
        {
            try
            {
                var frame = _videoSource.ReadFrame();

                if (frame == null || frame.IsEmpty)
                {
                    consecutiveFailures++;

                    if (consecutiveFailures == 1)
                    {
                        _logger.LogWarning(
                            "[流水线·取帧] 读取失败，IsOpened={IsOpened}",
                            _videoSource.IsOpened);
                    }

                    if (consecutiveFailures >= _maxConsecutiveReadFailures)
                    {
                        var error = new InvalidOperationException(
                            $"视频源连续 {consecutiveFailures} 帧读取失败");
                        _logger.LogError("[流水线·取帧] {Message}", error.Message);
                        PipelineError?.Invoke(error);
                        break;
                    }

                    await Task.Delay(100, ct);
                    continue;
                }

                consecutiveFailures = 0;
                TotalFramesRead++;
                frameIndex++;

                if (firstFrame)
                {
                    _logger.LogInformation(
                        "[流水线·取帧] 首帧就绪 {Width}x{Height}",
                        frame.Width, frame.Height);
                    firstFrame = false;
                }

                await DistributeFrameAsync(frame, ct).ConfigureAwait(false);

                // 本地视频按原始帧率节流，保持真实播放速度
                if (needsThrottle && frameIntervalMs > 0)
                {
                    var targetMs = frameIndex * frameIntervalMs;
                    var elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
                    var sleepMs = (int)(targetMs - elapsedMs);
                    if (sleepMs > 1)
                        await Task.Delay(sleepMs, ct);
                }
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[流水线·取帧] 广播循环异常");
                await Task.Delay(100, ct);
            }
        }

        _logger.LogDebug("[流水线] 取帧广播循环退出");
    }

    /// <summary>
    /// 将帧分发给所有工作者。
    /// 单工作者时直接传递原始帧（零克隆）；
    /// 多工作者时前 N-1 个收到克隆，最后一个收到原始帧。
    /// </summary>
    private async Task DistributeFrameAsync(Frame frame, CancellationToken ct)
    {
        if (_workers.Count == 1)
        {
            await _workers[0].EnqueueFrameAsync(frame, ct).ConfigureAwait(false);
            return;
        }

        for (int i = 0; i < _workers.Count - 1; i++)
        {
            var clone = frame.Clone();
            try
            {
                await _workers[i].EnqueueFrameAsync(clone, ct).ConfigureAwait(false);
            }
            catch
            {
                clone.Dispose();
                throw;
            }
        }

        await _workers[^1].EnqueueFrameAsync(frame, ct).ConfigureAwait(false);
    }

    private void OnWorkerResult(string algorithmType, DetectionResult result)
    {
        if (_workers.Count <= 1)
        {
            ResultAvailable?.Invoke(algorithmType, result);
            return;
        }

        // 先存储，再尝试合成 —— 存储本身是 ConcurrentDictionary 原子操作
        _latestResults[algorithmType] = result;

        // 只有自己一个算法有结果时直接透传，避免不必要的克隆开销
        if (_latestResults.Count <= 1)
        {
            ResultAvailable?.Invoke(algorithmType, result);
            return;
        }

        try
        {
            var compositeResult = CreateCompositeResult(algorithmType, result);
            ResultAvailable?.Invoke(algorithmType, compositeResult);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "[流水线] 多算法画框合成失败，已回退为单算法画面");
            ResultAvailable?.Invoke(algorithmType, result);
        }
    }

    /// <summary>
    /// 合成多算法结果。始终以「最新帧」（处理帧数最多的算法）的画面为底图，
    /// 叠加其他算法的检测框和区域轮廓。
    /// <para>
    /// 这样做的关键原因：快慢算法并行时，慢算法的帧远落后于快算法。
    /// 如果用慢算法的旧帧做底图，画面会「跳回过去」。始终以最快算法的
    /// 最新帧为底图，保证视频只前进不后退，慢算法的检测结果则叠加上去。
    /// </para>
    /// </summary>
    private DetectionResult CreateCompositeResult(string triggerAlgoType, DetectionResult triggerResult)
    {
        // ── 找到拥有最新帧的算法（处理帧数最多 = 帧最新） ──
        string baseAlgoType = triggerAlgoType;
        DetectionResult baseResult = triggerResult;
        long maxProcessed = 0;

        foreach (var worker in _workers)
        {
            if (!_latestResults.TryGetValue(worker.AlgorithmType, out var candidate))
                continue;

            if (worker.ProcessedFrameCount > maxProcessed)
            {
                maxProcessed = worker.ProcessedFrameCount;
                baseAlgoType = worker.AlgorithmType;
                baseResult = candidate;
            }
        }

        var baseMat = baseResult.AnnotatedFrame?.Mat;
        if (baseMat == null || baseMat.IsDisposed)
            return triggerResult;

        var compositeFrame = baseMat.Clone();
        var allDetections = new List<Detection>(baseResult.Detections);

        // ── 叠加所有非底图算法的轻量标注 ──
        foreach (var worker in _workers)
        {
            if (worker.AlgorithmType == baseAlgoType)
                continue;

            if (!_latestResults.TryGetValue(worker.AlgorithmType, out var otherResult))
                continue;

            try
            {
                worker.Algorithm.RenderAnnotations(compositeFrame, otherResult, 0);
                allDetections.AddRange(otherResult.Detections);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex,
                    "[流水线] [{AlgorithmType}] RenderAnnotations 异常，已跳过该路标注",
                    worker.AlgorithmType);
            }
        }

        return new DetectionResult
        {
            OriginalFrame = baseResult.OriginalFrame,
            AnnotatedFrame = new Frame(compositeFrame),
            Detections = allDetections,
            InferenceTime = baseResult.InferenceTime,
            TotalTime = triggerResult.TotalTime,
            ModelInferenceMs = triggerResult.ModelInferenceMs,
            InferenceTiming = triggerResult.InferenceTiming,
            PostProcessMs = triggerResult.PostProcessMs,
            RenderMs = triggerResult.RenderMs,
            Fps = baseResult.Fps,
            CurrentState = triggerResult.CurrentState,
            StateMessage = triggerResult.StateMessage,
            ShouldNotify = triggerResult.ShouldNotify,
            NotificationMessage = triggerResult.NotificationMessage,
            NotificationTitle = triggerResult.NotificationTitle,
            NotificationLevel = triggerResult.NotificationLevel,
            ShouldSaveImage = triggerResult.ShouldSaveImage,
            ShouldSaveVideo = triggerResult.ShouldSaveVideo,
            Judgement = triggerResult.Judgement,
            ExtraData = triggerResult.ExtraData,
            Timestamp = triggerResult.Timestamp
        };
    }

    private void OnWorkerError(string algorithmType, Exception error)
    {
        AlgorithmError?.Invoke(algorithmType, error);
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        StopAsync(TimeSpan.FromSeconds(3)).GetAwaiter().GetResult();

        foreach (var worker in _workers)
        {
            worker.ResultAvailable -= OnWorkerResult;
            worker.Error -= OnWorkerError;
            worker.Dispose();
        }

        try { _videoSource?.Dispose(); }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "[流水线] 释放视频源异常");
        }

        _cts?.Dispose();
    }
}
