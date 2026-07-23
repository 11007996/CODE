using System.Text.Json;
using LuxVideoDet.Core.Configuration.Models;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Configuration;

/// <summary>
/// JSON 文件配置存储 - 简单、易于编辑、适合小规模部署
/// </summary>
public class JsonConfigurationStore : IConfigurationStore
{
    private readonly string _configDirectory;
    private readonly ILogger<JsonConfigurationStore> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonConfigurationStore(
        string configDirectory,
        ILogger<JsonConfigurationStore> logger)
    {
        _configDirectory = configDirectory;
        _logger = logger;

        _jsonOptions = ConfigurationJsonOptions.ForConfigurationFile;

        // 确保配置目录存在
        Directory.CreateDirectory(_configDirectory);
    }

    public async Task<DetectionConfiguration?> LoadAsync(string id, CancellationToken cancellationToken = default)
    {
        var filePath = GetConfigFilePath(id);

        if (!File.Exists(filePath))
        {
            _logger.LogWarning("配置文件不存在: {FilePath}", filePath);
            return null;
        }

        try
        {
            var json = await File.ReadAllTextAsync(filePath, cancellationToken);
            var config = JsonSerializer.Deserialize<DetectionConfiguration>(json, _jsonOptions);

            _logger.LogInformation("成功加载配置: {ConfigId} from {FilePath}", id, filePath);
            return config;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载配置失败: {ConfigId} from {FilePath}", id, filePath);
            throw;
        }
    }

    public async Task SaveAsync(DetectionConfiguration configuration, CancellationToken cancellationToken = default)
    {
        var filePath = GetConfigFilePath(configuration.Id);

        try
        {
            // 更新修改时间
            configuration.UpdatedAt = DateTime.Now;

            var json = JsonSerializer.Serialize(configuration, _jsonOptions);
            await File.WriteAllTextAsync(filePath, json, cancellationToken);

            _logger.LogInformation("成功保存配置: {ConfigId} to {FilePath}", configuration.Id, filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存配置失败: {ConfigId} to {FilePath}", configuration.Id, filePath);
            throw;
        }
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var filePath = GetConfigFilePath(id);

        if (!File.Exists(filePath))
        {
            _logger.LogWarning("配置文件不存在，无法删除: {FilePath}", filePath);
            return;
        }

        try
        {
            await Task.Run(() => File.Delete(filePath), cancellationToken);
            _logger.LogInformation("成功删除配置: {ConfigId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除配置失败: {ConfigId}", id);
            throw;
        }
    }

    public async Task<List<DetectionConfiguration>> ListAsync(CancellationToken cancellationToken = default)
    {
        var configs = new List<DetectionConfiguration>();

        try
        {
            var files = Directory.GetFiles(_configDirectory, "*.json");

            foreach (var file in files)
            {
                try
                {
                    var json = await File.ReadAllTextAsync(file, cancellationToken);
                    var config = JsonSerializer.Deserialize<DetectionConfiguration>(json, _jsonOptions);

                    if (config != null)
                    {
                        configs.Add(config);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "跳过无效的配置文件: {FilePath}", file);
                }
            }

            _logger.LogInformation("成功加载 {Count} 个配置", configs.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "列出配置失败");
            throw;
        }

        return configs;
    }

    public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        var filePath = GetConfigFilePath(id);
        return Task.FromResult(File.Exists(filePath));
    }

    public Task<AppConfiguration> GetAppConfigurationAsync(CancellationToken cancellationToken = default)
    {
        // 单文件 JSON store（按每配置一个 json）不持久化 app 配置，返回默认值。
        return Task.FromResult(new AppConfiguration());
    }

    public Task SetAppConfigurationAsync(AppConfiguration appConfig, CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("JsonConfigurationStore 不支持持久化 AppConfiguration，设置将被忽略。");
        return Task.CompletedTask;
    }

    private string GetConfigFilePath(string id)
    {
        return Path.Combine(_configDirectory, $"{id}.json");
    }
}
