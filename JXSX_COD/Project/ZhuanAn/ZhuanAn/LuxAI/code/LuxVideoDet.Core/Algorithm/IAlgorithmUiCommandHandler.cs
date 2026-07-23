namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 支持从 Desktop/Web 实时画面对算法下发操作的运行时接口（与 Descriptor 元数据成对出现）。
/// </summary>
public interface IAlgorithmUiCommandHandler
{
    /// <summary>
    /// 执行 <paramref name="actionId"/>（与 <see cref="Ui.AlgorithmUiActionDefinition.ActionId"/> 一致）。
    /// </summary>
    /// <returns>是否已处理该 id（未知 id 应返回 false 并给出 <paramref name="errorMessage"/>）。</returns>
    bool TryInvokeUiAction(string actionId, out string? errorMessage);
}
