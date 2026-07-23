using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.VideoSource;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Algorithm.Pipeline;

/// <summary>
/// 流水线工厂 — 从 DetectionConfiguration 一步创建可运行的 MultiAlgorithmPipeline。
/// <para>
/// 将"创建视频源 → 创建算法实例 → 组装流水线"的编排逻辑内聚在 Core 层，
/// 使得 Desktop / Headless / Web 等前端只需调用工厂即可，无需重复编排。
/// </para>
/// </summary>
public class PipelineFactory
{
    private readonly DetectionAlgorithmFactory _algorithmFactory;
    private readonly VideoSourceFactory _videoSourceFactory;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<PipelineFactory> _logger;

    public PipelineFactory(
        DetectionAlgorithmFactory algorithmFactory,
        VideoSourceFactory videoSourceFactory,
        ILoggerFactory loggerFactory)
    {
        _algorithmFactory = algorithmFactory;
        _videoSourceFactory = videoSourceFactory;
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.CreateLogger<PipelineFactory>();
    }

    /// <summary>
    /// 从检测配置创建完整的多算法流水线。
    /// </summary>
    /// <returns>已就绪但尚未启动的流水线（调用方需 <see cref="MultiAlgorithmPipeline.Start"/>）</returns>
    /// <exception cref="InvalidOperationException">视频源打开失败或没有可用算法时抛出</exception>
    public async Task<MultiAlgorithmPipeline> CreatePipelineAsync(
        DetectionConfiguration config,
        int queueCapacity = 3,
        int maxConsecutiveReadFailures = 100)
    {
        var enabledAlgorithms = config.Algorithms.Where(a => a.Enabled).ToList();

        _logger.LogInformation(
            "[流水线·配置] 会话「{ConfigName}」Id={ConfigId} | 视频源={SourceType} | 路径={Source} | Loop={Loop} | 算法 已启用 {Enabled}/{Total}",
            config.Name, config.Id,
            config.VideoSource.Type, config.VideoSource.Source, config.VideoSource.Loop,
            enabledAlgorithms.Count, config.Algorithms.Count);

        foreach (var a in enabledAlgorithms)
        {
            var mp = string.IsNullOrWhiteSpace(a.Inference.ModelPath)
                ? "(未配置)"
                : Path.GetFullPath(a.Inference.ModelPath);
            _logger.LogInformation("[流水线·模型] [{AlgorithmType}] {ModelPath}", a.AlgorithmType, mp);
        }

        // 1. 创建并打开视频源
        _logger.LogInformation("[流水线·视频源] 正在打开…");
        var videoSource = _videoSourceFactory.CreateVideoSource(config.VideoSource);
        videoSource.Open();

        if (!videoSource.IsOpened)
        {
            videoSource.Dispose();
            throw new InvalidOperationException(
                $"视频源打开后仍未就绪: {config.VideoSource.Source}");
        }

        _logger.LogInformation(
            "[流水线·视频源] 已就绪 {SourceType} {Width}x{Height} @ {Fps:F1} fps",
            videoSource.SourceType,
            videoSource.GetWidth(), videoSource.GetHeight(),
            videoSource.GetFps());

        // 2. 创建所有算法实例
        var algorithmPairs = new List<(IDetectionAlgorithm, AlgorithmConfig)>();

        foreach (var algoConfig in enabledAlgorithms)
        {
            algoConfig.ParentConfigName = config.Name;

            try
            {
                var algorithm = await _algorithmFactory.CreateAlgorithmAsync(algoConfig);
                algorithmPairs.Add((algorithm, algoConfig));
                _logger.LogInformation(
                    "[流水线·算法] [{AlgorithmType}] 已就绪 | 推理运行时={Runtime}",
                    algoConfig.AlgorithmType,
                    $"{algorithm.GetEngineType()} + {algorithm.GetDeviceType()}");
            }
            catch (MissingAlgorithmRegionsException)
            {
                videoSource.Dispose();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[流水线·算法] 创建失败: [{AlgorithmType}]", algoConfig.AlgorithmType);
            }
        }

        if (algorithmPairs.Count == 0)
        {
            videoSource.Dispose();
            throw new InvalidOperationException("没有可用的算法");
        }

        // 3. 组装流水线
        var pipelineLogger = _loggerFactory.CreateLogger<MultiAlgorithmPipeline>();
        var pipeline = new MultiAlgorithmPipeline(
            videoSource, algorithmPairs, pipelineLogger,
            queueCapacity, maxConsecutiveReadFailures);

        _logger.LogInformation(
            "[流水线] 组装完成：{WorkerCount} 路并行工作者，会话「{ConfigName}」可启动",
            algorithmPairs.Count, config.Name);

        return pipeline;
    }

}
