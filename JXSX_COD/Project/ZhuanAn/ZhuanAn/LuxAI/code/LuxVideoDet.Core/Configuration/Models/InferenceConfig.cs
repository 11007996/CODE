using LuxVideoDet.Core.Inference;

namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 推理配置
/// </summary>
public class InferenceConfig
{
    /// <summary>模型文件路径</summary>
    public string ModelPath { get; set; } = string.Empty;

    /// <summary>推理设备</summary>
    public InferenceDevice Device { get; set; } = InferenceDevice.CPU;

    /// <summary>置信度阈值</summary>
    public float ConfidenceThreshold { get; set; } = 0.5f;

    /// <summary>IOU 阈值（NMS）</summary>
    public float IouThreshold { get; set; } = 0.45f;

    /// <summary>输入尺寸</summary>
    public ImageSize InputSize { get; set; } = new() { Width = 640, Height = 640 };

    /// <summary>类别列表</summary>
    public List<string> Classes { get; set; } = new();

    /// <summary>模型类型（自动检测或手动指定）。<see cref="ModelType.Track"/> 为原生追踪 ONNX 预留（当前无算法层 MOT）；需「检测+MOT」时选 <see cref="ModelType.DetectionTracking"/>。</summary>
    public ModelType ModelType { get; set; } = ModelType.Auto;

    /// <summary>
    /// 推理线程数。0 = 自动（由 PipelineFactory 根据并行算法数量分配），
    /// 正数 = 固定线程数。多算法同时运行时务必限制，否则 CPU 线程争抢会导致严重性能下降。
    /// </summary>
    public int ThreadCount { get; set; } = 0;
}

/// <summary>
/// 推理设备枚举
/// </summary>
public enum InferenceDevice
{
    CPU,
    /// <summary>Windows / Linux：使用 OpenVINO 加载 <c>.onnx</c> 或 <c>.xml</c>，与 ONNX Runtime CPU 区分；需用户显式选择。</summary>
    OpenVINO,
    /// <summary>ONNX Runtime CUDA EP；环境或库不可用则加载失败（不回退 CPU）。</summary>
    GPU,
    /// <summary>ONNX Runtime QNN EP（如 Qualcomm HTP）；不可用则加载失败（不回退 CPU）。</summary>
    QNN,
    /// <summary>macOS：ONNX Runtime CoreML EP；非 macOS 或不可用则加载失败（不回退 CPU）。</summary>
    CoreML,

    /// <summary>
    /// Windows / Linux（NVIDIA）：仅 ONNX Runtime TensorRT 执行提供器（不自动追加 CUDA EP；不可用则加载失败，便于验证环境）。
    /// 需安装 TensorRT/CUDA，并使用 GPU 版 ONNX Runtime 构建（<c>dotnet build -p:UseCUDA=true</c> 或 <c>-p:UseTensorRT=true</c>）。
    /// 需要纯 CUDA 回退时请使用 <see cref="GPU"/>。
    /// </summary>
    TensorRT
}



/// <summary>
/// 图像尺寸
/// </summary>
public class ImageSize
{
    public int Width { get; set; }
    public int Height { get; set; }
}
