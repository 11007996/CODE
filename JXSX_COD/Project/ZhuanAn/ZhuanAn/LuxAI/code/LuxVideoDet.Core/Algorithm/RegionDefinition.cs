using OpenCvSharp;

namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 区域定义 — 描述算法需要的功能区。
/// </summary>
public class RegionDefinition
{
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Scalar DefaultColor { get; set; } = new Scalar(0, 255, 0);
    public bool Required { get; set; } = true;
}
