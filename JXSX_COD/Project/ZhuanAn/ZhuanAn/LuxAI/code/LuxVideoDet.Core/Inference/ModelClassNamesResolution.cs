using LuxVideoDet.Core.Configuration.Models;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Inference;

/// <summary>
/// 将「模型元数据中的类别顺序」与「配置中的类别列表」合并为后处理所用的 <see cref="PostprocessContext.ClassNames"/>。
/// 列表第 i 项必须对应模型输出类别索引 i，否则 <see cref="Inference.Results.Detection.ClassName"/> 会错误。
/// </summary>
internal static class ModelClassNamesResolution
{
    public static void Resolve(
        List<string>? modelMetadataClassNames,
        InferenceConfig config,
        int numClasses,
        ILogger logger,
        out List<string> resolvedClassNames,
        out Dictionary<string, int> classIndexMap)
    {
        classIndexMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        if (modelMetadataClassNames != null && modelMetadataClassNames.Count > 0)
        {
            resolvedClassNames = modelMetadataClassNames.ToList();

            for (int i = 0; i < modelMetadataClassNames.Count; i++)
                classIndexMap[modelMetadataClassNames[i]] = i;

            if (config.Classes.Count > 0)
            {
                var configSet = new HashSet<string>(config.Classes, StringComparer.OrdinalIgnoreCase);
                var modelSet = new HashSet<string>(modelMetadataClassNames, StringComparer.OrdinalIgnoreCase);
                var missingInModel = configSet.Except(modelSet, StringComparer.OrdinalIgnoreCase).ToList();
                var extraInModel = modelSet.Except(configSet, StringComparer.OrdinalIgnoreCase).ToList();

                if (missingInModel.Count > 0)
                {
                    logger.LogWarning(
                        "[模型·类别] 配置中有而模型无: [{Missing}]",
                        string.Join(", ", missingInModel));
                }
                if (extraInModel.Count > 0)
                {
                    logger.LogInformation(
                        "[模型·类别] 模型较配置多出 [{Extra}]（后处理以模型完整列表为准）",
                        string.Join(", ", extraInModel));
                }

                var reordered = false;
                for (int i = 0; i < Math.Min(config.Classes.Count, modelMetadataClassNames.Count); i++)
                {
                    if (!config.Classes[i].Equals(modelMetadataClassNames[i], StringComparison.OrdinalIgnoreCase))
                    {
                        reordered = true;
                        break;
                    }
                }
                if (reordered)
                {
                    // 仅顺序变化而集合一致时，按名称匹配的算法不受影响，不应吓唬成 Warning。
                    var sameSet = missingInModel.Count == 0 && extraInModel.Count == 0;
                    if (sameSet)
                    {
                        logger.LogInformation(
                            "[模型·类别] 顺序与配置不同但集合一致，已采用模型索引顺序；算法侧请按类别名匹配。模型=[{Model}] | 配置=[{Config}]",
                            string.Join(", ", modelMetadataClassNames),
                            string.Join(", ", config.Classes));
                    }
                    else
                    {
                        logger.LogWarning(
                            "[模型·类别] 顺序/集合均不一致，已强制使用模型顺序。模型=[{Model}] | 配置=[{Config}] | 请核对缺失/多余类告警",
                            string.Join(", ", modelMetadataClassNames),
                            string.Join(", ", config.Classes));
                    }
                }
            }

            logger.LogDebug(
                "[模型·类别] 名称→索引（模型元数据）: {Map}",
                string.Join(", ", classIndexMap.OrderBy(kv => kv.Value).Select(kv => $"{kv.Key}={kv.Value}")));
        }
        else if (config.Classes.Count > 0)
        {
            resolvedClassNames = config.Classes.ToList();
            for (int i = 0; i < config.Classes.Count; i++)
                classIndexMap[config.Classes[i]] = i;

            logger.LogInformation(
                "模型元数据中无类别信息，使用配置中的类别列表（{Count} 个）。请确保顺序与模型索引一致",
                config.Classes.Count);
        }
        else
        {
            resolvedClassNames = Enumerable.Range(0, numClasses)
                .Select(i => $"class{i}").ToList();
            for (int i = 0; i < numClasses; i++)
                classIndexMap[$"class{i}"] = i;

            logger.LogWarning(
                "模型元数据和配置中均无类别名称，使用占位名称 class0~class{Max}",
                numClasses - 1);
        }

        // 重要：不要因为类别名数量不足而直接终止会话。
        // 在部分运行时（尤其 OpenVINO/某些 ONNX 导出）无法读取模型内嵌 names 时，
        // 配置中的 classes 往往只填写了业务关心的少数类。此时补齐占位名以保证后处理可运行，
        // 同时通过日志提示用户完善 classes 以避免“按名匹配”的算法逻辑失效。
        if (resolvedClassNames.Count < numClasses)
        {
            var missing = numClasses - resolvedClassNames.Count;
            logger.LogWarning(
                "[模型·类别] 类别名称数量不足：已有 {Have}，模型需要 {Need}。将自动补齐 {Missing} 个占位名 classN（建议完善 inference.classes 或使用带 names 的模型导出）。",
                resolvedClassNames.Count,
                numClasses,
                missing);

            for (var i = resolvedClassNames.Count; i < numClasses; i++)
            {
                var name = $"class{i}";
                resolvedClassNames.Add(name);
                classIndexMap[name] = i;
            }
        }
    }
}
