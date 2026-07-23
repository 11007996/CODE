namespace LuxVideoDet.Desktop.Models;

/// <summary>
/// 预览源选择项 — 用于 ComboBox 绑定
/// </summary>
public class PreviewSourceItem
{
    public string ConfigId { get; set; } = string.Empty;
    public string ConfigName { get; set; } = string.Empty;

    public override string ToString() => ConfigName;
}
