using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Storage;

/// <summary>
/// 按天按配置持久化 OK/NG 统计数据到 JSON 文件。
/// 线程安全；每次写入都立即持久化到磁盘。
/// </summary>
public class ProductionStatisticsStore
{
    private readonly string _filePath;
    private readonly ILogger<ProductionStatisticsStore> _logger;
    private readonly object _lock = new();

    private Dictionary<string, Dictionary<string, DailyStats>> _data = new();

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        // 键名/字符串中的中文直接写入 UTF-8，避免 \uXXXX 转义（便于人工查看与 diff）
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    public ProductionStatisticsStore(ILogger<ProductionStatisticsStore> logger, string? statsDir = null)
    {
        _logger = logger;
        statsDir ??= Path.Combine(AppContext.BaseDirectory, "stats");
        Directory.CreateDirectory(statsDir);
        _filePath = Path.Combine(statsDir, "production_stats.json");
        Load();
    }

    public void IncrementOk(string configDisplayName)
    {
        lock (_lock)
        {
            var stats = GetOrCreateToday(configDisplayName);
            stats.Ok++;
            Save();
        }
    }

    public void IncrementNg(string configDisplayName)
    {
        lock (_lock)
        {
            var stats = GetOrCreateToday(configDisplayName);
            stats.Ng++;
            Save();
        }
    }

    /// <summary>
    /// 获取今天某个配置的统计。
    /// </summary>
    public (int Ok, int Ng) GetTodayStats(string configDisplayName)
    {
        lock (_lock)
        {
            var dateKey = DateTime.Now.ToString("yyyy-MM-dd");
            if (_data.TryGetValue(dateKey, out var dayData) &&
                dayData.TryGetValue(configDisplayName, out var stats))
            {
                return (stats.Ok, stats.Ng);
            }
            return (0, 0);
        }
    }

    /// <summary>
    /// 获取今天所有配置的统计汇总。
    /// </summary>
    public (int Ok, int Ng) GetTodayTotalStats()
    {
        lock (_lock)
        {
            var dateKey = DateTime.Now.ToString("yyyy-MM-dd");
            if (!_data.TryGetValue(dateKey, out var dayData))
                return (0, 0);

            var ok = 0;
            var ng = 0;
            foreach (var stats in dayData.Values)
            {
                ok += stats.Ok;
                ng += stats.Ng;
            }
            return (ok, ng);
        }
    }

    /// <summary>
    /// 获取今天指定配置集合的统计汇总。
    /// </summary>
    public (int Ok, int Ng) GetTodayStatsForConfigs(IEnumerable<string> configDisplayNames)
    {
        lock (_lock)
        {
            var dateKey = DateTime.Now.ToString("yyyy-MM-dd");
            if (!_data.TryGetValue(dateKey, out var dayData))
                return (0, 0);

            var ok = 0;
            var ng = 0;
            foreach (var name in configDisplayNames)
            {
                if (dayData.TryGetValue(name, out var stats))
                {
                    ok += stats.Ok;
                    ng += stats.Ng;
                }
            }
            return (ok, ng);
        }
    }

    /// <summary>
    /// 检查今天是否有任何统计数据。
    /// </summary>
    public bool HasTodayStats()
    {
        lock (_lock)
        {
            var dateKey = DateTime.Now.ToString("yyyy-MM-dd");
            return _data.TryGetValue(dateKey, out var dayData) && dayData.Count > 0;
        }
    }

    /// <summary>
    /// 历史文件中是否已有任意 OK/NG 记录（任意日期、任意配置）。
    /// </summary>
    public bool HasAnyRecordedStats()
    {
        lock (_lock)
        {
            foreach (var day in _data.Values)
            {
                foreach (var stats in day.Values)
                {
                    if (stats.Ok > 0 || stats.Ng > 0)
                        return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// 所有日期、所有配置的 OK/NG 汇总。
    /// </summary>
    public (int Ok, int Ng, int PCount) GetAllTimeTotalStats()
    {
        lock (_lock)
        {
            var ok = 0;
            var ng = 0;
            var pCount = 0;
            foreach (var day in _data.Values)
            {
                foreach (var stats in day.Values)
                {
                    ok += stats.Ok;
                    ng += stats.Ng;
                    pCount += stats.PCount;
                }
            }

            return (ok, ng, pCount);
        }
    }

    /// <summary>
    /// 某个配置在所有历史日期上的 OK/NG 汇总。
    /// </summary>
    public (int Ok, int Ng) GetAllTimeStatsForConfig(string configDisplayName)
    {
        lock (_lock)
        {
            var ok = 0;
            var ng = 0;
            foreach (var day in _data.Values)
            {
                if (day.TryGetValue(configDisplayName, out var stats))
                {
                    ok += stats.Ok;
                    ng += stats.Ng;
                }
            }

            return (ok, ng);
        }
    }

    private DailyStats GetOrCreateToday(string configDisplayName)
    {
        var dateKey = DateTime.Now.ToString("yyyy-MM-dd");
        if (!_data.TryGetValue(dateKey, out var dayData))
        {
            dayData = new Dictionary<string, DailyStats>();
            _data[dateKey] = dayData;
        }
        if (!dayData.TryGetValue(configDisplayName, out var stats))
        {
            stats = new DailyStats();
            dayData[configDisplayName] = stats;
        }
        return stats;
    }

    private void Load()
    {
        try
        {
            if (!File.Exists(_filePath))
                return;

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return;

            _data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, DailyStats>>>(json, JsonOptions)
                    ?? new();

            _logger.LogInformation("已加载生产统计: {DateCount} 天数据, 文件={FilePath}", _data.Count, _filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载生产统计失败: {FilePath}", _filePath);
            _data = new();
        }
    }

    private void Save()
    {
        try
        {
            var json = JsonSerializer.Serialize(_data, JsonOptions);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存生产统计失败: {FilePath}", _filePath);
        }
    }

    public class DailyStats
    {
        public int Ok { get; set; }
        public int Ng { get; set; }
        public int PCount { get; set; }
    }
}
