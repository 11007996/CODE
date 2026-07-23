using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.VideoSource;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.VideoSource.LocalVideo;

public class LocalVideoSource : IVideoSource
{
    private readonly ILogger<LocalVideoSource> _logger;
    private readonly string _filePath;
    private readonly bool _loop;
    private VideoCapture? _capture;

    public string SourceType => "LocalVideo";
    public bool IsOpened => _capture?.IsOpened() ?? false;
    public bool IsLoop => _loop;

    public LocalVideoSource(string filePath, bool loop, ILogger<LocalVideoSource> logger)
    {
        _filePath = filePath;
        _loop = loop;
        _logger = logger;
    }

    public void Open()
    {
        var path = FfmpegCaptureEnvironment.PrepareLocalFilePathForOpen(_filePath);
        if (!File.Exists(path))
        {
            _logger.LogError("[视频源] 文件不存在: {FilePath}", path);
            throw new FileNotFoundException($"无法找到视频文件: {path}", path);
        }

        _logger.LogDebug("[视频源] 打开本地文件: {FilePath}", path);

        // Windows：FFmpeg 读文件选项 + 正斜杠路径，减轻解码卡顿与路径解析问题
        FfmpegCaptureEnvironment.ApplyForLocalFilePlaybackWindows();

        _capture = TryOpenWithBackends(path);

        if (!_capture.IsOpened())
        {
            _logger.LogError(
                "[视频源] 无法打开本地视频: {FilePath}（请检查路径与编码；Windows 上建议安装与 OpenCV 匹配的 FFmpeg 运行库）",
                path);
            throw new InvalidOperationException($"无法打开视频文件: {path}");
        }

        if (OperatingSystem.IsWindows())
            TryConfigureBufferSizeOne();

        _logger.LogInformation("[视频源] 本地文件已打开: {FilePath}", path);
    }

    private static VideoCapture TryOpenWithBackends(string path)
    {
        // 按优先级尝试各后端（macOS 上默认后端可能不支持某些 MP4 编码）
        var backends = new[] {
            VideoCaptureAPIs.FFMPEG,
            VideoCaptureAPIs.ANY,
            VideoCaptureAPIs.AVFOUNDATION,
        };

        foreach (var api in backends)
        {
            try
            {
                var cap = new VideoCapture(path, api);
                if (cap.IsOpened())
                    return cap;
                cap.Dispose();
            }
            catch
            {
                // 该后端不可用，继续尝试下一个
            }
        }

        // 最后的兜底
        return new VideoCapture(path);
    }

    private void TryConfigureBufferSizeOne()
    {
        if (_capture == null) return;
        try
        {
            _capture.Set(VideoCaptureProperties.BufferSize, 1);
            _logger.LogDebug("[视频源] Windows: 已尝试最小化 FFmpeg 内部缓冲 (BufferSize=1)");
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "[视频源] BufferSize=1 未生效（部分后端忽略）");
        }
    }

    public Frame? ReadFrame()
    {
        if (_capture == null || !_capture.IsOpened())
            return null;

        using var mat = new Mat();
        var success = _capture.Read(mat);

        if (!success || mat.Empty())
        {
            if (_loop)
            {
                Reset();
                return ReadFrame();
            }
            return null;
        }

        return new Frame(mat.Clone());
    }

    public void Reset()
    {
        if (_capture != null && _capture.IsOpened())
        {
            _capture.Set(VideoCaptureProperties.PosFrames, 0);
            _logger.LogDebug("视频已重置到开始位置");
        }
    }

    public void Close()
    {
        _capture?.Dispose();
        _capture = null;
    }

    public int GetWidth()
    {
        return (int)(_capture?.Get(VideoCaptureProperties.FrameWidth) ?? 0);
    }

    public int GetHeight()
    {
        return (int)(_capture?.Get(VideoCaptureProperties.FrameHeight) ?? 0);
    }

    public double GetFps()
    {
        return _capture?.Get(VideoCaptureProperties.Fps) ?? 30.0;
    }

    public void Dispose()
    {
        Close();
        GC.SuppressFinalize(this);
    }
}
