namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>
/// MediaPipe Hand Landmarker 输入分辨率工程默认值（与 feature/mediapipe-aoi-integration 中 UCS 引导路径一致）。
/// </summary>
public static class HandLandmarkerInferenceDefaults
{
    /// <summary>默认推理长边像素上限（等比缩小）；设为 0 表示全分辨率（见算法 args <c>inference_max_long_edge</c>）。</summary>
    public const int SuggestedInferenceMaxLongEdgePixels = 512;

    /// <summary>
    /// Hand Landmarker VIDEO 时间步：与视频源上限 <strong>30 fps</strong> 对应的标称帧间隔（<c>1000/30 ≈ 33</c> ms）。
    /// 由算法固定传入，不在配置 UI 中暴露。
    /// </summary>
    public const int NominalFrameDeltaMsAt30FpsCap = 33;
}
