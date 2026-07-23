using LuxVideoDet.Core.Common;
using LuxVideoDet.Core.Inference.Results;

namespace LuxVideoDet.Core.Inference;

/// <summary>
/// 推理引擎接口 - 所有推理引擎都需要实现此接口
/// </summary>
public interface IInferenceEngine : IDisposable
{
    /// <summary>引擎类型（例如：ONNX Runtime, OpenVINO）</summary>
    string EngineType { get; }

    /// <summary>设备类型（例如：CPU, CUDA, QNN, CoreML）</summary>
    string DeviceType { get; }

    /// <summary>模型是否已加载</summary>
    bool IsLoaded { get; }

    /// <summary>
    /// 加载模型
    /// </summary>
    Task LoadModelAsync(string modelPath, CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行推理
    /// </summary>
    Task<InferenceResult> InferAsync(
        Frame frame,
        float confidenceThreshold,
        float iouThreshold,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量推理
    /// </summary>
    Task<InferenceResult[]> InferBatchAsync(
        Frame[] frames,
        float confidenceThreshold,
        float iouThreshold,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取模型信息
    /// </summary>
    ModelInfo GetModelInfo();

    /// <summary>
    /// 获取类别名称列表
    /// </summary>
    List<string> GetClassNames();

    /// <summary>
    /// 获取模型的类别名称到索引的映射。
    /// 从 ONNX 元数据中读取，使得算法可以通过名称而非硬编码索引来引用类别。
    /// </summary>
    IReadOnlyDictionary<string, int> GetClassIndexMap();

    /// <summary>
    /// 执行推理（同步版本，用于算法调用）
    /// </summary>
    InferenceResult Infer(OpenCvSharp.Mat frame);
}
