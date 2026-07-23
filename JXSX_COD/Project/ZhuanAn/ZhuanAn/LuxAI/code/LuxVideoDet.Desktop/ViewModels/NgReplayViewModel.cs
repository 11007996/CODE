using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Localization;
using OpenCvSharp;

namespace LuxVideoDet.Desktop.ViewModels;

public partial class NgReplayViewModel : ViewModelBase, IDisposable
{
    private readonly ImageRenderService _imageRenderService;
    private readonly IAppLocalizer _appLocalizer;
    private readonly IUiCultureService _uiCulture;
    private readonly string _catchBaseDir;
    private readonly string _configName;
    private VideoCapture? _videoCapture;
    private DispatcherTimer? _playTimer;
    private bool _disposed;

    [ObservableProperty]
    private string _windowTitle = string.Empty;

    [ObservableProperty]
    private string _lblPanelTitle = string.Empty;

    [ObservableProperty]
    private string _btnRefreshText = string.Empty;

    [ObservableProperty]
    private string _lblDate = string.Empty;

    [ObservableProperty]
    private string _lblVideoFilesColumn = string.Empty;

    [ObservableProperty]
    private string _placeholderSelectVideo = string.Empty;

    [ObservableProperty]
    private string _tooltipPrev = string.Empty;

    [ObservableProperty]
    private string _tooltipNext = string.Empty;

    [ObservableProperty]
    private string _tooltipStop = string.Empty;

    [ObservableProperty]
    private string _lblSpeed = string.Empty;

    [ObservableProperty]
    private ObservableCollection<DateFolderItem> _dateFolders = new();

    [ObservableProperty]
    private DateFolderItem? _selectedDateFolder;

    [ObservableProperty]
    private ObservableCollection<VideoFileItem> _videoFiles = new();

    [ObservableProperty]
    private VideoFileItem? _selectedVideoFile;

    [ObservableProperty]
    private WriteableBitmap? _currentFrame;

    [ObservableProperty]
    private bool _isPlaying;

    [ObservableProperty]
    private string _playPauseText = string.Empty;

    [ObservableProperty]
    private double _currentPosition;

    [ObservableProperty]
    private double _totalFrames;

    [ObservableProperty]
    private string _positionText = "00:00 / 00:00";

    [ObservableProperty]
    private double _playbackSpeed = 1.0;

    [ObservableProperty]
    private string _speedText = "1.0x";

    [ObservableProperty]
    private bool _hasVideo;

    [ObservableProperty]
    private double _videoFps = 15;

    public string[] SpeedOptions { get; } = ["0.2x", "0.5x", "0.7x", "1.0x"];

    [ObservableProperty]
    private int _selectedSpeedIndex = 3;

    public NgReplayViewModel(
        ImageRenderService imageRenderService,
        IAppLocalizer appLocalizer,
        IUiCultureService uiCulture,
        string catchBaseDir,
        string configName)
    {
        _imageRenderService = imageRenderService;
        _appLocalizer = appLocalizer;
        _uiCulture = uiCulture;
        _catchBaseDir = catchBaseDir;
        _configName = configName;

        _uiCulture.CultureChanged += (_, _) =>
            Dispatcher.UIThread.Post(ApplyNgChrome);

        ApplyNgChrome();
        LoadDateFolders();
    }

    private void ApplyNgChrome()
    {
        Func<string, string> L = k => _appLocalizer.GetString(k);
        LblPanelTitle = L(UiKeys.Ng_PanelVideoFiles);
        BtnRefreshText = L(UiKeys.Ng_BtnRefresh);
        LblDate = L(UiKeys.Ng_LblDate);
        LblVideoFilesColumn = L(UiKeys.Ng_LblVideoFilesColumn);
        PlaceholderSelectVideo = L(UiKeys.Ng_PlaceholderSelect);
        TooltipPrev = L(UiKeys.Ng_TooltipPrev);
        TooltipNext = L(UiKeys.Ng_TooltipNext);
        TooltipStop = L(UiKeys.Ng_TooltipStop);
        LblSpeed = L(UiKeys.Ng_LblSpeed);
        WindowTitle = string.Format(L(UiKeys.Ng_WindowTitle), _configName);
        if (IsPlaying)
            PlayPauseText = L(UiKeys.Ng_Pause);
        else
            PlayPauseText = L(UiKeys.Ng_Play);
        RefreshDateFolderDisplays();
    }

    private void RefreshDateFolderDisplays()
    {
        var fmt = _appLocalizer.GetString(UiKeys.Ng_DateFolderFmt);
        foreach (var d in DateFolders)
            d.Display = string.Format(fmt, d.DateString, d.FileCount);
    }

    private void LoadDateFolders()
    {
        var selectedPath = SelectedDateFolder?.FullPath;

        DateFolders.Clear();
        if (!Directory.Exists(_catchBaseDir))
            return;

        var dirs = Directory.GetDirectories(_catchBaseDir)
            .Select(d => new DirectoryInfo(d))
            .OrderByDescending(d => d.Name)
            .ToList();

        var fmt = _appLocalizer.GetString(UiKeys.Ng_DateFolderFmt);
        foreach (var dir in dirs)
        {
            var mp4Count = Directory.GetFiles(dir.FullName, "*.mp4").Length;
            if (mp4Count > 0)
            {
                DateFolders.Add(new DateFolderItem
                {
                    DateString = dir.Name,
                    FullPath = dir.FullName,
                    FileCount = mp4Count,
                    Display = string.Format(fmt, dir.Name, mp4Count)
                });
            }
        }

        if (selectedPath != null)
            SelectedDateFolder = DateFolders.FirstOrDefault(d => d.FullPath == selectedPath);
    }

    partial void OnSelectedDateFolderChanged(DateFolderItem? value)
    {
        StopPlayback();
        VideoFiles.Clear();
        if (value == null) return;

        var files = Directory.GetFiles(value.FullPath, "*.mp4")
            .Select(f => new FileInfo(f))
            .OrderByDescending(f => f.Name)
            .ToList();

        foreach (var file in files)
        {
            VideoFiles.Add(new VideoFileItem
            {
                FileName = file.Name,
                FullPath = file.FullName,
                FileSize = FormatFileSize(file.Length),
                LastModified = file.LastWriteTime.ToString("HH:mm:ss")
            });
        }
    }

    partial void OnSelectedVideoFileChanged(VideoFileItem? value)
    {
        StopPlayback();
        if (value == null)
        {
            HasVideo = false;
            return;
        }
        LoadVideo(value.FullPath);
    }

    partial void OnSelectedSpeedIndexChanged(int value)
    {
        PlaybackSpeed = value switch
        {
            0 => 0.2,
            1 => 0.5,
            2 => 0.7,
            3 => 1.0,
            _ => 1.0
        };
        SpeedText = $"{PlaybackSpeed:F1}x";
        UpdateTimerInterval();
    }

    private void LoadVideo(string path)
    {
        _videoCapture?.Release();
        _videoCapture?.Dispose();
        _videoCapture = new VideoCapture(path);

        if (!_videoCapture.IsOpened())
        {
            HasVideo = false;
            return;
        }

        VideoFps = _videoCapture.Fps > 0 ? _videoCapture.Fps : 15;
        TotalFrames = _videoCapture.FrameCount;
        CurrentPosition = 0;
        HasVideo = true;

        ShowFrame(0);
        UpdatePositionText();
    }

    [RelayCommand]
    private void PlayPause()
    {
        if (_videoCapture == null || !_videoCapture.IsOpened())
            return;

        if (IsPlaying)
        {
            StopPlayback();
        }
        else
        {
            if (CurrentPosition >= TotalFrames - 1)
                CurrentPosition = 0;

            StartPlayback();
        }
    }

    [RelayCommand]
    private void Stop()
    {
        StopPlayback();
        if (_videoCapture != null && _videoCapture.IsOpened())
        {
            CurrentPosition = 0;
            ShowFrame(0);
            UpdatePositionText();
        }
    }

    public void SeekTo(double position)
    {
        if (_videoCapture == null || !_videoCapture.IsOpened())
            return;

        CurrentPosition = Math.Clamp(position, 0, TotalFrames - 1);
        ShowFrame((int)CurrentPosition);
        UpdatePositionText();
    }

    [RelayCommand]
    private void StepForward()
    {
        if (_videoCapture == null || !_videoCapture.IsOpened())
            return;

        StopPlayback();
        if (CurrentPosition < TotalFrames - 1)
        {
            CurrentPosition++;
            ShowFrame((int)CurrentPosition);
            UpdatePositionText();
        }
    }

    [RelayCommand]
    private void StepBackward()
    {
        if (_videoCapture == null || !_videoCapture.IsOpened())
            return;

        StopPlayback();
        if (CurrentPosition > 0)
        {
            CurrentPosition--;
            ShowFrame((int)CurrentPosition);
            UpdatePositionText();
        }
    }

    [RelayCommand]
    private void RefreshFiles()
    {
        LoadDateFolders();
    }

    private void StartPlayback()
    {
        IsPlaying = true;
        PlayPauseText = _appLocalizer.GetString(UiKeys.Ng_Pause);

        _playTimer = new DispatcherTimer();
        UpdateTimerInterval();
        _playTimer.Tick += OnPlayTimerTick;
        _playTimer.Start();
    }

    private void StopPlayback()
    {
        IsPlaying = false;
        PlayPauseText = _appLocalizer.GetString(UiKeys.Ng_Play);

        if (_playTimer != null)
        {
            _playTimer.Stop();
            _playTimer.Tick -= OnPlayTimerTick;
            _playTimer = null;
        }
    }

    private void UpdateTimerInterval()
    {
        if (_playTimer == null) return;
        var intervalMs = (1000.0 / VideoFps) / PlaybackSpeed;
        _playTimer.Interval = TimeSpan.FromMilliseconds(Math.Max(intervalMs, 10));
    }

    private void OnPlayTimerTick(object? sender, EventArgs e)
    {
        if (_videoCapture == null || !_videoCapture.IsOpened())
        {
            StopPlayback();
            return;
        }

        using var frame = new Mat();
        _videoCapture.Read(frame);

        if (frame.Empty())
        {
            StopPlayback();
            return;
        }

        CurrentPosition = _videoCapture.PosFrames;

        var bitmap = _imageRenderService.ConvertToWriteableBitmap(frame);
        if (bitmap != null)
            CurrentFrame = bitmap;

        UpdatePositionText();

        if (CurrentPosition >= TotalFrames - 1)
            StopPlayback();
    }

    private void ShowFrame(int frameIndex)
    {
        if (_videoCapture == null || !_videoCapture.IsOpened())
            return;

        _videoCapture.PosFrames = frameIndex;
        using var frame = new Mat();
        _videoCapture.Read(frame);

        if (!frame.Empty())
        {
            var bitmap = _imageRenderService.ConvertToWriteableBitmap(frame);
            if (bitmap != null)
                CurrentFrame = bitmap;
        }
    }

    private void UpdatePositionText()
    {
        if (VideoFps <= 0) return;
        var currentSec = CurrentPosition / VideoFps;
        var totalSec = TotalFrames / VideoFps;
        PositionText = $"{FormatTime(currentSec)} / {FormatTime(totalSec)}";
    }

    private static string FormatTime(double seconds)
    {
        var ts = TimeSpan.FromSeconds(seconds);
        return ts.TotalMinutes >= 1
            ? $"{(int)ts.TotalMinutes:D2}:{ts.Seconds:D2}"
            : $"00:{(int)ts.TotalSeconds:D2}";
    }

    private static string FormatFileSize(long bytes)
    {
        if (bytes < 1024) return $"{bytes} B";
        if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} KB";
        return $"{bytes / 1024.0 / 1024.0:F1} MB";
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        StopPlayback();
        _videoCapture?.Release();
        _videoCapture?.Dispose();
        GC.SuppressFinalize(this);
    }
}

public class DateFolderItem
{
    public string DateString { get; set; } = string.Empty;
    public string FullPath { get; set; } = string.Empty;
    public int FileCount { get; set; }
    public string Display { get; set; } = string.Empty;
}

public class VideoFileItem
{
    public string FileName { get; set; } = string.Empty;
    public string FullPath { get; set; } = string.Empty;
    public string FileSize { get; set; } = string.Empty;
    public string LastModified { get; set; } = string.Empty;
    public string Display => $"{FileName}  [{FileSize}]";
}
