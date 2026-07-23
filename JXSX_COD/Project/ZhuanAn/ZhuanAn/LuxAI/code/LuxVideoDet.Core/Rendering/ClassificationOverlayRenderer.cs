using LuxVideoDet.Core.Inference.Results;
using LuxVideoDet.Core.Utils;
using OpenCvSharp;
using SysPoint = System.Drawing.Point;

namespace LuxVideoDet.Core.Rendering;

/// <summary>
/// 图像分类任务专用叠加层：整图一个预测向量，无检测框；在画面一角展示 Top-K 类别与置信度。
/// </summary>
public static class ClassificationOverlayRenderer
{
    private const string DefaultTitle = "图像分类";

    public static void Draw(Mat image, Detection detection, Scalar accentColor, DetectionDrawOptions options)
    {
        var probs = detection.Probabilities;
        if (probs is not { Length: > 0 })
        {
            DrawFallbackTop1(image, detection, accentColor);
            return;
        }

        int k = Math.Min(options.ClassificationTopK, probs.Length);
        var indexed = probs
            .Select((p, i) => (Index: i, P: p))
            .OrderByDescending(x => x.P)
            .Take(k)
            .ToList();

        var labels = detection.PerClassLabels;
        string LabelFor(int idx) =>
            labels is { Count: > 0 } && idx >= 0 && idx < labels.Count
                ? labels[idx]
                : $"class{idx}";

        const int margin = 12;
        int lineSpacing = 26;
        int titleBlock = 36;
        int panelW = Math.Min(460, image.Width - 2 * margin);
        int panelH = titleBlock + 12 + indexed.Count * lineSpacing + 16;
        panelH = Math.Min(panelH, image.Height - 2 * margin);
        panelW = Math.Max(panelW, 120);

        var roi = new Rect(margin, margin, panelW, panelH);
        if (roi.X + roi.Width > image.Width) roi.Width = image.Width - roi.X - 1;
        if (roi.Y + roi.Height > image.Height) roi.Height = image.Height - roi.Y - 1;
        if (roi.Width <= 0 || roi.Height <= 0) return;

        if (options.ClassificationShowPanel)
        {
            using var roiMat = new Mat(image, roi);
            using var tint = new Mat(roi.Height, roi.Width, MatType.CV_8UC3, new Scalar(28, 32, 38));
            double blend = Math.Clamp(options.ClassificationPanelBlend, 0.15, 0.55);
            Cv2.AddWeighted(roiMat, 1.0 - blend, tint, blend, 0, roiMat);
        }

        var textColor = new Scalar(230, 230, 230);
        int x = margin + 14;
        int y = margin + 28;

        ChineseTextRenderer.DrawChineseText(
            image, DefaultTitle, new SysPoint(x, y),
            fontSize: 22, color: textColor, lineSpacing: 4, maxWidth: null, align: "left");
        y += titleBlock - 8;

        int rank = 1;
        foreach (var item in indexed)
        {
            var name = LabelFor(item.Index);
            var line = $"{rank}. {name}   {item.P * 100f:F1}%";
            var lineColor = rank == 1
                ? accentColor
                : textColor;

            ChineseTextRenderer.DrawChineseText(
                image, line, new SysPoint(x, y),
                fontSize: 18, color: lineColor, lineSpacing: 4, maxWidth: panelW - 28, align: "left");

            y += lineSpacing;
            rank++;
        }
    }

    private static void DrawFallbackTop1(Mat image, Detection detection, Scalar accentColor)
    {
        if (string.IsNullOrEmpty(detection.ClassName)) return;
        var line = $"{detection.ClassName}  {detection.Confidence:F2}";
        ChineseTextRenderer.DrawChineseText(
            image, line, new SysPoint(20, 32),
            fontSize: 20, color: accentColor, lineSpacing: 4, maxWidth: null, align: "left");
    }
}
