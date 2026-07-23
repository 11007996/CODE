using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.UDet;

/// <summary>
/// U型符号方向检测器
/// 基于凸包缺陷分析的U型开口方向检测算法
/// </summary>
public class UShapeDetector : AoiDetectorBase
{
    private readonly ImagePreprocessor _preprocessor = new();
    private ContourAnalyzer? _contourAnalyzer;
    private OrientationCalculator? _orientationCalculator;

    public override string Name => "UShapeDetector";

    public UShapeDetector(ILogger logger) : base(logger)
    {
    }

    protected override void OnInitialize()
    {
        _contourAnalyzer = new ContourAnalyzer(_logger);
        _orientationCalculator = new OrientationCalculator(_logger);
    }

    protected override AoiResult OnDetect(Mat roi, Dictionary<string, object> parameters)
    {
        var result = new AoiResult();
        var startTime = DateTime.Now;

        var targetSize = GetParameter("target_size", 256, parameters);
        var (processedRoi, scale) = _preprocessor.ResampleIfNeeded(roi, targetSize);
        var ownProcessedRoi = scale > 1.0f;

        Mat? enhanced = null;
        Mat? edges = null;

        try
        {
            _logger.LogDebug("开始 U 型检测，图像尺寸: {Width}x{Height}", roi.Width, roi.Height);

            if (scale > 1.0f)
            {
                _logger.LogDebug("图像重采样: {OldW}x{OldH} -> {NewW}x{NewH} (缩放: {Scale:F2}x)",
                    roi.Width, roi.Height, processedRoi.Width, processedRoi.Height, scale);
            }

            (enhanced, edges) = _preprocessor.Process(processedRoi);
            _logger.LogDebug("图像预处理完成: CLAHE增强 + Canny + Otsu + 形态学操作");

            var minAreaRatio = GetParameter("min_area_ratio", 0.005, parameters);
            var maxAreaRatio = GetParameter("max_area_ratio", 0.95, parameters);
            var validContours = _contourAnalyzer!.FindAndFilterContours(edges, processedRoi.Size(), minAreaRatio, maxAreaRatio);

            if (validContours.Length == 0)
            {
                result.Success = false;
                result.Message = "未找到有效的 U 型轮廓";
                result.Confidence = 0.0f;
                return result;
            }

            var smoothFactor = GetParameter("smooth_factor", 0.003, parameters);
            var targetContour = _contourAnalyzer.SelectAndSmoothContour(validContours, smoothFactor);

            var moments = Cv2.Moments(targetContour);
            if (moments.M00 == 0)
            {
                result.Success = false;
                result.Message = "无法计算轮廓质心";
                return result;
            }

            var centroid = new Point2f(
                (float)(moments.M10 / moments.M00),
                (float)(moments.M01 / moments.M00)
            );

            var orientation = _orientationCalculator!.AnalyzeUShapeOrientation(targetContour, centroid, processedRoi.Size());

            if (orientation == null)
            {
                result.Success = false;
                result.Message = "凸包缺陷检测失败，无法确定开口方向";
                result.Confidence = 0.0f;
                return result;
            }

            result.SetOrientation(orientation.Angle, orientation.Direction, orientation.OpeningCenter);
            result.Confidence = orientation.Confidence;
            result.Success = true;
            result.Message = "检测成功";

            result.Set("centroid", centroid);
            result.Set("endpoint1", orientation.Endpoint1);
            result.Set("endpoint2", orientation.Endpoint2);
            result.Set("contour", targetContour);
            result.Set("contour_point_count", targetContour.Length);
            result.Set("scale", scale);

            var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
            _logger.LogDebug("U 型检测完成: {Direction}, {Angle:F1}°, 耗时: {Elapsed:F1}ms",
                orientation.Direction, orientation.Angle, elapsed);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
            result.Confidence = 0.0f;
            _logger.LogError(ex, "U 型检测失败");
        }
        finally
        {
            enhanced?.Dispose();
            edges?.Dispose();
            if (ownProcessedRoi)
                processedRoi.Dispose();
        }

        return result;
    }
}
