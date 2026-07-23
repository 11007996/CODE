using System.Linq;
using LuxVideoDet.Core.Inference.Results;

namespace LuxVideoDet.Core.Utils;

/// <summary>
/// Sliding 60s window: arithmetic mean of each <see cref="InferenceTimingBreakdown"/> field (ms).
/// </summary>
public sealed class RollingMinuteInferenceStepStats
{
    private readonly Queue<(DateTime Utc, float Pre, float In, float Run, float Out, float Dec)> _samples = new();
    private const double WindowSeconds = 60;

    public void RecordFrame(InferenceTimingBreakdown t)
    {
        var now = DateTime.UtcNow;
        _samples.Enqueue((now, t.PreprocessMs, t.InputTensorMs, t.NativeRunMs, t.OutputToCpuMs, t.EnginePostprocessMs));
        Prune(now);
    }

    private void Prune(DateTime now)
    {
        while (_samples.Count > 0 && (now - _samples.Peek().Utc).TotalSeconds > WindowSeconds)
            _samples.Dequeue();
    }

    /// <summary>
    /// Null when the window has no samples (e.g. engine never supplied timing).
    /// </summary>
    public InferenceStepRollingAverages? GetAverages()
    {
        var now = DateTime.UtcNow;
        Prune(now);
        if (_samples.Count == 0)
            return null;

        var n = _samples.Count;
        return new InferenceStepRollingAverages(
            AvgPreprocessMs: _samples.Average(x => x.Pre),
            AvgInputTensorMs: _samples.Average(x => x.In),
            AvgNativeRunMs: _samples.Average(x => x.Run),
            AvgOutputToCpuMs: _samples.Average(x => x.Out),
            AvgEnginePostprocessMs: _samples.Average(x => x.Dec),
            AvgBreakdownSumMs: _samples.Average(x => x.Pre + x.In + x.Run + x.Out + x.Dec),
            SampleCount: n);
    }

    public void Reset() => _samples.Clear();
}

public readonly record struct InferenceStepRollingAverages(
    double AvgPreprocessMs,
    double AvgInputTensorMs,
    double AvgNativeRunMs,
    double AvgOutputToCpuMs,
    double AvgEnginePostprocessMs,
    double AvgBreakdownSumMs,
    int SampleCount);
