using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LuxVideoDet.Core.Configuration;

/// <summary>
/// 配置文件与区域 JSON 的序列化选项。
/// 默认 <see cref="JsonSerializerOptions"/> 会把中文等写成 \uXXXX；设置
/// <see cref="JavaScriptEncoder.UnsafeRelaxedJsonEscaping"/> 后按 UTF-8 字面量写出，便于人工编辑。
/// </summary>
public static class ConfigurationJsonOptions
{
    /// <summary>单文件 / 按 ID 存盘的检测配置（驼峰、缩进、可读中文）。</summary>
    public static JsonSerializerOptions ForConfigurationFile { get; } = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() },
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    /// <summary>区域编辑器写回 <see cref="AlgorithmConfig.RegionsJson"/>（缩进）。</summary>
    public static JsonSerializerOptions ForRegionsJsonIndented { get; } = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    /// <summary>从模型生成编辑区用的紧凑区域 JSON。</summary>
    public static JsonSerializerOptions ForRegionsJsonCompact { get; } = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}
