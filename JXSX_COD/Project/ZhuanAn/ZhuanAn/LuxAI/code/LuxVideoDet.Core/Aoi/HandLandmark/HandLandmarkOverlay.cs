using OpenCvSharp;

namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>
/// 在 BGR 画面上叠画手部骨架与关键点（与模型检测框同帧叠加）。
/// </summary>
public static class HandLandmarkOverlay
{
    private static readonly HandLandmarkOverlayDrawOptions DefaultOptions = new();

    /// <summary>原地绘制；无手或 result 为 null 时直接返回。</summary>
    public static void DrawHands(
        Mat frameBgr,
        HandLandmarkerFrameInferenceResult? result,
        HandLandmarkOverlayDrawOptions? options = null)
    {
        if (frameBgr == null || frameBgr.Empty() || result == null || result.Hands.Count == 0)
            return;

        options ??= DefaultOptions;
        var w = frameBgr.Width;
        var h = frameBgr.Height;
        if (w <= 0 || h <= 0)
            return;

        var dotR = Math.Max(4, Math.Min(w, h) / 200);

        foreach (var hand in result.Hands)
        {
            var lms = hand.Landmarks;
            if (lms.Count < 21)
                continue;

            var pts = ToPixelPoints(lms, frameBgr, result);

            foreach (var (start, end) in HandLandmarkTopology.HandConnections)
            {
                if (start >= pts.Length || end >= pts.Length)
                    continue;

                var lineBgr = HandLandmarkTopology.GetConnectionLineBgr(start, end);
                Cv2.Line(frameBgr, pts[start], pts[end], lineBgr, options.LineThickness, LineTypes.AntiAlias);
            }

            for (var li = 0; li < pts.Length; li++)
                DrawFilledDotWithOutline(frameBgr, pts[li], HandLandmarkTopology.GetLandmarkFillBgr(li), dotR);

            if (options.ShowHandednessLabel && hand.Handedness.Categories.Count > 0)
            {
                var cat = hand.Handedness.Categories[0];
                var label = cat.CategoryName;
                if (!string.IsNullOrEmpty(label))
                {
                    var wrist = pts[0];
                    var org = new Point(wrist.X + 8, wrist.Y - 8);
                    PutTextWithOutline(frameBgr, label, org, options.LabelFontScale, options.LabelForegroundBgr);
                }
            }
        }
    }

    private static Point[] ToPixelPoints(
        IReadOnlyList<HandLandmarkPoint> landmarks,
        Mat frame,
        HandLandmarkerFrameInferenceResult r)
    {
        var srcW = r.SourceImageWidth > 0 ? r.SourceImageWidth : frame.Width;
        var srcH = r.SourceImageHeight > 0 ? r.SourceImageHeight : frame.Height;
        var sx = frame.Width / (double)Math.Max(1, srcW);
        var sy = frame.Height / (double)Math.Max(1, srcH);

        var pts = new Point[landmarks.Count];
        for (var i = 0; i < landmarks.Count; i++)
        {
            double xSrc;
            double ySrc;
            if (r.ImageLandmarksCoordinateKind == HandLandmarkImageCoordinateKind.PixelCoordinatesInSourceImage)
            {
                xSrc = landmarks[i].X;
                ySrc = landmarks[i].Y;
            }
            else
            {
                xSrc = landmarks[i].X * srcW;
                ySrc = landmarks[i].Y * srcH;
            }

            pts[i] = new Point(
                (int)Math.Round(xSrc * sx),
                (int)Math.Round(ySrc * sy));
        }

        return pts;
    }

    private static void DrawFilledDotWithOutline(Mat frame, Point center, Scalar fillBgr, int radiusInner)
    {
        var rOut = radiusInner + 2;
        Cv2.Circle(frame, center, rOut, HandLandmarkTopology.OutlineBgr, -1, LineTypes.AntiAlias);
        Cv2.Circle(frame, center, radiusInner, fillBgr, -1, LineTypes.AntiAlias);
    }

    private static void PutTextWithOutline(Mat frame, string text, Point org, double fontScale, Scalar fgBgr)
    {
        const HersheyFonts font = HersheyFonts.HersheySimplex;
        foreach (var (dx, dy) in new (int, int)[]
                 {
                     (1, 0), (-1, 0), (0, 1), (0, -1), (1, 1), (1, -1), (-1, 1), (-1, -1),
                 })
        {
            Cv2.PutText(frame, text, new Point(org.X + dx, org.Y + dy), font, fontScale, Scalar.Black, 2,
                LineTypes.AntiAlias);
        }

        Cv2.PutText(frame, text, org, font, fontScale, fgBgr, 1, LineTypes.AntiAlias);
    }
}
