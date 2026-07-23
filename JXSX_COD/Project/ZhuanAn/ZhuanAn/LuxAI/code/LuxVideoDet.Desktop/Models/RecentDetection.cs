using CommunityToolkit.Mvvm.ComponentModel;

namespace LuxVideoDet.Desktop.Models;

/// <summary>
/// 最近检测记录展示模型，用于 ItemsControl 绑定。
/// </summary>
public partial class RecentDetection : ObservableObject
{
    [ObservableProperty]
    private string _configName = string.Empty;

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private string _severityColor = "#FFA500";

    [ObservableProperty]
    private string _timeAgo = string.Empty;
}
