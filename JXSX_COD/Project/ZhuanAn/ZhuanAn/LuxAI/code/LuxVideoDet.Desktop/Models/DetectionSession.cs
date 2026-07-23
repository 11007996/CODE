using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Algorithm.Pipeline;
using LuxVideoDet.Core.Algorithm.Results;
using LuxVideoDet.Core.Configuration.Models;

namespace LuxVideoDet.Desktop.Models;

/// <summary>
/// 检测会话 — 一个配置对应一个会话，持有流水线及其运行状态。
/// </summary>
public class DetectionSession
{
    public string ConfigId { get; set; } = string.Empty;
    public string ConfigName { get; set; } = string.Empty;

    /// <summary>多算法流水线（拥有视频源和所有算法工作者）</summary>
    public MultiAlgorithmPipeline Pipeline { get; set; } = null!;

    public DetectionConfiguration Configuration { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public bool IsRunning { get; set; }
    /// <summary>约 60 秒滑动窗口：各算法最近结果中「每帧平均管线耗时」的均值（毫秒）。</summary>
    public double AveragePipelineMs { get; set; }

    public int DetectionCount { get; set; }

    /// <summary>各算法最新结果（线程安全）</summary>
    public ConcurrentDictionary<string, DetectionResult> LastResults { get; set; } = new();

    /// <summary>
    /// 通过 Pipeline 获取所有算法实例的便捷属性。
    /// </summary>
    public IEnumerable<IDetectionAlgorithm> Algorithms =>
        Pipeline?.Workers.Select(w => w.Algorithm) ?? Enumerable.Empty<IDetectionAlgorithm>();
}
