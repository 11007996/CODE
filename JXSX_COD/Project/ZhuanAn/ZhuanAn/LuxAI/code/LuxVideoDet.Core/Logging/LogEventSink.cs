namespace LuxVideoDet.Core.Logging;

/// <summary>
/// 日志事件接收器实现 - 线程安全的事件推送
/// </summary>
public class LogEventSink : ILogEventSink
{
    private readonly object _lock = new();
    private EventHandler<LogEntry>? _logReceived;

    public event EventHandler<LogEntry>? LogReceived
    {
        add
        {
            lock (_lock)
            {
                _logReceived += value;
            }
        }
        remove
        {
            lock (_lock)
            {
                _logReceived -= value;
            }
        }
    }

    public void Emit(LogEntry logEntry)
    {
        EventHandler<LogEntry>? handler;

        lock (_lock)
        {
            handler = _logReceived;
        }

        // 在锁外调用事件处理器，避免死锁
        handler?.Invoke(this, logEntry);
    }
}
