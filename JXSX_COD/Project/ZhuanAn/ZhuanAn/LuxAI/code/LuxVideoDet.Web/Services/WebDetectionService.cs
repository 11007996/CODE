using System.Collections.Concurrent;
using LuxVideoDet.Core.Algorithm.Pipeline;
using LuxVideoDet.Core.Algorithm.Results;
using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Storage;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Web.Services;

/// <summary>
/// Web 检测服务 — 管理多路检测会话的生命周期。
/// 类似 Desktop 的 DetectionService，但面向多路并发 + MJPEG 流场景。
/// </summary>
public sealed class WebDetectionService : IDisposable
{
    private readonly PipelineFactory _pipelineFactory;
    private readonly ConfigurationService _configService;
    private readonly MjpegStreamManager _mjpegManager;
    private readonly ProductionStatisticsStore _statsStore;
    private readonly ILogger<WebDetectionService> _logger;
    private readonly ConcurrentDictionary<string, WebDetectionSession> _sessions = new();

    public WebDetectionService(
        PipelineFactory pipelineFactory,
        ConfigurationService configService,
        MjpegStreamManager mjpegManager,
        ProductionStatisticsStore statsStore,
        ILogger<WebDetectionService> logger)
    {
        _pipelineFactory = pipelineFactory;
        _configService = configService;
        _mjpegManager = mjpegManager;
        _statsStore = statsStore;
        _logger = logger;
    }

    public async Task StartAsync(string configId, CancellationToken ct = default)
    {
        if (_sessions.ContainsKey(configId))
        {
            _logger.LogWarning("会话已在运行: {ConfigId}", configId);
            return;
        }

        var config = await _configService.LoadAsync(configId, ct)
                     ?? throw new KeyNotFoundException($"配置不存在: {configId}");

        var pipeline = await _pipelineFactory.CreatePipelineAsync(config);

        var session = new WebDetectionSession
        {
            ConfigId = config.Id,
            ConfigName = config.Name,
            Pipeline = pipeline,
            Configuration = config,
            StartTime = DateTime.Now
        };

        pipeline.ResultAvailable += (algoType, result) =>
        {
            session.LastResult = result;
            session.LastAlgorithmStates[algoType] = new AlgorithmStateSnapshot(
                result.CurrentState ?? string.Empty,
                result.StateMessage);
            session.DetectionCount++;
            session.CurrentFps = result.Fps;
            session.AveragePipelineMs = result.InferenceTime;

            if (result.Judgement == ProductionJudgement.OK)
                _statsStore.IncrementOk(config.Name);
            else if (result.Judgement == ProductionJudgement.NG)
                _statsStore.IncrementNg(config.Name);

            if (result.AnnotatedFrame?.Mat != null)
            {
                var mat = result.AnnotatedFrame.Mat;
                session.LastFrameWidth = mat.Width;
                session.LastFrameHeight = mat.Height;
                _mjpegManager.PushFrame(config.Id, mat);
            }
        };

        pipeline.PipelineError += error =>
        {
            _logger.LogError(error, "流水线错误: {ConfigName}", config.Name);
            session.IsRunning = false;
            session.LastError = error.Message;
        };

        pipeline.AlgorithmError += (algoType, error) =>
        {
            _logger.LogError(error, "算法 [{AlgorithmType}] 错误: {ConfigName}", algoType, config.Name);
        };

        if (!_sessions.TryAdd(configId, session))
        {
            pipeline.Dispose();
            _logger.LogWarning("并发启动冲突: {ConfigId}", configId);
            return;
        }

        session.IsRunning = true;
        pipeline.Start();
        _logger.LogInformation("检测会话已启动: {ConfigName} (ID={ConfigId})", config.Name, config.Id);
    }

    public async Task StopAsync(string configId)
    {
        if (!_sessions.TryRemove(configId, out var session))
        {
            _logger.LogWarning("会话不存在: {ConfigId}", configId);
            return;
        }

        session.IsRunning = false;

        try
        {
            await session.Pipeline.StopAsync(TimeSpan.FromSeconds(5));
            session.Pipeline.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "停止会话失败: {ConfigName}", session.ConfigName);
        }

        _mjpegManager.RemoveChannel(configId);
        _logger.LogInformation("检测会话已停止: {ConfigName} (ID={ConfigId})", session.ConfigName, configId);
    }

    public WebDetectionSession? GetSession(string configId)
        => _sessions.TryGetValue(configId, out var s) ? s : null;

    public IReadOnlyList<WebDetectionSession> GetAllSessions()
        => _sessions.Values.ToList();

    public bool IsRunning(string configId) => _sessions.ContainsKey(configId);

    /// <summary>
    /// 根据配置名称查找运行中的会话。
    /// </summary>
    public WebDetectionSession? FindSessionByName(string configName)
        => _sessions.Values.FirstOrDefault(s =>
            s.ConfigName.Equals(configName, StringComparison.OrdinalIgnoreCase));

    public void Dispose()
    {
        foreach (var session in _sessions.Values)
        {
            try
            {
                session.IsRunning = false;
                session.Pipeline.StopAsync(TimeSpan.FromSeconds(2)).GetAwaiter().GetResult();
                session.Pipeline.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "释放会话异常: {ConfigName}", session.ConfigName);
            }
        }
        _sessions.Clear();
    }
}

public class WebDetectionSession
{
    public string ConfigId { get; set; } = string.Empty;
    public string ConfigName { get; set; } = string.Empty;
    public MultiAlgorithmPipeline Pipeline { get; set; } = null!;
    public DetectionConfiguration Configuration { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public bool IsRunning { get; set; }
    public double CurrentFps { get; set; }

    /// <summary>约 60 秒滑动窗口平均每帧管线耗时（毫秒），与 Core DetectionResult.InferenceTime 一致。</summary>
    public double AveragePipelineMs { get; set; }

    public int DetectionCount { get; set; }
    public DetectionResult? LastResult { get; set; }
    public string? LastError { get; set; }

    /// <summary>最近一帧检测画面（标注后 Mat）的宽高，与 MJPEG 源流一致；用于 Web 页展示分辨率。</summary>
    public int LastFrameWidth { get; set; }
    public int LastFrameHeight { get; set; }

    /// <summary>各算法最近一次帧结果的 <see cref="DetectionResult.CurrentState"/>，供 Web PLC 按钮等与 Desktop 对齐。</summary>
    public ConcurrentDictionary<string, AlgorithmStateSnapshot> LastAlgorithmStates { get; } =
        new(StringComparer.OrdinalIgnoreCase);
}

/// <param name="CurrentState">与 <see cref="DetectionResult.CurrentState"/> 一致（如 ERROR、IDLE）。</param>
public sealed record AlgorithmStateSnapshot(string CurrentState, string? StateMessage);
