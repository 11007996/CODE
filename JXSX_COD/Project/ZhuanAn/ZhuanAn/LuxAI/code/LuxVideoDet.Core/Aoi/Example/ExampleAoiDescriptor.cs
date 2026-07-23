// -----------------------------------------------------------------------------
// 示例 AOI 描述符
// 作用：向 AoiDetectorFactory 注册 TypeKey 与 Create 工厂；与 ExampleAoiDetector 成对出现。
//       复制上线时请去掉 [ExampleTemplate]，否则反射不会注册该检测器。
// -----------------------------------------------------------------------------

using LuxVideoDet.Core;
using LuxVideoDet.Core.Aoi;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Aoi.Example;

/// <summary>
/// 示例 Descriptor：与 <see cref="ExampleAoiDetector"/> 配对。
/// 带 <see cref="ExampleTemplateAttribute"/>，不会被 <see cref="AoiDetectorFactory"/> 注册。
/// </summary>
/// <remarks>
/// 实现 <see cref="IAoiDetectorDescriptor"/> 时接口成员 <b>全部为必须</b>（<c>TypeKey</c>、<c>Aliases</c>、
/// <c>DisplayName</c>、<c>Create</c>）。扩展时去掉 <see cref="ExampleTemplateAttribute"/>。
/// </remarks>
[ExampleTemplate]
public sealed class ExampleAoiDescriptor : IAoiDetectorDescriptor
{
    public string TypeKey => "example_aoi";

    public IReadOnlyList<string> Aliases => Array.Empty<string>();

    public string DisplayName => "（示例）自定义 AOI";

    public IAoiDetector Create(ILogger logger) => new ExampleAoiDetector(logger);
}
