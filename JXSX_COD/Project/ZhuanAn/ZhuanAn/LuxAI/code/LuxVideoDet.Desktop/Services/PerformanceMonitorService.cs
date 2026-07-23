using System;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading;
using LuxVideoDet.Desktop.Models;

namespace LuxVideoDet.Desktop.Services;

/// <summary>
/// 性能监控服务
/// </summary>
public class PerformanceMonitorService : IDisposable
{
    private readonly Process _currentProcess;
    private readonly PerformanceCounter? _cpuCounter;
    private readonly Timer _updateTimer;
    private DateTime _lastCpuCheck = DateTime.MinValue;
    private TimeSpan _lastTotalProcessorTime = TimeSpan.Zero;
    private PerformanceData _currentData = new();

    public event EventHandler<PerformanceData>? PerformanceUpdated;

    public PerformanceMonitorService()
    {
        _currentProcess = Process.GetCurrentProcess();

        // Windows：使用 WMI 性能计数器；其他平台为 null，走进程 CPU 估算
        _cpuCounter = OperatingSystem.IsWindows() ? TryCreateWindowsCpuCounter() : null;

        // 每秒更新一次
        _updateTimer = new Timer(UpdatePerformance, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
    }

    public PerformanceData GetCurrentData() => _currentData;

    private void UpdatePerformance(object? state)
    {
        try
        {
            var data = new PerformanceData();

            // CPU 使用率
            data.CpuUsage = GetCpuUsage();

            // 内存使用
            _currentProcess.Refresh();
            data.MemoryUsageBytes = _currentProcess.WorkingSet64;
            
            // 计算内存使用百分比（假设总内存 16GB）
            var totalMemory = 16L * 1024 * 1024 * 1024;
            data.MemoryUsagePercent = (double)data.MemoryUsageBytes / totalMemory * 100;

            // GPU 使用率（暂时不支持）
            data.HasGpu = false;
            data.GpuUsage = 0;

            _currentData = data;
            PerformanceUpdated?.Invoke(this, data);
        }
        catch
        {
            // 忽略错误
        }
    }

    private double GetCpuUsage()
    {
        try
        {
            if (_cpuCounter != null && OperatingSystem.IsWindows())
            {
                return ReadWindowsCpuCounter(_cpuCounter);
            }

            // 备用方法：计算进程 CPU 使用率
            var currentTime = DateTime.Now;
            var currentTotalProcessorTime = _currentProcess.TotalProcessorTime;

            if (_lastCpuCheck != DateTime.MinValue)
            {
                var timeDiff = (currentTime - _lastCpuCheck).TotalMilliseconds;
                var cpuDiff = (currentTotalProcessorTime - _lastTotalProcessorTime).TotalMilliseconds;

                if (timeDiff > 0)
                {
                    var cpuUsage = (cpuDiff / (Environment.ProcessorCount * timeDiff)) * 100;
                    _lastCpuCheck = currentTime;
                    _lastTotalProcessorTime = currentTotalProcessorTime;
                    return Math.Min(cpuUsage, 100);
                }
            }

            _lastCpuCheck = currentTime;
            _lastTotalProcessorTime = currentTotalProcessorTime;
            return 0;
        }
        catch
        {
            return 0;
        }
    }

    public void Dispose()
    {
        _updateTimer?.Dispose();
        _cpuCounter?.Dispose();
    }

    [SupportedOSPlatform("windows")]
    private static PerformanceCounter? TryCreateWindowsCpuCounter()
    {
        try
        {
            return new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }
        catch
        {
            return null;
        }
    }

    [SupportedOSPlatform("windows")]
    private static double ReadWindowsCpuCounter(PerformanceCounter counter) => counter.NextValue();
}
