namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 检测配置 - 强类型配置模型，易于 GUI 绑定和验证
/// </summary>
public class DetectionConfiguration
{
    /// <summary>配置唯一标识</summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>配置名称（显示用）</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>视频源配置</summary>
    public VideoSourceConfig VideoSource { get; set; } = new();

    /// <summary>算法池 - 支持多个算法同时运行</summary>
    public List<AlgorithmConfig> Algorithms { get; set; } = new();

    /// <summary>是否启用</summary>
    public bool Enabled { get; set; } = true;

    /// <summary>创建时间</summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>最后修改时间</summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
