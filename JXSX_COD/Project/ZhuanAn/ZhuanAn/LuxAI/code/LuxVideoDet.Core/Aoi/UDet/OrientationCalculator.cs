using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.UDet;

/// <summary>
/// 方向计算器
/// 负责U型开口方向的分析和计算（凸包缺陷 + 端点局部平均）
/// </summary>
public class OrientationCalculator
{
    private readonly ILogger _logger;

    /// <summary>端点局部平均的采样点数（每侧）</summary>
    private const int EndpointK = 10;

    public OrientationCalculator(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 方向分析结果
    /// </summary>
    public record OrientationResult(
        float Angle,
        string Direction,
        Point2f OpeningCenter,
        Point Endpoint1,
        Point Endpoint2,
        float Confidence
    );

    /// <summary>
    /// U型方向分析（核心算法）
    /// 使用凸包缺陷分析找到U型开口的两个端点，并通过局部平均消除单点偏移
    /// </summary>
    /// <returns>分析结果；若凸包缺陷检测失败则返回 null</returns>
    public OrientationResult? AnalyzeUShapeOrientation(Point[] contour, Point2f centroid, Size imageSize)
    {
        var hull = Cv2.ConvexHullIndices(contour);

        Vec4i[]? defects;
        try
        {
            defects = Cv2.ConvexityDefects(contour, hull);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "凸包缺陷检测异常（轮廓可能自交或索引不单调），回退为 null");
            defects = null;
        }

        if (defects == null || defects.Length == 0)
        {
            _logger.LogDebug("未找到凸包缺陷，检测失败");
            return null;
        }

        _logger.LogDebug("找到 {Count} 个凸包缺陷", defects.Length);

        // 找到跨度最大的缺陷
        var n = contour.Length;
        var maxSpan = 0.0;
        Vec4i bestDefect = default;

        foreach (var defect in defects)
        {
            var startIdx = defect.Item0;
            var endIdx = defect.Item1;

            var startPt = contour[startIdx];
            var endPt = contour[endIdx];

            var dx = endPt.X - startPt.X;
            var dy = endPt.Y - startPt.Y;
            var span = Math.Sqrt(dx * dx + dy * dy);

            if (span > maxSpan)
            {
                maxSpan = span;
                bestDefect = defect;
            }
        }

        _logger.LogDebug("最大端点跨度: {Span:F1} 像素", maxSpan);

        // 端点局部平均：取缺陷点周围 k 个点的质心，抵消单点弧度偏移
        var k = Math.Min(EndpointK, n / 5);
        var avg1 = AverageAroundIndex(contour, bestDefect.Item0, k);
        var avg2 = AverageAroundIndex(contour, bestDefect.Item1, k);

        var endpoint1 = new Point((int)Math.Round(avg1.X), (int)Math.Round(avg1.Y));
        var endpoint2 = new Point((int)Math.Round(avg2.X), (int)Math.Round(avg2.Y));
        var defectDepth = bestDefect.Item3 / 256.0f;

        _logger.LogDebug("端点1: ({X1}, {Y1}), 端点2: ({X2}, {Y2}), 缺陷深度: {Depth:F1}, 局部平均k={K}",
            endpoint1.X, endpoint1.Y, endpoint2.X, endpoint2.Y, defectDepth, k);

        var openingCenter = new Point2f(
            (endpoint1.X + endpoint2.X) / 2f,
            (endpoint1.Y + endpoint2.Y) / 2f
        );

        var deltaX = openingCenter.X - centroid.X;
        var deltaY = openingCenter.Y - centroid.Y;
        var angleRad = Math.Atan2(-deltaY, deltaX);
        var angleDeg = angleRad * 180.0 / Math.PI;
        var orientationAngle = (float)((90 - angleDeg + 360) % 360);

        var direction = DetermineDirection(orientationAngle);
        var confidence = CalculateConfidence(contour, (float)maxSpan, defectDepth, imageSize);

        _logger.LogDebug("质心: ({CX:F1}, {CY:F1}), 开口中心: ({OX:F1}, {OY:F1})",
            centroid.X, centroid.Y, openingCenter.X, openingCenter.Y);
        _logger.LogDebug("偏移: dx={DX:F1}, dy={DY:F1}, 角度: {Angle:F1}°, 方向: {Direction}",
            deltaX, deltaY, orientationAngle, direction);

        return new OrientationResult(orientationAngle, direction, openingCenter, endpoint1, endpoint2, confidence);
    }

    /// <summary>
    /// 端点局部平均：取 contour[index] 周围 ±k 个点的质心
    /// </summary>
    private static Point2f AverageAroundIndex(Point[] contour, int index, int k)
    {
        var n = contour.Length;
        float sumX = 0, sumY = 0;
        var count = 0;

        for (var i = -k; i <= k; i++)
        {
            var idx = (index + i + n) % n;
            sumX += contour[idx].X;
            sumY += contour[idx].Y;
            count++;
        }

        return new Point2f(sumX / count, sumY / count);
    }

    /// <summary>
    /// 根据角度确定方向标签
    /// </summary>
    private static string DetermineDirection(float angle)
    {
        if (angle >= 315 || angle < 45)
        {
            return "Up";
        }
        else if (angle >= 45 && angle < 135)
        {
            return "Right";
        }
        else if (angle >= 135 && angle < 225)
        {
            return "Down";
        }
        else
        {
            return "Left";
        }
    }

    /// <summary>
    /// 计算检测置信度
    /// </summary>
    private static float CalculateConfidence(Point[] contour, float endpointSpan, float defectDepth, Size imageSize)
    {
        var minImageSize = Math.Min(imageSize.Width, imageSize.Height);
        var spanRatio = endpointSpan / minImageSize;
        var spanScore = Math.Min(1.0f, spanRatio * 3);

        var depthScore = Math.Min(1.0f, defectDepth / 50);

        var area = Cv2.ContourArea(contour);
        var perimeter = Cv2.ArcLength(contour, true);
        var compactness = perimeter > 0 ? (float)(4 * Math.PI * area / (perimeter * perimeter)) : 0;
        var compactnessScore = compactness * 2;

        var confidence = spanScore * 0.5f + depthScore * 0.3f + compactnessScore * 0.2f;

        return Math.Min(1.0f, Math.Max(0.0f, confidence));
    }
}
