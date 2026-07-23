namespace LuxVideoDet.Desktop.Models;

/// <summary>
/// 系统性能快照数据。
/// </summary>
public class PerformanceData
{
    public double CpuUsage { get; set; }
    public long MemoryUsageBytes { get; set; }
    public double MemoryUsagePercent { get; set; }
    public double GpuUsage { get; set; }
    public bool HasGpu { get; set; }
}
