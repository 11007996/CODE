namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 生产统计提供者接口。
/// 算法可选择实现此接口以向 UI 暴露 OK/NG 计数。
/// 未实现此接口的算法不会在界面显示生产统计面板。
/// </summary>
public interface IProductionStatisticsProvider
{
    /// <summary>合格品数量</summary>
    int OkCount { get; }

    /// <summary>不良品数量</summary>
    int NgCount { get; }

    /// <summary>总检测数量（OK + NG）</summary>
    int TotalCount => OkCount + NgCount;

    /// <summary>
    /// 重置统计计数。
    /// </summary>
    void ResetStatistics();
}
