using LuxVideoDet.Core.Algorithm.Results;
using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.Configuration.Models;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Algorithm.Pipeline;

/// <summary>
/// 算法工作者 — 拥有独立的帧队列和处理线程，
/// 使每个算法以自己的节奏异步推理，互不干涉。
/// </summary>
public sealed class AlgorithmWorker : IDisposable
{
    private readonly IDetectionAlgorithm _algorithm;
    private readonly AlgorithmConfig _config;
    private readonly ILogger _logger;

    private readonly Queue<Frame> _frameQueue = new();
    private readonly object _queueLock = new();
    private readonly int _capacity;
    private readonly SemaphoreSlim _workSignal = new(0, int.MaxValue);
    private long _droppedOldestCount;

    private Task? _processingTask;
    private CancellationTokenSource? _linkedCts;
    private bool _disposed;

    /// <summary>某个算法产出结果时触发</summary>
    public event Action<string, DetectionResult>? ResultAvailable;

    /// <summary>算法处理异常时触发</summary>
    public event Action<string, Exception>? Error;

    public string AlgorithmType => _config.AlgorithmType;
    public string DisplayName => _config.DisplayName;
    public IDetectionAlgorithm Algorithm => _algorithm;

    /// <summary>该工作者已处理的帧数</summary>
    public long ProcessedFrameCount { get; private set; }

    /// <summary>为给新帧腾位而丢弃的旧帧数（实时预览 / RTSP 式跳帧）。</summary>
    public long DroppedFrameCount => Interlocked.Read(ref _droppedOldestCount);

    /// <summary>
    /// 创建算法工作者。
    /// </summary>
    /// <param name="algorithm">算法实例（已初始化）</param>
    /// <param name="config">对应的算法配置</param>
    /// <param name="queueCapacity">帧队列容量；满时丢弃最旧帧以压低延迟，行为接近 RTSP 实时预览</param>
    /// <param name="logger">日志</param>
    public AlgorithmWorker(
        IDetectionAlgorithm algorithm,
        AlgorithmConfig config,
        int queueCapacity,
        ILogger logger)
    {
        _algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _capacity = Math.Max(1, queueCapacity);
    }

    /// <summary>
    /// 将帧送入队列；队列满时丢弃最旧帧并释放资源（实时预览允许跳帧，贴近 RTSP 处理语义）。
    /// </summary>
    public ValueTask EnqueueFrameAsync(Frame frame, CancellationToken cancellationToken = default)
    {
        lock (_queueLock)
        {
            while (_frameQueue.Count >= _capacity)
            {
                var stale = _frameQueue.Dequeue();
                stale.Dispose();
                Interlocked.Increment(ref _droppedOldestCount);
            }

            _frameQueue.Enqueue(frame);
        }

        _workSignal.Release();
        return default;
    }

    /// <summary>
    /// 启动工作者的处理循环。
    /// </summary>
    public void Start(CancellationToken externalToken)
    {
        _linkedCts = CancellationTokenSource.CreateLinkedTokenSource(externalToken);
        _processingTask = Task.Run(() => ProcessingLoopAsync(_linkedCts.Token), _linkedCts.Token);

        _logger.LogInformation(
            "[流水线·工作者] 启动 [{AlgorithmType}] 显示名={DisplayName}",
            AlgorithmType, DisplayName);
    }

    /// <summary>
    /// 停止工作者，完成队列写入端并取消令牌。
    /// </summary>
    public void Stop()
    {
        _linkedCts?.Cancel();
        try { _workSignal.Release(); } catch (ObjectDisposedException) { }
    }

    /// <summary>
    /// 等待处理循环结束（带超时）。
    /// </summary>
    public async Task WaitForCompletionAsync(TimeSpan timeout)
    {
        if (_processingTask != null)
        {
            try
            {
                await _processingTask.WaitAsync(timeout);
            }
            catch (TimeoutException)
            {
                _logger.LogWarning(
                    "[流水线·工作者] 等待 [{AlgorithmType}] 退出超时", AlgorithmType);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger.LogWarning(ex,
                    "[流水线·工作者] 等待 [{AlgorithmType}] 退出异常", AlgorithmType);
            }
        }
    }

    private async Task ProcessingLoopAsync(CancellationToken ct)
    {
        _logger.LogDebug("[流水线·工作者] [{AlgorithmType}] 进入处理循环", AlgorithmType);

        try
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    await _workSignal.WaitAsync(ct).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                Frame? frame;
                lock (_queueLock)
                {
                    if (_frameQueue.Count == 0)
                        continue;
                    frame = _frameQueue.Dequeue();
                }

                if (ct.IsCancellationRequested)
                {
                    frame.Dispose();
                    break;
                }

                try
                {
                    var result = _algorithm.Process(frame);
                    ProcessedFrameCount++;

                    ResultAvailable?.Invoke(AlgorithmType, result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "[流水线·工作者] [{AlgorithmType}] 处理帧失败（第 {N} 帧）",
                        AlgorithmType, ProcessedFrameCount + 1);
                    Error?.Invoke(AlgorithmType, ex);
                }
                finally
                {
                    frame.Dispose();
                }
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[流水线·工作者] [{AlgorithmType}] 处理循环异常退出", AlgorithmType);
            Error?.Invoke(AlgorithmType, ex);
        }

        _logger.LogInformation(
            "[流水线·工作者] [{AlgorithmType}] 处理循环结束 | 已处理={Count} | 丢旧帧(实时预览)={Dropped}",
            AlgorithmType, ProcessedFrameCount, DroppedFrameCount);
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        Stop();

        lock (_queueLock)
        {
            while (_frameQueue.Count > 0)
                _frameQueue.Dequeue().Dispose();
        }

        _workSignal.Dispose();

        try { _algorithm?.Dispose(); }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "[流水线·工作者] 释放算法 [{AlgorithmType}] 异常", AlgorithmType);
        }

        _linkedCts?.Dispose();
    }
}
