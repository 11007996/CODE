namespace LuxVideoDet.Core.Configuration;

/// <summary>
/// 已启用算法的配置中缺少 Descriptor 要求的必选区域时抛出，用于保存校验与运行时创建算法。
/// </summary>
public sealed class MissingAlgorithmRegionsException : InvalidOperationException
{
    public MissingAlgorithmRegionsException(string algorithmType, IReadOnlyList<string> detailLines)
        : base(FormatMessage(algorithmType, detailLines))
    {
        AlgorithmType = algorithmType;
        Details = detailLines;
    }

    public string AlgorithmType { get; }

    /// <summary>每条对应一个缺失区域的人类可读说明。</summary>
    public IReadOnlyList<string> Details { get; }

    private static string FormatMessage(string algorithmType, IReadOnlyList<string> detailLines)
    {
        var body = detailLines.Count == 0
            ? "未提供必选区域。"
            : string.Join(" ", detailLines);
        return $"算法 [{algorithmType}] 区域配置不完整：{body}";
    }
}
