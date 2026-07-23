using System.Text.Json;
using System.Text.Json.Serialization;
using LuxVideoDet.Core.Inference;

namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 算法配置 - 单个算法的完整配置
/// </summary>
public class AlgorithmConfig
{
    /// <summary>算法类型（test, tearofftab, u7lite）</summary>
    public string AlgorithmType { get; set; } = string.Empty;

    /// <summary>算法显示名称</summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 运行时由 PipelineFactory 设置的父配置名称，用于 catch/retrain 目录组织。
    /// 不序列化到 JSON 文件。
    /// </summary>
    [JsonIgnore]
    public string? ParentConfigName { get; set; }

    /// <summary>推理配置</summary>
    public InferenceConfig Inference { get; set; } = new();

    /// <summary>区域配置列表</summary>
    public List<RegionConfig> Regions { get; set; } = new();

    /// <summary>
    /// 各类别检测框颜色（<c>#RRGGBB</c>）。
    /// 一般算法：与 <see cref="InferenceConfig.Classes"/>（或模型类别表）顺序一致。
    /// UCS / UCSHand / TearOffTab：与 <c>DetectionClass</c> 业务语义顺序一致（box→label→…），与模型类别索引顺序无关。
    /// MidFrameScan：与 A～F 语义顺序一致。
    /// MidFrameScanLoose / MidFrameScanStrict / MidFrameScanLooseHand：与 A～F 语义顺序一致（各类算法目录内 <c>*ResolvedClassIds</c> 解析条目）。
    /// SafetyPpeWork：与 finger_cot→glove 语义顺序一致（见算法参数 <c>class_finger_cot</c> / <c>class_glove</c>）。
    /// null 或某项为空/<c>inherit</c> 表示该项未指定，使用 Descriptor 的 <c>DefaultClassColors</c>，再回退到自动生成调色板。
    /// </summary>
    public List<string>? ClassColors { get; set; }

    /// <summary>存储配置</summary>
    public StorageConfig Storage { get; set; } = new();

    /// <summary>通知配置</summary>
    public NotificationConfig Notification { get; set; } = new();

    /// <summary>是否启用</summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 算法专用扩展参数（JSON 对象），例如 U7Lite 的 <c>require_u_shape_aoi</c>、<c>target_size</c> 等。
    /// </summary>
    public Dictionary<string, JsonElement>? Args { get; set; }

    /// <summary>
    /// 可选：当推理任务为 <see cref="ModelType.DetectionTracking"/> 或 <see cref="ModelType.SegmentationTracking"/> 时 MOT 参数；未配置时使用默认值。
    /// </summary>
    public TrackingConfig? Tracking { get; set; }
}
