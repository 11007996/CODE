namespace LuxVideoDet.Web.Models;

/// <summary>POST /api/sessions/{configId}/ui-action 请求体。</summary>
public sealed class UiActionInvokeDto
{
    /// <summary>与配置中 algorithms[].algorithmType 一致（如 ucs、tearofftab）。</summary>
    public string AlgorithmType { get; set; } = string.Empty;

    /// <summary>与 <see cref="LuxVideoDet.Core.Algorithm.Ui.AlgorithmUiActionDefinition.ActionId"/> 一致。</summary>
    public string ActionId { get; set; } = string.Empty;
}
