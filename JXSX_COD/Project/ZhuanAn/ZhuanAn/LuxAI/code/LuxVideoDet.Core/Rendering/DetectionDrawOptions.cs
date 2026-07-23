using LuxVideoDet.Core.Inference.Results;

namespace LuxVideoDet.Core.Rendering;

/// <summary>
/// 单帧内绘制单个 <see cref="Detection"/> 时的可选参数。
/// </summary>
public sealed class DetectionDrawOptions
{
    public bool ShowBox { get; init; } = true;
    public bool ShowLabel { get; init; } = true;

    /// <summary>
    /// 若提供，绘制标签时用其返回值代替 <see cref="Detection.ClassName"/>（不含置信度，置信度仍自动追加）。
    /// </summary>
    public Func<Detection, string>? LabelFormatter { get; init; }
    public int BoxThickness { get; init; } = 2;
    public float MaskAlpha { get; init; } = 0.35f;
    /// <summary>姿态任务是否绘制骨架连线（COCO-17）</summary>
    public bool DrawPoseSkeleton { get; init; } = true;

    // ─── 图像分类（整图分类，无框）───

    /// <summary>分类任务展示的 Top-K 类别数</summary>
    public int ClassificationTopK { get; init; } = 5;

    /// <summary>是否在左上角绘制半透明说明面板</summary>
    public bool ClassificationShowPanel { get; init; } = true;

    /// <summary>面板底色混合强度（越大越暗，建议 0.25～0.45）</summary>
    public float ClassificationPanelBlend { get; init; } = 0.38f;
}
