namespace LuxVideoDet.Core.Inference.Results;

/// <summary>
/// 推理结果基类
/// </summary>
public class InferenceResult
{
    /// <summary>推理耗时（毫秒）</summary>
    public float InferenceTime { get; set; }

    /// <summary>各阶段细分（仅部分引擎填充，如 ONNX/OpenVINO）。</summary>
    public InferenceTimingBreakdown? Timing { get; set; }

    /// <summary>检测结果列表</summary>
    public List<Detection> Detections { get; set; } = new();

    /// <summary>原始输出（用于调试）</summary>
    public float[]? RawOutput { get; set; }
}

/// <summary>
/// 检测结果
/// </summary>
public class Detection
{
    /// <summary>类别 ID</summary>
    public int ClassId { get; set; }

    /// <summary>类别名称</summary>
    public string ClassName { get; set; } = string.Empty;

    /// <summary>置信度</summary>
    public float Confidence { get; set; }

    /// <summary>边界框</summary>
    public BoundingBox BoundingBox { get; set; } = new();

    /// <summary>分割掩码（仅分割模型）</summary>
    public byte[,]? Mask { get; set; }

    /// <summary>掩码系数（分割模型中间结果）</summary>
    public float[]? MaskCoefficients { get; set; }

    /// <summary>关键点（仅姿态估计模型）</summary>
    public KeyPoint[]? KeyPoints { get; set; }

    /// <summary>旋转角度（仅 OBB 模型）</summary>
    public float? RotationAngle { get; set; }

    /// <summary>类别概率分布（仅分类模型）</summary>
    public float[]? Probabilities { get; set; }

    /// <summary>
    /// 图像分类：与 <see cref="Probabilities"/> 下标一一对应的类别名列表（由分类后处理器填充，便于渲染 Top-K）。
    /// </summary>
    public List<string>? PerClassLabels { get; set; }

    /// <summary>
    /// 多目标跟踪轨迹 ID（<see cref="ModelType.DetectionTracking"/> 或 <see cref="ModelType.SegmentationTracking"/> 下常由 <see cref="Tracking.MultiObjectTracker"/> 写入；原生 <see cref="ModelType.Track"/> 若模型侧输出则可能由解码器填充）。
    /// </summary>
    public int? TrackId { get; set; }
}

/// <summary>
/// 边界框
/// </summary>
public class BoundingBox
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public float X1 => X;
    public float Y1 => Y;
    public float X2 => X + Width;
    public float Y2 => Y + Height;

    public float CenterX => X + Width / 2;
    public float CenterY => Y + Height / 2;

    public float Area => Width * Height;

    /// <summary>
    /// 计算与另一个边界框的 IOU
    /// </summary>
    public float CalculateIou(BoundingBox other)
    {
        var x1 = Math.Max(X1, other.X1);
        var y1 = Math.Max(Y1, other.Y1);
        var x2 = Math.Min(X2, other.X2);
        var y2 = Math.Min(Y2, other.Y2);

        var intersectionArea = Math.Max(0, x2 - x1) * Math.Max(0, y2 - y1);
        var unionArea = Area + other.Area - intersectionArea;

        return unionArea > 0 ? intersectionArea / unionArea : 0;
    }

    /// <summary>
    /// 检查点是否在边界框内
    /// </summary>
    public bool Contains(float x, float y)
    {
        return x >= X1 && x <= X2 && y >= Y1 && y <= Y2;
    }
}

/// <summary>
/// 关键点
/// </summary>
public class KeyPoint
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Confidence { get; set; }
    public bool Visible { get; set; }
}
