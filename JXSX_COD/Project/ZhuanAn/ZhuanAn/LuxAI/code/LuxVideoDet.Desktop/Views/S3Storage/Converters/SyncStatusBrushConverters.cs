using Avalonia.Data.Converters;
using Avalonia.Media;
using LuxVideoDet.Core.Configuration.Models;
using System;
using System.Globalization;

namespace LuxVideoDet.Desktop.Views.S3Storage.Converters;

public static class SyncStatusBrushConverters
{
    public static readonly IValueConverter StatusBackground = new SyncStatusBackgroundConverter();
    public static readonly IValueConverter StatusForeground = new SyncStatusForegroundConverter();
    public static readonly IValueConverter StatusText = new SyncStatusTextConverter();
}

internal sealed class SyncStatusBackgroundConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not SyncStatus status)
            return new SolidColorBrush(Color.Parse("#E5E7EB"));

        // 本地缺失(待下载): 灰色；其余代表本地已有: 绿色
        return status == SyncStatus.Pending
            ? new SolidColorBrush(Color.Parse("#E5E7EB"))
            : new SolidColorBrush(Color.Parse("#DCFCE7"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

internal sealed class SyncStatusForegroundConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not SyncStatus status)
            return new SolidColorBrush(Color.Parse("#374151"));

        return status == SyncStatus.Pending
            ? new SolidColorBrush(Color.Parse("#4B5563"))
            : new SolidColorBrush(Color.Parse("#166534"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

internal sealed class SyncStatusTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not SyncStatus status)
            return "未知";

        return status switch
        {
            SyncStatus.Pending => "待下载",
            SyncStatus.Skipped => "已存在",
            SyncStatus.Updated => "需更新",
            SyncStatus.Downloaded => "已下载",
            SyncStatus.Failed => "失败",
            SyncStatus.Syncing => "同步中",
            _ => "已处理"
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
