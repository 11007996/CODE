using System.Collections.Concurrent;
using LuxVideoDet.Core.Algorithm.Pipeline;
using LuxVideoDet.Core.Algorithm.Results;
using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

// ══════════════════════════════════════════════
//  Serilog 初始化
// ══════════════════════════════════════════════

var envLevel = Environment.GetEnvironmentVariable("LUXVIDEODET_LOG_LEVEL")
            ?? Environment.GetEnvironmentVariable("LUXVIDEODET_MIN_LOG_LEVEL");

var minLevel = envLevel?.ToLowerInvariant() switch
{
    "verbose" => LogEventLevel.Verbose,
    "debug" => LogEventLevel.Debug,
    "warning" => LogEventLevel.Warning,
    "error" => LogEventLevel.Error,
    _ => LogEventLevel.Information
};

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Is(minLevel)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/headless-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        rollOnFileSizeLimit: true,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

// ══════════════════════════════════════════════
//  DI 容器
// ══════════════════════════════════════════════

var services = new ServiceCollection();
services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.AddSerilog(dispose: true);
});
services.AddLuxVideoDetCore();

var provider = services.BuildServiceProvider();
var configService = provider.GetRequiredService<ConfigurationService>();
var pipelineFactory = provider.GetRequiredService<PipelineFactory>();
var logger = provider.GetRequiredService<ILogger<Program>>();

// ══════════════════════════════════════════════
//  解析命令行参数
// ══════════════════════════════════════════════

var configFilters = ParseConfigFilters(args);

// ══════════════════════════════════════════════
//  加载配置 & 筛选
// ══════════════════════════════════════════════

var allConfigs = await configService.ListAsync();

if (allConfigs.Count == 0)
{
    Log.Error("configs.json 中没有任何配置，请先通过 Desktop 或 Web 端创建配置");
    return 1;
}

List<DetectionConfiguration> targetConfigs;

if (configFilters.Count > 0)
{
    targetConfigs = allConfigs
        .Where(c => configFilters.Any(f =>
            c.Name.Equals(f, StringComparison.OrdinalIgnoreCase) ||
            c.Id.Equals(f, StringComparison.OrdinalIgnoreCase)))
        .ToList();

    var notFound = configFilters
        .Where(f => !targetConfigs.Any(c =>
            c.Name.Equals(f, StringComparison.OrdinalIgnoreCase) ||
            c.Id.Equals(f, StringComparison.OrdinalIgnoreCase)))
        .ToList();

    foreach (var name in notFound)
        Log.Warning("未找到配置: {Filter}", name);

    if (targetConfigs.Count == 0)
    {
        Log.Error("指定的配置均不存在。可用配置: {Names}",
            string.Join(", ", allConfigs.Select(c => c.Name)));
        return 1;
    }
}
else
{
    targetConfigs = allConfigs.Where(c => c.Enabled).ToList();

    if (targetConfigs.Count == 0)
    {
        Log.Error("所有配置均已禁用，请启用至少一个配置");
        return 1;
    }
}

// ══════════════════════════════════════════════
//  启动流水线
// ══════════════════════════════════════════════

PrintBanner(targetConfigs);

var pipelines = new ConcurrentDictionary<string, (MultiAlgorithmPipeline Pipeline, DetectionConfiguration Config)>();
var exitCode = 0;

foreach (var config in targetConfigs)
{
    try
    {
        var pipeline = await pipelineFactory.CreatePipelineAsync(config);

        pipeline.ResultAvailable += (algoType, result) =>
            OnResultAvailable(config.Name, algoType, result, logger);

        pipeline.PipelineError += error =>
        {
            Log.Error(error, "[{ConfigName}] 流水线错误", config.Name);
        };

        pipeline.AlgorithmError += (algoType, error) =>
        {
            Log.Error(error, "[{ConfigName}] 算法 [{AlgorithmType}] 处理错误", config.Name, algoType);
        };

        pipelines.TryAdd(config.Id, (pipeline, config));
        pipeline.Start();

        Log.Information("检测已启动: {ConfigName} (ID={ConfigId})", config.Name, config.Id);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "启动配置 [{ConfigName}] 失败", config.Name);
        exitCode = 1;
    }
}

if (pipelines.IsEmpty)
{
    Log.Error("没有任何配置成功启动");
    return 1;
}

Log.Information("共 {Count}/{Total} 个检测任务正在运行，按 Ctrl+C 停止",
    pipelines.Count, targetConfigs.Count);

// ══════════════════════════════════════════════
//  等待退出信号 (Ctrl+C / SIGTERM)
// ══════════════════════════════════════════════

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    Log.Information("收到退出信号，正在停止所有检测任务...");
    cts.Cancel();
};

AppDomain.CurrentDomain.ProcessExit += (_, _) =>
{
    if (!cts.IsCancellationRequested)
        cts.Cancel();
};

try
{
    await Task.Delay(Timeout.Infinite, cts.Token);
}
catch (OperationCanceledException)
{
    // expected
}

// ══════════════════════════════════════════════
//  优雅关闭
// ══════════════════════════════════════════════

Log.Information("正在关闭 {Count} 个检测任务...", pipelines.Count);

var stopTasks = pipelines.Values.Select(async entry =>
{
    try
    {
        await entry.Pipeline.StopAsync(TimeSpan.FromSeconds(5));
        entry.Pipeline.Dispose();
        Log.Information("已停止: {ConfigName}", entry.Config.Name);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "停止 [{ConfigName}] 时出错", entry.Config.Name);
    }
});

await Task.WhenAll(stopTasks);

Log.Information("所有检测任务已停止，程序退出");
await Log.CloseAndFlushAsync();

return exitCode;

// ══════════════════════════════════════════════
//  辅助方法
// ══════════════════════════════════════════════

static List<string> ParseConfigFilters(string[] args)
{
    var filters = new List<string>();

    for (int i = 0; i < args.Length; i++)
    {
        switch (args[i].ToLowerInvariant())
        {
            case "--config":
            case "-c":
                if (i + 1 < args.Length)
                    filters.Add(args[++i]);
                break;

            case "--help":
            case "-h":
                PrintHelp();
                Environment.Exit(0);
                break;

            case "--list":
            case "-l":
                PrintConfigList(args).GetAwaiter().GetResult();
                Environment.Exit(0);
                break;
        }
    }

    return filters;
}

static async Task PrintConfigList(string[] args)
{
    var svc = new ServiceCollection();
    svc.AddLogging(b => { b.ClearProviders(); b.AddSerilog(); });
    svc.AddLuxVideoDetCore();
    var sp = svc.BuildServiceProvider();
    var cfgSvc = sp.GetRequiredService<ConfigurationService>();

    var configs = await cfgSvc.ListAsync();

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"  共 {configs.Count} 个配置:");
    Console.ResetColor();
    Console.WriteLine();

    foreach (var c in configs)
    {
        var status = c.Enabled ? "启用" : "禁用";
        var color = c.Enabled ? ConsoleColor.Green : ConsoleColor.DarkGray;
        Console.ForegroundColor = color;
        Console.Write($"  [{status}] ");
        Console.ResetColor();
        Console.Write($"{c.Name}");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  (ID: {c.Id})");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"         视频源: {c.VideoSource.Type} → {c.VideoSource.Source}");
        Console.WriteLine($"         算法: {string.Join(", ", c.Algorithms.Select(a => $"{a.AlgorithmType}({a.DisplayName})"))}");
        Console.ResetColor();
        Console.WriteLine();
    }
}

static void PrintHelp()
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("  LuxVideoDet Headless — 无头检测服务");
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine("  用法:");
    Console.WriteLine("    dotnet run --project LuxVideoDet.Headless [选项]");
    Console.WriteLine();
    Console.WriteLine("  选项:");
    Console.WriteLine("    -c, --config <名称|ID>   指定要运行的配置（可多次使用）");
    Console.WriteLine("    -l, --list               列出所有可用配置");
    Console.WriteLine("    -h, --help               显示帮助信息");
    Console.WriteLine();
    Console.WriteLine("  示例:");
    Console.WriteLine("    # 运行所有已启用的配置");
    Console.WriteLine("    dotnet run --project LuxVideoDet.Headless");
    Console.WriteLine();
    Console.WriteLine("    # 只运行指定配置");
    Console.WriteLine("    dotnet run --project LuxVideoDet.Headless -- -c UCS");
    Console.WriteLine("    dotnet run --project LuxVideoDet.Headless -- -c UCS -c Test");
    Console.WriteLine();
    Console.WriteLine("  环境变量:");
    Console.WriteLine("    LUXVIDEODET_LOG_LEVEL    日志级别 (Verbose/Debug/Information/Warning/Error)");
    Console.WriteLine();
}

static void OnResultAvailable(string configName, string algoType, DetectionResult result, Microsoft.Extensions.Logging.ILogger logger)
{
    if (result.ShouldNotify && result.NotificationMessage != null)
    {
        var level = result.NotificationLevel switch
        {
            LuxVideoDet.Core.Algorithm.Results.NotificationLevel.Critical => LogLevel.Critical,
            LuxVideoDet.Core.Algorithm.Results.NotificationLevel.Error => LogLevel.Error,
            LuxVideoDet.Core.Algorithm.Results.NotificationLevel.Warning => LogLevel.Warning,
            _ => LogLevel.Information
        };

        logger.Log(level, "[{ConfigName}][{AlgorithmType}] 通知: {Message}",
            configName, algoType, result.NotificationMessage);
    }

    if (result.Judgement != ProductionJudgement.None)
    {
        var judgeLevel = result.Judgement == ProductionJudgement.NG ? LogLevel.Warning : LogLevel.Information;
        logger.Log(judgeLevel, "[{ConfigName}][{AlgorithmType}] 判定: {Judgement} | {State}",
            configName, algoType, result.Judgement, result.StateMessage);
    }
}

static void PrintBanner(List<DetectionConfiguration> configs)
{
    const string banner = @"
  ██╗     ██╗   ██╗██╗  ██╗██╗   ██╗██╗██████╗ ███████╗ ██████╗ ██████╗ ███████╗████████╗
  ██║     ██║   ██║╚██╗██╔╝██║   ██║██║██╔══██╗██╔════╝██╔═══██╗██╔══██╗██╔════╝╚══██╔══╝
  ██║     ██║   ██║ ╚███╔╝ ██║   ██║██║██║  ██║█████╗  ██║   ██║██║  ██║█████╗     ██║
  ██║     ██║   ██║ ██╔██╗ ╚██╗ ██╔╝██║██║  ██║██╔══╝  ██║   ██║██║  ██║██╔══╝     ██║
  ███████╗╚██████╔╝██╔╝ ██╗ ╚████╔╝ ██║██████╔╝███████╗╚██████╔╝██████╔╝███████╗   ██║
  ╚══════╝ ╚═════╝ ╚═╝  ╚═╝  ╚═══╝  ╚═╝╚═════╝ ╚══════╝ ╚═════╝ ╚═════╝ ╚══════╝   ╚═╝
";

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(banner);
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("  ──────────────────────────────────────────────────────────────────────────────────");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("  跨平台视频检测系统 — Headless 无头模式");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"  .NET {Environment.Version} | {Environment.OSVersion}");
    Console.WriteLine();
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"  ✓ 即将启动 {configs.Count} 个检测任务:");
    Console.ResetColor();

    foreach (var config in configs)
    {
        var algos = string.Join(", ", config.Algorithms.Where(a => a.Enabled).Select(a => a.AlgorithmType));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"    ► {config.Name}");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  [{algos}] → {config.VideoSource.Source}");
        Console.ResetColor();
    }

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine();
    Console.WriteLine("  按 Ctrl+C 停止服务");
    Console.WriteLine("  ──────────────────────────────────────────────────────────────────────────────────");
    Console.ResetColor();
    Console.WriteLine();
}
