using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using LuxVideoDet.Core.Logging;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Desktop.Services;

/// <summary>
/// 日志服务 - 管理日志显示和过滤
/// </summary>
public class LogService
{
    private readonly ILogEventSink _logSink;
    private readonly ObservableCollection<LogEntry> _logs = new();
    private readonly object _lock = new();
    private const int MaxLogEntries = 1000;

    public event EventHandler<LogEntry>? LogReceived;

    public LogService(ILogEventSink logSink)
    {
        _logSink = logSink;
        _logSink.LogReceived += OnLogReceived;
    }

    /// <summary>
    /// 所有日志
    /// </summary>
    public ObservableCollection<LogEntry> Logs => _logs;

    /// <summary>
    /// 按级别过滤
    /// </summary>
    public ObservableCollection<LogEntry> FilterByLevel(LogLevel minLevel)
    {
        lock (_lock)
        {
            var filtered = new ObservableCollection<LogEntry>(
                _logs.Where(log => log.Level >= minLevel));
            return filtered;
        }
    }

    /// <summary>
    /// 搜索日志
    /// </summary>
    public ObservableCollection<LogEntry> Search(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return new ObservableCollection<LogEntry>(_logs);

        lock (_lock)
        {
            var filtered = new ObservableCollection<LogEntry>(
                _logs.Where(log =>
                    log.Message.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    log.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
            return filtered;
        }
    }

    /// <summary>
    /// 清空日志
    /// </summary>
    public void Clear()
    {
        lock (_lock)
        {
            Dispatcher.UIThread.Post(() => _logs.Clear());
        }
    }

    /// <summary>
    /// 导出日志
    /// </summary>
    public async Task ExportAsync(string filePath)
    {
        lock (_lock)
        {
            var lines = _logs.Select(log =>
                $"{log.Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{log.Level}] {log.Category}: {log.Message}");
            
            System.IO.File.WriteAllLines(filePath, lines);
        }

        await Task.CompletedTask;
    }

    private void OnLogReceived(object? sender, LogEntry logEntry)
    {
        lock (_lock)
        {
            Dispatcher.UIThread.Post(() =>
            {
                _logs.Add(logEntry);

                // 限制日志数量
                while (_logs.Count > MaxLogEntries)
                {
                    _logs.RemoveAt(0);
                }
            });
        }

        LogReceived?.Invoke(this, logEntry);
    }
}
