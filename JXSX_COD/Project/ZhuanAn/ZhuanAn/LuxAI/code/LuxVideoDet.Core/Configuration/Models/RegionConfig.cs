namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 区域配置 - 强类型，不再使用 List&lt;List&lt;int&gt;&gt;
/// </summary>
public class RegionConfig
{
    /// <summary>区域名称（内部使用）</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>区域显示名称（用户看到的）</summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>区域描述</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>区域点集合</summary>
    public List<Point> Points { get; set; } = new();

    /// <summary>
    /// 区域轮廓颜色（RGB 十六进制 <c>#RRGGBB</c>）。为空、<c>inherit</c> 表示未在配置中指定，运行时使用 Descriptor 默认色，再回退到内置名→色表。
    /// </summary>
    public string Color { get; set; } = "";

    /// <summary>是否必需</summary>
    public bool Required { get; set; } = true;

    /// <summary>是否可见</summary>
    public bool Visible { get; set; } = true;
}

/// <summary>
/// 点坐标 - 强类型
/// </summary>
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point() { }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
