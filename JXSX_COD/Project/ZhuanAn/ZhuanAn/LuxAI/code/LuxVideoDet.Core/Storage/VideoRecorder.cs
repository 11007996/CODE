using Microsoft.Extensions.Logging;
using OpenCvSharp;
using System.Threading.Channels;

namespace LuxVideoDet.Core.Storage;

public class RecordingTask
{
    public string TaskId { get; set; } = string.Empty;
    public DateTime TriggerTime { get; set; }
    public List<Mat> PreFramesRaw { get; set; } = [];
    public List<Mat> PreFramesRender { get; set; } = [];
    /// <summary>与 PreFrames* 对齐的采集时间（UTC），用于按实际墙钟计算输出视频帧率。</summary>
    public List<DateTime> PreFrameTimesUtc { get; set; } = [];
    public List<Mat> PostFramesRaw { get; set; } = [];
    public List<Mat> PostFramesRender { get; set; } = [];
    /// <summary>与 PostFrames* 对齐的采集时间（UTC）。</summary>
    public List<DateTime> PostFrameTimesUtc { get; set; } = [];
    public double DelaySeconds { get; set; }
    public int TargetPostFrames { get; set; }
    public bool IsCompleted { get; set; }

    public void AddPostFrame(Mat rawFrame, Mat renderFrame, DateTime capturedAtUtc)
    {
        PostFramesRaw.Add(rawFrame.Clone());
        PostFramesRender.Add(renderFrame.Clone());
        PostFrameTimesUtc.Add(capturedAtUtc);
    }

    /// <summary>
    /// 按首末帧采集时间间隔计算与真实时长一致的播放帧率（固定 FPS 封装用）。
    /// </summary>
    public double ComputePlaybackFps()
    {
        var n = PreFrameTimesUtc.Count + PostFrameTimesUtc.Count;
        if (n == 0) return 15;
        if (n == 1) return 15;

        var first = PreFrameTimesUtc.Count > 0 ? PreFrameTimesUtc[0] : PostFrameTimesUtc[0];
        var last = PostFrameTimesUtc.Count > 0 ? PostFrameTimesUtc[^1] : PreFrameTimesUtc[^1];
        var span = (last - first).TotalSeconds;
        if (span <= 1e-9)
            return Math.Clamp(n * 30.0, 1.0, 120.0);

        var fps = n / span;
        return Math.Clamp(fps, 1.0, 240.0);
    }

    public bool IsReadyToSave(DateTime currentTime)
    {
        if (IsCompleted)
            return true;

        if (PostFramesRaw.Count >= TargetPostFrames)
        {
            IsCompleted = true;
            return true;
        }

        if ((currentTime - TriggerTime).TotalSeconds >= DelaySeconds)
        {
            IsCompleted = true;
            return true;
        }

        return false;
    }

    public (List<Mat> rawFrames, List<Mat> renderFrames) GetAllFrames()
    {
        List<Mat> allRaw = [.. PreFramesRaw, .. PostFramesRaw];
        List<Mat> allRender = [.. PreFramesRender, .. PostFramesRender];

        return (allRaw, allRender);
    }

    public void Clear()
    {
        foreach (var frame in PreFramesRaw) frame?.Dispose();
        foreach (var frame in PreFramesRender) frame?.Dispose();
        foreach (var frame in PostFramesRaw) frame?.Dispose();
        foreach (var frame in PostFramesRender) frame?.Dispose();

        PreFramesRaw.Clear();
        PreFramesRender.Clear();
        PreFrameTimesUtc.Clear();
        PostFramesRaw.Clear();
        PostFramesRender.Clear();
        PostFrameTimesUtc.Clear();
    }
}

public class VideoRecorder : IDisposable
{
    private sealed class RecordingIngestJob : IDisposable
    {
        public Mat Raw { get; }
        public Mat Render { get; }

        public RecordingIngestJob(Mat raw, Mat render)
        {
            Raw = raw;
            Render = render;
        }

        public void Dispose()
        {
            Raw.Dispose();
            Render.Dispose();
        }
    }

    /// <summary>自动模式下「2K」宽度上限（QHD 常见横向像素）；源更宽才缩放，更窄保持原分辨率。</summary>
    private const int AutoMode2KMaxWidth = 2560;

    /// <summary>极高帧率时的安全上限（每秒最多保留帧数），与 <see cref="_bufferSeconds"/> 相乘得到队列长度上限。</summary>
    private const int MaxFramesPerSecondSafetyCap = 300;

    private readonly ILogger _logger;
    private readonly double _bufferSeconds;
    private readonly double _postBufferSeconds;
    private readonly int _maxBufferFrames;
    private readonly int _recordingMaxWidth;
    private readonly bool _ngVideoUseSourceResolution;
    private readonly int _maxConcurrentRecordings;

    /// <summary>环形缓冲：每元素为 (raw, render, 采集 UTC 时间)。</summary>
    private readonly Queue<(Mat Raw, Mat Render, DateTime CapturedAtUtc)> _frameBuffer;
    private readonly List<RecordingTask> _activeTasks;
    private readonly object _lock = new();

    private readonly Channel<IngestPacket> _ingestChannel;
    private readonly ChannelWriter<IngestPacket> _ingestWriter;
    private readonly CancellationTokenSource _ingestCts;
    private readonly Task _ingestTask;

    private bool _disposed;

    public VideoRecorder(
        ILogger logger,
        double bufferSeconds = 4.0,
        double postBufferSeconds = 4.0,
        int recordingMaxWidth = 0,
        bool ngVideoUseSourceResolution = false,
        int maxConcurrentRecordings = 2)
    {
        _logger = logger;
        _bufferSeconds = bufferSeconds;
        _postBufferSeconds = postBufferSeconds;
        _recordingMaxWidth = recordingMaxWidth;
        _ngVideoUseSourceResolution = ngVideoUseSourceResolution;
        _maxConcurrentRecordings = Math.Max(1, maxConcurrentRecordings);

        // 仅作极高 FPS 时的内存保护；正常情况由墙钟滑动窗口裁剪（见 AddFrameCore）。
        _maxBufferFrames = Math.Max(32, (int)Math.Ceiling(bufferSeconds * MaxFramesPerSecondSafetyCap * 1.5));

        _frameBuffer = [];
        _activeTasks = [];

        _ingestCts = new CancellationTokenSource();
        _ingestChannel = Channel.CreateBounded<IngestPacket>(new BoundedChannelOptions(64)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true,
            SingleWriter = false
        });
        _ingestWriter = _ingestChannel.Writer;
        _ingestTask = Task.Run(() => IngestLoopAsync(_ingestChannel.Reader, _ingestCts.Token));

        var recDesc = ngVideoUseSourceResolution
            ? "源分辨率"
            : (recordingMaxWidth <= 0 ? $"自动≤{AutoMode2KMaxWidth}" : recordingMaxWidth.ToString());
        _logger.LogDebug(
            "VideoRecorder初始化: 前置缓冲按墙钟保留最近{BufferSeconds}s(至多{MaxFrames}帧作高帧率保护), 后置{PostSeconds}s(按墙钟结束), 输出帧率=采集实况, 录像宽{RecMaxW}, 并发≤{Concurrent}",
            bufferSeconds, _maxBufferFrames, postBufferSeconds,
            recDesc,
            _maxConcurrentRecordings);
    }

    /// <summary>
    /// 将一帧送入环形缓冲并追加到进行中的录制任务。
    /// 推理线程上仅做缩放/Prepare 与入队；持锁写入与任务追加在后台摄取线程执行。
    /// </summary>
    public void AddFrame(Mat rawFrame, Mat renderFrame)
    {
        try
        {
            if (rawFrame == null || rawFrame.IsDisposed || rawFrame.Empty() ||
                renderFrame == null || renderFrame.IsDisposed || renderFrame.Empty())
                return;
        }
        catch (ObjectDisposedException)
        {
            return;
        }

        var rawRec = PrepareRecordingFrame(rawFrame);
        var renRec = PrepareRecordingFrame(renderFrame);
        if (rawRec == null || renRec == null)
        {
            rawRec?.Dispose();
            renRec?.Dispose();
            return;
        }

        var job = new RecordingIngestJob(rawRec, renRec);
        if (!_ingestWriter.TryWrite(new IngestPacket { FrameJob = job }))
        {
            job.Dispose();
            _logger.LogTrace("录像摄取队列已满，本帧未入队（丢弃一次环形缓冲更新）");
        }
    }

    /// <summary>摄取队列项：帧更新与 NG 触发互斥，由单消费者顺序处理。</summary>
    private readonly struct IngestPacket
    {
        public RecordingIngestJob? FrameJob { get; init; }
        /// <summary>非 null 表示 NG 录制触发（勿与 <see cref="FrameJob"/> 同时设置）。</summary>
        public string? TriggerTimestamp { get; init; }
    }

    private async Task IngestLoopAsync(ChannelReader<IngestPacket> reader, CancellationToken ct)
    {
        try
        {
            while (await reader.WaitToReadAsync(ct).ConfigureAwait(false))
            {
                while (reader.TryRead(out var packet))
                {
                    try
                    {
                        if (packet.TriggerTimestamp != null)
                            ProcessTriggerRecording(packet.TriggerTimestamp);
                        else if (packet.FrameJob != null)
                            AddFrameCore(packet.FrameJob);
                    }
                    finally
                    {
                        packet.FrameJob?.Dispose();
                    }
                }
            }
        }
        catch (OperationCanceledException) { }
    }

    private void AddFrameCore(RecordingIngestJob job)
    {
        var rawRec = job.Raw;
        var renRec = job.Render;
        var capturedAtUtc = DateTime.UtcNow;

        lock (_lock)
        {
            _frameBuffer.Enqueue((rawRec.Clone(), renRec.Clone(), capturedAtUtc));

            // 前置缓冲必须按墙钟裁剪：仅保留「最近 bufferSeconds 秒」内的帧。
            // 若只按最大帧数裁剪，在真实 FPS 较低时会堆积远超 bufferSeconds 的帧，
            // 触发 NG 时会把整段误当作「前置」，导致前置过长。
            while (_frameBuffer.Count > 0)
            {
                var oldest = _frameBuffer.Peek();
                if ((capturedAtUtc - oldest.CapturedAtUtc).TotalSeconds <= _bufferSeconds)
                    break;
                var drop = _frameBuffer.Dequeue();
                drop.Raw.Dispose();
                drop.Render.Dispose();
            }

            while (_frameBuffer.Count > _maxBufferFrames)
            {
                var old = _frameBuffer.Dequeue();
                old.Raw.Dispose();
                old.Render.Dispose();
            }

            // 必须在摄取路径上判定后置墙钟：若仅依赖后台保存循环里的 GetReadyTasks，
            // SaveTask（编码）阻塞时无法调用 IsReadyToSave，会导致 post 帧在整段阻塞期内持续堆积，
            // 成片可达数十秒且与 VideoDuration 无关。
            var nowLocal = DateTime.Now;
            foreach (var task in _activeTasks)
            {
                if (task.IsReadyToSave(nowLocal))
                    continue;
                task.AddPostFrame(rawRec, renRec, capturedAtUtc);
            }
        }
    }

    /// <summary>
    /// 请求创建 NG 录制任务：仅将触发入摄取队列，不在调用线程上克隆环形缓冲（避免推理/渲染卡顿）。
    /// 返回值表示是否成功入队；若摄取队列满则返回 null。并发达上限等判定在摄取线程执行，晚于队列中排在前面的帧处理。
    /// </summary>
    public string? TriggerRecording(string? timestamp = null)
    {
        if (_disposed)
            return null;

        timestamp ??= DateTime.Now.ToString("yyyyMMddHHmmss");

        var packet = new IngestPacket { TriggerTimestamp = timestamp };
        if (!_ingestWriter.TryWrite(packet))
        {
            _logger.LogWarning(
                "NG 录像触发未入队（摄取队列满），跳过 {TaskId}；可调大队列或检查摄取线程是否积压",
                timestamp);
            return null;
        }

        return timestamp;
    }

    /// <summary>
    /// 在摄取线程上快照环形缓冲并创建任务（重克隆，勿在推理线程调用）。
    /// </summary>
    private void ProcessTriggerRecording(string timestamp)
    {
        lock (_lock)
        {
            var stillCollecting = _activeTasks.Count(t => !t.IsCompleted);
            if (stillCollecting >= _maxConcurrentRecordings)
            {
                _logger.LogWarning(
                    "跳过 NG 录像任务 {TaskId}：正在采集的任务已达上限 {Max}（可调大 MaxConcurrentRecordings；保存线程会尽快落盘释放槽位）",
                    timestamp, _maxConcurrentRecordings);
                return;
            }

            var preFramesRaw = new List<Mat>();
            var preFramesRender = new List<Mat>();
            var preTimesUtc = new List<DateTime>();
            // 摄取长期停滞时环形缓冲不会按墙钟推进，可能残留很旧时间戳；快照时丢弃明显过旧的前置帧，
            // 避免 ComputePlaybackFps 首末跨度异常、成片时长膨胀。
            var preStaleCutoffUtc = DateTime.UtcNow.AddSeconds(-(_bufferSeconds + 1.0));
            foreach (var sample in _frameBuffer)
            {
                if (sample.CapturedAtUtc < preStaleCutoffUtc)
                    continue;
                preFramesRaw.Add(sample.Raw.Clone());
                preFramesRender.Add(sample.Render.Clone());
                preTimesUtc.Add(sample.CapturedAtUtc);
            }

            var task = new RecordingTask
            {
                TaskId = timestamp,
                TriggerTime = DateTime.Now,
                PreFramesRaw = preFramesRaw,
                PreFramesRender = preFramesRender,
                PreFrameTimesUtc = preTimesUtc,
                DelaySeconds = _postBufferSeconds,
                TargetPostFrames = int.MaxValue
            };

            _activeTasks.Add(task);

            _logger.LogDebug("录制任务已创建: {TaskId}, 前置帧数={PreFrames}, 后续按墙钟约{PostSeconds}s结束",
                timestamp, preFramesRaw.Count, _postBufferSeconds);
        }
    }

    private Mat? PrepareRecordingFrame(Mat src)
    {
        try
        {
            if (src == null || src.IsDisposed || src.Empty())
                return null;

            if (_ngVideoUseSourceResolution)
                return src.Clone();

            var maxW = _recordingMaxWidth > 0 ? _recordingMaxWidth : AutoMode2KMaxWidth;
            if (src.Width <= maxW)
                return src.Clone();

            var w = maxW;
            var h = Math.Max(1, (int)Math.Round(src.Height * (double)w / src.Width));
            var dst = new Mat();
            Cv2.Resize(src, dst, new OpenCvSharp.Size(w, h), 0, 0, InterpolationFlags.Nearest);
            return dst;
        }
        catch (ObjectDisposedException)
        {
            return null;
        }
    }

    public List<RecordingTask> GetReadyTasks()
    {
        var readyTasks = new List<RecordingTask>();
        var currentTime = DateTime.Now;

        lock (_lock)
        {
            var remainingTasks = new List<RecordingTask>();

            foreach (var task in _activeTasks)
            {
                if (task.IsReadyToSave(currentTime))
                {
                    readyTasks.Add(task);
                    var elapsed = (currentTime - task.TriggerTime).TotalSeconds;
                    var actualPostFps = task.PostFramesRaw.Count / Math.Max(elapsed, 0.001);
                    _logger.LogDebug("录制任务已完成: {TaskId}, 前置帧={PreFrames}, 后续帧={PostFrames} (耗时{Elapsed:F1}秒, 实际{ActualFps:F1}fps), 总帧数={TotalFrames}",
                        task.TaskId, task.PreFramesRaw.Count, task.PostFramesRaw.Count, elapsed, actualPostFps,
                        task.PreFramesRaw.Count + task.PostFramesRaw.Count);
                }
                else
                {
                    remainingTasks.Add(task);
                }
            }

            _activeTasks.Clear();
            _activeTasks.AddRange(remainingTasks);
        }

        return readyTasks;
    }

    public void Clear()
    {
        lock (_lock)
        {
            while (_frameBuffer.Count > 0)
            {
                var s = _frameBuffer.Dequeue();
                s.Raw.Dispose();
                s.Render.Dispose();
            }

            foreach (var task in _activeTasks)
                task.Clear();

            _activeTasks.Clear();
        }

        _logger.LogDebug("VideoRecorder已清空");
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _ingestWriter.TryComplete();
        _ingestCts.Cancel();
        try
        {
            _ingestTask.Wait(TimeSpan.FromSeconds(5));
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "等待录像摄取线程结束异常（可忽略）");
        }

        Clear();
        _ingestCts.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
