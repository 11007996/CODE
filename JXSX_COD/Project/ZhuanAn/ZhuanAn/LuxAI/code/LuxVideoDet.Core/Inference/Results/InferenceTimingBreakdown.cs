namespace LuxVideoDet.Core.Inference.Results;

/// <summary>
/// 单帧推理各阶段耗时（毫秒），用于与日志中「模型=整段 Infer」对照定位瓶颈。
/// </summary>
public sealed class InferenceTimingBreakdown
{
    /// <summary>CPU：OpenCV 缩放、padding、BGR→RGB、拼 float[] 等。</summary>
    public float PreprocessMs { get; set; }

    /// <summary>组装 DenseTensor / FP32→FP16 等（在 Run 之前）。</summary>
    public float InputTensorMs { get; set; }

    /// <summary>仅推理主会话：<c>InferenceSession.Run</c> / OpenVINO <c>infer()</c>。</summary>
    public float NativeRunMs { get; set; }

    /// <summary>输出从原生张量读到托管数组（含设备→主机与类型转换）。</summary>
    public float OutputToCpuMs { get; set; }

    /// <summary>引擎内解码：NMS、mask、还原框等（<c>IPostprocessor.Process</c>）。</summary>
    public float EnginePostprocessMs { get; set; }

    /// <summary>各分项之和，应与 <see cref="InferenceResult.InferenceTime"/> 接近。</summary>
    public float SumMs =>
        PreprocessMs + InputTensorMs + NativeRunMs + OutputToCpuMs + EnginePostprocessMs;
}
