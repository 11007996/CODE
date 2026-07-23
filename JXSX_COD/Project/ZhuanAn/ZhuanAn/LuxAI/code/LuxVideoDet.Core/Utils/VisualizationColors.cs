using OpenCvSharp;

namespace LuxVideoDet.Core.Utils;

/// <summary>
/// 可视化颜色解析：配置中为 RGB 十六进制（#RRGGBB），运行时为 OpenCV BGR <see cref="Scalar"/>。
/// </summary>
public static class VisualizationColors
{
    /// <summary>
    /// 将 <c>#RRGGBB</c> 解析为 BGR。空、仅空白、<c>inherit</c>（不区分大小写）表示「未在 config 中指定」，走 Descriptor / 默认链。
    /// </summary>
    public static bool TryParseRgbHexToBgrScalar(string? hex, out Scalar scalar)
    {
        scalar = default;
        if (string.IsNullOrWhiteSpace(hex)) return false;
        var t = hex.Trim();
        if (t.Equals("inherit", StringComparison.OrdinalIgnoreCase)) return false;
        if (t.Length != 7 || !t.StartsWith('#')) return false;

        try
        {
            var h = t[1..];
            var r = Convert.ToInt32(h.Substring(0, 2), 16);
            var g = Convert.ToInt32(h.Substring(2, 2), 16);
            var b = Convert.ToInt32(h.Substring(4, 2), 16);
            scalar = new Scalar(b, g, r);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
