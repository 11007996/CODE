using FluentValidation;
using LuxVideoDet.Core.Configuration.Models;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Configuration;

/// <summary>
/// 配置服务 - 统一管理配置的加载、保存、验证
/// </summary>
public class ConfigurationService
{
    private readonly IConfigurationStore _store;
    private readonly IValidator<DetectionConfiguration> _validator;
    private readonly ILogger<ConfigurationService> _logger;

    public event EventHandler<DetectionConfiguration>? ConfigurationChanged;
    public event EventHandler<string>? ConfigurationDeleted;

    public ConfigurationService(
        IConfigurationStore store,
        IValidator<DetectionConfiguration> validator,
        ILogger<ConfigurationService> logger)
    {
        _store = store;
        _validator = validator;
        _logger = logger;
    }

    /// <summary>
    /// 加载配置
    /// </summary>
    public async Task<DetectionConfiguration?> LoadAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始加载配置: {ConfigId}", id);

        var config = await _store.LoadAsync(id, cancellationToken);

        if (config == null)
        {
            _logger.LogWarning("配置不存在: {ConfigId}", id);
            return null;
        }

        // 验证配置
        var validationResult = await _validator.ValidateAsync(config, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("配置验证失败: {ConfigId}, 错误: {Errors}",
                id, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

            // 可以选择抛出异常或返回 null
            // throw new ValidationException(validationResult.Errors);
        }

        return config;
    }

    /// <summary>
    /// 保存配置（带验证）
    /// </summary>
    public async Task SaveAsync(DetectionConfiguration configuration, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始保存配置: {ConfigId}", configuration.Id);

        // 验证配置
        var validationResult = await _validator.ValidateAsync(configuration, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            _logger.LogError("配置验证失败: {ConfigId}, 错误: {Errors}", configuration.Id, errors);
            throw new ValidationException(validationResult.Errors);
        }

        await _store.SaveAsync(configuration, cancellationToken);
        _logger.LogInformation("配置保存成功: {ConfigId}", configuration.Id);
        
        // 触发配置变更事件
        ConfigurationChanged?.Invoke(this, configuration);
    }

    /// <summary>
    /// 导入配置（验证失败只记录警告，不抛出异常）
    /// </summary>
    public async Task ImportAsync(DetectionConfiguration configuration, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始导入配置: {ConfigId} ({ConfigName})", configuration.Id, configuration.Name);

        var validationResult = await _validator.ValidateAsync(configuration, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            _logger.LogWarning("导入配置验证有警告: {ConfigId}, 问题: {Errors}（仍将保存）", configuration.Id, errors);
        }

        await _store.SaveAsync(configuration, cancellationToken);
        _logger.LogInformation("配置导入成功: {ConfigId} ({ConfigName})", configuration.Id, configuration.Name);

        ConfigurationChanged?.Invoke(this, configuration);
    }

    /// <summary>
    /// 删除配置
    /// </summary>
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始删除配置: {ConfigId}", id);
        await _store.DeleteAsync(id, cancellationToken);
        _logger.LogInformation("配置删除成功: {ConfigId}", id);
        
        // 触发配置删除事件
        ConfigurationDeleted?.Invoke(this, id);
    }

    /// <summary>
    /// 获取所有配置
    /// </summary>
    public async Task<List<DetectionConfiguration>> ListAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("列出所有配置");
        var configs = await _store.ListAsync(cancellationToken);
        _logger.LogDebug("列出 {Count} 个配置", configs.Count);
        return configs;
    }

    /// <summary>
    /// 检查配置是否存在
    /// </summary>
    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _store.ExistsAsync(id, cancellationToken);
    }

    /// <summary>
    /// 验证配置（不保存）
    /// </summary>
    public async Task<(bool IsValid, List<string> Errors)> ValidateAsync(
        DetectionConfiguration configuration,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(configuration, cancellationToken);

        return (
            validationResult.IsValid,
            validationResult.Errors.Select(e => e.ErrorMessage).ToList()
        );
    }

    public async Task<AppConfiguration> GetAppConfigurationAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("获取应用配置");
        return await _store.GetAppConfigurationAsync(cancellationToken);
    }

    public async Task SetAppConfigurationAsync(AppConfiguration appConfig, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("保存应用配置：PluginDir={PluginDir}, ModelDir={ModelDir}",
            appConfig.PluginDirectory, appConfig.ModelDirectory);
        await _store.SetAppConfigurationAsync(appConfig, cancellationToken);
    }
}
