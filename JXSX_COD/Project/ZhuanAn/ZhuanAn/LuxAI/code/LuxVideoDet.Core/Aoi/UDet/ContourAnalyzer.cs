using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.UDet;

/// <summary>
/// 轮廓分析器
/// 负责轮廓检测、筛选、平滑处理、弧长重采样与滑动窗口平滑
/// </summary>
public class ContourAnalyzer
{
    private readonly ILogger _logger;

    public ContourAnalyzer(ILogger logger)
    {
        _logger = logger;
    }

    private const double MinSolidity = 0.4;

    /// <summary>
    /// 查找并筛选有效轮廓
    /// </summary>
    public Point[][] FindAndFilterContours(Mat edges, Size imageSize,
        double minAreaRatio = 0.005, double maxAreaRatio = 0.95)
    {
        Cv2.FindContours(edges, out var contours, out _,
            RetrievalModes.External, ContourApproximationModes.ApproxSimple);

        if (contours.Length == 0)
        {
            _logger.LogDebug("未找到任何轮廓");
            return Array.Empty<Point[]>();
        }

        _logger.LogDebug("找到 {Count} 个轮廓", contours.Length);

        var imgArea = imageSize.Width * imageSize.Height;
        var imgCenter = new Point2f(imageSize.Width / 2f, imageSize.Height / 2f);
        var minAreaPixels = Math.Min(500, imgArea * 0.01);
        var maxDistToCenter = Math.Sqrt(imgCenter.X * imgCenter.X + imgCenter.Y * imgCenter.Y) * 0.8;

        var validContours = new List<Point[]>();

        for (var i = 0; i < contours.Length; i++)
        {
            var contour = contours[i];
            var area = Cv2.ContourArea(contour);
            var areaRatio = area / imgArea;

            var m = Cv2.Moments(contour);
            if (m.M00 == 0)
                continue;

            var cx = (float)(m.M10 / m.M00);
            var cy = (float)(m.M01 / m.M00);
            var distToCenter = Math.Sqrt((cx - imgCenter.X) * (cx - imgCenter.X) +
                                        (cy - imgCenter.Y) * (cy - imgCenter.Y));

            if (!(areaRatio > minAreaRatio && areaRatio < maxAreaRatio &&
                  area > minAreaPixels && distToCenter < maxDistToCenter))
                continue;

            if (IsMessyContour(contour))
            {
                _logger.LogDebug("  轮廓{Index}: 混乱轮廓，跳过", i);
                continue;
            }

            validContours.Add(contour);
            _logger.LogDebug("  轮廓{Index}: 面积={Area:F0} ({Ratio:F1}%), 距中心={Dist:F1} ✓",
                i, area, areaRatio * 100, distToCenter);
        }

        _logger.LogDebug("筛选后有效轮廓数: {Count}", validContours.Count);
        return validContours.ToArray();
    }

    /// <summary>
    /// 判断轮廓是否混乱：solidity 过低（形状不规则）
    /// </summary>
    private static bool IsMessyContour(Point[] contour)
    {
        if (contour.Length < 10)
            return true;

        var area = Cv2.ContourArea(contour);
        if (area <= 0)
            return true;

        var hull = Cv2.ConvexHullIndices(contour);
        if (hull.Length < 3)
            return true;

        var hullArea = Cv2.ContourArea(Cv2.ConvexHull(contour));
        var solidity = hullArea > 0 ? area / hullArea : 0;

        return solidity < MinSolidity;
    }

    /// <summary>
    /// 选择最大轮廓并进行平滑处理（approxPolyDP + 弧长重采样 + 滑动窗口平滑）
    /// </summary>
    public Point[] SelectAndSmoothContour(Point[][] contours, double smoothFactor = 0.003)
    {
        var targetContour = contours.OrderByDescending(c => Cv2.ContourArea(c)).First();
        var area = Cv2.ContourArea(targetContour);

        _logger.LogDebug("选择最大轮廓，面积: {Area:F0}", area);

        // 第1步：approxPolyDP 多边形近似
        var epsilon = smoothFactor * Cv2.ArcLength(targetContour, true);
        var polyApprox = Cv2.ApproxPolyDP(targetContour, epsilon, true);

        Point[] working;
        if (polyApprox.Length >= 10)
        {
            _logger.LogDebug("多边形近似: {Original} -> {Smoothed} 个点", targetContour.Length, polyApprox.Length);
            working = polyApprox;
        }
        else
        {
            _logger.LogDebug("多边形近似后点数太少({Count})，使用原始轮廓", polyApprox.Length);
            working = targetContour;
        }

        // 第2步：均匀弧长重采样
        var resampled = ResampleUniformArcLength(working);

        // 第3步：圆形滑动窗口平滑
        var smoothed = SlidingWindowSmooth(resampled);

        _logger.LogDebug("轮廓处理完成: {Raw} -> {Poly} -> {Resampled} -> {Smoothed} 点",
            targetContour.Length, working.Length, resampled.Length, smoothed.Length);

        return smoothed;
    }

    /// <summary>
    /// 均匀弧长重采样：沿轮廓以等弧长间距重新采样，消除点疏密不均
    /// </summary>
    private static Point[] ResampleUniformArcLength(Point[] contour)
    {
        if (contour.Length < 3)
            return contour;

        var pts = contour.Select(p => new Point2f(p.X, p.Y)).ToArray();
        var n = pts.Length;

        // 目标采样点数：50-200，取原轮廓点数的3倍
        var numSamples = Math.Max(50, Math.Min(200, n * 3));

        // 计算每段弧长
        var segLens = new float[n];
        for (var i = 0; i < n; i++)
        {
            var next = (i + 1) % n;
            var dx = pts[next].X - pts[i].X;
            var dy = pts[next].Y - pts[i].Y;
            segLens[i] = MathF.Sqrt(dx * dx + dy * dy);
        }

        // 累积弧长
        var cumLen = new float[n];
        cumLen[0] = 0;
        for (var i = 1; i < n; i++)
        {
            cumLen[i] = cumLen[i - 1] + segLens[i - 1];
        }

        var totalArc = cumLen[n - 1] + segLens[n - 1];
        if (totalArc <= 0)
            return contour;

        // 等间距采样
        var result = new Point[numSamples];
        for (var i = 0; i < numSamples; i++)
        {
            var targetDist = totalArc * i / numSamples;

            // 找到 targetDist 落在哪个线段上
            var segIdx = 0;
            for (var j = 1; j < n; j++)
            {
                if (cumLen[j] > targetDist)
                {
                    segIdx = j - 1;
                    break;
                }
                segIdx = n - 1;
            }

            var segStart = cumLen[segIdx];
            var segEnd = segIdx == n - 1 ? totalArc : cumLen[(segIdx + 1) % n];
            var t = segEnd > segStart ? (targetDist - segStart) / (segEnd - segStart) : 0;
            t = Math.Clamp(t, 0, 1);

            var nextIdx = (segIdx + 1) % n;
            var x = pts[segIdx].X + t * (pts[nextIdx].X - pts[segIdx].X);
            var y = pts[segIdx].Y + t * (pts[nextIdx].Y - pts[segIdx].Y);

            result[i] = new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        return result;
    }

    /// <summary>
    /// 圆形滑动窗口平滑：每个点取周围窗口内的均值，消除锯齿
    /// </summary>
    private static Point[] SlidingWindowSmooth(Point[] contour)
    {
        if (contour.Length < 3)
            return contour;

        var n = contour.Length;
        // 窗口大小约为轮廓长度的 5%
        var window = Math.Max(3, n / 20);
        var half = window / 2;

        var result = new Point[n];
        for (var i = 0; i < n; i++)
        {
            float sumX = 0, sumY = 0;
            var count = 0;
            for (var k = -half; k <= half; k++)
            {
                var idx = (i + k + n) % n;
                sumX += contour[idx].X;
                sumY += contour[idx].Y;
                count++;
            }

            result[i] = new Point((int)Math.Round(sumX / count), (int)Math.Round(sumY / count));
        }

        return result;
    }
}
