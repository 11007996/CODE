using LuxVideoDet.Core.S3Storage.Minio;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.S3Storage;

/// <summary>
/// S3 兼容对象存储（文件同步）依赖注入。
/// </summary>
public static class S3StorageServiceCollectionExtensions
{
    /// <summary>
    /// 注册对象存储同步服务。当前默认实现为 <see cref="MinioSdkS3StorageSyncService"/>（S3 兼容）。
    /// </summary>
    public static IServiceCollection AddS3StorageSyncService(this IServiceCollection services)
    {
        services.TryAddSingleton<MinioSdkS3StorageSyncService>();
        services.TryAddSingleton<IS3StorageSyncService>(sp =>
            sp.GetRequiredService<MinioSdkS3StorageSyncService>());
        return services;
    }

    public static IServiceCollection AddS3StorageSyncServiceWithAutoStart(this IServiceCollection services)
    {
        services.AddS3StorageSyncService();
        services.AddHostedService<S3StorageSyncBackgroundService>();
        return services;
    }
}

/// <summary>
/// 启动时初始化对象存储同步服务。
/// </summary>
public class S3StorageSyncBackgroundService : BackgroundService
{
    private readonly IS3StorageSyncService _syncService;
    private readonly ILogger<S3StorageSyncBackgroundService> _logger;

    public S3StorageSyncBackgroundService(
        IS3StorageSyncService syncService,
        ILogger<S3StorageSyncBackgroundService> logger)
    {
        _syncService = syncService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("对象存储同步后台服务启动");

        try
        {
            var initialized = await _syncService.InitializeAsync(stoppingToken);

            if (initialized)
                _logger.LogInformation("对象存储同步服务初始化成功");
            else
                _logger.LogDebug("对象存储同步未就绪（未启用或无法连接），以本地模式继续");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "对象存储同步后台服务启动失败");
        }

        while (!stoppingToken.IsCancellationRequested)
            await Task.Delay(1000, stoppingToken);

        _logger.LogInformation("对象存储同步后台服务停止");
    }
}
