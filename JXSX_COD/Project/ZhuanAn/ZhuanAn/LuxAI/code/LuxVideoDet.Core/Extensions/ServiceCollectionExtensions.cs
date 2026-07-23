using System.IO;
using FluentValidation;
using LuxVideoDet.Core.Algorithm.Pipeline;
using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Configuration.Validation;
using LuxVideoDet.Core.Inference.Postprocessors;
using LuxVideoDet.Core.Logging;
using LuxVideoDet.Core.S3Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace LuxVideoDet.Core.Extensions;

/// <summary>
/// 服务集合扩展方法 - 简化 DI 注册
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加 LuxVideoDet 核心服务
    /// </summary>
    public static IServiceCollection AddLuxVideoDetCore(
        this IServiceCollection services,
        string configDirectory = "configs")
    {
        // 注册配置服务（使用单文件存储）
        services.AddSingleton<IConfigurationStore>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<SingleFileConfigurationStore>>();
            var path = Path.Combine(AppContext.BaseDirectory, "configs.json");
            return new SingleFileConfigurationStore(path, logger);
        });

        services.AddSingleton<IValidator<DetectionConfiguration>, DetectionConfigurationValidator>();
        services.AddSingleton<ConfigurationService>();

        // 注册日志事件接收器（单例，供 UI 订阅）
        services.TryAddSingleton<ILogEventSink, LogEventSink>();

        // 注册 HttpClient（用于通知服务）
        services.AddHttpClient();

        // 注册通知服务工厂
        services.AddSingleton<Notification.NotificationServiceFactory>();

        // 注册 AOI 检测器工厂
        services.AddSingleton<Aoi.AoiDetectorFactory>();

        // 注册检测算法工厂
        services.AddSingleton<Algorithm.DetectionAlgorithmFactory>();

        // 注册后处理器工厂
        services.AddSingleton<PostprocessorFactory>();

        // 注册推理引擎工厂
        services.AddSingleton<Inference.InferenceEngineFactory>();

        // 注册视频源工厂
        services.AddSingleton<VideoSource.VideoSourceFactory>();

        // 注册流水线工厂（编排视频源 + 算法 → 可运行的 Pipeline）
        services.AddSingleton<PipelineFactory>();

        // 注册 S3/MinIO 同步服务（按需启用）
        services.AddS3StorageSyncService();

        return services;
    }

    /// <summary>
    /// 添加 Serilog 日志（文件 + 控制台）
    /// </summary>
    public static IServiceCollection AddSerilogLogging(
        this IServiceCollection services,
        string logDirectory = "logs",
        LogEventLevel minLevel = LogEventLevel.Information)
    {
        // 配置 Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(minLevel)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()

            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(
                path: Path.Combine(logDirectory, "luxvideodet-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        // 注册 Serilog 到 Microsoft.Extensions.Logging
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog(dispose: true);
        });

        return services;
    }

    /// <summary>
    /// 添加 Desktop 日志提供器（用于 UI 实时显示）
    /// </summary>
    public static IServiceCollection AddDesktopLogging(
        this IServiceCollection services,
        LogLevel minLevel = LogLevel.Information)
    {
        services.AddLogging(builder =>
        {
            builder.Services.AddSingleton<ILoggerProvider>(sp =>
            {
                var sink = sp.GetRequiredService<ILogEventSink>();
                return new DesktopLoggerProvider(sink, minLevel);
            });
        });

        return services;
    }
}
