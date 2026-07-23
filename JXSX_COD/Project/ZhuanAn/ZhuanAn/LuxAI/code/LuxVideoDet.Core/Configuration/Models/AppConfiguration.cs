using System.Text.Json.Serialization;

namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// 应用级配置（插件目录、模型目录、S3/MinIO 同步）。
/// </summary>
public class AppConfiguration
{
    public string PluginDirectory { get; set; } = "plugins";
    public string ModelDirectory { get; set; } = "resources/models";

    /// <summary>
    /// 保持与历史字段兼容：configs.json 中使用 minIO。
    /// </summary>
    [JsonPropertyName("minIO")]
    public S3StorageConfiguration S3Storage { get; set; } = new();
}

/// <summary>
/// 配置文件根对象：app + configurations。
/// </summary>
public class ConfigurationRoot
{
    public AppConfiguration App { get; set; } = new();
    public List<DetectionConfiguration> Configurations { get; set; } = [];
}
