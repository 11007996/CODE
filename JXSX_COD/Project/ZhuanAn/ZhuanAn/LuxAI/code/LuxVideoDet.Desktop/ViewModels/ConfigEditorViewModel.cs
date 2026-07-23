using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Desktop.Views;
using LuxVideoDet.Localization;
using Serilog;

namespace LuxVideoDet.Desktop.ViewModels;

public partial class ConfigEditorViewModel : ViewModelBase
{
    private static readonly Serilog.ILogger Logger = Log.ForContext<ConfigEditorViewModel>();
    private readonly DetectionConfiguration _config;
    private readonly bool _isNewConfig;
    private readonly IAppLocalizer _appLocalizer;
    private readonly IUiCultureService _uiCulture;
    private string? _statusMessageKey;
    private object[] _statusFormatArgs = Array.Empty<object>();

    public event EventHandler? SaveRequested;
    public event EventHandler? CancelRequested;

    [ObservableProperty]
    private string _windowTitle = string.Empty;

    [ObservableProperty]
    private string _lblSectionBasic = string.Empty;

    [ObservableProperty]
    private string _lblConfigName = string.Empty;

    [ObservableProperty]
    private string _wmConfigName = string.Empty;

    [ObservableProperty]
    private string _lblSectionVideo = string.Empty;

    [ObservableProperty]
    private string _lblVideoType = string.Empty;

    [ObservableProperty]
    private string _lblVideoPath = string.Empty;

    [ObservableProperty]
    private string _wmVideoPath = string.Empty;

    [ObservableProperty]
    private string _btnBrowseVideoText = string.Empty;

    [ObservableProperty]
    private string _chkLoopText = string.Empty;

    [ObservableProperty]
    private string _lblSectionAlgoPool = string.Empty;

    [ObservableProperty]
    private string _lblPickAlgo = string.Empty;

    [ObservableProperty]
    private string _phPickAlgo = string.Empty;

    [ObservableProperty]
    private string _lblAddedAlgos = string.Empty;

    [ObservableProperty]
    private string _btnRemoveSelectedText = string.Empty;

    [ObservableProperty]
    private string _lblSectionInference = string.Empty;

    [ObservableProperty]
    private string _lblModelPath = string.Empty;

    [ObservableProperty]
    private string _wmModelPath = string.Empty;

    [ObservableProperty]
    private string _btnBrowseModelText = string.Empty;

    [ObservableProperty]
    private string _lblInferenceDevice = string.Empty;

    [ObservableProperty]
    private string _lblModelType = string.Empty;

    [ObservableProperty]
    private string _lblConfidence = string.Empty;

    [ObservableProperty]
    private string _lblIou = string.Empty;

    [ObservableProperty]
    private string _lblInputSize = string.Empty;

    [ObservableProperty]
    private string _lblClassesReadonly = string.Empty;

    [ObservableProperty]
    private string _wmClasses = string.Empty;

    [ObservableProperty]
    private string _lblSectionRegions = string.Empty;

    [ObservableProperty]
    private string _btnVisualRegionsText = string.Empty;

    [ObservableProperty]
    private string _lblRegionsJson = string.Empty;

    [ObservableProperty]
    private string _lblSectionStorage = string.Empty;

    [ObservableProperty]
    private string _chkSaveErrorImageText = string.Empty;

    [ObservableProperty]
    private string _chkSaveVideoText = string.Empty;

    [ObservableProperty]
    private string _chkNgVideoSourceResText = string.Empty;

    [ObservableProperty]
    private string _hintRecordingMaxWidth = string.Empty;

    [ObservableProperty]
    private string _lblVideoDuration = string.Empty;

    [ObservableProperty]
    private string _hintNgFps = string.Empty;

    [ObservableProperty]
    private string _lblSectionNotification = string.Empty;

    [ObservableProperty]
    private string _chkNotifyEnableText = string.Empty;

    [ObservableProperty]
    private string _hintNotifyChannels = string.Empty;

    [ObservableProperty]
    private string _btnAddNotifierText = string.Empty;

    [ObservableProperty]
    private string _btnSaveText = string.Empty;

    [ObservableProperty]
    private string _btnCancelText = string.Empty;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private VideoSourceType _sourceType = VideoSourceType.LocalVideo;

    [ObservableProperty]
    private string _sourcePath = string.Empty;

    [ObservableProperty]
    private bool _loop;

    [ObservableProperty]
    private string[] _sourceTypeNames = [];

    [ObservableProperty]
    private ObservableCollection<AlgorithmConfigViewModel> _algorithms = new();

    [ObservableProperty]
    private AlgorithmConfigViewModel? _selectedAlgorithm;

    [ObservableProperty]
    private int _selectedAlgorithmToAddIndex = -1;

    [ObservableProperty]
    private string _statusText = string.Empty;

    public ObservableCollection<string> AvailableAlgorithmTypes { get; } = new();
    public ObservableCollection<string> AvailableModels { get; } = new();

    public int SourceTypeIndex
    {
        get => (int)SourceType;
        set { SourceType = (VideoSourceType)value; OnPropertyChanged(); }
    }

    partial void OnSourceTypeChanged(VideoSourceType value)
        => OnPropertyChanged(nameof(SourceTypeIndex));

    public ConfigEditorViewModel(
        IAppLocalizer appLocalizer,
        IUiCultureService uiCulture,
        DetectionConfiguration? config = null)
    {
        _appLocalizer = appLocalizer;
        _uiCulture = uiCulture;
        _isNewConfig = config == null;
        _config = config ?? new DetectionConfiguration();

        Logger.Debug("打开配置编辑器，模式={Mode}，配置名={ConfigName}",
            _isNewConfig ? "Create" : "Edit",
            config?.Name ?? "新配置");

        _uiCulture.CultureChanged += (_, _) =>
            Dispatcher.UIThread.Post(ApplyEditorLocalization);

        LoadAvailableAlgorithmTypes();
        LoadAvailableModels();

        if (!_isNewConfig)
        {
            LoadFromConfig(_config);
            _statusMessageKey = null;
            _statusFormatArgs = Array.Empty<object>();
            StatusText = string.Empty;
        }
        else
        {
            Name = _appLocalizer.GetString(UiKeys.Editor_DefaultConfigName);
            SetStatus(UiKeys.Editor_StatusNewHint);
        }

        ApplyEditorLocalization();
    }

    private void SetStatus(string key, params object[] args)
    {
        _statusMessageKey = key;
        _statusFormatArgs = args ?? Array.Empty<object>();
        RefreshStatusText();
    }

    private void RefreshStatusText()
    {
        if (string.IsNullOrEmpty(_statusMessageKey))
        {
            StatusText = string.Empty;
            return;
        }

        var template = _appLocalizer.GetString(_statusMessageKey);
        StatusText = _statusFormatArgs.Length == 0
            ? template
            : string.Format(template, _statusFormatArgs);
    }

    public void ApplyEditorLocalization()
    {
        Func<string, string> L = k => _appLocalizer.GetString(k);
        WindowTitle = _isNewConfig
            ? L(UiKeys.Editor_WindowTitleNew)
            : string.Format(L(UiKeys.Editor_WindowTitleEdit), Name);

        LblSectionBasic = L(UiKeys.Editor_SectionBasic);
        LblConfigName = L(UiKeys.Editor_LblConfigName);
        WmConfigName = L(UiKeys.Editor_WatermarkConfigName);
        LblSectionVideo = L(UiKeys.Editor_SectionVideo);
        LblVideoType = L(UiKeys.Editor_LblVideoType);
        LblVideoPath = L(UiKeys.Editor_LblVideoPath);
        WmVideoPath = L(UiKeys.Editor_WatermarkVideoPath);
        BtnBrowseVideoText = L(UiKeys.Editor_BtnBrowseVideo);
        ChkLoopText = L(UiKeys.Editor_ChkLoopLocal);
        LblSectionAlgoPool = L(UiKeys.Editor_SectionAlgoPool);
        LblPickAlgo = L(UiKeys.Editor_LblPickAlgo);
        PhPickAlgo = L(UiKeys.Editor_PlaceholderPickAlgo);
        LblAddedAlgos = L(UiKeys.Editor_LblAddedAlgos);
        BtnRemoveSelectedText = L(UiKeys.Editor_BtnRemoveSelected);
        LblSectionInference = L(UiKeys.Editor_SectionInference);
        LblModelPath = L(UiKeys.Editor_LblModelPath);
        WmModelPath = L(UiKeys.Editor_WatermarkModelPath);
        BtnBrowseModelText = L(UiKeys.Editor_BtnBrowseModel);
        LblInferenceDevice = L(UiKeys.Editor_LblInferenceDevice);
        LblModelType = L(UiKeys.Editor_LblModelType);
        LblConfidence = L(UiKeys.Editor_LblConfidence);
        LblIou = L(UiKeys.Editor_LblIou);
        LblInputSize = L(UiKeys.Editor_LblInputSize);
        LblClassesReadonly = L(UiKeys.Editor_LblClassesReadonly);
        WmClasses = L(UiKeys.Editor_WatermarkClasses);
        LblSectionRegions = L(UiKeys.Editor_SectionRegions);
        BtnVisualRegionsText = L(UiKeys.Editor_BtnVisualRegions);
        LblRegionsJson = L(UiKeys.Editor_LblRegionsJson);
        LblSectionStorage = L(UiKeys.Editor_SectionStorage);
        ChkSaveErrorImageText = L(UiKeys.Editor_ChkSaveErrorImage);
        ChkSaveVideoText = L(UiKeys.Editor_ChkSaveVideo);
        ChkNgVideoSourceResText = L(UiKeys.Editor_ChkNgVideoSourceRes);
        HintRecordingMaxWidth = L(UiKeys.Editor_HintRecordingMaxWidth);
        LblVideoDuration = L(UiKeys.Editor_LblVideoDuration);
        HintNgFps = L(UiKeys.Editor_HintNgFps);
        LblSectionNotification = L(UiKeys.Editor_SectionNotification);
        ChkNotifyEnableText = L(UiKeys.Editor_ChkNotifyEnable);
        HintNotifyChannels = L(UiKeys.Editor_HintNotifyChannels);
        BtnAddNotifierText = L(UiKeys.Editor_BtnAddNotifier);
        BtnSaveText = L(UiKeys.Editor_BtnSave);
        BtnCancelText = L(UiKeys.Editor_BtnCancel);

        SourceTypeNames =
        [
            L(UiKeys.Editor_SourceLocalVideo),
            L(UiKeys.Editor_SourceRtsp),
            L(UiKeys.Editor_SourceCamera)
        ];

        RefreshStatusText();
        foreach (var a in Algorithms)
            a.RefreshLocalization();
    }

    partial void OnNameChanged(string value)
    {
        if (!_isNewConfig)
        {
            Func<string, string> L = k => _appLocalizer.GetString(k);
            WindowTitle = string.Format(L(UiKeys.Editor_WindowTitleEdit), Name);
        }
    }

    private void LoadAvailableAlgorithmTypes()
    {
        var types = DetectionAlgorithmFactory.GetSupportedAlgorithms();
        foreach (var type in types)
        {
            var displayName = GetAlgorithmDisplayName(type);
            AvailableAlgorithmTypes.Add(displayName);
        }
    }

    private static string GetAlgorithmDisplayName(string type)
    {
        var name = DetectionAlgorithmFactory.GetDisplayName(type);
        return $"{name} ({type})";
    }

    private static string ExtractAlgorithmType(string displayName)
    {
        var start = displayName.IndexOf('(');
        var end = displayName.IndexOf(')');
        return start >= 0 && end > start
            ? displayName.Substring(start + 1, end - start - 1).Trim()
            : displayName;
    }

    private void LoadAvailableModels()
    {
        var modelsPath = FindResourcePath("models");
        if (modelsPath == null || !Directory.Exists(modelsPath)) return;

        foreach (var file in Directory.GetFiles(modelsPath, "*.onnx"))
            AvailableModels.Add(file);
    }

    partial void OnSelectedAlgorithmToAddIndexChanged(int value)
    {
        if (value < 0 || value >= AvailableAlgorithmTypes.Count) return;

        var displayName = AvailableAlgorithmTypes[value];
        var algorithmType = ExtractAlgorithmType(displayName);

        var defaultClasses = DetectionAlgorithmFactory.GetDefaultClasses(algorithmType);

        var algorithmVm = new AlgorithmConfigViewModel(_appLocalizer)
        {
            AlgorithmType = algorithmType,
            DisplayName = $"{algorithmType}_{Algorithms.Count + 1}",
            ModelPath = string.Empty,
            Classes = defaultClasses.Count > 0 ? string.Join("\n", defaultClasses) : string.Empty
        };

        Algorithms.Add(algorithmVm);
        SelectedAlgorithm = algorithmVm;

        var suffix = defaultClasses.Count > 0
            ? _appLocalizer.GetString(UiKeys.Editor_StatusAlgoAddedSuffixClasses)
            : _appLocalizer.GetString(UiKeys.Editor_StatusAlgoAddedSuffixNoClasses);
        SetStatus(UiKeys.Editor_StatusAlgoAdded, algorithmVm.DisplayName, suffix);
        Logger.Information("添加算法到配置：AlgorithmType={AlgorithmType}，DisplayName={DisplayName}",
            algorithmType, algorithmVm.DisplayName);

        Dispatcher.UIThread.Post(() => SelectedAlgorithmToAddIndex = -1);
    }

    [RelayCommand]
    private void RemoveAlgorithm()
    {
        if (SelectedAlgorithm == null)
        {
            SetStatus(UiKeys.Editor_StatusSelectAlgoFirst);
            return;
        }

        var toRemove = SelectedAlgorithm;
        Algorithms.Remove(toRemove);
        SelectedAlgorithm = Algorithms.FirstOrDefault();
        SetStatus(UiKeys.Editor_StatusAlgoRemoved, toRemove.DisplayName);
        Logger.Information("从配置中删除算法：DisplayName={DisplayName}，AlgorithmType={AlgorithmType}",
            toRemove.DisplayName, toRemove.AlgorithmType);
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(Name))
        { SetStatus(UiKeys.Editor_StatusEnterName); return; }

        if (string.IsNullOrWhiteSpace(SourcePath))
        { SetStatus(UiKeys.Editor_StatusSetVideoPath); return; }

        if (Algorithms.Count == 0)
        { SetStatus(UiKeys.Editor_StatusAddOneAlgo); return; }

        SaveRequested?.Invoke(this, EventArgs.Empty);
    }

    [RelayCommand]
    private void Cancel() => CancelRequested?.Invoke(this, EventArgs.Empty);

    private void LoadFromConfig(DetectionConfiguration config)
    {
        Name = config.Name;
        SourceType = config.VideoSource.Type;
        SourcePath = config.VideoSource.Source;
        Loop = config.VideoSource.Loop;

        Algorithms.Clear();
        foreach (var algoConfig in config.Algorithms)
            Algorithms.Add(AlgorithmConfigViewModel.FromAlgorithmConfig(algoConfig, _appLocalizer));

        if (Algorithms.Count > 0)
            SelectedAlgorithm = Algorithms[0];
    }

    public DetectionConfiguration GetConfiguration()
    {
        _config.Name = Name;
        _config.VideoSource.Type = SourceType;
        _config.VideoSource.Source = SourcePath;
        _config.VideoSource.Loop = Loop;
        _config.Algorithms = Algorithms.Select(a => a.ToAlgorithmConfig()).ToList();
        _config.UpdatedAt = DateTime.Now;
        return _config;
    }

    [RelayCommand]
    private async Task BrowseSource()
    {
        var topLevel = GetTopLevel();
        if (topLevel == null) return;

        var suggestedPath = FindResourcePath("videos");
        var suggestedFolder = suggestedPath != null && Directory.Exists(suggestedPath)
            ? await topLevel.StorageProvider.TryGetFolderFromPathAsync(new Uri($"file:///{suggestedPath.Replace('\\', '/')}"))
            : null;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = _appLocalizer.GetString(UiKeys.Editor_PickerTitleVideo),
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType(_appLocalizer.GetString(UiKeys.Editor_PickerFilterVideo))
                {
                    Patterns = new[] { "*.mp4", "*.avi", "*.mkv", "*.mov", "*.flv" }
                }
            },
            SuggestedStartLocation = suggestedFolder
        });

        if (files.Count > 0)
            SourcePath = files[0].Path.LocalPath;
    }

    [RelayCommand]
    private async Task BrowseModel()
    {
        if (SelectedAlgorithm == null)
        { SetStatus(UiKeys.Editor_StatusSelectAlgoFirst); return; }

        var topLevel = GetTopLevel();
        if (topLevel == null) return;

        var suggestedPath = FindResourcePath("models");
        var suggestedFolder = suggestedPath != null && Directory.Exists(suggestedPath)
            ? await topLevel.StorageProvider.TryGetFolderFromPathAsync(new Uri($"file:///{suggestedPath.Replace('\\', '/')}"))
            : null;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = _appLocalizer.GetString(UiKeys.Editor_PickerTitleModel),
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType(_appLocalizer.GetString(UiKeys.Editor_PickerFilterOnnx))
                {
                    Patterns = new[] { "*.onnx" }
                }
            },
            SuggestedStartLocation = suggestedFolder
        });

        if (files.Count > 0)
            SelectedAlgorithm.ModelPath = files[0].Path.LocalPath;
    }

    [RelayCommand]
    private async Task EditRegions()
    {
        if (SelectedAlgorithm == null)
        { SetStatus(UiKeys.Editor_StatusSelectAlgoFirst); return; }
        if (string.IsNullOrWhiteSpace(SourcePath))
        { SetStatus(UiKeys.Editor_StatusSetVideoPath); return; }

        var regionDefinitions = DetectionAlgorithmFactory.GetRequiredRegions(SelectedAlgorithm.AlgorithmType);
        Logger.Debug("准备打开区域编辑器：AlgorithmType={AlgorithmType}，DisplayName={DisplayName}，RequiredRegionCount={RequiredRegionCount}",
            SelectedAlgorithm.AlgorithmType, SelectedAlgorithm.DisplayName, regionDefinitions.Count);

        List<RegionConfig>? existingRegions = null;
        try
        {
            existingRegions = System.Text.Json.JsonSerializer.Deserialize<List<RegionConfig>>(
                SelectedAlgorithm.RegionsJson);
            Logger.Debug("解析已有区域成功：Algorithm={AlgorithmType}，ExistingRegionCount={ExistingRegionCount}",
                SelectedAlgorithm.AlgorithmType, existingRegions?.Count ?? 0);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "解析已有区域 JSON 失败：Algorithm={AlgorithmType}", SelectedAlgorithm.AlgorithmType);
        }

        var owner = GetMainWindow();
        if (owner is null)
        {
            Logger.Warning("无法打开区域编辑器：主窗口不可用");
            return;
        }

        var window = new RegionEditorWindow(SourcePath, SourceType, regionDefinitions, existingRegions);
        var result = await window.ShowDialog<List<RegionConfig>?>(owner);

        if (result != null)
        {
            SelectedAlgorithm.RegionsJson = System.Text.Json.JsonSerializer.Serialize(
                result, ConfigurationJsonOptions.ForRegionsJsonIndented);
            SetStatus(UiKeys.Editor_StatusRegionsUpdated, result.Count);
            Logger.Information("区域编辑完成：Algorithm={AlgorithmType}，DisplayName={DisplayName}，RegionCount={RegionCount}",
                SelectedAlgorithm.AlgorithmType, SelectedAlgorithm.DisplayName, result.Count);
        }
        else
        {
            Logger.Information("区域编辑已取消：Algorithm={AlgorithmType}，DisplayName={DisplayName}",
                SelectedAlgorithm.AlgorithmType, SelectedAlgorithm.DisplayName);
        }
    }

    private static TopLevel? GetTopLevel()
        => Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

    private static Window? GetMainWindow()
        => Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

    private static string? FindResourcePath(string subFolder)
    {
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        for (var i = 0; i < 6; i++)
        {
            var candidate = Path.Combine(dir, "resources", subFolder);
            if (Directory.Exists(candidate)) return candidate;

            var parent = Directory.GetParent(dir)?.FullName;
            if (parent == null || parent == dir) break;
            dir = parent;
        }
        return null;
    }
}
