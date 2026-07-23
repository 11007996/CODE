using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuxVideoDet.Core.Algorithm.Pipeline;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Desktop.Models;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Desktop.Services;

/// <summary>
/// 检测管理服务 — 管理多个检测会话的生命周期。
/// 编排逻辑由 Core 层 <see cref="PipelineFactory"/> 负责，本服务只做会话管理和 UI 事件桥接。
/// </summary>
public class DetectionService
{
    private readonly PipelineFactory _pipelineFactory;
    private readonly ILogger<DetectionService> _logger;
    private readonly Dictionary<string, DetectionSession> _sessions = new();
    private readonly object _lock = new();

    public event EventHandler<DetectionResultEventArgs>? ResultAvailable;
    public event EventHandler<string>? SessionStarted;
    public event EventHandler<string>? SessionStopped;
    public event EventHandler<(string ConfigId, Exception Error)>? SessionError;

    public DetectionService(
        PipelineFactory pipelineFactory,
        ILogger<DetectionService> logger)
    {
        _pipelineFactory = pipelineFactory;
        _logger = logger;
    }

    /// <summary>
    /// 启动检测会话。
    /// </summary>
    public async Task StartDetectionAsync(DetectionConfiguration config)
    {
        lock (_lock)
        {
            if (_sessions.ContainsKey(config.Id))
            {
                _logger.LogWarning("检测会话已存在: {ConfigId}", config.Id);
                return;
            }
        }

        try
        {
            // Core 层工厂一步完成：视频源 + 算法 + 流水线
            var pipeline = await _pipelineFactory.CreatePipelineAsync(config);

            var session = new DetectionSession
            {
                ConfigId = config.Id,
                ConfigName = config.Name,
                Pipeline = pipeline,
                Configuration = config,
                StartTime = DateTime.Now,
                IsRunning = true
            };

            // 订阅流水线事件 → 桥接到 UI
            pipeline.ResultAvailable += (algoType, result) =>
            {
                session.LastResults[algoType] = result;
                session.DetectionCount++;
                session.AveragePipelineMs = session.LastResults.Values.Count > 0
                    ? session.LastResults.Values.Average(r => r.InferenceTime)
                    : result.InferenceTime;

                ResultAvailable?.Invoke(this, new DetectionResultEventArgs
                {
                    ConfigId = session.ConfigId,
                    AlgorithmType = algoType,
                    Result = result
                });
            };

            pipeline.PipelineError += error =>
            {
                session.IsRunning = false;
                SessionError?.Invoke(this, (config.Id, error));
            };

            pipeline.AlgorithmError += (algoType, error) =>
            {
                _logger.LogError(error,
                    "算法 [{AlgorithmType}] 处理异常，会话: {ConfigName}",
                    algoType, config.Name);
            };

            lock (_lock)
            {
                _sessions[config.Id] = session;
            }

            pipeline.Start();
            SessionStarted?.Invoke(this, config.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "启动检测会话失败: {Summary}",
                DetectionSessionErrorFormatting.FormatStartFailure(config.Name, ex));
            throw;
        }
    }

    /// <summary>
    /// 停止检测会话。
    /// </summary>
    public async Task StopDetectionAsync(string configId)
    {
        DetectionSession? session;
        lock (_lock)
        {
            if (!_sessions.TryGetValue(configId, out session))
            {
                _logger.LogWarning("检测会话不存在: {ConfigId}", configId);
                return;
            }
        }

        _logger.LogInformation("停止检测会话: {ConfigName}", session.ConfigName);

        try
        {
            session.IsRunning = false;
            await session.Pipeline.StopAsync(TimeSpan.FromSeconds(5));
            session.Pipeline.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "停止检测会话失败: {Summary}",
                DetectionSessionErrorFormatting.FormatStartFailure(session.ConfigName, ex));
        }
        finally
        {
            lock (_lock) { _sessions.Remove(configId); }
            SessionStopped?.Invoke(this, configId);
            _logger.LogInformation("检测会话已停止: {ConfigName}", session.ConfigName);
        }
    }

    public DetectionSession? GetSession(string configId)
    {
        lock (_lock)
        {
            return _sessions.TryGetValue(configId, out var session) ? session : null;
        }
    }

    public IReadOnlyList<DetectionSession> GetAllSessions()
    {
        lock (_lock)
        {
            return _sessions.Values.ToList();
        }
    }

    public bool IsRunning(string configId)
    {
        lock (_lock)
        {
            return _sessions.TryGetValue(configId, out var session) && session.IsRunning;
        }
    }

    /// <summary>
    /// 向运行中会话的指定算法下发 UI 操作（如「确认解除错误」），由 <see cref="LuxVideoDet.Core.Algorithm.IAlgorithmUiCommandHandler"/> 处理。
    /// </summary>
    public bool TryInvokeUiAction(string configId, string algorithmType, string actionId, out string? errorMessage)
    {
        errorMessage = null;
        lock (_lock)
        {
            if (!_sessions.TryGetValue(configId, out var session) || !session.IsRunning)
            {
                errorMessage = "会话未运行或不存在";
                return false;
            }

            return session.Pipeline.TryInvokeUiAction(algorithmType, actionId, out errorMessage);
        }
    }

    /// <summary>
    /// 更新所有运行中算法的渲染选项。
    /// </summary>
    public void UpdateRenderOptions(bool showBoxes, bool showRegions, bool showLabels)
    {
        lock (_lock)
        {
            foreach (var session in _sessions.Values)
            {
                foreach (var algorithm in session.Algorithms)
                {
                    algorithm.RenderOptions.ShowDetectionBoxes = showBoxes;
                    algorithm.RenderOptions.ShowRegions = showRegions;
                    algorithm.RenderOptions.ShowLabels = showLabels;
                }
            }
        }
    }
}
