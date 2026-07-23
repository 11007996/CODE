using LuxVideoDet.Core.Inference.Results;

namespace LuxVideoDet.Core.Inference.Postprocessors;

/// <summary>
/// 后处理器接口 - 处理不同任务类型的模型输出
/// </summary>
public interface IPostprocessor
{
    /// <summary>任务类型</summary>
    ModelType TaskType { get; }

    /// <summary>
    /// 处理模型输出
    /// </summary>
    List<Detection> Process(
        float[][] outputs,
        PostprocessContext context);
}

/// <summary>
/// 后处理上下文 - 包含后处理所需的所有信息
/// </summary>
public class PostprocessContext
{
    /// <summary>原始图像宽度</summary>
    public int OriginalWidth { get; set; }

    /// <summary>原始图像高度</summary>
    public int OriginalHeight { get; set; }

    /// <summary>缩放比例</summary>
    public float Ratio { get; set; }

    /// <summary>水平填充</summary>
    public float PadW { get; set; }

    /// <summary>垂直填充</summary>
    public float PadH { get; set; }

    /// <summary>置信度阈值</summary>
    public float ConfThreshold { get; set; }

    /// <summary>IOU 阈值</summary>
    public float IouThreshold { get; set; }

    /// <summary>类别名称列表</summary>
    public List<string> ClassNames { get; set; } = new();

    /// <summary>类别数量</summary>
    public int NumClasses { get; set; }

    /// <summary>锚点数量</summary>
    public int NumAnchors { get; set; }

    /// <summary>属性数量</summary>
    public int NumAttributes { get; set; }
}
