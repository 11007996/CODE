using System.Linq;

namespace LuxVideoDet.Core.Utils;

/// <summary>
/// 滑动 60 秒窗口内若干次耗时样本的算术平均（毫秒），用于 AOI 等分项调试。
/// </summary>
public sealed class RollingMinuteMillisecondsStats
{
    private readonly Queue<(DateTime Utc, double Ms)> _samples = new();
    private const double WindowSeconds = 60;

    /// <summary>记录一次耗时（毫秒）。</summary>
    public void Record(double milliseconds)
    {
        if (milliseconds < 0 || double.IsNaN(milliseconds) || double.IsInfinity(milliseconds))
            return;

        var now = DateTime.UtcNow;
        _samples.Enqueue((now, milliseconds));
        Prune(now);
    }

    private void Prune(DateTime now)
    {
        while (_samples.Count > 0 && (now - _samples.Peek().Utc).TotalSeconds > WindowSeconds)
            _samples.Dequeue();
    }

    /// <summary>当前窗口内平均耗时（毫秒）与样本数；无样本时平均为 0。</summary>
    public (double AvgMs, int SampleCount) GetAverageAndCount()
    {
        var now = DateTime.UtcNow;
        Prune(now);
        if (_samples.Count == 0)
            return (0, 0);

        return (_samples.Average(x => x.Ms), _samples.Count);
    }

    public void Reset() => _samples.Clear();
}
