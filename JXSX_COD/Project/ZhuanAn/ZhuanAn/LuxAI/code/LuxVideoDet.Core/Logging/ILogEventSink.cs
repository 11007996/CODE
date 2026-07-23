namespace LuxVideoDet.Core.Logging;

/// <summary>
/// 日志事件接收器 - 用于将日志推送到 UI 或其他订阅者
/// </summary>
public interface ILogEventSink
{
    /// <summary>
    /// 日志事件
    /// </summary>
    event EventHandler<LogEntry>? LogReceived;

    /// <summary>
    /// 发送日志条目
    /// </summary>
    void Emit(LogEntry logEntry);
}
