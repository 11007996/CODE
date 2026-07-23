using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace LuxVideoDet.Desktop.Views.S3Storage.Converters;

public static class FileSizeConverters
{
    public static readonly IValueConverter BytesToMb = new BytesToMbConverter();
}

internal sealed class BytesToMbConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return "0.00 MB";

        double bytes = value switch
        {
            long l => l,
            int i => i,
            double d => d,
            float f => f,
            _ => 0
        };

        var mb = bytes / (1024d * 1024d);
        return $"{mb:N2} MB";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
