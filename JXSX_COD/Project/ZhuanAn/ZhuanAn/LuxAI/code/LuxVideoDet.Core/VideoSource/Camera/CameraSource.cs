using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.VideoSource;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.VideoSource.Camera;

public class CameraSource : IVideoSource
{
    private readonly ILogger<CameraSource> _logger;
    private readonly int _cameraId;
    private VideoCapture? _capture;

    public string SourceType => "Camera";
    public bool IsOpened => _capture?.IsOpened() ?? false;
    public bool IsLoop => false;

    public CameraSource(int cameraId, ILogger<CameraSource> logger)
    {
        _cameraId = cameraId;
        _logger = logger;
    }

    public void Open()
    {
        _capture = VideoFrameCapture.OpenCameraVideoCapture(_cameraId, _logger);
        if (_capture == null)
        {
            _logger.LogError(
                "无法打开摄像头 {CameraId}。{Hint}",
                _cameraId, VideoFrameCapture.GetCameraPermissionHint());
            throw new InvalidOperationException(
                $"无法打开摄像头 {_cameraId}。{VideoFrameCapture.GetCameraPermissionHint()}");
        }

        ConfigureLowLatency();
        TryPreferMjpegOnWindows();

        _logger.LogInformation("摄像头 {CameraId} 已就绪（低延迟模式）", _cameraId);
    }

    private void ConfigureLowLatency()
    {
        if (_capture == null) return;

        try
        {
            _capture.Set(VideoCaptureProperties.BufferSize, 1);
            _logger.LogInformation("摄像头已配置低延迟参数: 最小缓冲区");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "配置低延迟参数时出现警告");
        }
    }

    /// <summary>
    /// Windows 上大量 USB 摄像头在 YUY2 未压缩模式下高分辨率只能稳定 ~15fps；
    /// MJPEG 压缩模式常可达 30fps。在保持当前分辨率前提下尝试切换，失败则静默忽略。
    /// </summary>
    private void TryPreferMjpegOnWindows()
    {
        if (_capture == null || !OperatingSystem.IsWindows())
            return;

        try
        {
            var w = (int)_capture.Get(VideoCaptureProperties.FrameWidth);
            var h = (int)_capture.Get(VideoCaptureProperties.FrameHeight);
            if (w <= 0 || h <= 0)
                return;

            var mjpg = VideoWriter.FourCC('M', 'J', 'P', 'G');
            _capture.Set(VideoCaptureProperties.FourCC, mjpg);
            _capture.Set(VideoCaptureProperties.FrameWidth, w);
            _capture.Set(VideoCaptureProperties.FrameHeight, h);

            using var test = new Mat();
            if (!_capture.Read(test) || test.Empty())
            {
                _logger.LogDebug("MJPEG 协商后无法读帧，保持设备默认格式");
                return;
            }

            var fps = _capture.Get(VideoCaptureProperties.Fps);
            _logger.LogInformation(
                "Windows: 已尝试使用 MJPEG 模式（同等分辨率下常比 YUY2 更高帧率）| {Width}x{Height} 报告FPS≈{Fps:F1}",
                w, h, fps > 0 ? fps : 0);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "MJPEG 模式切换跳过（设备可能不支持）");
        }
    }

    public Frame? ReadFrame()
    {
        if (_capture == null || !_capture.IsOpened())
            return null;

        using var mat = new Mat();
        var success = _capture.Read(mat);

        if (!success || mat.Empty())
            return null;

        return new Frame(mat.Clone());
    }

    public void Reset()
    {
        // 摄像头不支持重置
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
        var fps = _capture?.Get(VideoCaptureProperties.Fps) ?? 30.0;
        return fps > 0 ? fps : 30.0;
    }

    public void Dispose()
    {
        Close();
        GC.SuppressFinalize(this);
    }
}
