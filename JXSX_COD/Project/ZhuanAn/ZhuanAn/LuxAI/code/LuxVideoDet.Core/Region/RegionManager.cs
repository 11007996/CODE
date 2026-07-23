using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Inference.Results;
using LuxVideoDet.Core.Utils;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Region;

public class RegionManager
{
    private readonly ILogger<RegionManager> _logger;
    private readonly List<Region> _regions = [];

    public RegionManager(ILogger<RegionManager> logger)
    {
        _logger = logger;
    }

    public void AddRegion(Region region)
    {
        _regions.Add(region);
        _logger.LogDebug("添加区域: {Name}, 点数: {Count}", region.Name, region.Points.Count);
    }

    public Region? FindRegionByName(string name)
    {
        var region = _regions.FirstOrDefault(r => 
            r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        
        if (region == null)
        {
            _logger.LogDebug("未找到区域: {Name}", name);
        }
        
        return region;
    }

    public int CountBoxesInRegion(Detection[] results, string regionName, int classId)
    {
        var region = FindRegionByName(regionName);
        if (region == null) return 0;

        return results.Count(r => 
            r.ClassId == classId && 
            region.ContainsBox([r.BoundingBox.X, r.BoundingBox.Y, 
                               r.BoundingBox.X + r.BoundingBox.Width, 
                               r.BoundingBox.Y + r.BoundingBox.Height]));
    }

    public int CountInRegion(List<Detection> results, string regionName, int classId)
    {
        var region = FindRegionByName(regionName);
        if (region == null) return 0;

        return results.Count(r => 
            r.ClassId == classId && 
            region.ContainsPoint(r.BoundingBox.CenterX, r.BoundingBox.CenterY));
    }

    /// <summary>
    /// 按类别名称统计区域内的检测结果数量（不依赖硬编码索引）。
    /// </summary>
    public int CountInRegion(List<Detection> results, string regionName, string className)
    {
        var region = FindRegionByName(regionName);
        if (region == null) return 0;

        return results.Count(r =>
            r.ClassName.Equals(className, StringComparison.OrdinalIgnoreCase) &&
            region.ContainsPoint(r.BoundingBox.CenterX, r.BoundingBox.CenterY));
    }

    public List<Detection> FilterByRegion(Detection[] results, string regionName)
    {
        var region = FindRegionByName(regionName);
        if (region == null) return [];

        return results.Where(r => 
            region.ContainsBox([r.BoundingBox.X, r.BoundingBox.Y, 
                               r.BoundingBox.X + r.BoundingBox.Width, 
                               r.BoundingBox.Y + r.BoundingBox.Height])).ToList();
    }

    public void Clear()
    {
        _logger.LogDebug("清空所有区域 (数量: {Count})", _regions.Count);
        _regions.Clear();
    }

    public IReadOnlyList<Region> GetAllRegions()
    {
        return _regions.AsReadOnly();
    }

    public void LoadFromConfig(Dictionary<string, List<List<int>>>? regionsConfig)
    {
        if (regionsConfig == null)
        {
            _logger.LogWarning("[配置·区域] 未提供区域配置");
            return;
        }

        foreach (var (name, points) in regionsConfig)
        {
            var color = GetDefaultColor(name);
            var region = Region.FromPoints(points, name, color);
            AddRegion(region);
        }

        _logger.LogInformation("[配置·区域] 已从配置载入 {Count} 个区域", _regions.Count);
    }

    /// <param name="algorithmTypeForColorResolution">
    /// 配置文件中的算法类型（如 <c>midframescan</c>）。用于在区域 <see cref="Configuration.Models.RegionConfig.Color"/> 未指定时，从 Descriptor 取 <see cref="RegionDefinition.DefaultColor"/>。
    /// </param>
    public void LoadFromConfig(
        List<Configuration.Models.RegionConfig>? regionsConfig,
        string? algorithmTypeForColorResolution = null)
    {
        if (regionsConfig == null || regionsConfig.Count == 0)
        {
            _logger.LogWarning("[配置·区域] 未提供区域配置");
            return;
        }

        foreach (var regionConfig in regionsConfig)
        {
            var points = regionConfig.Points.Select(p => new List<int> { p.X, p.Y }).ToList();
            var color = ResolveRegionColor(regionConfig.Name, regionConfig.Color, algorithmTypeForColorResolution);
            var region = Region.FromPoints(points, regionConfig.Name, color);
            AddRegion(region);
        }

        _logger.LogInformation("[配置·区域] 已从配置载入 {Count} 个区域", _regions.Count);
    }

    /// <summary>
    /// 区域颜色优先级：config 显式 #RRGGBB → Descriptor 同名区域 → 内置名映射 → 纯绿。
    /// </summary>
    private static Scalar ResolveRegionColor(
        string regionName,
        string? colorFromConfig,
        string? algorithmType)
    {
        if (VisualizationColors.TryParseRgbHexToBgrScalar(colorFromConfig, out var explicitColor))
            return explicitColor;

        if (!string.IsNullOrWhiteSpace(algorithmType))
        {
            var defs = DetectionAlgorithmFactory.GetRequiredRegions(algorithmType);
            var def = defs.FirstOrDefault(r =>
                r.Name.Equals(regionName, StringComparison.OrdinalIgnoreCase));
            if (def != null)
                return def.DefaultColor;
        }

        return GetDefaultColor(regionName);
    }

    private static Scalar GetDefaultColor(string regionName)
    {
        return regionName.ToLower() switch
        {
            "pickuparea" => new Scalar(0, 165, 255),
            "finisharea" => new Scalar(144, 238, 144),
            "dumparea" => new Scalar(128, 128, 128),
            _ => new Scalar(0, 255, 0)
        };
    }
}
