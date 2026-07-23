using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxVideoDet.Core.Algorithm.Ui;

namespace LuxVideoDet.Desktop.ViewModels;

/// <summary>
/// 实时预览区圆形操作按钮（与 Core <c>AlgorithmUiActionDefinition</c> 对应，文案由算法 Descriptor 提供）。
/// </summary>
public partial class AlgorithmUiActionItemVm : ObservableObject
{
    private static readonly SolidColorBrush NgOutlineBrushAlert = new(Color.Parse("#D32F2F"));

    private readonly string _fallbackDisplayName;
    private readonly string? _displayNameWhenOk;
    private readonly string? _displayNameWhenNg;

    /// <summary>与配置中 <c>algorithmType</c> 一致，用于与检测结果对齐。</summary>
    [ObservableProperty]
    private string _algorithmType = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TooltipText))]
    private string _displayName = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TooltipText))]
    private string _description = string.Empty;

    /// <summary>当前算法输出是否为 ERROR（仅此时允许点击「确认」类操作）。</summary>
    [ObservableProperty]
    private bool _isActionEnabled;

    /// <summary>外圈始终占位 5px，仅 NG 时为红色；透明时不占布局变化，避免右下角锚点漂移。</summary>
    [ObservableProperty]
    private IBrush _ngOutlineBorderBrush = Brushes.Transparent;

    /// <summary>与 Descriptor 中声明一致，决定按钮落在预览区九宫格哪一格。</summary>
    public AlgorithmUiActionPlacement Placement { get; }

    /// <summary>悬停提示：完整按钮文案 + 可选说明（圆内文字过长省略时用于看全）。</summary>
    public string TooltipText =>
        string.IsNullOrWhiteSpace(Description)
            ? DisplayName
            : $"{DisplayName}\n{Description}";

    public IRelayCommand ExecuteCommand { get; }

    public AlgorithmUiActionItemVm(
        string algorithmType,
        string displayName,
        string description,
        IRelayCommand executeCommand,
        AlgorithmUiActionPlacement placement = AlgorithmUiActionPlacement.BottomRight,
        string? displayNameWhenOk = null,
        string? displayNameWhenNg = null)
    {
        AlgorithmType = algorithmType;
        _fallbackDisplayName = displayName;
        _displayNameWhenOk = displayNameWhenOk;
        _displayNameWhenNg = displayNameWhenNg;
        Description = description;
        ExecuteCommand = executeCommand;
        Placement = placement;
        DisplayName = ResolveDisplayName(inError: false);
    }

    /// <summary>与实时检测的 ERROR 态对齐：启用/红框/圆内短文案（OK→PLC、NG→恢复 等）。</summary>
    public void ApplyPreviewActionState(bool inError)
    {
        IsActionEnabled = inError;
        NgOutlineBorderBrush = inError ? NgOutlineBrushAlert : Brushes.Transparent;
        var next = ResolveDisplayName(inError);
        if (DisplayName != next)
            DisplayName = next;
    }

    private string ResolveDisplayName(bool inError)
    {
        if (_displayNameWhenOk == null && _displayNameWhenNg == null)
            return _fallbackDisplayName;
        return inError ? (_displayNameWhenNg ?? _fallbackDisplayName) : (_displayNameWhenOk ?? _fallbackDisplayName);
    }
}
