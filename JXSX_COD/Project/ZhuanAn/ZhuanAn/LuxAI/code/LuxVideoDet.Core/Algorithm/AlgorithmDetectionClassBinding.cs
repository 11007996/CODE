namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 算法声明的一条「业务类别」：与训练/metadata 中的 <see cref="ModelClassName"/> 一致，
/// 初始化时由基类调用 <see cref="ClassIdsResolution.Resolve"/> 得到 <see cref="LogicalName"/> → ClassId，
/// 并建立 ClassId → <see cref="DisplayLabel"/>（画面角标等）。
/// </summary>
/// <param name="LogicalName">业务键（稳定标识），帧内可与解析结果字典对齐。</param>
/// <param name="ModelClassName">模型类别名字符串，须与引擎 <c>GetClassIndexMap</c> 键一致。</param>
/// <param name="DisplayLabel">该 ClassId 在画面上的业务展示名（可与 <see cref="ModelClassName"/> 不同形）。</param>
public sealed record AlgorithmDetectionClassBinding(
    string LogicalName,
    string ModelClassName,
    string DisplayLabel);
