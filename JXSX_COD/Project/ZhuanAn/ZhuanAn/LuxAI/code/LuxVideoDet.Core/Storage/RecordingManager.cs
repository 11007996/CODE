using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Storage;

public class RecordingManager : IDisposable
{
    private readonly ILogger _logger;
    private readonly VideoRecorder _recorder;
    private readonly StorageManager _storage;
    private readonly string _videoCodec;

    private Thread? _saveThread;
    private bool _stopEvent;
    private bool _disposed;

    public RecordingManager(
        VideoRecorder recorder,
        StorageManager storage,
        ILogger logger,
        string videoCodec = "mp4v")
    {
        _recorder = recorder;
        _storage = storage;
        _logger = logger;
        _videoCodec = videoCodec;
    }

    public void StartBackgroundSaver()
    {
        if (_saveThread != null)
            return;

        _stopEvent = false;
        _saveThread = new Thread(BackgroundSaveLoop)
        {
            IsBackground = true,
            Name = "VideoSaver"
        };
        _saveThread.Start();

        _logger.LogDebug("后台视频保存线程已启动");
    }

    public void StopBackgroundSaver()
    {
        if (_saveThread == null)
            return;

        _stopEvent = true;
        _saveThread.Join(5000);
        _saveThread = null;

        _recorder.Clear();

        _logger.LogInformation("后台视频保存线程已停止，内存已释放");
    }

    private void BackgroundSaveLoop()
    {
        while (!_stopEvent)
        {
            try
            {
                var readyTasks = _recorder.GetReadyTasks();

                foreach (var task in readyTasks)
                {
                    SaveTask(task);
                }

                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "后台保存循环异常");
                Thread.Sleep(1000);
            }
        }
    }

    private void SaveTask(RecordingTask task)
    {
        try
        {
            var (rawFrames, renderFrames) = task.GetAllFrames();

            if (rawFrames.Count == 0 || renderFrames.Count == 0)
            {
                _logger.LogWarning("[{MachineName}] 录制任务 {TaskId} 没有帧数据 (raw={RawCount}, render={RenderCount})",
                    _storage.MachineName, task.TaskId, rawFrames.Count, renderFrames.Count);
                return;
            }

            var playbackFps = task.ComputePlaybackFps();
            _logger.LogError("[{MachineName}] 开始保存 NG 错误视频录制任务 {TaskId}: render={RenderCount}帧, raw={RawCount}帧, 实况fps={PlaybackFps}",
                _storage.MachineName, task.TaskId, renderFrames.Count, rawFrames.Count, playbackFps);

            var errorVideoResult = _storage.SaveErrorVideo(renderFrames, playbackFps, _videoCodec, task.TaskId);
            var retrainResult = _storage.SaveRetrainVideo(rawFrames, playbackFps, _videoCodec, task.TaskId);

            if (errorVideoResult != null)
            {
                var parts = errorVideoResult.Split('|');
                if (parts.Length == 2)
                {
                    _logger.LogError("[{MachineName}] [{TaskId}] 错误视频已保存: {FilePath} ({FrameCount}帧)",
                        _storage.MachineName, task.TaskId, parts[0], parts[1]);
                }
            }
            else
            {
                _logger.LogError("[{MachineName}] [{TaskId}] 错误视频保存失败！请检查 FFmpeg 是否可用以及磁盘空间",
                    _storage.MachineName, task.TaskId);
            }

            if (!retrainResult)
            {
                _logger.LogWarning("[{MachineName}] [{TaskId}] 重训练视频保存失败",
                    _storage.MachineName, task.TaskId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{MachineName}] 保存录制任务 {TaskId} 异常", _storage.MachineName, task.TaskId);
        }
        finally
        {
            task.Clear();
        }
    }

    public string? TriggerAndSave(string? timestamp = null)
    {
        return _recorder.TriggerRecording(timestamp);
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        StopBackgroundSaver();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
