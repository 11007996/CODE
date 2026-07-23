using System.Text.Json;
using LuxVideoDet.Core.Configuration.Models;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Configuration;

/// <summary>
/// 单文件配置存储 - 所有配置存储在一个 JSON 文件中
/// </summary>
public class SingleFileConfigurationStore : IConfigurationStore
{
    private readonly string _configFilePath;
    private readonly ILogger<SingleFileConfigurationStore> _logger;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly SemaphoreSlim _fileLock = new(1, 1);
    private Dictionary<string, DetectionConfiguration> _configCache = new();
    private AppConfiguration _appConfig = new();
    private readonly Task _initializeTask;

    public SingleFileConfigurationStore(
        string configFilePath,
        ILogger<SingleFileConfigurationStore> logger)
    {
        _configFilePath = configFilePath;
        _logger = logger;

        _jsonOptions = ConfigurationJsonOptions.ForConfigurationFile;

        // 确保配置文件目录存在
        var directory = Path.GetDirectoryName(_configFilePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // 初始化加载
        _initializeTask = LoadAllConfigsAsync();
    }

    private async Task EnsureInitializedAsync(CancellationToken cancellationToken = default)
    {
        if (!_initializeTask.IsCompleted)
            await _initializeTask.WaitAsync(cancellationToken);
    }

    private async Task LoadAllConfigsAsync()
    {
        var shouldMigrateLegacyFormat = false;
        await _fileLock.WaitAsync();
        try
        {
            if (!File.Exists(_configFilePath))
            {
                _configCache = new Dictionary<string, DetectionConfiguration>();
                _appConfig = new AppConfiguration();
                return;
            }

            var json = await File.ReadAllTextAsync(_configFilePath);
            var trimmed = json.TrimStart();
            if (trimmed.StartsWith("{", StringComparison.Ordinal))
            {
                // 优先按新格式（app + configurations）读取
                try
                {
                    var root = JsonSerializer.Deserialize<ConfigurationRoot>(json, _jsonOptions);
                    if (root != null)
                    {
                        _appConfig = root.App ?? new AppConfiguration();
                        _configCache = root.Configurations.ToDictionary(c => c.Id);
                        _logger.LogInformation("成功加载新格式配置：{Count} 个检测配置", _configCache.Count);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "解析新格式配置失败，回退旧格式解析");
                }
            }

            // 兼容旧格式（纯数组）
            var configs = JsonSerializer.Deserialize<List<DetectionConfiguration>>(json, _jsonOptions) ?? [];
            _configCache = configs.ToDictionary(c => c.Id);
            _appConfig = new AppConfiguration();
            _logger.LogInformation("成功加载旧格式配置：{Count} 个检测配置", _configCache.Count);
            // 旧格式读取成功后自动迁移保存为新格式，避免下次启动重复回退。
            shouldMigrateLegacyFormat = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载配置文件失败: {FilePath}", _configFilePath);
            _configCache = new Dictionary<string, DetectionConfiguration>();
            _appConfig = new AppConfiguration();
        }
        finally
        {
            _fileLock.Release();
        }

        if (shouldMigrateLegacyFormat)
        {
            try
            {
                await SaveAllConfigsAsync();
                _logger.LogInformation("已将旧格式配置迁移为新格式（app + configurations）");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "旧格式配置迁移保存失败，将在后续写入时重试");
            }
        }
    }

    private async Task SaveAllConfigsAsync()
    {
        await _fileLock.WaitAsync();
        try
        {
            var root = new ConfigurationRoot
            {
                App = _appConfig,
                Configurations = _configCache.Values.ToList()
            };
            var json = JsonSerializer.Serialize(root, _jsonOptions);
            await File.WriteAllTextAsync(_configFilePath, json);
            _logger.LogInformation("成功保存配置到 {FilePath}", _configFilePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存配置文件失败: {FilePath}", _configFilePath);
            throw;
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task<DetectionConfiguration?> LoadAsync(string id, CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);
        await _fileLock.WaitAsync(cancellationToken);
        try
        {
            return _configCache.TryGetValue(id, out var config) ? config : null;
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task SaveAsync(DetectionConfiguration configuration, CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);
        configuration.UpdatedAt = DateTime.Now;
        
        await _fileLock.WaitAsync(cancellationToken);
        try
        {
            _configCache[configuration.Id] = configuration;
        }
        finally
        {
            _fileLock.Release();
        }

        await SaveAllConfigsAsync();
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);
        await _fileLock.WaitAsync(cancellationToken);
        try
        {
            _configCache.Remove(id);
        }
        finally
        {
            _fileLock.Release();
        }

        await SaveAllConfigsAsync();
    }

    public async Task<List<DetectionConfiguration>> ListAsync(CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);
        await _fileLock.WaitAsync(cancellationToken);
        try
        {
            return _configCache.Values.ToList();
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);
        await _fileLock.WaitAsync(cancellationToken);
        try
        {
            return _configCache.ContainsKey(id);
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task<AppConfiguration> GetAppConfigurationAsync(CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);
        await _fileLock.WaitAsync(cancellationToken);
        try
        {
            return _appConfig;
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task SetAppConfigurationAsync(AppConfiguration appConfig, CancellationToken cancellationToken = default)
    {
        await EnsureInitializedAsync(cancellationToken);
        await _fileLock.WaitAsync(cancellationToken);
        try
        {
            _appConfig = appConfig ?? new AppConfiguration();
        }
        finally
        {
            _fileLock.Release();
        }

        await SaveAllConfigsAsync();
    }
}
