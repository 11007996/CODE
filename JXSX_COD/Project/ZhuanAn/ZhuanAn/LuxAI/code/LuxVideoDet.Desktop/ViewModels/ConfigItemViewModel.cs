using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Desktop.Views;
using LuxVideoDet.Localization;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Desktop.ViewModels;

public partial class ConfigItemViewModel : ViewModelBase
{
    private enum LocalizedStatusKind
    {
        Stopped,
        Running,
        Error
    }

    private DetectionConfiguration _config;
    private readonly DetectionService _detectionService;
    private readonly ConfigurationService _configService;
    private readonly ImageRenderService? _imageRenderService;
    private readonly ILogger _logger;
    private readonly IAppLocalizer _appLocalizer;
    private readonly IUiCultureService _uiCulture;
    private LocalizedStatusKind _statusKind = LocalizedStatusKind.Stopped;

    [ObservableProperty]
    private string _configId = string.Empty;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _status = string.Empty;

    [ObservableProperty]
    private string _statusColor = "#9E9E9E";

    [ObservableProperty]
    private string _sourceType = string.Empty;

    [ObservableProperty]
    private string _algorithmType = string.Empty;

    [ObservableProperty]
    private bool _isRunning;

    [ObservableProperty]
    private double _averagePipelineMs;

    [ObservableProperty]
    private int _detectionCount;

    [ObservableProperty]
    private string _startStopButtonText = string.Empty;

    [ObservableProperty]
    private string _sourcePrefixLabel = string.Empty;

    [ObservableProperty]
    private string _editButtonText = string.Empty;

    [ObservableProperty]
    private string _deleteButtonText = string.Empty;

    [ObservableProperty]
    private string _ngReplayButtonText = string.Empty;

    [ObservableProperty]
    private string _ngReplayTooltip = string.Empty;

    /// <summary>最近一次失败时的完整原因（悬停在「错误」状态徽章上可见）；成功时为 null。</summary>
    [ObservableProperty]
    private string? _statusErrorDetail;

    public ConfigItemViewModel(
        DetectionConfiguration config,
        DetectionService detectionService,
        ConfigurationService configService,
        ILogger logger,
        IAppLocalizer appLocalizer,
        IUiCultureService uiCulture,
        ImageRenderService? imageRenderService = null)
    {
        _config = config;
        _detectionService = detectionService;
        _configService = configService;
        _imageRenderService = imageRenderService;
        _logger = logger;
        _appLocalizer = appLocalizer;
        _uiCulture = uiCulture;

        ConfigId = config.Id;
        Name = config.Name;
        SourceType = config.VideoSource.Type.ToString();
        AlgorithmType = "未指定";

        _uiCulture.CultureChanged += (_, _) =>
            Dispatcher.UIThread.Post(ApplyChromeLocalization);

        ApplyChromeLocalization();
        UpdateStatus();
    }

    private void ApplyChromeLocalization()
    {
        SourcePrefixLabel = _appLocalizer.GetString(UiKeys.Main_SourcePrefix);
        EditButtonText = _appLocalizer.GetString(UiKeys.Main_BtnEdit);
        DeleteButtonText = _appLocalizer.GetString(UiKeys.Main_BtnDelete);
        NgReplayButtonText = _appLocalizer.GetString(UiKeys.Main_BtnNgReplay);
        NgReplayTooltip = _appLocalizer.GetString(UiKeys.Main_NgReplayTooltip);
        ApplyStatusLocalization();
    }

    private void ApplyStatusLocalization()
    {
        Status = _statusKind switch
        {
            LocalizedStatusKind.Stopped => _appLocalizer.GetString(UiKeys.Config_Status_Stopped),
            LocalizedStatusKind.Running => _appLocalizer.GetString(UiKeys.Config_Status_Running),
            LocalizedStatusKind.Error => _appLocalizer.GetString(UiKeys.Config_Status_Error),
            _ => Status
        };
        StartStopButtonText = IsRunning
            ? _appLocalizer.GetString(UiKeys.Config_Btn_Stop)
            : _appLocalizer.GetString(UiKeys.Config_Btn_Start);
    }

    [RelayCommand]
    private async Task StartStop()
    {
        try
        {
            if (IsRunning)
            {
                await _detectionService.StopDetectionAsync(ConfigId);
                IsRunning = false;
                _statusKind = LocalizedStatusKind.Stopped;
                StatusColor = "#9E9E9E";
                StatusErrorDetail = null;
                ApplyStatusLocalization();
            }
            else
            {
                await _detectionService.StartDetectionAsync(_config);
                IsRunning = true;
                _statusKind = LocalizedStatusKind.Running;
                StatusColor = "#4CAF50";
                StatusErrorDetail = null;
                ApplyStatusLocalization();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "启动/停止检测失败: {Summary}",
                DetectionSessionErrorFormatting.FormatStartFailure(Name, ex));
            _statusKind = LocalizedStatusKind.Error;
            StatusColor = "#F44336";
            StatusErrorDetail = ex.Message;
            ApplyStatusLocalization();
        }
    }

    [RelayCommand]
    private async Task Edit()
    {
        var owner = GetMainWindow();
        if (owner is null)
        {
            _logger.LogWarning("无法打开编辑窗口：主窗口不可用");
            return;
        }

        var editor = new Views.ConfigEditorWindow(_config);
        var result = await editor.ShowDialog<DetectionConfiguration?>(owner);

        if (result != null)
        {
            try
            {
                await _configService.SaveAsync(result);
                _logger.LogInformation("编辑配置成功: {ConfigName}", result.Name);
                if (IsRunning)
                {
                    _logger.LogWarning(
                        "配置已写入磁盘，但本配置检测仍在运行，流水线仍使用「启动时」的模型与推理设备；" +
                        "修改推理设备/模型等需停止检测后再启动才会生效。");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存配置失败");
            }
        }
    }

    private static Window? GetMainWindow()
        => Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

    [RelayCommand]
    private async Task Delete()
    {
        try
        {
            var owner = GetMainWindow();
            if (owner == null) return;

            var confirmed = await ShowConfirmDialogAsync(owner,
                "确认删除",
                $"确定要删除配置「{Name}」吗？\n此操作不可撤销。");

            if (!confirmed) return;

            if (IsRunning)
            {
                await _detectionService.StopDetectionAsync(ConfigId);
            }

            await _configService.DeleteAsync(ConfigId);
            _logger.LogInformation("删除配置: {ConfigName}", Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除配置失败: {ConfigName}", Name);
        }
    }

    /// <summary>用 Border 模拟按钮，避过 Fluent 主题的样式覆盖，完全自定义悬停颜色</summary>
    private static Border FakeButton(string text, string defaultBg, string defaultFg, string hoverBg, string hoverFg)
    {
        var tb = new TextBlock { Text = text };
        var border = new Border
        {
            Child = tb,
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(16, 8),
            Background = new SolidColorBrush(Color.Parse(defaultBg)),
            Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand)
        };
        tb.Foreground = new SolidColorBrush(Color.Parse(defaultFg));

        var defBgBrush = new SolidColorBrush(Color.Parse(defaultBg));
        var defFgBrush = new SolidColorBrush(Color.Parse(defaultFg));
        var hovBgBrush = new SolidColorBrush(Color.Parse(hoverBg));
        var hovFgBrush = new SolidColorBrush(Color.Parse(hoverFg));

        border.PointerEntered += (_, _) => { border.Background = hovBgBrush; tb.Foreground = hovFgBrush; };
        border.PointerExited  += (_, _) => { border.Background = defBgBrush; tb.Foreground = defFgBrush; };
        border.PointerPressed += (_, _) => { border.Background = hovBgBrush; tb.Foreground = hovFgBrush; };

        return border;
    }

    private static async Task<bool> ShowConfirmDialogAsync(Window owner, string title, string message)
    {
        var tcs = new TaskCompletionSource<bool>();

        var isDark = Application.Current?.RequestedThemeVariant == Avalonia.Styling.ThemeVariant.Dark;

        var bg = isDark ? "#1E1E2E" : "#F4F6FA";
        var textFg = isDark ? "#E0E0E0" : "#1A1A1A";
        var cancelBg = isDark ? "#374151" : "#E5E7EB";
        var cancelFg = isDark ? "#D1D5DB" : "#111827";
        var dangerBg = isDark ? "#7F1D1D" : "#FEE2E2";
        var dangerFg = isDark ? "#FCA5A5" : "#B91C1C";

        var dialog = new Window
        {
            Title = title,
            Width = 420,
            SizeToContent = SizeToContent.Height,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            CanResize = false,
            Background = new SolidColorBrush(Color.Parse(bg))
        };

        var panel = new StackPanel { Margin = new Thickness(28, 24), Spacing = 20 };

        panel.Children.Add(new TextBlock
        {
            Text = title,
            FontSize = 16,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Color.Parse(textFg))
        });

        panel.Children.Add(new TextBlock
        {
            Text = message,
            FontSize = 14,
            TextWrapping = TextWrapping.Wrap,
            Foreground = new SolidColorBrush(Color.Parse(textFg)),
            Opacity = 0.85
        });

        var buttonPanel = new StackPanel
        {
            Orientation = Avalonia.Layout.Orientation.Horizontal,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
            Spacing = 10
        };

        var cancelBtn = new Button
        {
            Content = "取消",
            Background = new SolidColorBrush(Color.Parse(cancelBg)),
            Foreground = new SolidColorBrush(Color.Parse(cancelFg)),
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(16, 8)
        };
        cancelBtn.Click += (_, _) => { tcs.TrySetResult(false); dialog.Close(); };

        var confirmBtn = FakeButton("确认删除", cancelBg, cancelFg, dangerBg, dangerFg);
        confirmBtn.PointerPressed += (_, _) => { tcs.TrySetResult(true); dialog.Close(); };

        buttonPanel.Children.Add(cancelBtn);
        buttonPanel.Children.Add(confirmBtn);
        panel.Children.Add(buttonPanel);
        dialog.Content = panel;

        await dialog.ShowDialog(owner);
        return tcs.Task.Result;
    }

    [RelayCommand]
    private void OpenNgReplay()
    {
        var algo = _config.Algorithms.FirstOrDefault(a => a.Enabled);
        var catchDir = algo?.Storage?.ErrorImagePath ?? "catch";

        var fullCatchDir = Path.Combine(catchDir, Name);

        if (!Directory.Exists(fullCatchDir))
        {
            _logger.LogWarning("NG 回放目录不存在: {Dir}，尝试打开基础目录", fullCatchDir);
            fullCatchDir = catchDir;
            if (!Directory.Exists(fullCatchDir))
            {
                _logger.LogWarning("catch 目录不存在: {Dir}", fullCatchDir);
                return;
            }
        }

        if (_imageRenderService == null)
        {
            _logger.LogWarning("ImageRenderService 不可用，无法打开 NG 回放");
            return;
        }

        var vm = new NgReplayViewModel(_imageRenderService, _appLocalizer, _uiCulture, fullCatchDir, Name);
        var window = new NgReplayWindow(vm);
        window.Show();

        _logger.LogInformation("打开 NG 回放: {ConfigName}, 目录={Dir}", Name, fullCatchDir);
    }

    private void UpdateStatus()
    {
        var session = _detectionService.GetSession(ConfigId);
        if (session != null && session.IsRunning)
        {
            IsRunning = true;
            _statusKind = LocalizedStatusKind.Running;
            StatusColor = "#4CAF50";
            AveragePipelineMs = session.AveragePipelineMs;
            DetectionCount = session.DetectionCount;
        }
        else
        {
            IsRunning = false;
            _statusKind = LocalizedStatusKind.Stopped;
            StatusColor = "#9E9E9E";
        }

        ApplyStatusLocalization();
    }

    public void UpdateFromConfiguration(DetectionConfiguration config)
    {
        _config = config;
        ConfigId = config.Id;
        Name = config.Name;
        SourceType = config.VideoSource.Type.ToString();
    }
}
