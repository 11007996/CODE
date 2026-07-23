using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Configuration.Models;

namespace LuxVideoDet.Core.Configuration.Validation;

/// <summary>
/// 校验「已启用」算法的配置是否包含 Descriptor 声明的全部必选区域（可多不可少）。
/// </summary>
public static class AlgorithmRegionRequirements
{
    /// <summary>
    /// 返回缺失的必选区域说明列表；无缺失则返回空列表。
    /// 未启用的算法不做校验（允许草稿缺区域）。
    /// </summary>
    public static List<string> GetMissingRegionMessages(AlgorithmConfig config)
    {
        var list = new List<string>();
        if (!config.Enabled || string.IsNullOrWhiteSpace(config.AlgorithmType))
            return list;

        if (!DetectionAlgorithmFactory.IsSupported(config.AlgorithmType))
            return list;

        var required = DetectionAlgorithmFactory.GetRequiredRegions(config.AlgorithmType)
            .Where(d => d.Required)
            .ToList();
        if (required.Count == 0)
            return list;

        var configured = new HashSet<string>(
            (config.Regions ?? [])
                .Select(r => r.Name)
                .Where(n => !string.IsNullOrWhiteSpace(n)),
            StringComparer.OrdinalIgnoreCase);

        foreach (var def in required)
        {
            if (!configured.Contains(def.Name))
            {
                list.Add(
                    $"缺少必选区域「{def.Name}」（{def.DisplayName}）。");
            }
        }

        return list;
    }

    /// <summary>
    /// 存在缺失时抛出 <see cref="MissingAlgorithmRegionsException"/>。
    /// </summary>
    public static void EnsureRequiredRegionsPresent(AlgorithmConfig config)
    {
        var missing = GetMissingRegionMessages(config);
        if (missing.Count > 0)
            throw new MissingAlgorithmRegionsException(config.AlgorithmType, missing);
    }
}
