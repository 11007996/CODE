using LuxVideoDet.Core.Algorithm.Ui;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Notification;
using LuxVideoDet.Core.Region;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 算法描述符接口 — 每个算法模块提供一个实现，用于自动发现和注册。
/// <para>
/// 工厂在启动时通过反射扫描程序集中所有 <see cref="IAlgorithmDescriptor"/> 实现，
/// 自动完成算法注册，无需手动修改工厂代码。
/// </para>
/// <para>
/// <b>必须实现</b>：<see cref="TypeKey"/>、<see cref="DisplayName"/>、<see cref="DefaultClasses"/>、
/// <see cref="RequiredRegions"/>、<see cref="DefaultClassColors"/>、<see cref="Create"/>。
/// </para>
/// <para>
/// <b>可选（接口默认实现，一般不必写）</b>：<see cref="SupportedNotifierTypes"/>、
/// <see cref="GetDefaultNotifierParameters"/>、<see cref="GetUiActionDefinitions"/>、
/// <see cref="AoiParameterSections"/>（AOI/MediaPipe/U 型检测器等写入 <c>args</c> 的表单）、
/// <see cref="AlgorithmParameterSections"/>（纯算法逻辑门控，如缠胶圈数；无则均为空列表）。
/// </para>
/// </summary>
public interface IAlgorithmDescriptor
{
    /// <summary>配置文件中使用的算法类型标识（小写，如 "midframescan"）</summary>
    string TypeKey { get; }

    /// <summary>UI 下拉框中的显示名称（如 "中框外框检测"）</summary>
    string DisplayName { get; }

    /// <summary>模型默认类别列表</summary>
    List<string> DefaultClasses { get; }

    /// <summary>
    /// 算法需要的区域定义（含 <see cref="RegionDefinition.DefaultColor"/>，用于预览/编辑器中的区域轮廓默认色）。
    /// </summary>
    List<RegionDefinition> RequiredRegions { get; }

    /// <summary>
    /// 与 <see cref="DefaultClasses"/> 顺序一致的检测框默认颜色（BGR）。
    /// <para>
    /// 运行时由 <see cref="DetectionAlgorithmBase"/> 按语义顺序应用（默认 <see cref="DefaultClasses"/> 与引擎类名匹配）；
    /// 若某语义下标超出列表长度或本属性为 <c>null</c>，则对该类使用自动生成的调色板填充。
    /// </para>
    /// 调试时只需改 Descriptor 中本列表与区域颜色，无需在各算法中重复实现配色逻辑。
    /// </summary>
    IReadOnlyList<Scalar>? DefaultClassColors { get; }

    /// <summary>
    /// 创建算法实例。
    /// 额外依赖（如 AoiDetectorFactory）可通过 <paramref name="serviceProvider"/> 解析。
    /// </summary>
    IDetectionAlgorithm Create(
        IInferenceEngine inferenceEngine,
        RegionManager regionManager,
        List<INotificationService> notifiers,
        ILogger logger,
        IServiceProvider serviceProvider);

    /// <summary>
    /// 本算法允许使用的通知渠道（与 <c>NotificationConfig.Notifiers[].Type</c> 一致，小写）。
    /// 为 null 或空集合表示不限制，可使用系统注册的全部通知渠道。
    /// </summary>
    IReadOnlyList<string>? SupportedNotifierTypes => null;

    /// <summary>
    /// 某通知渠道（如 <c>speaker</c>）的默认参数，与 <c>config.json</c> 中对应项合并时以配置为准。
    /// </summary>
    IReadOnlyDictionary<string, string>? GetDefaultNotifierParameters(string notifierType) => null;

    /// <summary>
    /// Desktop/Web 实时画面等可展示的按钮/操作元数据；无则返回空列表。
    /// 若声明了某项，运行实例宜实现 <see cref="IAlgorithmUiCommandHandler"/> 并在 <see cref="IAlgorithmUiCommandHandler.TryInvokeUiAction"/> 中处理对应 <see cref="Ui.AlgorithmUiActionDefinition.ActionId"/>。
    /// </summary>
    IReadOnlyList<AlgorithmUiActionDefinition> GetUiActionDefinitions() => Array.Empty<AlgorithmUiActionDefinition>();

    /// <summary>
    /// AOI 检测器及相关子系统（U 型、MediaPipe 手部等）在配置中的表单区块，映射到 <see cref="Configuration.Models.AlgorithmConfig.Args"/>。
    /// </summary>
    /// <remarks>与 <see cref="AlgorithmParameterSections"/> 区分：此处为 AOI 业务参数，非核心算法绕圈/门限逻辑。</remarks>
    IReadOnlyList<AlgorithmArgsFormSection> AoiParameterSections => Array.Empty<AlgorithmArgsFormSection>();

    /// <summary>
    /// 纯算法逻辑参数（如缠胶圈数门控）；映射到 <see cref="Configuration.Models.AlgorithmConfig.Args"/>。
    /// </summary>
    /// <remarks>
    /// 不需要在界面编辑算法专用门限时，不要在具体 Descriptor 中声明本属性（默认空列表）。
    /// </remarks>
    IReadOnlyList<AlgorithmArgsFormSection> AlgorithmParameterSections => Array.Empty<AlgorithmArgsFormSection>();
}
