using Avalonia.Data.Converters;
using Avalonia.Media;
using LuxVideoDet.Desktop.ViewModels;
using System;
using System.Globalization;

namespace LuxVideoDet.Desktop.Views.S3Storage.Converters;

public static class UiLogConverters
{
    public static readonly IValueConverter LevelText = new UiLogLevelTextConverter();
    public static readonly IValueConverter LevelForeground = new UiLogLevelForegroundConverter();
    public static readonly IValueConverter LevelBackground = new UiLogLevelBackgroundConverter();
}

internal sealed class UiLogLevelTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not UiLogLevel level) return "INFO";
        return level switch
        {
            UiLogLevel.Success => "SUCCESS",
            UiLogLevel.Warning => "WARN",
            UiLogLevel.Error => "ERROR",
            _ => "INFO"
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}

internal sealed class UiLogLevelForegroundConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not UiLogLevel level) return Brushes.White;
        return level switch
        {
            UiLogLevel.Success => new SolidColorBrush(Color.Parse("#166534")),
            UiLogLevel.Warning => new SolidColorBrush(Color.Parse("#92400E")),
            UiLogLevel.Error => new SolidColorBrush(Color.Parse("#991B1B")),
            _ => new SolidColorBrush(Color.Parse("#1E3A8A"))
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}

internal sealed class UiLogLevelBackgroundConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not UiLogLevel level) return new SolidColorBrush(Color.Parse("#DBEAFE"));
        return level switch
        {
            UiLogLevel.Success => new SolidColorBrush(Color.Parse("#DCFCE7")),
            UiLogLevel.Warning => new SolidColorBrush(Color.Parse("#FEF3C7")),
            UiLogLevel.Error => new SolidColorBrush(Color.Parse("#FEE2E2")),
            _ => new SolidColorBrush(Color.Parse("#DBEAFE"))
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}
