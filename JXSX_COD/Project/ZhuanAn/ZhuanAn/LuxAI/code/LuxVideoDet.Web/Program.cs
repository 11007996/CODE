using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using LuxVideoDet.Core;
using LuxVideoDet.Core.Configuration;
using Microsoft.Extensions.Logging;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Extensions;
using LuxVideoDet.Core.Storage;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.VideoSource;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.S3Storage;
using LuxVideoDet.Core.S3Storage.Minio;
using LuxVideoDet.Localization;
using LuxVideoDet.Web.Models;
using LuxVideoDet.Web.Services;
using Microsoft.AspNetCore.Localization;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// ── Serilog ──
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/web-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        rollOnFileSizeLimit: true)
    .CreateLogger();

builder.Host.UseSerilog();

// ── Core 服务 ──
builder.Services.AddLuxVideoDetCore();

builder.Services.AddLuxVideoDetLocalization();
var supportedUiCultures = new[] { "en-US", "zh-CN", "vi-VN" }.Select(c => new CultureInfo(c)).ToList();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("zh-CN");
    options.SupportedCultures = supportedUiCultures;
    options.SupportedUICultures = supportedUiCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    options.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider());
});

// ── 生产统计 ──
builder.Services.AddSingleton(sp =>
{
    var logger = sp.GetRequiredService<ILogger<ProductionStatisticsStore>>();
    return new ProductionStatisticsStore(logger, "stats");
});

// ── Web 服务 ──
builder.Services.AddSingleton(sp =>
{
    var logger = sp.GetRequiredService<ILogger<MjpegStreamManager>>();
    // maxPreviewWidth=0：不缩放，按原推理画面分辨率编码；jpegQuality 适当提高以减轻块状伪影（带宽/CPU 会上升）
    return new MjpegStreamManager(logger, jpegQuality: 85, maxPreviewWidth: 0);
});
builder.Services.AddSingleton<WebDetectionService>();
builder.Services.AddS3StorageSyncService();

// ── JSON 配置 ──
var jsonOpts = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Converters = { new JsonStringEnumConverter() },
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

builder.Services.ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opts.SerializerOptions.PropertyNameCaseInsensitive = true;
    opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opts.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    opts.SerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});

var app = builder.Build();

Log.Information("LuxVideoDet.Web 启动，版本 {DisplayVersion}（完整 {InformationalVersion}）", AppMetadata.DisplayVersion, AppMetadata.InformationalVersion);

app.UseRequestLocalization();

// ── 静态文件（wwwroot）— 确保 HTML 返回 UTF-8 编码以支持中文 ──
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        var contentType = ctx.Context.Response.ContentType;
        if (contentType != null && contentType.StartsWith("text/") && !contentType.Contains("charset"))
        {
            ctx.Context.Response.ContentType = contentType + "; charset=utf-8";
        }
    }
});

// ══════════════════════════════════════════════
//  配置管理 API
// ══════════════════════════════════════════════

var api = app.MapGroup("/api");

api.MapGet("/version", () => Results.Json(new
{
    version = AppMetadata.DisplayVersion,
    fullVersion = AppMetadata.InformationalVersion
}));

api.MapGet("/ui/i18n", (IAppLocalizer loc) =>
{
    var dict = new Dictionary<string, string>(StringComparer.Ordinal);
    foreach (var key in UiKeys.All)
        dict[key] = loc.GetString(key);
    return Results.Json(dict);
});

api.MapGet("/configs", async (ConfigurationService configSvc, WebDetectionService detSvc) =>
{
    var configs = await configSvc.ListAsync();
    var result = configs.Select(c => new
    {
        c.Id,
        c.Name,
        c.Enabled,
        c.CreatedAt,
        c.UpdatedAt,
        VideoSourceType = c.VideoSource.Type.ToString(),
        VideoSource = c.VideoSource.Source,
        AlgorithmCount = c.Algorithms.Count,
        Algorithms = c.Algorithms.Select(a => a.AlgorithmType).ToList(),
        IsRunning = detSvc.IsRunning(c.Id),
        Fps = detSvc.GetSession(c.Id)?.CurrentFps ?? 0
    });
    return Results.Ok(result);
});

api.MapGet("/configs/{id}", async (string id, ConfigurationService configSvc) =>
{
    var config = await configSvc.LoadAsync(id);
    return config is null ? Results.NotFound(new { error = "配置不存在" }) : Results.Ok(config);
});

api.MapPost("/configs", async (DetectionConfiguration config, ConfigurationService configSvc) =>
{
    config.Id = Guid.NewGuid().ToString();
    config.CreatedAt = DateTime.Now;
    config.UpdatedAt = DateTime.Now;
    await configSvc.SaveAsync(config);
    return Results.Created($"/api/configs/{config.Id}", config);
});

api.MapPut("/configs/{id}", async (string id, DetectionConfiguration config, ConfigurationService configSvc, WebDetectionService detSvc) =>
{
    if (!await configSvc.ExistsAsync(id))
        return Results.NotFound(new { error = "配置不存在" });

    if (detSvc.IsRunning(id))
        return Results.BadRequest(new { error = "请先停止运行中的检测再修改配置" });

    config.Id = id;
    config.UpdatedAt = DateTime.Now;
    await configSvc.SaveAsync(config);
    return Results.Ok(config);
});

api.MapDelete("/configs/{id}", async (string id, ConfigurationService configSvc, WebDetectionService detSvc) =>
{
    if (detSvc.IsRunning(id))
        return Results.BadRequest(new { error = "请先停止运行中的检测再删除配置" });

    await configSvc.DeleteAsync(id);
    return Results.Ok(new { message = "已删除" });
});

api.MapPost("/configs/validate", async (DetectionConfiguration config, ConfigurationService configSvc) =>
{
    var (isValid, errors) = await configSvc.ValidateAsync(config);
    return Results.Ok(new { isValid, errors });
});

// 导入配置 — 支持单个对象、数组、{ configurations: [...] } 三种格式
api.MapPost("/configs/import", async (HttpRequest req, ConfigurationService configSvc) =>
{
    string json;
    using (var reader = new StreamReader(req.Body))
        json = await reader.ReadToEndAsync();

    var configs = ParseConfigJson(json, jsonOpts);

    if (configs.Count == 0)
        return Results.BadRequest(new { error = "未找到有效配置，支持格式：单个对象 / 数组 / { configurations: [...] }" });

    var imported = new List<string>();
    var errors = new List<object>();

    foreach (var config in configs)
    {
        try
        {
            if (string.IsNullOrEmpty(config.Id))
                config.Id = Guid.NewGuid().ToString();
            config.CreatedAt = DateTime.Now;
            config.UpdatedAt = DateTime.Now;

            await configSvc.ImportAsync(config);
            imported.Add(config.Name);
            Log.Information("已导入配置: {ConfigName}", config.Name);
        }
        catch (Exception ex)
        {
            errors.Add(new { Name = config.Name, Error = ex.Message });
        }
    }

    return Results.Ok(new { imported, errors, total = configs.Count });
});

// 导出全部配置
api.MapGet("/configs/export", async (ConfigurationService configSvc) =>
{
    var configs = await configSvc.ListAsync();
    var json = JsonSerializer.Serialize(configs, jsonOpts);
    return Results.Text(json, "application/json");
});

// ══════════════════════════════════════════════
//  检测会话控制 API
// ══════════════════════════════════════════════

api.MapPost("/sessions/{configId}/start", async (string configId, WebDetectionService detSvc) =>
{
    try
    {
        await detSvc.StartAsync(configId);
        return Results.Ok(new { message = "检测已启动", configId });
    }
    catch (KeyNotFoundException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

api.MapPost("/sessions/{configId}/stop", async (string configId, WebDetectionService detSvc) =>
{
    await detSvc.StopAsync(configId);
    return Results.Ok(new { message = "检测已停止", configId });
});

api.MapGet("/sessions", (WebDetectionService detSvc) =>
{
    var sessions = detSvc.GetAllSessions().Select(s => new
    {
        s.ConfigId,
        s.ConfigName,
        s.IsRunning,
        s.StartTime,
        s.CurrentFps,
        s.AveragePipelineMs,
        s.DetectionCount,
        s.LastError,
        RunningTime = (DateTime.Now - s.StartTime).ToString(@"hh\:mm\:ss")
    });
    return Results.Ok(sessions);
});

api.MapGet("/sessions/{configId}", (string configId, WebDetectionService detSvc) =>
{
    var session = detSvc.GetSession(configId);
    if (session is null)
        return Results.NotFound(new { error = "会话不存在" });

    var r = session.LastResult;
    var algorithmStates = session.LastAlgorithmStates.ToDictionary(
        kvp => kvp.Key,
        kvp => new
        {
            currentState = kvp.Value.CurrentState,
            stateMessage = kvp.Value.StateMessage
        },
        StringComparer.OrdinalIgnoreCase);

    return Results.Ok(new
    {
        session.ConfigId,
        session.ConfigName,
        session.IsRunning,
        session.StartTime,
        session.CurrentFps,
        session.AveragePipelineMs,
        session.DetectionCount,
        session.LastError,
        RunningTime = (DateTime.Now - session.StartTime).ToString(@"hh\:mm\:ss"),
        State = r?.CurrentState,
        StateMessage = r?.StateMessage,
        // 与 Core DetectionResult 一致，供 Web 端浏览器 TTS 等使用（与 Desktop 喇叭播报同源文案）
        ShouldNotify = r?.ShouldNotify ?? false,
        NotificationTitle = r?.NotificationTitle,
        NotificationMessage = r?.NotificationMessage,
        NotificationLevel = r?.NotificationLevel.ToString(),
        // 与 Desktop 预览区 PLC 按钮对齐：按算法类型取 CurrentState（如 ERROR）
        AlgorithmStates = algorithmStates,
        session.LastFrameWidth,
        session.LastFrameHeight
    });
});

api.MapPost("/sessions/start-all", async (WebDetectionService detSvc, ConfigurationService configSvc) =>
{
    var configs = await configSvc.ListAsync();
    var started = new List<string>();
    var errors = new List<object>();

    foreach (var config in configs.Where(c => c.Enabled && !detSvc.IsRunning(c.Id)))
    {
        try
        {
            await detSvc.StartAsync(config.Id);
            started.Add(config.Name);
        }
        catch (Exception ex)
        {
            errors.Add(new { config.Name, Error = ex.Message });
        }
    }

    return Results.Ok(new { started, errors });
});

api.MapPost("/sessions/stop-all", async (WebDetectionService detSvc) =>
{
    var sessions = detSvc.GetAllSessions();
    foreach (var s in sessions)
        await detSvc.StopAsync(s.ConfigId);

    return Results.Ok(new { message = $"已停止 {sessions.Count} 个会话" });
});

// ══════════════════════════════════════════════
//  算法元数据 + 帧捕获 API
// ══════════════════════════════════════════════

api.MapGet("/algorithms/types", () =>
{
    var types = LuxVideoDet.Core.Algorithm.DetectionAlgorithmFactory.GetSupportedAlgorithms();
    return Results.Ok(types.Select(t =>
    {
        static object MapSection(LuxVideoDet.Core.Algorithm.AlgorithmArgsFormSection u) => new
        {
            u.AoiDetectorTypeKey,
            u.SectionTitle,
            u.Description,
            ArgFields = u.ArgFields.Select(f => new
            {
                f.Name,
                f.DisplayName,
                f.Description,
                f.ParameterType,
                f.DefaultValue,
                f.Required,
                f.Example
            })
        };

        var aoiParameterSections = LuxVideoDet.Core.Algorithm.DetectionAlgorithmFactory.GetAoiParameterSections(t)
            .Select(MapSection).ToList();
        var parameterSections = LuxVideoDet.Core.Algorithm.DetectionAlgorithmFactory.GetAlgorithmParameterSections(t)
            .Select(MapSection).ToList();

        return new
        {
            Type = t,
            DefaultClasses = LuxVideoDet.Core.Algorithm.DetectionAlgorithmFactory.GetDefaultClasses(t),
            Regions = LuxVideoDet.Core.Algorithm.DetectionAlgorithmFactory.GetRequiredRegions(t).Select(r => new
            {
                r.Name,
                r.DisplayName,
                r.Description,
                r.Required,
                Color = $"rgba({r.DefaultColor.Val2},{r.DefaultColor.Val1},{r.DefaultColor.Val0},1)"
            }),
            AoiParameterSections = aoiParameterSections,
            ParameterSections = parameterSections,
            // 兼容：旧键等同 AOI
            AoiUsages = aoiParameterSections
        };
    }));
});

api.MapGet("/algorithms/{type}/regions", (string type) =>
{
    var regions = LuxVideoDet.Core.Algorithm.DetectionAlgorithmFactory.GetRequiredRegions(type);
    return Results.Ok(regions.Select(r => new
    {
        r.Name,
        r.DisplayName,
        r.Description,
        r.Required,
        Color = $"rgba({r.DefaultColor.Val2},{r.DefaultColor.Val1},{r.DefaultColor.Val0},1)"
    }));
});

api.MapGet("/algorithms/{type}/ui-actions", (string type) =>
{
    var actions = DetectionAlgorithmFactory.GetUiActionDefinitions(type);
    return Results.Ok(actions.Select(a => new
    {
        a.ActionId,
        a.DisplayName,
        a.Description,
        a.Placement,
        a.DisplayNameWhenOk,
        a.DisplayNameWhenNg
    }));
});

api.MapPost("/sessions/{configId}/ui-action", (string configId, WebDetectionService detSvc, UiActionInvokeDto dto) =>
{
    if (string.IsNullOrWhiteSpace(dto.AlgorithmType) || string.IsNullOrWhiteSpace(dto.ActionId))
        return Results.BadRequest(new { error = "缺少 algorithmType 或 actionId" });

    var session = detSvc.GetSession(configId);
    if (session is null || !session.IsRunning)
        return Results.BadRequest(new { error = "会话未运行或不存在" });

    var ok = session.Pipeline.TryInvokeUiAction(dto.AlgorithmType, dto.ActionId, out var err);
    if (!ok)
        return Results.BadRequest(new { error = err ?? "操作失败" });

    return Results.Ok(new { success = true });
});

// GET /api/inference/devices?current=CoreML — 与 Core InferenceDeviceRegistry 一致；current 用于合并已保存但本机默认不列出的设备。
api.MapGet("/inference/devices", (InferenceDevice? current) =>
{
    var device = current ?? InferenceDevice.CPU;
    var list = InferenceDeviceRegistry.GetDescriptorsForUi(device);
    return Results.Ok(list.Select(d => new { device = d.Device, displayName = d.DisplayName }));
});

// 从视频源直接捕获一帧 — 用于区域编辑器获取背景图（跳过 H.264 黑帧；JPEG 用 SkiaSharp，避免 ImEncode）
api.MapPost("/capture-frame", (
    LuxVideoDet.Core.Configuration.Models.VideoSourceConfig videoConfig,
    LuxVideoDet.Core.VideoSource.VideoSourceFactory vsFactory,
    ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("VideoFrameCapture");
    try
    {
        using var source = vsFactory.CreateVideoSource(videoConfig);
        source.Open();

        if (!source.IsOpened)
            return Results.BadRequest(new { error = "无法打开视频源: " + videoConfig.Source });

        using var frame = VideoFrameCapture.CaptureRepresentativeFrame(source, logger);
        if (frame == null || frame.IsEmpty)
            return Results.BadRequest(new { error = "无法读取有效帧" });

        var jpeg = CrossPlatformMediaWriter.EncodeMatToJpeg(frame.Mat, 90);
        if (jpeg == null || jpeg.Length == 0)
            return Results.BadRequest(new { error = "无法编码图像（JPEG）" });

        return Results.File(jpeg, "image/jpeg");
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "帧捕获失败");
        return Results.BadRequest(new { error = ex.Message });
    }
});

// ══════════════════════════════════════════════
//  生产统计 API
// ══════════════════════════════════════════════

api.MapGet("/stats", (ProductionStatisticsStore statsStore) =>
{
    var (ok, ng, pcount) = statsStore.GetAllTimeTotalStats();
    return Results.Ok(new { ok, ng, total = ok + ng, yield = ok + ng > 0 ? (double)ok / (ok + ng) : 0.0, pcount });
});

api.MapGet("/stats/{configName}", (string configName, ProductionStatisticsStore statsStore) =>
{
    var decoded = Uri.UnescapeDataString(configName);
    var (ok, ng) = statsStore.GetAllTimeStatsForConfig(decoded);
    var total = ok + ng;
    return Results.Ok(new
    {
        configName = decoded,
        ok,
        ng,
        total,
        yield = total > 0 ? (double)ok / total : 0.0
    });
});

// ══════════════════════════════════════════════
//  NG 回放 API
// ══════════════════════════════════════════════

api.MapGet("/ng-replay/{configName}/dates", (string configName) =>
{
    var decoded = Uri.UnescapeDataString(configName);
    var catchDir = Path.Combine("catch", decoded);
    if (!Directory.Exists(catchDir))
        return Results.Ok(Array.Empty<object>());

    var dirs = Directory.GetDirectories(catchDir)
        .Select(d => new DirectoryInfo(d))
        .Where(d => d.Name.Length == 10 && d.Name[4] == '-')
        .OrderByDescending(d => d.Name)
        .Select(d => new
        {
            date = d.Name,
            fileCount = d.GetFiles("*.mp4").Length + d.GetFiles("*.jpg").Length
        });

    return Results.Ok(dirs);
});

api.MapGet("/ng-replay/{configName}/dates/{date}/files", (string configName, string date) =>
{
    var decoded = Uri.UnescapeDataString(configName);
    var dateDir = Path.Combine("catch", decoded, date);
    if (!Directory.Exists(dateDir))
        return Results.Ok(Array.Empty<object>());

    var files = new DirectoryInfo(dateDir)
        .GetFiles()
        .Where(f => f.Extension is ".mp4" or ".jpg" or ".jpeg" or ".png")
        .OrderByDescending(f => f.Name)
        .Select(f => new
        {
            name = f.Name,
            type = f.Extension is ".mp4" ? "video" : "image",
            size = f.Length,
            url = $"/api/ng-replay/file?path={Uri.EscapeDataString(Path.Combine(decoded, date, f.Name))}"
        });

    return Results.Ok(files);
});

app.MapGet("/api/ng-replay/file", (string path, HttpContext ctx) =>
{
    var sanitized = path.Replace("..", "").Replace("\\", "/");
    var fullPath = Path.GetFullPath(Path.Combine("catch", sanitized));
    var catchRoot = Path.GetFullPath("catch");

    if (!fullPath.StartsWith(catchRoot) || !File.Exists(fullPath))
        return Results.NotFound(new { error = "文件不存在" });

    var ext = Path.GetExtension(fullPath).ToLowerInvariant();
    var contentType = ext switch
    {
        ".mp4" => "video/mp4",
        ".jpg" or ".jpeg" => "image/jpeg",
        ".png" => "image/png",
        _ => "application/octet-stream"
    };

    ctx.Response.Headers.CacheControl = "public, max-age=86400";
    return Results.File(fullPath, contentType, enableRangeProcessing: true);
});

// ══════════════════════════════════════════════
//  MJPEG 视频流端点
// ══════════════════════════════════════════════

app.MapGet("/api/stream/{configId}", async (string configId, MjpegStreamManager mjpeg, HttpContext ctx) =>
{
    if (!mjpeg.HasChannel(configId))
        return Results.NotFound(new { error = "没有可用的视频流" });

    ctx.Response.ContentType = "multipart/x-mixed-replace; boundary=--frame";
    ctx.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
    ctx.Response.Headers.Pragma = "no-cache";
    ctx.Response.Headers.Connection = "keep-alive";

    await mjpeg.SubscribeAsync(configId, ctx.Response.Body, ctx.RequestAborted);
    return Results.Empty;
});

app.MapGet("/api/snapshot/{configId}", (string configId, MjpegStreamManager mjpeg) =>
{
    var jpeg = mjpeg.GetSnapshot(configId);
    return jpeg is null
        ? Results.NotFound(new { error = "没有可用的快照" })
        : Results.File(jpeg, "image/jpeg");
});

api.MapGet("/resolve", async (string config, ConfigurationService configSvc, WebDetectionService detSvc) =>
{
    var configs = await configSvc.ListAsync();
    var match = configs.FirstOrDefault(c =>
        c.Name.Equals(config, StringComparison.OrdinalIgnoreCase));

    if (match is null)
        return Results.NotFound(new { error = $"找不到名为 '{config}' 的配置" });

    return Results.Ok(new
    {
        match.Id,
        match.Name,
        IsRunning = detSvc.IsRunning(match.Id),
        StreamUrl = $"/api/stream/{match.Id}",
        SnapshotUrl = $"/api/snapshot/{match.Id}"
    });
});

// ══════════════════════════════════════════════
//  应用配置 & S3 同步 API
// ══════════════════════════════════════════════
api.MapGet("/app-config", async (ConfigurationService configurationService) =>
{
    var cfg = await configurationService.GetAppConfigurationAsync();
    return Results.Ok(cfg);
});

api.MapPost("/app-config", async (ConfigurationService configurationService, AppConfiguration appConfig) =>
{
    await configurationService.SetAppConfigurationAsync(appConfig);
    return Results.Ok();
});

api.MapGet("/file-system/directories", (string path = ".") =>
{
    try
    {
        string projectRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), ".."));
        var sanitizedPath = path.Replace("..", "").Replace("\\", "/");
        var fullPath = Path.GetFullPath(Path.Combine(projectRoot, sanitizedPath));
        if (!fullPath.StartsWith(projectRoot))
            return Results.BadRequest(new { error = "路径无效" });

        var directories = Directory.GetDirectories(fullPath)
            .Select(d => new DirectoryInfo(d))
            .Select(d => new { name = d.Name, path = Path.GetRelativePath(projectRoot, d.FullName).Replace("\\", "/"), isDirectory = true })
            .ToList();

        var parentPath = Path.GetDirectoryName(fullPath);
        string? parentRelativePath = null;
        if (parentPath != null && parentPath != projectRoot)
            parentRelativePath = Path.GetRelativePath(projectRoot, parentPath).Replace("\\", "/");

        return Results.Ok(new
        {
            currentPath = Path.GetRelativePath(projectRoot, fullPath).Replace("\\", "/"),
            parentPath = parentRelativePath,
            directories
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

api.MapGet("/models", async (ConfigurationService configurationService) =>
{
    try
    {
        var appCfg = await configurationService.GetAppConfigurationAsync();
        var modelDirectory = appCfg.ModelDirectory ?? "models";
        string projectRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), ".."));
        string fullModelPath = Path.GetFullPath(Path.Combine(projectRoot, modelDirectory));
        Directory.CreateDirectory(fullModelPath);

        var modelFiles = Directory.GetFiles(fullModelPath)
            .Where(f => Path.GetExtension(f).Equals(".onnx", StringComparison.OrdinalIgnoreCase))
            .Select(f => new { name = Path.GetFileName(f), path = Path.GetRelativePath(projectRoot, f).Replace("\\", "/") })
            .ToList();

        return Results.Ok(new { modelDirectory, fullPath = fullModelPath, models = modelFiles });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

api.MapGet("/minio/config", async (ConfigurationService configSvc) =>
{
    var appCfg = await configSvc.GetAppConfigurationAsync();
    appCfg.S3Storage.Enabled = true;
    return Results.Ok(appCfg.S3Storage);
});

api.MapPost("/minio/config", async (S3StorageConfiguration minioConfig, IS3StorageSyncService minioSvc, ConfigurationService configSvc) =>
{
    try
    {
        minioConfig.Enabled = true;
        var appCfg = await configSvc.GetAppConfigurationAsync();
        appCfg.S3Storage = minioConfig;
        await configSvc.SetAppConfigurationAsync(appCfg);
        await minioSvc.InitializeAsync();
        return Results.Ok(new { message = "配置已保存" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

api.MapGet("/minio/providers", () =>
{
    var providers = S3StorageProviderRegistry.DiscoverAvailableProviders();
    return Results.Ok(providers);
});

api.MapPost("/minio/check-connection", async (IS3StorageSyncService minioSvc) =>
{
    var result = await minioSvc.CheckConnectionAsync();
    return Results.Ok(new { success = result.Success, message = result.Message, errorDetail = result.ErrorDetail });
});

api.MapPost("/minio/sync-plugins", async (IS3StorageSyncService minioSvc) =>
{
    var result = await minioSvc.SyncPluginsAsync();
    return Results.Ok(new { result.Success, result.Message, result.FileCount, result.UpdatedCount, result.DownloadedCount, result.SkippedCount, result.FailedCount });
});

api.MapPost("/minio/sync-models", async (IS3StorageSyncService minioSvc) =>
{
    var result = await minioSvc.SyncModelsAsync();
    return Results.Ok(new { result.Success, result.Message, result.FileCount, result.UpdatedCount, result.DownloadedCount, result.SkippedCount, result.FailedCount });
});

api.MapPost("/minio/sync-videos", async (IS3StorageSyncService minioSvc) =>
{
    var result = await minioSvc.SyncVideosAsync();
    return Results.Ok(new { success = result.Success, message = result.Message, fileCount = result.FileCount, updatedCount = result.UpdatedCount, downloadedCount = result.DownloadedCount, skippedCount = result.SkippedCount, failedCount = result.FailedCount });
});

api.MapPost("/minio/sync-zips", async (IS3StorageSyncService minioSvc) =>
{
    var result = await minioSvc.SyncZipPackagesAsync();
    return Results.Ok(new { success = result.Success, message = result.Message, fileCount = result.FileCount, updatedCount = result.UpdatedCount, downloadedCount = result.DownloadedCount, skippedCount = result.SkippedCount, failedCount = result.FailedCount });
});

api.MapPost("/minio/force-sync-all", async (IS3StorageSyncService minioSvc) =>
{
    var result = await minioSvc.ForceSyncAllAsync();
    return Results.Ok(new { result.Success, result.Message, result.FileCount, result.UpdatedCount, result.DownloadedCount, result.SkippedCount, result.FailedCount });
});

api.MapGet("/minio/files/{fileType}", async (string fileType, IS3StorageSyncService minioSvc) =>
{
    if (fileType != "plugin" && fileType != "model" && fileType != "video" && fileType != "zip")
        return Results.BadRequest(new { error = "fileType必须是plugin/model/video/zip" });
    var files = await minioSvc.GetFileListAsync(fileType);
    return Results.Ok(files.Select(f => new
    {
        f.FileName,
        f.FileType,
        f.LocalPath,
        f.RemotePath,
        f.FileSize,
        f.LastModified,
        f.ETag,
        Status = f.Status.ToString(),
        f.Message,
        f.SyncTime
    }));
});

api.MapPost("/minio/download-file", async (DownloadFileRequest req, IS3StorageSyncService minioSvc, ConfigurationService configSvc) =>
{
    var appCfg = await configSvc.GetAppConfigurationAsync();
    var fileName = Path.GetFileName(req.RemotePath);
    string baseDir = req.FileType switch
    {
        "plugin" => appCfg.PluginDirectory,
        "model" => appCfg.ModelDirectory,
        "video" => appCfg.S3Storage.VideoDirectory,
        "zip" => appCfg.S3Storage.ZipDirectory,
        _ => appCfg.ModelDirectory
    };
    if (!Path.IsPathRooted(baseDir))
        baseDir = Path.Combine(AppContext.BaseDirectory, baseDir);
    var localPath = Path.Combine(baseDir, fileName);
    var result = await minioSvc.DownloadFileAsync(req.RemotePath, localPath);
    return Results.Ok(new { result.FileName, result.FileSize, Status = result.Status.ToString(), result.Message });
});

api.MapGet("/minio/stats", async (IS3StorageSyncService minioSvc) =>
{
    var stats = await minioSvc.GetSyncStatsAsync();
    return Results.Ok(new { stats.TotalFiles, stats.PluginFiles, stats.ModelFiles, stats.VideoFiles, stats.ZipFiles, stats.TotalSize, stats.LastSyncTime });
});

// ══════════════════════════════════════════════
//  启动
// ══════════════════════════════════════════════

var port = builder.Configuration.GetValue<int?>("WebServer:Port") ?? 5050;
var bindAddress = builder.Configuration.GetValue<string?>("WebServer:BindAddress") ?? "*";
ApplyWebServerUrls(app, bindAddress, port);

app.Lifetime.ApplicationStopping.Register(() =>
{
    Log.Information("Web 服务正在关闭，停止所有检测会话...");
    var detSvc = app.Services.GetRequiredService<WebDetectionService>();
    detSvc.Dispose();
});

PrintBanner(port, bindAddress);

try
{
    app.Run();
}
catch (IOException ex) when (ex.Message.Contains("address already in use"))
{
    Log.Error("端口 {Port} 绑定失败（地址被占用）", port);
    Log.Warning("可能原因: VPN/代理软件的出站连接占用了该端口。");
    Log.Warning("解决方法: 1) 修改 appsettings.json 中 WebServer:Port 换一个端口");
    Log.Warning("          2) 设置 WebServer:BindAddress 为 localhost 或 127.0.0.1（仅限本机）");
    Log.Warning("          3) 运行 netstat -ano | findstr \":{Port}\" 查找并结束占用进程", port);
    throw;
}

// ══════════════════════════════════════════════
//  辅助方法
// ══════════════════════════════════════════════

/// <summary>
/// 配置 Kestrel 监听地址。单独使用 "localhost" 时，部分环境只绑定 IPv4 或 IPv6 会导致
/// 浏览器访问 localhost（解析到另一端）时出现「拒绝访问」或无法连接；因此同时绑定 127.0.0.1 与 ::1。
/// </summary>
static void ApplyWebServerUrls(WebApplication app, string? bindAddress, int port)
{
    var a = (bindAddress ?? "*").Trim();
    if (string.IsNullOrEmpty(a))
        a = "*";

    if (string.Equals(a, "localhost", StringComparison.OrdinalIgnoreCase))
    {
        app.Urls.Add($"http://127.0.0.1:{port}");
        app.Urls.Add($"http://[::1]:{port}");
        return;
    }

    if (a is "*" or "0.0.0.0" or "+")
    {
        app.Urls.Add($"http://0.0.0.0:{port}");
        return;
    }

    app.Urls.Add($"http://{a}:{port}");
}

static void PrintBanner(int port, string bindAddress)
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
    Console.WriteLine($"  跨平台视频检测系统 — Web 控制台");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"  .NET {Environment.Version} | {Environment.OSVersion}");
    Console.WriteLine();
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"  ✓ Web 服务已就绪");
    Console.ResetColor();
    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.Yellow;
    var ba = (bindAddress ?? "*").Trim();
    if (string.Equals(ba, "localhost", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"  ► 本机访问(推荐): http://127.0.0.1:{port}");
        Console.WriteLine($"  ► 本机访问:       http://localhost:{port}");
    }
    else if (ba is "*" or "0.0.0.0" or "+")
    {
        Console.WriteLine($"  ► 本机访问:    http://127.0.0.1:{port}");
        Console.WriteLine($"  ► 局域网访问:  http://<本机IP>:{port}");
    }
    else
    {
        Console.WriteLine($"  ► 访问: http://{ba}:{port}");
    }

    Console.WriteLine($"  ► 视频流查看:  http://127.0.0.1:{port}/view.html?config=<配置名称>");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine();
    Console.WriteLine("  按 Ctrl+C 停止服务");
    Console.WriteLine("  ──────────────────────────────────────────────────────────────────────────────────");
    Console.ResetColor();
    Console.WriteLine();

    // macOS：系统「隔空播放接收器」默认占用 5000；若仍手动配置为 5000 会冲突。
    if (port == 5000 && RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("  提示：5000 常被「隔空播放接收器」占用。仓库默认端口已为 5050；若你改回 5000，请关闭");
        Console.WriteLine("        「系统设置 → 通用 → 隔空播放与接力」中的隔空播放接收器。");
        Console.ResetColor();
        Console.WriteLine();
    }
}

static List<DetectionConfiguration> ParseConfigJson(string json, JsonSerializerOptions opts)
{
    var trimmed = json.TrimStart();

    try
    {
        if (trimmed.StartsWith('['))
        {
            return JsonSerializer.Deserialize<List<DetectionConfiguration>>(json, opts)
                   ?? new List<DetectionConfiguration>();
        }

        if (trimmed.StartsWith('{'))
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("configurations", out var arr) && arr.ValueKind == JsonValueKind.Array)
            {
                return JsonSerializer.Deserialize<List<DetectionConfiguration>>(arr.GetRawText(), opts)
                       ?? new List<DetectionConfiguration>();
            }

            var single = JsonSerializer.Deserialize<DetectionConfiguration>(json, opts);
            if (single != null && !string.IsNullOrEmpty(single.Name))
                return new List<DetectionConfiguration> { single };
        }
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "解析导入 JSON 失败");
    }

    return new List<DetectionConfiguration>();
}
