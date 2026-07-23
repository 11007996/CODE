using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.UDet;

/// <summary>
/// 图像预处理器
/// 负责图像重采样、增强和边缘检测
/// </summary>
public class ImagePreprocessor
{
    /// <summary>
    /// 图像重采样（如果尺寸太小）
    /// </summary>
    /// <remarks>
    /// 若返回的 <paramref name="scale"/> 大于 1，则 <paramref name="resampledRoi"/> 为新分配的 <see cref="Mat"/>，
    /// 调用方必须在用完后 <see cref="Mat.Dispose"/>；若 <paramref name="scale"/> 为 1，则与输入 <paramref name="roi"/> 为同一引用，不得释放。
    /// </remarks>
    public (Mat resampledRoi, float scale) ResampleIfNeeded(Mat roi, int targetSize = 384)
    {
        var minSize = Math.Min(roi.Width, roi.Height);

        if (minSize < targetSize)
        {
            var scale = (float)targetSize / minSize;
            var newWidth = (int)(roi.Width * scale);
            var newHeight = (int)(roi.Height * scale);

            var resampled = new Mat();
            Cv2.Resize(roi, resampled, new Size(newWidth, newHeight), 0, 0, InterpolationFlags.Cubic);

            return (resampled, scale);
        }

        return (roi, 1.0f);
    }

    /// <summary>
    /// 图像预处理：增强对比度 + 双重边缘检测
    /// </summary>
    /// <returns>调用方对返回的 <c>enhanced</c> 与 <c>edges</c> 负责释放。</returns>
    public (Mat enhanced, Mat edges) Process(Mat roi)
    {
        using var gray = new Mat();
        if (roi.Channels() > 1)
        {
            Cv2.CvtColor(roi, gray, ColorConversionCodes.BGR2GRAY);
        }
        else
        {
            roi.CopyTo(gray);
        }

        Cv2.GaussianBlur(gray, gray, new Size(3, 3), 0);

        using var clahe = Cv2.CreateCLAHE(2.0, new Size(8, 8));
        var enhanced = new Mat();
        clahe.Apply(gray, enhanced);

        var edges = DetectEdges(enhanced);

        return (enhanced, edges);
    }

    /// <summary>
    /// 双重边缘检测策略：Canny + Otsu
    /// </summary>
    private static Mat DetectEdges(Mat enhanced)
    {
        using var edgesCanny = new Mat();
        Cv2.Canny(enhanced, edgesCanny, 15, 100);

        using var binary = new Mat();
        Cv2.Threshold(enhanced, binary, 0, 255, ThresholdTypes.BinaryInv | ThresholdTypes.Otsu);

        using (var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3)))
        {
            Cv2.MorphologyEx(binary, binary, MorphTypes.Close, kernel, iterations: 2);
            Cv2.MorphologyEx(binary, binary, MorphTypes.Open, kernel, iterations: 1);

            Cv2.MorphologyEx(edgesCanny, edgesCanny, MorphTypes.Close, kernel, iterations: 4);
            Cv2.Dilate(edgesCanny, edgesCanny, kernel, iterations: 3);
            Cv2.MorphologyEx(edgesCanny, edgesCanny, MorphTypes.Close, kernel, iterations: 3);
        }

        var edges = new Mat();
        Cv2.BitwiseOr(edgesCanny, binary, edges);

        // 中心椭圆蒙版：抑制边缘区域杂讯，专注中心U型字符分割
        using var mask = new Mat(edges.Size(), MatType.CV_8UC1, Scalar.Black);
        var center = new Point(edges.Width / 2, edges.Height / 2);
        var maskAxes = new Size((int)(edges.Width * 0.47), (int)(edges.Height * 0.47));
        Cv2.Ellipse(mask, center, maskAxes, 0, 0, 360, Scalar.White, -1);
        Cv2.BitwiseAnd(edges, mask, edges);

        return edges;
    }
}
