namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 存储配置
/// </summary>
public class StorageConfig
{
    /// <summary>是否保存错误图像</summary>
    public bool SaveErrorImage { get; set; } = true;

    /// <summary>是否保存重训练图像</summary>
    public bool SaveRetrainImage { get; set; } = true;

    /// <summary>是否保存视频</summary>
    public bool SaveVideo { get; set; } = false;

    /// <summary>视频录制时长（秒）</summary>
    public int VideoDuration { get; set; } = 10;

    /// <summary>视频编码器</summary>
    public string VideoCodec { get; set; } = "mp4v";

    /// <summary>
    /// 为 true 时 NG 录像按源分辨率保存（不做宽度缩放）；为 false 时遵循 <see cref="RecordingMaxWidth"/>（0=自动≤2K）。
    /// </summary>
    public bool NgVideoUseSourceResolution { get; set; }

    /// <summary>
    /// NG 录像最大宽度（像素），高度按比例缩放；在 <see cref="NgVideoUseSourceResolution"/> 为 true 时忽略。
    /// 为 0（默认）：源宽大于 2K（2560）时缩至 2560；否则保持源分辨率。
    /// 大于 0：强制不超过该宽度（用于兼容旧配置或自定义上限）。
    /// </summary>
    public int RecordingMaxWidth { get; set; } = 0;

    /// <summary>
    /// 可同时存在的 NG 录像采集任务上限（未完成保存前的任务数）。
    /// 短时间连续 NG 时限制该值可避免每帧对多任务成倍拷贝拖垮推理线程。
    /// </summary>
    public int MaxConcurrentRecordings { get; set; } = 2;

    /// <summary>错误图像保存路径</summary>
    public string ErrorImagePath { get; set; } = "catch";

    /// <summary>重训练图像保存路径</summary>
    public string RetrainImagePath { get; set; } = "retrain";

    /// <summary>文件保留天数（0 表示永久保留）</summary>
    public int RetentionDays { get; set; } = 7;
}
