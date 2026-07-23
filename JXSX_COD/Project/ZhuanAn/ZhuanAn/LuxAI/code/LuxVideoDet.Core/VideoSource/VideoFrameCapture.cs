using System.Linq;
using System.Runtime.InteropServices;
using LuxVideoDet.Core.Common;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.VideoSource;

/// <summary>
/// 从视频源抓取「有内容」的一帧，供区域编辑器、Web 抓帧等使用。
/// 跳过 H.264 解码初期常见的全黑占位帧，行为与 Desktop 区域编辑器一致。
/// </summary>
public static class VideoFrameCapture
{
    /// <summary>
    /// 跨平台打开摄像头（索引）。macOS 优先 AVFoundation，Windows 优先 MSMF，再回退默认后端。
    /// </summary>
    /// <returns>成功则返回已打开的 <see cref="VideoCapture"/>，失败返回 null（调用方勿释放 null）。</returns>
    public static VideoCapture? OpenCameraVideoCapture(int cameraId, ILogger? logger = null)
    {
        logger?.LogInformation(
            "正在打开摄像头: CameraId={CameraId}, OS={OS}, Arch={Arch}",
            cameraId, RuntimeInformation.OSDescription, RuntimeInformation.OSArchitecture);

        VideoCapture? cap = null;
        try
        {
            if (OperatingSystem.IsMacOS())
            {
                logger?.LogDebug("macOS: 尝试 AVFoundation 后端 (VideoCaptureAPIs.AVFOUNDATION)");
                cap = new VideoCapture(cameraId, VideoCaptureAPIs.AVFOUNDATION);
                if (cap.IsOpened())
                {
                    // 首次授权或设备就绪可能略慢，给系统一点时间再读帧验证
                    Thread.Sleep(200);
                }

                if (cap.IsOpened() && TryVerifyCameraFirstFrame(cap, logger))
                {
                    logger?.LogInformation("摄像头已打开 (AVFoundation): CameraId={CameraId}", cameraId);
                    return cap;
                }

                cap.Dispose();
                cap = null;
                logger?.LogWarning("AVFoundation 未就绪或无法读取画面，尝试默认后端");
            }
            else if (OperatingSystem.IsWindows())
            {
                logger?.LogDebug("Windows: 尝试 MSMF 后端 (Media Foundation)");
                cap = new VideoCapture(cameraId, VideoCaptureAPIs.MSMF);
                if (cap.IsOpened() && TryVerifyCameraFirstFrame(cap, logger))
                {
                    logger?.LogInformation("摄像头已打开 (MSMF): CameraId={CameraId}", cameraId);
                    return cap;
                }

                cap.Dispose();
                cap = null;
                logger?.LogWarning("MSMF 未就绪，尝试 DirectShow 后端");

                cap = new VideoCapture(cameraId, VideoCaptureAPIs.DSHOW);
                if (cap.IsOpened() && TryVerifyCameraFirstFrame(cap, logger))
                {
                    logger?.LogInformation("摄像头已打开 (DirectShow): CameraId={CameraId}", cameraId);
                    return cap;
                }

                cap.Dispose();
                cap = null;
                logger?.LogWarning("DirectShow 未就绪，尝试 OpenCV 默认后端");
            }

            cap = new VideoCapture(cameraId, VideoCaptureAPIs.ANY);
            if (cap.IsOpened() && TryVerifyCameraFirstFrame(cap, logger))
            {
                logger?.LogInformation("摄像头已打开 (默认后端): CameraId={CameraId}", cameraId);
                return cap;
            }

            cap.Dispose();
            logger?.LogError(
                "摄像头打开失败: CameraId={CameraId}。提示: {Hint}",
                cameraId, GetCameraPermissionHint());
            return null;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "打开摄像头异常: CameraId={CameraId}", cameraId);
            cap?.Dispose();
            return null;
        }
    }

    /// <summary>
    /// 供 UI 或异常消息展示：摄像头无法打开时的跨平台说明（尤其 macOS 权限）。
    /// </summary>
    public static string GetCameraPermissionHint()
    {
        if (OperatingSystem.IsMacOS())
        {
            return "macOS：请在「系统设置 → 隐私与安全性 → 相机」中允许当前启动方式访问相机（例如从终端运行则授予「终端」或「Cursor」；独立 .app 需在 Info.plist 声明 NSCameraUsageDescription）。若系统提示「未授权」，请确认未拒绝权限。OpenCV 日志若出现 not authorized to capture video 即权限问题。";
        }

        if (OperatingSystem.IsWindows())
        {
            return "Windows：请确认摄像头未被其他程序独占，检查「设置 → 隐私 → 相机」已允许桌面应用访问。";
        }

        return "Linux：请确认当前用户在 video 组，且设备节点可用（如 /dev/video0），无其他进程占用摄像头。";
    }

    private static bool TryVerifyCameraFirstFrame(VideoCapture capture, ILogger? logger)
    {
        using var test = new Mat();
        for (var i = 0; i < 15; i++)
        {
            if (capture.Read(test) && !test.Empty())
                return true;
        }

        logger?.LogWarning(
            "摄像头 IsOpened=true 但连续 {Attempts} 次读取为空帧，可能被占用、权限未生效或设备未就绪",
            15);
        return false;
    }

    /// <summary>
    /// 优先使用 FFmpeg 打开本地/网络文件，失败则回退默认后端。
    /// </summary>
    public static VideoCapture OpenVideoCaptureForFile(string path)
    {
        var cap = new VideoCapture(path, VideoCaptureAPIs.FFMPEG);
        if (cap.IsOpened())
            return cap;
        cap.Dispose();
        return new VideoCapture(path);
    }

    /// <summary>
    /// 从已打开的 <see cref="VideoCapture"/> 读取一帧到 <paramref name="dst"/>（覆盖写入）。
    /// </summary>
    public static bool ReadRepresentativeFrame(VideoCapture capture, Mat dst, ILogger? logger = null)
    {
        const int maxSkips = 90;
        for (var i = 0; i < maxSkips; i++)
        {
            if (!capture.Read(dst) || dst.Empty())
                continue;

            if (!IsNearlyBlackFrame(dst))
            {
                if (i > 0)
                    logger?.LogInformation(
                        "已跳过前 {Skipped} 帧以取得可视底图（常见于 H.264 首帧解码为黑）", i);
                return true;
            }
        }

        var totalFrames = (int)capture.Get(VideoCaptureProperties.FrameCount);
        if (totalFrames > 1)
        {
            var candidates = new List<int>();
            foreach (var c in new[] { 15, 30, 60, totalFrames / 4, totalFrames / 2 })
            {
                if (c > 0 && c < totalFrames)
                    candidates.Add(c);
            }

            foreach (var pos in candidates.Distinct().OrderBy(x => x))
            {
                capture.Set(VideoCaptureProperties.PosFrames, pos);
                if (!capture.Read(dst) || dst.Empty())
                    continue;
                if (!IsNearlyBlackFrame(dst))
                {
                    logger?.LogWarning("视频前段多为黑帧，已从第 {FrameIndex} 帧读取底图", pos);
                    return true;
                }
            }
        }

        capture.Set(VideoCaptureProperties.PosFrames, 0);
        for (var i = 0; i < 5; i++)
            capture.Read(dst);

        return !dst.Empty();
    }

    /// <summary>
    /// 从已打开的 <see cref="IVideoSource"/> 读取一帧 <see cref="Frame"/>（调用方负责释放）。
    /// </summary>
    public static Frame? CaptureRepresentativeFrame(IVideoSource source, ILogger? logger = null)
    {
        const int maxSkips = 90;
        for (var i = 0; i < maxSkips; i++)
        {
            var f = source.ReadFrame();
            if (f == null || f.IsEmpty)
            {
                f?.Dispose();
                continue;
            }

            if (!IsNearlyBlackFrame(f.Mat))
            {
                if (i > 0)
                    logger?.LogInformation(
                        "已跳过前 {Skipped} 帧以取得可视画面（常见于 H.264 首帧为黑）", i);
                return f;
            }

            f.Dispose();
        }

        try
        {
            source.Reset();
        }
        catch (Exception ex)
        {
            logger?.LogDebug(ex, "VideoFrameCapture: Reset 失败");
        }

        for (var i = 0; i < 60; i++)
        {
            var f = source.ReadFrame();
            if (f == null || f.IsEmpty)
            {
                f?.Dispose();
                continue;
            }

            if (!IsNearlyBlackFrame(f.Mat))
                return f;
            f.Dispose();
        }

        try
        {
            source.Reset();
        }
        catch { /* ignore */ }

        return source.ReadFrame();
    }

    /// <summary>
    /// 判断是否接近全黑帧（解码占位或黑场）。
    /// </summary>
    public static bool IsNearlyBlackFrame(Mat mat)
    {
        if (mat.Empty())
            return true;

        var mean = Cv2.Mean(mat);
        var ch = mat.Channels();
        var sum = mean.Val0;
        if (ch >= 2) sum += mean.Val1;
        if (ch >= 3) sum += mean.Val2;
        return sum < 0.75;
    }
}
