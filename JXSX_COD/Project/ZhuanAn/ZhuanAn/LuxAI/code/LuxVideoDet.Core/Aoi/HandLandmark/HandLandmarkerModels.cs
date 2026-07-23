using System.Text.Json.Serialization;

namespace LuxVideoDet.Core.Aoi.HandLandmark;

/// <summary>与 Python luxvideopyplugin JSON 中 handedness 类别项一致。</summary>
public sealed class HandLandmarkCategoryEntry
{
    [JsonPropertyName("index")]
    public int? Index { get; set; }

    [JsonPropertyName("score")]
    public double? Score { get; set; }

    [JsonPropertyName("category_name")]
    public string? CategoryName { get; set; }
}

/// <summary>单点：图像或世界坐标。</summary>
public sealed class HandLandmarkPoint
{
    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    [JsonPropertyName("z")]
    public double Z { get; set; }
}

/// <summary>左右手性块。</summary>
public sealed class HandLandmarkHandednessBlock
{
    [JsonPropertyName("categories")]
    public List<HandLandmarkCategoryEntry> Categories { get; set; } = new();
}

/// <summary>单只手的 21 点与左右手性。</summary>
public sealed class HandLandmarkSingleHandResult
{
    [JsonPropertyName("handedness")]
    public HandLandmarkHandednessBlock Handedness { get; set; } = new();

    [JsonPropertyName("landmarks")]
    public List<HandLandmarkPoint> Landmarks { get; set; } = new();

    [JsonPropertyName("world_landmarks")]
    public List<HandLandmarkPoint> WorldLandmarks { get; set; } = new();
}

/// <summary>单帧推理结果：<c>infer_bgr_ptr_result_json</c> 顶层结构。</summary>
public sealed class HandLandmarkerFrameInferenceResult
{
    [JsonPropertyName("n_hands")]
    public int HandCount { get; set; }

    [JsonPropertyName("timestamp_ms")]
    public long TimestampMs { get; set; }

    [JsonPropertyName("hands")]
    public List<HandLandmarkSingleHandResult> Hands { get; set; } = new();

    [JsonIgnore]
    public int SourceImageWidth { get; set; }

    [JsonIgnore]
    public int SourceImageHeight { get; set; }

    [JsonIgnore]
    public int InferenceImageWidth { get; set; }

    [JsonIgnore]
    public int InferenceImageHeight { get; set; }

    [JsonIgnore]
    public HandLandmarkImageCoordinateKind ImageLandmarksCoordinateKind { get; set; } =
        HandLandmarkImageCoordinateKind.Normalized01RelativeToSourceImage;
}
