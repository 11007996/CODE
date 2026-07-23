using OpenCvSharp;
using SkiaSharp;
using SysPoint = System.Drawing.Point;

namespace LuxVideoDet.Core.Utils;

public static class ChineseTextRenderer
{
    private static SKTypeface? _cachedTypeface;
    private static readonly object _typefaceLock = new();

    private static readonly string[] FontNames =
    [
        "Microsoft YaHei",
        "SimHei",
        "Noto Sans CJK SC",
        "Noto Sans SC",
        "WenQuanYi Micro Hei",
        "PingFang SC",
        "Hiragino Sans GB",
        "Source Han Sans SC"
    ];

    private static readonly string[] FontFilePaths =
    [
        "C:/Windows/Fonts/msyh.ttc",
        "C:/Windows/Fonts/simhei.ttf",
        "C:/Windows/Fonts/simsun.ttc",
        "/usr/share/fonts/opentype/noto/NotoSansCJK-Regular.ttc",
        "/usr/share/fonts/truetype/noto/NotoSansCJK-Regular.ttc",
        "/usr/share/fonts/truetype/wqy/wqy-microhei.ttc",
        "/System/Library/Fonts/PingFang.ttc"
    ];

    /// <summary>
    /// 在图像上绘制 UTF-8 文字（Skia；失败时退回 OpenCV 英文）。签名与引入 <c>slightBold</c> 之前版本二进制兼容。
    /// </summary>
    public static void DrawChineseText(
        Mat image,
        string text,
        SysPoint position,
        int fontSize = 24,
        Scalar? color = null,
        int lineSpacing = 6,
        int? maxWidth = 300,
        string align = "left")
    {
        DrawChineseTextCore(image, text, position, fontSize, color, lineSpacing, maxWidth, align, slightBold: false);
    }

    /// <summary>
    /// 同上，<paramref name="slightBold"/> 为 <c>true</c> 时略微加粗（向右 1px 叠画）。
    /// </summary>
    public static void DrawChineseText(
        Mat image,
        string text,
        SysPoint position,
        int fontSize,
        Scalar? color,
        int lineSpacing,
        int? maxWidth,
        string align,
        bool slightBold)
    {
        DrawChineseTextCore(image, text, position, fontSize, color, lineSpacing, maxWidth, align, slightBold);
    }

    private static void DrawChineseTextCore(
        Mat image,
        string text,
        SysPoint position,
        int fontSize,
        Scalar? color,
        int lineSpacing,
        int? maxWidth,
        string align,
        bool slightBold)
    {
        if (string.IsNullOrEmpty(text))
            return;

        var textColor = color ?? new Scalar(0, 255, 0);

        try
        {
            DrawTextSkia(image, text, position, fontSize, textColor, lineSpacing, maxWidth, align, slightBold);
        }
        catch
        {
            DrawTextOpenCvFallback(image, text, position, fontSize, textColor);
        }
    }

    private static void DrawTextSkia(
        Mat image, string text, SysPoint position,
        int fontSize, Scalar textColor, int lineSpacing,
        int? maxWidth, string align,
        bool slightBold)
    {
        var typeface = GetTypeface();

        using var font = new SKFont(typeface, fontSize)
        {
            Edging = SKFontEdging.SubpixelAntialias
        };

        using var paint = new SKPaint
        {
            IsAntialias = true,
            Color = new SKColor((byte)textColor.Val2, (byte)textColor.Val1, (byte)textColor.Val0)
        };

        var lines = SplitAndWrapLines(text, font, maxWidth);

        var metrics = font.Metrics;
        float ascent = -metrics.Ascent;
        float descent = metrics.Descent;
        float lineHeight = Math.Max(fontSize, ascent + descent) + lineSpacing;

        float maxLineWidth = 0;
        foreach (var line in lines)
        {
            var w = font.MeasureText(line);
            if (w > maxLineWidth) maxLineWidth = w;
        }

        if (slightBold)
            maxLineWidth += 1;

        const float pad = 2;
        float firstBaseline = ascent + pad;
        float lastBaseline = firstBaseline + (lines.Count - 1) * lineHeight;

        int bmpWidth = (int)Math.Ceiling(maxLineWidth + pad * 2);
        int bmpHeight = (int)Math.Ceiling(lastBaseline + descent + pad);

        if (bmpWidth <= 0 || bmpHeight <= 0)
            return;

        using var surface = SKSurface.Create(new SKImageInfo(bmpWidth, bmpHeight, SKColorType.Bgra8888, SKAlphaType.Premul));
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.Transparent);

        float y = firstBaseline;
        foreach (var line in lines)
        {
            float x = pad;
            if (align == "right")
            {
                var w = font.MeasureText(line);
                x = bmpWidth - w - pad;
            }

            canvas.DrawText(line, x, y, SKTextAlign.Left, font, paint);
            if (slightBold)
                canvas.DrawText(line, x + 1, y, SKTextAlign.Left, font, paint);
            y += lineHeight;
        }

        using var snapshot = surface.Snapshot();
        using var pixmap = snapshot.PeekPixels();

        int overlayX = position.X;
        int overlayY = position.Y;
        if (align == "right")
        {
            overlayX = position.X - bmpWidth;
        }

        OverlayPixels(image, pixmap, overlayX, overlayY);
    }

    private static List<string> SplitAndWrapLines(string text, SKFont font, int? maxWidth)
    {
        var result = new List<string>();
        var rawLines = text.Split('\n');

        foreach (var rawLine in rawLines)
        {
            if (!maxWidth.HasValue || string.IsNullOrEmpty(rawLine))
            {
                result.Add(rawLine);
                continue;
            }

            var line = "";
            foreach (var ch in rawLine)
            {
                var testLine = line + ch;
                var w = font.MeasureText(testLine);
                if (w > maxWidth.Value && line.Length > 0)
                {
                    result.Add(line);
                    line = ch.ToString();
                }
                else
                {
                    line = testLine;
                }
            }

            if (!string.IsNullOrEmpty(line))
                result.Add(line);
        }

        return result;
    }

    /// <summary>
    /// 与 <see cref="DrawChineseText"/> 中 Skia 多行布局一致，用于绘制底栏、卡片前计算占位尺寸。
    /// </summary>
    public static (int Width, int Height) MeasureChineseText(
        string text,
        int fontSize,
        int? maxWidth = null,
        int lineSpacing = 6)
    {
        if (string.IsNullOrEmpty(text))
            return (0, 0);

        var typeface = GetTypeface();
        using var font = new SKFont(typeface, fontSize)
        {
            Edging = SKFontEdging.SubpixelAntialias
        };

        var lines = SplitAndWrapLines(text, font, maxWidth);
        var metrics = font.Metrics;
        float ascent = -metrics.Ascent;
        float descent = metrics.Descent;
        float lineHeight = Math.Max(fontSize, ascent + descent) + lineSpacing;

        float maxLineWidth = 0;
        foreach (var line in lines)
        {
            var w = font.MeasureText(line);
            if (w > maxLineWidth) maxLineWidth = w;
        }

        const float pad = 2;
        float firstBaseline = ascent + pad;
        float lastBaseline = firstBaseline + (lines.Count - 1) * lineHeight;

        int bmpWidth = (int)Math.Ceiling(maxLineWidth + pad * 2);
        int bmpHeight = (int)Math.Ceiling(lastBaseline + descent + pad);
        return (bmpWidth, bmpHeight);
    }

    /// <summary>
    /// 与 <see cref="DrawChineseText"/> 中单行 Skia 布局一致，用于在 OpenCV 中计算标签背景矩形尺寸。
    /// </summary>
    /// <param name="slightBold">与绘制时 <c>slightBold: true</c> 配合，宽度含 1px 加粗占位。</param>
    public static (int Width, int Height) MeasureTextLine(string text, int fontSize, bool slightBold = false)
    {
        if (string.IsNullOrEmpty(text))
            return (0, 0);

        var typeface = GetTypeface();
        using var font = new SKFont(typeface, fontSize)
        {
            Edging = SKFontEdging.SubpixelAntialias
        };

        var metrics = font.Metrics;
        float ascent = -metrics.Ascent;
        float descent = metrics.Descent;
        const float pad = 2;
        float w = font.MeasureText(text);
        int bmpWidth = (int)Math.Ceiling(w + pad * 2);
        if (slightBold)
            bmpWidth += 1;
        float firstBaseline = ascent + pad;
        float lastBaseline = firstBaseline;
        int bmpHeight = (int)Math.Ceiling(lastBaseline + descent + pad);
        return (bmpWidth, bmpHeight);
    }

    private static SKTypeface GetTypeface()
    {
        if (_cachedTypeface != null)
            return _cachedTypeface;

        lock (_typefaceLock)
        {
            if (_cachedTypeface != null)
                return _cachedTypeface;

            foreach (var filePath in FontFilePaths)
            {
                if (File.Exists(filePath))
                {
                    var tf = SKTypeface.FromFile(filePath);
                    if (tf != null)
                    {
                        _cachedTypeface = tf;
                        return tf;
                    }
                }
            }

            foreach (var name in FontNames)
            {
                var tf = SKTypeface.FromFamilyName(name);
                if (tf != null && tf.FamilyName != SKTypeface.Default.FamilyName)
                {
                    _cachedTypeface = tf;
                    return tf;
                }
            }

            _cachedTypeface = SKTypeface.Default;
            return _cachedTypeface;
        }
    }

    private static void OverlayPixels(Mat image, SKPixmap pixmap, int offsetX, int offsetY)
    {
        int startX = Math.Max(0, offsetX);
        int startY = Math.Max(0, offsetY);
        int endX = Math.Min(offsetX + pixmap.Width, image.Width);
        int endY = Math.Min(offsetY + pixmap.Height, image.Height);

        int drawWidth = endX - startX;
        int drawHeight = endY - startY;

        if (drawWidth <= 0 || drawHeight <= 0)
            return;

        int srcOffX = startX - offsetX;
        int srcOffY = startY - offsetY;

        unsafe
        {
            byte* imgPtr = (byte*)image.DataPointer;
            int imgStep = (int)image.Step();
            byte* srcPtr = (byte*)pixmap.GetPixels();
            int srcStride = pixmap.RowBytes;

            for (int dy = 0; dy < drawHeight; dy++)
            {
                byte* srcRow = srcPtr + (srcOffY + dy) * srcStride + srcOffX * 4;
                byte* dstRow = imgPtr + (startY + dy) * imgStep + startX * 3;

                for (int dx = 0; dx < drawWidth; dx++)
                {
                    byte b = srcRow[0];
                    byte g = srcRow[1];
                    byte r = srcRow[2];
                    byte a = srcRow[3];

                    if (a > 0)
                    {
                        if (a >= 255)
                        {
                            dstRow[0] = b;
                            dstRow[1] = g;
                            dstRow[2] = r;
                        }
                        else
                        {
                            float fa = a / 255f;
                            float inv = 1f - fa;
                            dstRow[0] = (byte)(b + dstRow[0] * inv);
                            dstRow[1] = (byte)(g + dstRow[1] * inv);
                            dstRow[2] = (byte)(r + dstRow[2] * inv);
                        }
                    }

                    srcRow += 4;
                    dstRow += 3;
                }
            }
        }
    }

    private static void DrawTextOpenCvFallback(
        Mat image, string text, SysPoint position, int fontSize, Scalar textColor)
    {
        var fontScale = fontSize / 30.0;
        var thickness = Math.Max(1, fontSize / 15);
        Cv2.PutText(image, text,
            new Point(position.X, position.Y + fontSize),
            HersheyFonts.HersheySimplex, fontScale, textColor, thickness);
    }
}
