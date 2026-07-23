using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Logging;

/// <summary>
/// 日志条目 - 用于 UI 显示和事件推送
/// </summary>
public class LogEntry
{
    /// <summary>时间戳</summary>
    public DateTime Timestamp { get; set; }

    /// <summary>日志级别</summary>
    public LogLevel Level { get; set; }

    /// <summary>类别（通常是类名）</summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>日志消息</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>异常信息</summary>
    public Exception? Exception { get; set; }

    /// <summary>事件 ID</summary>
    public EventId EventId { get; set; }

    /// <summary>作用域信息（如 MachineName）</summary>
    public Dictionary<string, object?> Scopes { get; set; } = new();

    /// <summary>结构化属性</summary>
    public Dictionary<string, object?> Properties { get; set; } = new();

    /// <summary>线程 ID</summary>
    public int ThreadId { get; set; }

    /// <summary>
    /// 获取日志级别的显示颜色
    /// </summary>
    public string GetLevelColor()
    {
        return Level switch
        {
            LogLevel.Trace => "#808080",
            LogLevel.Debug => "#00BFFF",
            LogLevel.Information => "#00FF00",
            LogLevel.Warning => "#FFA500",
            LogLevel.Error => "#FF0000",
            LogLevel.Critical => "#8B0000",
            _ => "#FFFFFF"
        };
    }

    /// <summary>
    /// 获取日志级别的简短名称
    /// </summary>
    public string GetLevelShortName()
    {
        return Level switch
        {
            LogLevel.Trace => "TRC",
            LogLevel.Debug => "DBG",
            LogLevel.Information => "INF",
            LogLevel.Warning => "WRN",
            LogLevel.Error => "ERR",
            LogLevel.Critical => "CRT",
            _ => "???"
        };
    }
}
