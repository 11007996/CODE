namespace LuxVideoDet.Core.VideoSource;

/// <summary>
/// OpenCV FFmpeg 后端通过环境变量 <c>OPENCV_FFMPEG_CAPTURE_OPTIONS</c> 注入 demux/解码选项。
/// 单进程内以「最后一次打开视频源前」的赋值为准；与 RTSP / 本地文件打开顺序无关时各自在 Open 前调用。
/// </summary>
public static class FfmpegCaptureEnvironment
{
    private const string EnvKey = "OPENCV_FFMPEG_CAPTURE_OPTIONS";

    /// <summary>
    /// Windows 上本地文件：统一绝对路径、斜杠形式，并设置较稳妥的 FFmpeg 读文件选项（解码线程、丢弃损坏包等）。
    /// </summary>
    public static string PrepareLocalFilePathForOpen(string filePath)
    {
        var path = Path.GetFullPath(filePath);
        if (!OperatingSystem.IsWindows())
            return path;

        // 部分 Windows + FFmpeg 组合下反斜杠路径偶发异常，统一为正斜杠（OpenCV/FFmpeg 均接受）
        return path.Replace('\\', '/');
    }

    /// <summary>
    /// 在打开本地文件 <see cref="VideoCapture"/> 之前调用。
    /// </summary>
    public static void ApplyForLocalFilePlaybackWindows()
    {
        if (!OperatingSystem.IsWindows())
            return;

        // threads;0 表示交由 FFmpeg 自选解码线程；discardcorrupt 减少坏包导致的卡顿；max_delay 降低缓冲积压感
        Environment.SetEnvironmentVariable(EnvKey,
            "threads;0|fflags;discardcorrupt|max_delay;0");
    }

    /// <summary>
    /// RTSP 低延迟选项（全平台）；<paramref name="appendWindowsExtras"/> 为 true 时追加 Windows 上常见的套接字超时，减轻“假死”与断连重试过慢。
    /// </summary>
    public static string BuildRtspOptions(bool appendWindowsExtras)
    {
        var parts = new List<string>
        {
            "rtsp_transport;tcp",
            "fflags;nobuffer",
            "flags;low_delay",
            "max_delay;0",
            "analyzeduration;100000",
            "probesize;32768",
            "reorder_queue_size;0",
            "rtbufsize;0"
        };

        if (appendWindowsExtras)
        {
            // 微秒；部分 Windows 网络栈下默认 RTSP 超时偏短，易误判失败或积压
            parts.Add("stimeout;5000000");
        }

        return string.Join("|", parts);
    }

    public static void ApplyRtspOptions(bool appendWindowsExtras)
    {
        Environment.SetEnvironmentVariable(EnvKey, BuildRtspOptions(appendWindowsExtras));
    }
}
