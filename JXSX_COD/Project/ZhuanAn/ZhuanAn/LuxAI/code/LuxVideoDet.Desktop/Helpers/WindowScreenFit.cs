using System;
using Avalonia;
using Avalonia.Controls;

namespace LuxVideoDet.Desktop.Helpers;

/// <summary>
/// 将窗口限制在当前屏幕工作区内，并校正最小尺寸与位置（逻辑单位与 DPI 缩放一致）。
/// </summary>
public static class WindowScreenFit
{
    public static void Apply(Window window)
    {
        if (window.Screens is null)
            return;

        if (window.WindowState is WindowState.FullScreen or WindowState.Maximized)
            return;

        var screen = window.Screens.ScreenFromWindow(window) ?? window.Screens.Primary;
        if (screen is null)
            return;

        var workArea = screen.WorkingArea;
        var scale = window.DesktopScaling;
        if (scale <= 0)
            return;

        var maxLogicalW = workArea.Width / scale;
        var maxLogicalH = workArea.Height / scale;

        window.MinWidth = Math.Min(window.MinWidth, maxLogicalW);
        window.MinHeight = Math.Min(window.MinHeight, maxLogicalH);

        if (!double.IsNaN(window.Width) && window.Width > maxLogicalW)
            window.Width = maxLogicalW;
        if (!double.IsNaN(window.Height) && window.Height > maxLogicalH)
            window.Height = maxLogicalH;

        var winWpx = (int)Math.Ceiling(window.Bounds.Width * scale);
        var winHpx = (int)Math.Ceiling(window.Bounds.Height * scale);
        var px = window.Position.X;
        var py = window.Position.Y;
        var left = workArea.X;
        var top = workArea.Y;
        var right = workArea.X + workArea.Width;
        var bottom = workArea.Y + workArea.Height;

        if (px < left)
            px = left;
        if (py < top)
            py = top;
        if (px + winWpx > right)
            px = right - winWpx;
        if (py + winHpx > bottom)
            py = bottom - winHpx;

        if (px != window.Position.X || py != window.Position.Y)
            window.Position = new PixelPoint(px, py);
    }
}
