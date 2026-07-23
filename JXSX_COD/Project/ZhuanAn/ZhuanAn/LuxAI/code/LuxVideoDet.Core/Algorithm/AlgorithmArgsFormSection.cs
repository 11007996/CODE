using LuxVideoDet.Core.Notification;

namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 算法在配置中声明的一段「扩展参数」表单，映射到 <see cref="Configuration.Models.AlgorithmConfig.Args"/>。
/// 可与 AOI 无关（纯数值门限等）；若仅声明了 <see cref="AoiDetectorTypeKey"/> 而未给 <see cref="SectionTitle"/>，
/// Desktop/Web 可用 AOI 注册表解析默认标题（与历史 U 型等区块兼容）。
/// </summary>
public sealed class AlgorithmArgsFormSection
{
    /// <summary>卡片标题；为空且 <see cref="AoiDetectorTypeKey"/> 有值时由 <see cref="Aoi.AoiDetectorFactory.GetDisplayName"/> 推断。</summary>
    public string? SectionTitle { get; init; }

    /// <summary>
    /// 可选。与 <see cref="Aoi.AoiDetectorFactory"/> 注册键一致时，在未指定 <see cref="SectionTitle"/> 时用作默认标题来源。
    /// 非 AOI 类算法参数可留空，此时应提供 <see cref="SectionTitle"/>。
    /// </summary>
    public string? AoiDetectorTypeKey { get; init; }

    /// <summary>卡片说明（可选）。</summary>
    public string? Description { get; init; }

    /// <summary>
    /// 写入 <c>args</c> 的键名与类型；复用 <see cref="NotificationParameterDefinition"/>（与通知表单一致）。
    /// </summary>
    public IReadOnlyList<NotificationParameterDefinition> ArgFields { get; init; } =
        Array.Empty<NotificationParameterDefinition>();
}
