namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 视频源配置
/// </summary>
public class VideoSourceConfig
{
    /// <summary>视频源类型</summary>
    public VideoSourceType Type { get; set; } = VideoSourceType.LocalVideo;

    /// <summary>视频源地址（文件路径、RTSP URL、摄像头索引）</summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>是否循环播放（仅本地视频）</summary>
    public bool Loop { get; set; } = false;

    /// <summary>RTSP 重连间隔（秒）</summary>
    public int ReconnectInterval { get; set; } = 5;

    /// <summary>RTSP 超时时间（秒）</summary>
    public int Timeout { get; set; } = 10;
}

/// <summary>
/// 视频源类型枚举
/// </summary>
public enum VideoSourceType
{
    /// <summary>本地视频文件</summary>
    LocalVideo,

    /// <summary>RTSP 流</summary>
    Rtsp,

    /// <summary>本地摄像头</summary>
    Camera
}
