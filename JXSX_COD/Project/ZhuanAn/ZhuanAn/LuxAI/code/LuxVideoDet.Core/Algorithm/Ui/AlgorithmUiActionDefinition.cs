namespace LuxVideoDet.Core.Algorithm.Ui;

/// <summary>
/// 实时画面等 UI 上可展示的算法操作元数据（由 <see cref="IAlgorithmDescriptor.GetUiActionDefinitions"/> 声明）。
/// </summary>
/// <param name="Placement">按钮在预览区九宫格中的区域；默认 <see cref="AlgorithmUiActionPlacement.BottomRight"/> 与旧版右下角一致。</param>
/// <param name="DisplayNameWhenOk">
/// 非错误态（如待机、正常运行）时按钮短文案；与 <see cref="DisplayNameWhenNg"/> 均未指定时，全状态使用 <see cref="DisplayName"/>。
/// </param>
/// <param name="DisplayNameWhenNg">
/// 错误/NG 态时按钮短文案（例如「恢复」）；未指定时回退为 <see cref="DisplayName"/>。
/// </param>
public sealed record AlgorithmUiActionDefinition(
    string ActionId,
    string DisplayName,
    string Description,
    AlgorithmUiActionPlacement Placement = AlgorithmUiActionPlacement.BottomRight,
    string? DisplayNameWhenOk = null,
    string? DisplayNameWhenNg = null);
