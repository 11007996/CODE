using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Events;

namespace LuxVideoDet.Core.Logging;

/// <summary>
/// 将 Serilog 事件桥接到桌面 UI 日志系统。
/// 这样直接使用 Serilog.Log 的日志也能在界面面板中实时显示。
/// </summary>
public sealed class SerilogUiSink : Serilog.Core.ILogEventSink
{
    private readonly Logging.ILogEventSink _sink;
    private readonly LogLevel _minLevel;

    public SerilogUiSink(Logging.ILogEventSink sink, LogLevel minLevel = LogLevel.Debug)
    {
        _sink = sink;
        _minLevel = minLevel;
    }

    public void Emit(LogEvent logEvent)
    {
        var level = MapLevel(logEvent.Level);
        if (level < _minLevel)
        {
            return;
        }

        var logEntry = new Logging.LogEntry
        {
            Timestamp = logEvent.Timestamp.LocalDateTime,
            Level = level,
            Category = logEvent.Properties.TryGetValue("SourceContext", out var sourceContext)
                ? TrimQuotes(sourceContext.ToString())
                : "Serilog",
            Message = logEvent.RenderMessage(),
            Exception = logEvent.Exception,
            ThreadId = TryGetThreadId(logEvent)
        };

        foreach (var property in logEvent.Properties)
        {
            logEntry.Properties[property.Key] = TrimQuotes(property.Value.ToString());
        }

        _sink.Emit(logEntry);
    }

    private static LogLevel MapLevel(LogEventLevel level) => level switch
    {
        LogEventLevel.Verbose => LogLevel.Trace,
        LogEventLevel.Debug => LogLevel.Debug,
        LogEventLevel.Information => LogLevel.Information,
        LogEventLevel.Warning => LogLevel.Warning,
        LogEventLevel.Error => LogLevel.Error,
        LogEventLevel.Fatal => LogLevel.Critical,
        _ => LogLevel.Information
    };

    private static int TryGetThreadId(LogEvent logEvent)
    {
        if (logEvent.Properties.TryGetValue("ThreadId", out var threadId)
            && int.TryParse(TrimQuotes(threadId.ToString()), out var parsed))
        {
            return parsed;
        }

        return Environment.CurrentManagedThreadId;
    }

    private static string TrimQuotes(string value)
        => value.Length >= 2 && value[0] == '"' && value[^1] == '"'
            ? value[1..^1]
            : value;
}
