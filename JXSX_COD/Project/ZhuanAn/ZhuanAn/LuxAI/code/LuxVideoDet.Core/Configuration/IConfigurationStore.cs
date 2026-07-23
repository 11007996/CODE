using LuxVideoDet.Core.Configuration.Models;

namespace LuxVideoDet.Core.Configuration;

/// <summary>
/// 配置存储接口 - 支持多种存储方式（JSON、SQLite、数据库等）
/// </summary>
public interface IConfigurationStore
{
    /// <summary>
    /// 加载配置
    /// </summary>
    Task<DetectionConfiguration?> LoadAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 保存配置
    /// </summary>
    Task SaveAsync(DetectionConfiguration configuration, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除配置
    /// </summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取所有配置列表
    /// </summary>
    Task<List<DetectionConfiguration>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 检查配置是否存在
    /// </summary>
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取应用级配置。
    /// </summary>
    Task<AppConfiguration> GetAppConfigurationAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 保存应用级配置。
    /// </summary>
    Task SetAppConfigurationAsync(AppConfiguration appConfig, CancellationToken cancellationToken = default);
}
