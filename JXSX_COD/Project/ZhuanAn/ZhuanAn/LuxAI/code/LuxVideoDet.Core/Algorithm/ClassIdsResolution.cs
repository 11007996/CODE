using LuxVideoDet.Core.Inference;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 在算法初始化阶段，通过 <see cref="IInferenceEngine.GetClassIndexMap"/> 将「模型中的类名字符串」
/// 解析为 <see cref="Inference.Results.Detection.ClassId"/>，供各业务算法复用。
/// 推荐由 <see cref="DetectionAlgorithmBase.GetDetectionClassBindings"/> 声明业务键与模型类名，基类统一调用本方法；
/// 亦可自行组 <c>(LogicalName, ModelClassName)</c> 元组列表后调用本方法，再映射到各算法强类型 <c>*ResolvedClassIds</c>，
/// 帧内逻辑只比较整型 ClassId。
/// </summary>
public static class ClassIdsResolution
{
    /// <summary>
    /// 将多组「业务键 → 模型类名」解析为 ClassId。缺少任一类名或映射为空时抛出 <see cref="InvalidOperationException"/>。
    /// </summary>
    /// <param name="engine">已加载模型、可提供类别映射的推理引擎。</param>
    /// <param name="logger">用于调试/汇总日志。</param>
    /// <param name="entries">业务键与模型 metadata / 训练一致的类名；仅需包含本算法用到的子集。引擎中多出的其它类别会被忽略。</param>
    /// <param name="algorithmDisplayName">用于日志与异常文案，如 UCS、U7Lite。</param>
    /// <returns>键为 <paramref name="entries"/> 中的业务键，值为模型 ClassId。</returns>
    public static IReadOnlyDictionary<string, int> Resolve(
        IInferenceEngine engine,
        ILogger logger,
        IEnumerable<(string LogicalName, string ModelClassName)> entries,
        string algorithmDisplayName)
    {
        ArgumentNullException.ThrowIfNull(engine);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentException.ThrowIfNullOrWhiteSpace(algorithmDisplayName);

        var list = entries as IList<(string LogicalName, string ModelClassName)> ?? entries.ToList();
        if (list.Count == 0)
            throw new ArgumentException("至少提供一组 (LogicalName, ModelClassName)。", nameof(entries));

        var map = engine.GetClassIndexMap();
        if (map.Count == 0)
        {
            throw new InvalidOperationException(
                $"{algorithmDisplayName} 需要推理引擎提供类别名称→索引映射（名称→索引为空）。请确认模型已加载且类别元数据或 inference.classes 可用。");
        }

        var result = new Dictionary<string, int>(StringComparer.Ordinal);
        foreach (var (logicalName, modelClassName) in list)
        {
            if (string.IsNullOrWhiteSpace(logicalName))
                throw new ArgumentException("LogicalName 不能为空。", nameof(entries));
            if (string.IsNullOrWhiteSpace(modelClassName))
                throw new ArgumentException($"业务键 \"{logicalName}\" 对应的 ModelClassName 不能为空。", nameof(entries));

            if (map.TryGetValue(modelClassName, out var id))
            {
                logger.LogDebug(
                    "[算法·{Algorithm}] {Logical} ← 模型类名 \"{ModelName}\" → ClassId={Id}",
                    algorithmDisplayName, logicalName, modelClassName, id);
                result[logicalName] = id;
            }
            else
            {
                var available = string.Join(", ", map.OrderBy(kv => kv.Value).Select(kv => $"{kv.Key}={kv.Value}"));
                throw new InvalidOperationException(
                    $"{algorithmDisplayName} 要求模型类别中包含 \"{modelClassName}\"（业务项: {logicalName}）。当前名称→索引: [{available}]");
            }
        }

        var summary = string.Join(", ", result.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key}={kv.Value}"));
        logger.LogInformation("[算法·{Algorithm}] 业务 ClassId: {Summary}", algorithmDisplayName, summary);

        return result;
    }
}
