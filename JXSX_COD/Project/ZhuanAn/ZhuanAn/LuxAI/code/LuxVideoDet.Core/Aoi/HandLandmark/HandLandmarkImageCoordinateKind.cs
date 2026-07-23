namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>
/// <see cref="HandLandmarkSingleHandResult.Landmarks"/> 中 x/y 的含义（由 <see cref="HandLandmarkerSubprocessInferenceSession.Detect"/> 推理后设置）。
/// </summary>
public enum HandLandmarkImageCoordinateKind
{
    /// <summary>相对源图宽高的 [0,1] 归一化（MediaPipe 风格）。</summary>
    Normalized01RelativeToSourceImage,

    /// <summary>已映射为源图像素坐标。</summary>
    PixelCoordinatesInSourceImage,
}
