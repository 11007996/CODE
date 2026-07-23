using System.Linq;

namespace LuxVideoDet.Core.Utils;

/// <summary>
/// 基于滑动 60 秒窗口的帧统计：平均 FPS（吞吐）与平均每帧管线耗时（毫秒）。
/// </summary>
public sealed class RollingMinutePipelineStats
{
    private readonly Queue<(DateTime Utc, double PipelineMs)> _samples = new();
    private const double WindowSeconds = 60;

    /// <summary>记录一帧的整段管线耗时（毫秒）。</summary>
    public void RecordFrame(double pipelineMs)
    {
        var now = DateTime.UtcNow;
        _samples.Enqueue((now, pipelineMs));
        Prune(now);
    }

    private void Prune(DateTime now)
    {
        while (_samples.Count > 0 && (now - _samples.Peek().Utc).TotalSeconds > WindowSeconds)
            _samples.Dequeue();
    }

    /// <summary>
    /// 当前窗口内：平均帧耗时 = 各帧管线耗时的算术平均（毫秒）；
    /// 平均 FPS = 1000 / 平均耗时，与画面左上角「fps / inference ms」同一套语义，避免窗口时长过短时出现帧数/秒虚高。
    /// </summary>
    public (double AvgFps, double AvgPipelineMs) GetAverages()
    {
        var now = DateTime.UtcNow;
        Prune(now);
        if (_samples.Count == 0)
            return (0, 0);

        var avgPipelineMs = _samples.Average(x => x.PipelineMs);
        var avgFps = avgPipelineMs >= 0.001 ? 1000.0 / avgPipelineMs : 0;
        return (avgFps, avgPipelineMs);
    }

    public void Reset()
    {
        _samples.Clear();
    }
}
