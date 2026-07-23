using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Localization;

namespace LuxVideoDet.Desktop.Views;

public partial class FloatingPreviewWindow : ScreenFitWindow
{
    private string _configName = string.Empty;
    private IAppLocalizer? _appLocalizer;
    private IUiCultureService? _uiCulture;

    public string ConfigId { get; private set; } = string.Empty;

    public FloatingPreviewWindow()
    {
        InitializeComponent();
        WireEvents();
    }

    public FloatingPreviewWindow(
        string configId,
        string configName,
        IAppLocalizer appLocalizer,
        IUiCultureService uiCulture) : this()
    {
        ConfigId = configId;
        _configName = configName;
        _appLocalizer = appLocalizer;
        _uiCulture = uiCulture;

        _uiCulture.CultureChanged += OnCultureChanged;
        ApplyFloatingChrome();

        if (ConfigNameText != null)
            ConfigNameText.Text = configName;
    }

    private void OnCultureChanged(object? sender, EventArgs e)
        => Dispatcher.UIThread.Post(ApplyFloatingChrome);

    private void ApplyFloatingChrome()
    {
        if (_appLocalizer == null) return;
        Title = string.Format(_appLocalizer.GetString(UiKeys.Main_FloatingTitleFmt), _configName);
        if (FullscreenButton != null)
            ToolTip.SetTip(FullscreenButton, _appLocalizer.GetString(UiKeys.Main_FloatingFullscreenTooltip));
    }

    private void WireEvents()
    {
        KeyDown += OnKeyDown;
        if (FullscreenButton != null)
            FullscreenButton.Click += (_, _) => ToggleFullscreen();
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape && WindowState == WindowState.FullScreen)
        {
            WindowState = WindowState.Normal;
            SystemDecorations = SystemDecorations.Full;
        }
        else if (e.Key == Key.F11 || (e.Key == Key.Enter && e.KeyModifiers == KeyModifiers.Alt))
        {
            ToggleFullscreen();
        }
    }

    private void ToggleFullscreen()
    {
        if (WindowState == WindowState.FullScreen)
        {
            WindowState = WindowState.Normal;
            SystemDecorations = SystemDecorations.Full;
        }
        else
        {
            WindowState = WindowState.FullScreen;
            SystemDecorations = SystemDecorations.None;
        }
    }

    public void UpdatePreview(WriteableBitmap? image, double averagePipelineMs, string? stateMessage = null)
    {
        if (PreviewImage != null)
            PreviewImage.Source = image;

        if (FpsText != null)
        {
            var fmt = _appLocalizer != null
                ? _appLocalizer.GetString(UiKeys.Main_FloatingMsPerFrameFmt)
                : "{0:F1} ms/frame";
            FpsText.Text = string.Format(System.Globalization.CultureInfo.CurrentUICulture, fmt, averagePipelineMs);
        }

        if (StateText != null)
            StateText.Text = stateMessage ?? string.Empty;
    }
}
