using LuxVideoDet.Core.Utils;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 按「业务语义顺序」的类名列表为每个模型 <see cref="Inference.Results.Detection.ClassId"/> 填色：
/// 引擎返回的第 <c>i</c> 个类别名若与某语义名一致，则使用与该语义同下标的配置/描述符颜色，
/// 使模型输出类别顺序或总类数变化时，同一语义的框颜色仍稳定（类名需与 <see cref="ClassIdsResolution"/> 所用一致）。
/// </summary>
public static class SemanticOrderClassColors
{
    /// <param name="target">ClassId → 颜色。</param>
    /// <param name="engineClassNamesInIndexOrder"><see cref="IInferenceEngine.GetClassNames"/>，下标即 ClassId。</param>
    /// <param name="semanticClassNamesInOrder">业务顺序下的模型类名（与同序的 classColors、Descriptor 默认色对应）。</param>
    /// <param name="configHexBySemanticIndex">配置中按语义下标的 #RRGGBB。</param>
    /// <param name="descriptorColorsBySemanticIndex">描述符中与语义同序的默认色。</param>
    public static void FillClassColorsBySemanticName(
        Dictionary<int, Scalar> target,
        IReadOnlyList<string> engineClassNamesInIndexOrder,
        IReadOnlyList<string> semanticClassNamesInOrder,
        IReadOnlyList<string>? configHexBySemanticIndex,
        IReadOnlyList<Scalar>? descriptorColorsBySemanticIndex,
        ILogger? logger)
    {
        ArgumentNullException.ThrowIfNull(target);
        ArgumentNullException.ThrowIfNull(engineClassNamesInIndexOrder);
        ArgumentNullException.ThrowIfNull(semanticClassNamesInOrder);

        var n = engineClassNamesInIndexOrder.Count;
        if (n == 0)
            return;

        var semCount = semanticClassNamesInOrder.Count;
        var paletteLen = Math.Max(n, semCount);
        var palette = DrawingHelper.GenerateColorPalette(paletteLen);

        for (var classId = 0; classId < n; classId++)
        {
            var cn = engineClassNamesInIndexOrder[classId];
            var semanticIdx = -1;
            for (var s = 0; s < semCount; s++)
            {
                if (cn.Equals(semanticClassNamesInOrder[s], StringComparison.OrdinalIgnoreCase))
                {
                    semanticIdx = s;
                    break;
                }
            }

            if (semanticIdx >= 0)
            {
                string? hex = null;
                if (configHexBySemanticIndex != null && semanticIdx < configHexBySemanticIndex.Count)
                    hex = configHexBySemanticIndex[semanticIdx];

                if (VisualizationColors.TryParseRgbHexToBgrScalar(hex, out var fromConfig))
                {
                    target[classId] = fromConfig;
                    continue;
                }

                if (descriptorColorsBySemanticIndex != null && semanticIdx < descriptorColorsBySemanticIndex.Count)
                {
                    target[classId] = descriptorColorsBySemanticIndex[semanticIdx];
                    continue;
                }

                target[classId] = palette[semanticIdx % palette.Length];
            }
            else
            {
                target[classId] = palette[classId % palette.Length];
                logger?.LogDebug(
                    "[算法·颜色] 模型类别 \"{Name}\" (ClassId={Id}) 未列入业务语义表，使用生成色板回退",
                    cn, classId);
            }
        }
    }
}
