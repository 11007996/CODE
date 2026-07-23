namespace LuxVideoDet.Core.Inference;

/// <summary>
/// 预留：<see cref="ModelType.Track"/> 原生追踪 ONNX（非标准 detect 张量）判定时可选用专门后处理。当前恒为 <c>false</c>。
/// </summary>
public static class NativeTrackOnnxDetector
{
    public static bool IsLikelyNativeTrackOnnx() => false;
}
