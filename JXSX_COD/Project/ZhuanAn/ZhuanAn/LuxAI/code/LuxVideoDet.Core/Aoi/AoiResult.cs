using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi;

/// <summary>
/// AOI 检测结果
/// </summary>
public class AoiResult
{
    /// <summary>是否检测成功</summary>
    public bool Success { get; set; } = true;

    /// <summary>置信度 (0-1)</summary>
    public float Confidence { get; set; } = 1.0f;

    /// <summary>结果消息</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>结果数据（灵活的键值对）</summary>
    public Dictionary<string, object> Data { get; set; } = new();

    /// <summary>
    /// 获取结果数据
    /// </summary>
    public T Get<T>(string key, T defaultValue = default!)
    {
        if (Data.TryGetValue(key, out var value))
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// 设置结果数据
    /// </summary>
    public void Set(string key, object value)
    {
        Data[key] = value;
    }
}

/// <summary>
/// U 型方向检测结果的便捷扩展
/// </summary>
public static class UShapeResultExtensions
{
    public static float GetAngle(this AoiResult result) => result.Get<float>("angle");
    public static string GetDirection(this AoiResult result) => result.Get<string>("direction", string.Empty);
    public static Point2f GetCenter(this AoiResult result) => result.Get<Point2f>("center");
    public static Point[]? GetContour(this AoiResult result) =>
        result.Data.TryGetValue("contour", out var obj) ? obj as Point[] : null;

    public static void SetOrientation(this AoiResult result, float angle, string direction, Point2f center)
    {
        result.Set("angle", angle);
        result.Set("direction", direction);
        result.Set("center", center);
    }
}
