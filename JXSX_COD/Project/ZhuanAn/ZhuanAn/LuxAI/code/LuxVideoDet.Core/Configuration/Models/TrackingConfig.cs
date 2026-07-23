using LuxVideoDet.Core.Inference;

namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 多目标跟踪参数（<see cref="ModelType.DetectionTracking"/>：检测+MOT；<see cref="ModelType.SegmentationTracking"/>：实例分割+MOT，掩膜不参与关联）。
/// </summary>
public sealed class TrackingConfig
{
    /// <summary>同类轨迹与检测框 IoU 关联下限。</summary>
    public float TrackIouThreshold { get; set; } = 0.3f;

    /// <summary>轨迹连续未匹配达到该帧数后删除。</summary>
    public int MaxMissedFrames { get; set; } = 30;

    /// <summary>轨迹累计命中帧数达到此值后才在 <see cref="Inference.Results.Detection.TrackId"/> 上输出 ID。</summary>
    public int MinHits { get; set; } = 2;

    /// <summary>
    /// <c>TrackId</c> 取 <c>0</c>～本值（含）。计数器超过本值后回到 <c>0</c>；若该号仍被活跃轨迹占用则顺延下一个，避免重复。
    /// </summary>
    public int MaxTrackId { get; set; } = 65535;
}
