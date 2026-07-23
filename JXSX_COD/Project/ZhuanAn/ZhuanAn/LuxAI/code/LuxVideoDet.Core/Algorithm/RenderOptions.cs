namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 渲染选项 — 控制预览画面上哪些元素可见。
/// </summary>
public class RenderOptions
{
    public bool ShowDetectionBoxes { get; set; } = true;
    public bool ShowRegions { get; set; } = true;
    public bool ShowLabels { get; set; } = true;
}
