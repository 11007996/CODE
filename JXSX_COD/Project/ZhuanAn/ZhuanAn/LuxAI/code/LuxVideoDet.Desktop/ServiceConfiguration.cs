using System;
using System.IO;
using FluentValidation;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Configuration.Validation;
using LuxVideoDet.Core.Extensions;
using LuxVideoDet.Core.Logging;
using LuxVideoDet.Core.Storage;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Desktop.ViewModels;
using LuxVideoDet.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace LuxVideoDet.Desktop;

public static class ServiceConfiguration
{
    /// <summary>
    /// 最低日志级别（文件 + 界面日志面板）。默认 Information，避免高帧率推理时 DBG 刷屏。
    /// 调试时设置环境变量 <c>LUXVIDEODET_LOG_LEVEL=Debug</c> 即可恢复 DBG 写入文件与 UI。
    /// 可选值：Verbose / Debug / Information / Warning / Error（不区分大小写）。
    /// </summary>
    public static LogEventLevel ResolveMinLogLevelFromEnvironment()
    {
        var env = Environment.GetEnvironmentVariable("LUXVIDEODET_LOG_LEVEL")
                  ?? Environment.GetEnvironmentVariable("LUXVIDEODET_MIN_LOG_LEVEL");
        if (string.IsNullOrWhiteSpace(env))
            return LogEventLevel.Information;

        return env.Trim().ToUpperInvariant() switch
        {
            "VERBOSE" or "TRACE" or "TRC" => LogEventLevel.Verbose,
            "DEBUG" or "DBG" => LogEventLevel.Debug,
            "INFORMATION" or "INFO" or "INF" => LogEventLevel.Information,
            "WARNING" or "WARN" or "WRN" => LogEventLevel.Warning,
            "ERROR" or "ERR" => LogEventLevel.Error,
            _ => LogEventLevel.Information
        };
    }

    private static LogLevel ToMicrosoftLogLevel(LogEventLevel level) => level switch
    {
        LogEventLevel.Verbose => LogLevel.Trace,
        LogEventLevel.Debug => LogLevel.Debug,
        LogEventLevel.Information => LogLevel.Information,
        LogEventLevel.Warning => LogLevel.Warning,
        LogEventLevel.Error => LogLevel.Error,
        LogEventLevel.Fatal => LogLevel.Critical,
        _ => LogLevel.Information
    };

    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        var logSink = new LogEventSink();

        // 注册统一的日志事件接收器，供文件日志、Microsoft Logger 和 UI 共用。
        services.AddSingleton<ILogEventSink>(logSink);

        var minLevel = ResolveMinLogLevelFromEnvironment();
        var uiMinLevel = ToMicrosoftLogLevel(minLevel);

        var logDir = Path.Combine(AppContext.BaseDirectory, "logs");
        Directory.CreateDirectory(logDir);

        // 配置 Serilog（文件路径相对 AppContext.BaseDirectory，避免 macOS .app 启动时 cwd 为 / 导致写日志失败并闪退）
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .WriteTo.Console(restrictedToMinimumLevel: minLevel)
            .WriteTo.Sink(new SerilogUiSink(logSink, uiMinLevel))
            .WriteTo.File(
                path: Path.Combine(logDir, "desktop-.log"),
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: minLevel,
                fileSizeLimitBytes: 10 * 1024 * 1024,  // 10 MB
                retainedFileCountLimit: 7,
                rollOnFileSizeLimit: true,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [{ThreadId}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        // 添加日志
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog(dispose: true);
        });

        // 注册 Core 服务
        services.AddLuxVideoDetCore();

        // 注册 Desktop 服务
        services.AddSingleton<LogService>();
        services.AddSingleton<DetectionService>();
        services.AddSingleton<ImageRenderService>();
        services.AddSingleton<PerformanceMonitorService>();
        services.AddSingleton<ProductionStatisticsStore>();

        services.AddLuxVideoDetLocalization();
        services.AddSingleton<IUiCultureService, UiCultureService>();

        // 注册 ViewModels
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<MinIOSyncViewModel>();

        return services.BuildServiceProvider();
    }
}
