using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace LuxVideoDet.Desktop.Views.S3Storage.Converters;

/// <summary>
/// 布尔值到画刷转换器
/// </summary>
public static class BoolToBrushConverters
{
    /// <summary>
    /// True为绿色，False为红色
    /// </summary>
    public static readonly IValueConverter TrueGreenFalseRed = new BoolToBrushConverter(
        trueBrush: Brushes.Green,
        falseBrush: Brushes.Red);

    /// <summary>
    /// True为绿色，False为灰色
    /// </summary>
    public static readonly IValueConverter TrueGreenFalseGray = new BoolToBrushConverter(
        trueBrush: Brushes.Green,
        falseBrush: Brushes.Gray);

    /// <summary>
    /// True为蓝色，False为灰色
    /// </summary>
    public static readonly IValueConverter TrueBlueFalseGray = new BoolToBrushConverter(
        trueBrush: Brushes.Blue,
        falseBrush: Brushes.Gray);

    /// <summary>
    /// True为黑色，False为浅灰色
    /// </summary>
    public static readonly IValueConverter TrueBlackFalseLightGray = new BoolToBrushConverter(
        trueBrush: Brushes.Black,
        falseBrush: Brushes.LightGray);
}

/// <summary>
/// 布尔值到画刷转换器实现
/// </summary>
public class BoolToBrushConverter : IValueConverter
{
    private readonly IBrush _trueBrush;
    private readonly IBrush _falseBrush;

    public BoolToBrushConverter(IBrush trueBrush, IBrush falseBrush)
    {
        _trueBrush = trueBrush;
        _falseBrush = falseBrush;
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? _trueBrush : _falseBrush;
        }

        return _falseBrush;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
