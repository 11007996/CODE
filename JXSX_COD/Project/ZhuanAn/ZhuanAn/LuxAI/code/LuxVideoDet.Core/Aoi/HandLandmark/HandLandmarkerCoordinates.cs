using System.Text.Json;

namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>
/// 下采样尺寸计算、JSON 反序列化与坐标后处理（归一化 → 原图像素、世界坐标尺度补偿）。
/// </summary>
internal static class HandLandmarkerCoordinates
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = false,
        ReadCommentHandling = JsonCommentHandling.Disallow,
    };

    internal static (int Width, int Height) ComputeInferenceSize(int sourceWidth, int sourceHeight, int maxLongEdge)
    {
        if (sourceWidth <= 0 || sourceHeight <= 0)
            throw new ArgumentOutOfRangeException(nameof(sourceWidth));
        if (maxLongEdge <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxLongEdge));

        var longEdge = Math.Max(sourceWidth, sourceHeight);
        if (longEdge <= maxLongEdge)
            return (sourceWidth, sourceHeight);

        var scale = maxLongEdge / (double)longEdge;
        var w = Math.Max(1, (int)Math.Round(sourceWidth * scale));
        var h = Math.Max(1, (int)Math.Round(sourceHeight * scale));
        return (w, h);
    }

    internal static HandLandmarkerFrameInferenceResult DeserializeAndMap(
        string json,
        int origW,
        int origH,
        int infW,
        int infH,
        bool mapImageLandmarksToOriginalPixels,
        bool scaleWorldLandmarksWhenDownsampled)
    {
        var dto = JsonSerializer.Deserialize<HandLandmarkerFrameInferenceResult>(json, JsonOptions)
                  ?? throw new InvalidOperationException("JSON 反序列化结果为 null。");

        dto.SourceImageWidth = origW;
        dto.SourceImageHeight = origH;
        dto.InferenceImageWidth = infW;
        dto.InferenceImageHeight = infH;

        var didResize = infW != origW || infH != origH;
        var zScale = origW / (double)infW;

        if (didResize && scaleWorldLandmarksWhenDownsampled)
            ScaleWorldLandmarks(dto, zScale);

        if (mapImageLandmarksToOriginalPixels)
        {
            MapImageLandmarksToOriginalPixels(dto, origW, origH, zScale);
            dto.ImageLandmarksCoordinateKind = HandLandmarkImageCoordinateKind.PixelCoordinatesInSourceImage;
        }
        else
        {
            dto.ImageLandmarksCoordinateKind = HandLandmarkImageCoordinateKind.Normalized01RelativeToSourceImage;
        }

        return dto;
    }

    private static void MapImageLandmarksToOriginalPixels(
        HandLandmarkerFrameInferenceResult result,
        int originalWidth,
        int originalHeight,
        double zScaleFromInferenceWidth)
    {
        foreach (var hand in result.Hands)
        {
            foreach (var p in hand.Landmarks)
            {
                p.X *= originalWidth;
                p.Y *= originalHeight;
                p.Z *= zScaleFromInferenceWidth;
            }
        }
    }

    private static void ScaleWorldLandmarks(HandLandmarkerFrameInferenceResult result, double uniformScale)
    {
        if (uniformScale == 1.0)
            return;

        foreach (var hand in result.Hands)
        {
            foreach (var p in hand.WorldLandmarks)
            {
                p.X *= uniformScale;
                p.Y *= uniformScale;
                p.Z *= uniformScale;
            }
        }
    }
}
