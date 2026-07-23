namespace LuxVideoDet.Core.Inference;

/// <summary>
/// 模型信息
/// </summary>
public class ModelInfo
{
    /// <summary>模型名称</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>模型类型</summary>
    public ModelType Type { get; set; }

    /// <summary>输入尺寸</summary>
    public (int Width, int Height) InputSize { get; set; }

    /// <summary>类别数量</summary>
    public int ClassCount { get; set; }

    /// <summary>类别名称列表</summary>
    public string[] ClassNames { get; set; } = Array.Empty<string>();

    /// <summary>模型版本</summary>
    public string Version { get; set; } = string.Empty;
}

/// <summary>
/// 模型任务类型；与 JSON 中枚举名及 YOLO CLI 习惯（detect/segment/pose/obb 等）大致对应，人类可读标签见 <see cref="ModelTypeYoloLabels"/>。
/// </summary>
public enum ModelType
{
    Auto,
    Detection,
    Segmentation,
    Classification,
    PoseEstimation,
    Obb,

    /// <summary>Reserved native multi-object tracking ONNX; decodes like <see cref="Detection"/> until a dedicated head exists.</summary>
    Track,

    /// <summary>Instance segmentation + algorithm MOT (<c>bbox + ClassId</c>).</summary>
    SegmentationTracking,

    /// <summary>YOLO detection + algorithm MOT (<c>yolo detect</c> plus frame-to-frame IDs).</summary>
    DetectionTracking
}

/// <summary>
/// 供模型类型下拉绑定的一行；<see cref="IsSelectable"/> 为 <c>false</c> 时为预留项（灰显且不可作为新选项）。
/// </summary>
public sealed record ModelTypePickerItem(ModelType Type, string Label, bool IsSelectable);

/// <summary>
/// Fixed English labels for dropdowns/logs (YOLO-style)；不跟随 i18n 资源切换，
/// 与 <see cref="ModelType"/> 数值顺序一致（见 <see cref="ComboOrdered"/> / <see cref="PickerOrdered"/>）。
/// </summary>
public static class ModelTypeYoloLabels
{
    /// <summary>原生 <see cref="ModelType.Track"/> 为预留枚举，编辑器中不可点选为该值。</summary>
    public static bool IsSelectable(ModelType type) => type != ModelType.Track;

    public static readonly string[] ComboOrdered =
    [
        Format(ModelType.Auto),
        Format(ModelType.Detection),
        Format(ModelType.Segmentation),
        Format(ModelType.Classification),
        Format(ModelType.PoseEstimation),
        Format(ModelType.Obb),
        Format(ModelType.Track),
        Format(ModelType.SegmentationTracking),
        Format(ModelType.DetectionTracking)
    ];

    public static readonly ModelTypePickerItem[] PickerOrdered =
    [
        new(ModelType.Auto, Format(ModelType.Auto), IsSelectable(ModelType.Auto)),
        new(ModelType.Detection, Format(ModelType.Detection), IsSelectable(ModelType.Detection)),
        new(ModelType.Segmentation, Format(ModelType.Segmentation), IsSelectable(ModelType.Segmentation)),
        new(ModelType.Classification, Format(ModelType.Classification), IsSelectable(ModelType.Classification)),
        new(ModelType.PoseEstimation, Format(ModelType.PoseEstimation), IsSelectable(ModelType.PoseEstimation)),
        new(ModelType.Obb, Format(ModelType.Obb), IsSelectable(ModelType.Obb)),
        new(ModelType.Track, Format(ModelType.Track), IsSelectable(ModelType.Track)),
        new(ModelType.SegmentationTracking, Format(ModelType.SegmentationTracking),
            IsSelectable(ModelType.SegmentationTracking)),
        new(ModelType.DetectionTracking, Format(ModelType.DetectionTracking),
            IsSelectable(ModelType.DetectionTracking))
    ];

    public static string Format(ModelType type) =>
        type switch
        {
            ModelType.Auto => "Auto",
            ModelType.Detection => "Detect",
            ModelType.Segmentation => "Segment",
            ModelType.Classification => "Classify",
            ModelType.PoseEstimation => "Pose",
            ModelType.Obb => "Rotated",
            ModelType.Track => "Track (Reserved)",
            ModelType.SegmentationTracking => "Track (Segment)",
            ModelType.DetectionTracking => "Track (Detect)",
            _ => type.ToString()
        };
}

