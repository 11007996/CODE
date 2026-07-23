using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LuxVideoDet.Desktop.Models;

public partial class LogEntry : ObservableObject
{
    [ObservableProperty]
    private string _level = string.Empty;

    [ObservableProperty]
    private string _levelShort = string.Empty;

    [ObservableProperty]
    private string _levelColor = "#808080";

    [ObservableProperty]
    private DateTime _timestamp;

    [ObservableProperty]
    private string _source = string.Empty;

    [ObservableProperty]
    private string _message = string.Empty;

    public string FormattedLine
        => $"[{Timestamp:HH:mm:ss.fff}] [{LevelShort}] [{Source}] {Message}";
}
