using System;
using Avalonia.Controls;
using LuxVideoDet.Desktop.Helpers;

namespace LuxVideoDet.Desktop.Views;

/// <summary>
/// 在 <see cref="Opened"/> 时根据当前屏幕工作区做尺寸与位置自适应。
/// </summary>
public class ScreenFitWindow : Window
{
    public ScreenFitWindow()
    {
        Opened += OnScreenFitOpened;
    }

    private void OnScreenFitOpened(object? sender, EventArgs e)
    {
        WindowScreenFit.Apply(this);
    }
}
