using LuxVideoDet.Core.Inference.Results;
using OpenCvSharp;
using System;
using SysPoint = System.Drawing.Point;

namespace LuxVideoDet.Core.Utils;

public static class DrawingHelper
{
    public static void DrawText(Mat image, string text, SysPoint position,
        double fontScale = 0.7, Scalar? color = null, int thickness = 2)
    {
        var textColor = color ?? new Scalar(0, 255, 0);
        Cv2.PutText(image, text, new Point(position.X, position.Y),
            HersheyFonts.HersheySimplex, fontScale, textColor, thickness);
    }

    /// <summary>
    /// 左上角叠加：约 60 秒滑动窗口的平均管线耗时（毫秒），以及展示用 fps（与 <see cref="RollingMinutePipelineStats.GetAverages"/> 一致，
    /// 且由 <see cref="DetectionAlgorithmBase"/> 按视频源标称帧率上限裁剪后传入）。
    /// </summary>
    public static void DrawPipelineMinuteStats(Mat image, double avgFpsLastMinute, float avgPipelineMsLastMinute,
        int yPosition = 30)
    {
        var text =
            $"fps: {avgFpsLastMinute:F1}  inference: {avgPipelineMsLastMinute:F1}ms";
        DrawText(image, text, new SysPoint(20, yPosition), 0.7, new Scalar(0, 255, 0), 2);
    }

    /// <summary>
    /// 在整幅图像上叠加实例分割掩膜（半透明着色）。掩膜坐标相对于 <paramref name="bbox"/> 左上角。
    /// </summary>
    public static void DrawInstanceMask(Mat image, BoundingBox bbox, byte[,] mask, Scalar color,
        float alpha = 0.35f)
    {
        if (mask == null || image.Empty()) return;

        int h = mask.GetLength(0);
        int w = mask.GetLength(1);
        if (h == 0 || w == 0) return;

        int x1 = (int)bbox.X1;
        int y1 = (int)bbox.Y1;
        int imgH = image.Height;
        int imgW = image.Width;

        double a = Math.Clamp(alpha, 0.0, 1.0);
        double inv = 1.0 - a;

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if (mask[y, x] < 128) continue;

                int iy = y1 + y;
                int ix = x1 + x;
                if (iy < 0 || ix < 0 || iy >= imgH || ix >= imgW) continue;

                var p = image.At<Vec3b>(iy, ix);
                p.Item0 = (byte)Math.Clamp(p.Item0 * inv + color.Val0 * a, 0, 255);
                p.Item1 = (byte)Math.Clamp(p.Item1 * inv + color.Val1 * a, 0, 255);
                p.Item2 = (byte)Math.Clamp(p.Item2 * inv + color.Val2 * a, 0, 255);
                image.At<Vec3b>(iy, ix) = p;
            }
        }
    }

    /// <summary>
    /// 绘制单个检测结果：若 <see cref="Detection.Mask"/> 存在则先叠加掩膜，再绘制检测框与标签。
    /// （实例分割模型与检测模型共用此入口。）
    /// </summary>
    public static void DrawDetectionAnnotation(
        Mat image,
        Detection detection,
        Scalar color,
        bool showBox = true,
        bool showLabel = true,
        int boxThickness = 2,
        float maskAlpha = 0.35f,
        Func<Detection, string>? labelFormatter = null)
    {
        if (detection.Mask != null)
            DrawInstanceMask(image, detection.BoundingBox, detection.Mask, color, maskAlpha);

        if (showBox)
        {
            var bbox = detection.BoundingBox;
            Cv2.Rectangle(image,
                new Point((int)bbox.X1, (int)bbox.Y1),
                new Point((int)bbox.X2, (int)bbox.Y2),
                color, boxThickness);
        }

        if (showLabel)
        {
            var name = labelFormatter?.Invoke(detection) ?? detection.ClassName ?? string.Empty;
            var pos = new Point((int)detection.BoundingBox.X1, (int)detection.BoundingBox.Y1 - 5);
            if (detection.TrackId is int tid)
                DrawTrackAwareLabel(image, pos, tid, name, detection.Confidence, color);
            else
            {
                var label = $"{name} {detection.Confidence:F2}";
                DrawLabel(image, label, pos, color);
            }
        }
    }

    /// <summary>
    /// 追踪模式角标：<c>#轨迹号</c> 为纯白并略加粗，其后类别与置信度为白字，黑底与类别色描边同 <see cref="DrawLabel"/>。
    /// </summary>
    private static void DrawTrackAwareLabel(Mat image, Point position, int trackId,
        string classLabel, float confidence, Scalar boxAccentColor)
    {
        const int labelFontSize = 20;
        const int padX = 6;
        const int padY = 4;
        const int borderThickness = 2;
        const int segmentGap = 8;
        var white = new Scalar(255, 255, 255);

        var trackText = $"#{trackId}";
        var tail = string.IsNullOrWhiteSpace(classLabel)
            ? $"{confidence:F2}"
            : $" {classLabel.Trim()}  {confidence:F2}";

        var (twT, thT) = ChineseTextRenderer.MeasureTextLine(trackText, labelFontSize, slightBold: true);
        var (twR, thR) = ChineseTextRenderer.MeasureTextLine(tail, labelFontSize);
        if (twT <= 0 || thT <= 0 || twR <= 0 || thR <= 0)
            return;

        var innerW = twT + segmentGap + twR;
        var innerH = Math.Max(thT, thR);
        var boxW = innerW + padX * 2;
        var boxH = innerH + padY * 2;
        var labelY = Math.Max(0, position.Y - boxH - 5);
        var rect = new Rect(position.X, labelY, boxW, boxH);

        Cv2.Rectangle(image, rect, new Scalar(0, 0, 0), -1);
        Cv2.Rectangle(image, rect, boxAccentColor, borderThickness);

        var textBaseY = labelY + padY;
        var x0 = position.X + padX;
        ChineseTextRenderer.DrawChineseText(
            image, trackText, new SysPoint(x0, textBaseY),
            labelFontSize, white, lineSpacing: 6, maxWidth: null, align: "left", slightBold: true);
        ChineseTextRenderer.DrawChineseText(
            image, tail, new SysPoint(x0 + twT + segmentGap, textBaseY),
            labelFontSize, white, lineSpacing: 6, maxWidth: null);
    }

    public static void DrawDetectionBox(Mat image, float[] box, string label,
        float confidence, Scalar color, int thickness = 2)
    {
        var x1 = (int)box[0];
        var y1 = (int)box[1];
        var x2 = (int)box[2];
        var y2 = (int)box[3];

        Cv2.Rectangle(image, new Rect(x1, y1, x2 - x1, y2 - y1), color, thickness);

        var labelText = $"{label}: {confidence:F2}";
        DrawLabel(image, labelText, new Point(x1, y1), color);
    }

    /// <summary>
    /// 检测框角标：黑底、白字、类别色描边，避免与框同色填充时对比不足。
    /// </summary>
    public static void DrawLabel(Mat image, string label, Point position, Scalar color)
    {
        if (string.IsNullOrEmpty(label))
            return;

        const int labelFontSize = 20;
        const int padX = 6;
        const int padY = 4;
        const int borderThickness = 2;
        var (tw, th) = ChineseTextRenderer.MeasureTextLine(label, labelFontSize);
        if (tw <= 0 || th <= 0)
            return;

        int boxW = tw + padX * 2;
        int boxH = th + padY * 2;
        var labelY = Math.Max(0, position.Y - boxH - 5);
        var rect = new Rect(position.X, labelY, boxW, boxH);

        Cv2.Rectangle(image, rect, new Scalar(0, 0, 0), -1);
        Cv2.Rectangle(image, rect, color, borderThickness);

        ChineseTextRenderer.DrawChineseText(
            image,
            label,
            new SysPoint(position.X + padX, labelY + padY),
            labelFontSize,
            new Scalar(255, 255, 255),
            lineSpacing: 6,
            maxWidth: null);
    }

    public static void DrawStateInfo(Mat image, string stateMessage, Scalar color, int fontSize = 35, int yPosition = 30)
    {
        var rightX = image.Width - 20;
        var position = new SysPoint(rightX, yPosition);
        
        ChineseTextRenderer.DrawChineseText(
            image, 
            stateMessage, 
            position,
            fontSize,
            color,
            lineSpacing: 6,
            maxWidth: null,
            align: "right"
        );
    }

    /// <summary>
    /// 右上角 HUD：深色半透明底、左侧强调条、自动换行，适合产线监控（替代裸字 <see cref="DrawStateInfo"/>）。
    /// </summary>
    /// <param name="accentColor">左侧强调条 BGR，默认琥珀色。</param>
    /// <param name="minimumTextBlockWidth">
    /// 文本块最小宽度（像素）：用于避免数字位数变化导致面板忽大忽小；0 表示不限制。
    /// </param>
    public static void DrawStateInfoPanel(
        Mat image,
        string stateMessage,
        Scalar? accentColor = null,
        int fontSize = 26,
        int topMargin = 18,
        int rightMargin = 18,
        int minimumTextBlockWidth = 0)
    {
        if (string.IsNullOrEmpty(stateMessage) || image.Empty())
            return;

        var maxTextWidth = Math.Min(920, Math.Max(260, image.Width * 48 / 100));
        var (tw, th) = ChineseTextRenderer.MeasureChineseText(stateMessage, fontSize, maxTextWidth, 6);
        if (tw <= 0 || th <= 0)
        {
            DrawStateInfo(image, stateMessage, new Scalar(220, 230, 240), fontSize + 2, topMargin);
            return;
        }

        if (minimumTextBlockWidth > 0)
        {
            tw = Math.Max(tw, minimumTextBlockWidth);
        }

        const int padH = 16;
        const int padV = 13;
        const int accentW = 4;
        const int shadowOff = 3;

        var panelW = tw + padH * 2 + accentW;
        var panelH = th + padV * 2;

        var x2 = image.Width - rightMargin;
        var x1 = x2 - panelW;
        var y1 = topMargin;
        var y2 = y1 + panelH;

        if (x1 < 8)
        {
            x1 = 8;
            x2 = x1 + panelW;
        }

        var accent = accentColor ?? new Scalar(64, 152, 255);

        // 轻阴影（略偏移的深色块）
        Cv2.Rectangle(
            image,
            new Point(x1 + shadowOff, y1 + shadowOff),
            new Point(x2 + shadowOff, y2 + shadowOff),
            new Scalar(18, 16, 14),
            -1);

        // 主底：深灰蓝
        Cv2.Rectangle(image, new Point(x1, y1), new Point(x2, y2), new Scalar(48, 46, 42), -1);

        // 左侧强调条
        Cv2.Rectangle(image, new Point(x1, y1), new Point(x1 + accentW, y2), accent, -1);

        // 细边框
        Cv2.Rectangle(image, new Point(x1, y1), new Point(x2, y2), new Scalar(95, 88, 82), 1);

        var textX = x1 + accentW + padH;
        var textY = y1 + padV;
        ChineseTextRenderer.DrawChineseText(
            image,
            stateMessage,
            new SysPoint(textX, textY),
            fontSize,
            new Scalar(248, 250, 252),
            lineSpacing: 6,
            maxWidth: maxTextWidth,
            align: "left");
    }

    public static void DrawMachineInfo(Mat image, string machineName, string algorithmType)
    {
        var text = $"{machineName} - {algorithmType}";
        DrawText(image, text, new SysPoint(20, 70), 0.8, new Scalar(0, 255, 0), 2);
    }

    public static Scalar[] GenerateColorPalette(int numColors, int seed = 42)
    {
        var random = new Random(seed);
        var colors = new Scalar[numColors];

        for (int i = 0; i < numColors; i++)
        {
            colors[i] = new Scalar(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256)
            );
        }

        return colors;
    }
}
