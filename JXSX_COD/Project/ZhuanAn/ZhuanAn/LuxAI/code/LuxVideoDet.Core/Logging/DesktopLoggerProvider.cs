using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Logging;

/// <summary>
/// Desktop 日志提供器 - 将日志推送到 UI
/// </summary>
[ProviderAlias("Desktop")]
public class DesktopLoggerProvider : ILoggerProvider
{
    private readonly ILogEventSink _sink;
    private readonly LogLevel _minLevel;

    public DesktopLoggerProvider(ILogEventSink sink, LogLevel minLevel = LogLevel.Information)
    {
        _sink = sink;
        _minLevel = minLevel;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DesktopLogger(categoryName, _sink, _minLevel);
    }

    public void Dispose()
    {
        // 清理资源
    }
}

/// <summary>
/// Desktop 日志记录器
/// </summary>
internal class DesktopLogger : ILogger
{
    private readonly string _categoryName;
    private readonly ILogEventSink _sink;
    private readonly LogLevel _minLevel;

    public DesktopLogger(string categoryName, ILogEventSink sink, LogLevel minLevel)
    {
        _categoryName = categoryName;
        _sink = sink;
        _minLevel = minLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null; // 简化实现，可以后续扩展
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _minLevel;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var logEntry = new LogEntry
        {
            Timestamp = DateTime.Now,
            Level = logLevel,
            Category = _categoryName,
            Message = formatter(state, exception),
            Exception = exception,
            EventId = eventId,
            ThreadId = Environment.CurrentManagedThreadId
        };

        // 提取结构化属性
        if (state is IEnumerable<KeyValuePair<string, object?>> properties)
        {
            foreach (var property in properties)
            {
                if (property.Key != "{OriginalFormat}")
                {
                    logEntry.Properties[property.Key] = property.Value;
                }
            }
        }

        _sink.Emit(logEntry);
    }
}
