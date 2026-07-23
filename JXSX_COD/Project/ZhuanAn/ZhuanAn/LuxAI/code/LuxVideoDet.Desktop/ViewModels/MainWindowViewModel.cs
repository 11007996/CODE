using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Controls.Templates;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxVideoDet.Core;
using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Algorithm.Ui;
using LuxVideoDet.Core.Storage;
using LuxVideoDet.Desktop.Models;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Desktop.Views;
using LuxVideoDet.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Desktop.ViewModels;

file sealed class LangPick
{
    public required string Code { get; init; }
    public required string Text { get; init; }
}

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ConfigurationService _configService;
    private readonly DetectionService _detectionService;
    private readonly LogService _logService;
    private readonly ImageRenderService _imageRenderService;
    private readonly PerformanceMonitorService _performanceMonitor;
    private readonly ProductionStatisticsStore _statsStore;
    private readonly ILogger<MainWindowViewModel> _logger;
    private readonly DispatcherTimer _statsTimer;
    private readonly DateTime _startTime = DateTime.Now;
    private Views.FullscreenPreviewWindow? _fullscreenWindow;
    private readonly Dictionary<string, Views.FloatingPreviewWindow> _floatingWindows = new();
    private readonly List<LogEntry> _allLogs = new();

    // Theme
    [ObservableProperty]
    private bool _isDarkTheme;

    // Search
    [ObservableProperty]
    private string _searchText = string.Empty;

    // Configurations
    [ObservableProperty]
    private ObservableCollection<ConfigItemViewModel> _configurations = new();

    [ObservableProperty]
    private ObservableCollection<ConfigItemViewModel> _filteredConfigurations = new();

    // Preview
    [ObservableProperty]
    private WriteableBitmap? _previewImage;

    [ObservableProperty]
    private List<ProcessStepInfo> _processSteps = [];

    [ObservableProperty]
    private string _currentPreviewConfig = string.Empty;

    [ObservableProperty]
    private bool _hasPreview;

    /// <summary>预览区九宫格：每格一组算法操作按钮（下标与 <see cref="AlgorithmUiActionPlacement"/> 数值 0…8 一致）。</summary>
    private readonly ObservableCollection<AlgorithmUiActionItemVm>[] _previewUiByPlacement =
        Enumerable.Range(0, 9).Select(_ => new ObservableCollection<AlgorithmUiActionItemVm>()).ToArray();

    [ObservableProperty]
    private bool _hasPreviewUiActions;

    public ObservableCollection<AlgorithmUiActionItemVm> PreviewUiActionsTopLeft =>
        _previewUiByPlacement[(int)AlgorithmUiActionPlacement.TopLeft];

    public ObservableCollection<AlgorithmUiActionItemVm> PreviewUiActionsTopCenter =>
        _previewUiByPlacement[(int)AlgorithmUiActionPlacement.TopCenter];

    public ObservableCollection<AlgorithmUiActionItemVm> PreviewUiActionsTopRight =>
        _previewUiByPlacement[(int)AlgorithmUiActionPlacement.TopRight];

    public ObservableCollection<AlgorithmUiActionItemVm> PreviewUiActionsMiddleLeft =>
        _previewUiByPlacement[(int)AlgorithmUiActionPlacement.MiddleLeft];

    public ObservableCollection<AlgorithmUiActionItemVm> PreviewUiActionsCenter =>
        _previewUiByPlacement[(int)AlgorithmUiActionPlacement.Center];

    public ObservableCollection<AlgorithmUiActionItemVm> PreviewUiActionsMiddleRight =>
        _previewUiByPlacement[(int)AlgorithmUiActionPlacement.MiddleRight];

    public ObservableCollection<AlgorithmUiActionItemVm> PreviewUiActionsBottomLeft =>
        _previewUiByPlacement[(int)AlgorithmUiActionPlacement.BottomLeft];

    public ObservableCollection<AlgorithmUiActionItemVm> PreviewUiActionsBottomCenter =>
        _previewUiByPlacement[(int)AlgorithmUiActionPlacement.BottomCenter];

    public ObservableCollection<AlgorithmUiActionItemVm> PreviewUiActionsBottomRight =>
        _previewUiByPlacement[(int)AlgorithmUiActionPlacement.BottomRight];

    [ObservableProperty]
    private ObservableCollection<PreviewSourceItem> _previewSources = new();

    [ObservableProperty]
    private PreviewSourceItem? _selectedPreviewSource;

    [ObservableProperty]
    private bool _showDetectionBoxes = true;

    [ObservableProperty]
    private bool _showRegions = true;

    [ObservableProperty]
    private bool _showLabels = true;

    // System Status
    [ObservableProperty]
    private int _runningCount;

    [ObservableProperty]
    private int _totalConfigCount;

    [ObservableProperty]
    private string _uptime = "00:00:00";

    // Performance
    [ObservableProperty]
    private double _cpuUsage;

    [ObservableProperty]
    private string _memoryUsage = "0 MB";

    [ObservableProperty]
    private double _memoryUsagePercent;

    [ObservableProperty]
    private bool _hasGpu;

    [ObservableProperty]
    private double _gpuUsage;

    [ObservableProperty]
    private double _averagePipelineMs;

    // Recent Detections
    [ObservableProperty]
    private ObservableCollection<RecentDetection> _recentDetections = new();

    // Production Stats
    [ObservableProperty]
    private int _todayOkCount;

    [ObservableProperty]
    private int _todayNgCount;

    [ObservableProperty]
    private int _todayTotalCount;

    [ObservableProperty]
    private int _productTotalSum;

    [ObservableProperty]
    private bool _hasProductionStats;

    // Per-selected-config stats (shown below preview header)
    [ObservableProperty]
    private int _selectedConfigOk;

    [ObservableProperty]
    private int _selectedConfigNg;

    [ObservableProperty]
    private string _selectedConfigYield = "—";

    [ObservableProperty]
    private bool _hasSelectedConfigStats;

    // Logs
    [ObservableProperty]
    private bool _isLogPanelExpanded;

    [ObservableProperty]
    private int _logCount;

    [ObservableProperty]
    private string _logPanelExpandIcon = "M7.41,8.58L12,13.17L16.59,8.58L18,10L12,16L6,10L7.41,8.58Z";

    [ObservableProperty]
    private bool _showDebugLogs;

    [ObservableProperty]
    private bool _showInfoLogs = true;

    [ObservableProperty]
    private bool _showWarningLogs = true;

    [ObservableProperty]
    private bool _showErrorLogs = true;

    [ObservableProperty]
    private string _logSearchText = string.Empty;

    [ObservableProperty]
    private ObservableCollection<LogEntry> _filteredLogs = new();

    // 本地化（工具栏/窗口标题等）
    [ObservableProperty]
    private string _windowTitle = string.Empty;

    [ObservableProperty]
    private string _toolbarNewConfig = string.Empty;

    [ObservableProperty]
    private string _toolbarImportConfig = string.Empty;

    [ObservableProperty]
    private string _toolbarStartAll = string.Empty;

    [ObservableProperty]
    private string _toolbarStopAll = string.Empty;

    [ObservableProperty]
    private string _tooltipToggleTheme = string.Empty;

    [ObservableProperty]
    private string _tooltipSettings = string.Empty;

    [ObservableProperty]
    private string _toolbarLanguageLabel = string.Empty;

    [ObservableProperty]
    private int _toolbarLanguageIndex;

    [ObservableProperty]
    private string _searchConfigWatermark = string.Empty;

    [ObservableProperty]
    private string _livePreviewTitle = string.Empty;

    [ObservableProperty]
    private string _previewComboPlaceholder = string.Empty;

    [ObservableProperty]
    private string _tooltipPreviewBoxes = string.Empty;

    [ObservableProperty]
    private string _tooltipPreviewRegions = string.Empty;

    [ObservableProperty]
    private string _tooltipPreviewLabels = string.Empty;

    [ObservableProperty]
    private string _tooltipPreviewPopOut = string.Empty;

    [ObservableProperty]
    private string _tooltipPreviewFullscreen = string.Empty;

    [ObservableProperty]
    private string _tooltipPreviewSnapshot = string.Empty;

    [ObservableProperty]
    private string _previewEmptyTitle = string.Empty;

    [ObservableProperty]
    private string _previewEmptySubtitle = string.Empty;

    [ObservableProperty]
    private string _stripYieldLabel = string.Empty;

    [ObservableProperty]
    private string _systemStatusTitle = string.Empty;

    [ObservableProperty]
    private string _systemHealthyText = string.Empty;

    [ObservableProperty]
    private string _systemLabelRunning = string.Empty;

    [ObservableProperty]
    private string _systemLabelTotal = string.Empty;

    [ObservableProperty]
    private string _systemLabelUptime = string.Empty;

    [ObservableProperty]
    private string _runningConfigsDisplay = string.Empty;

    [ObservableProperty]
    private string _totalConfigsDisplay = string.Empty;

    [ObservableProperty]
    private string _performanceTitle = string.Empty;

    [ObservableProperty]
    private string _perfLabelCpu = string.Empty;

    [ObservableProperty]
    private string _perfLabelMemory = string.Empty;

    [ObservableProperty]
    private string _perfLabelGpu = string.Empty;

    [ObservableProperty]
    private string _perfLabelAvgFrame = string.Empty;

    [ObservableProperty]
    private string _recentDetectionsTitle = string.Empty;

    [ObservableProperty]
    private string _productionStatsTitle = string.Empty;

    [ObservableProperty]
    private string _productionTotalCaption = string.Empty;

    [ObservableProperty]
    private string _productionTotalCaption2 = string.Empty;

    [ObservableProperty]
    private string _logsTitle = string.Empty;

    [ObservableProperty]
    private string _logFilterDebug = string.Empty;

    [ObservableProperty]
    private string _logFilterInfo = string.Empty;

    [ObservableProperty]
    private string _logFilterWarning = string.Empty;

    [ObservableProperty]
    private string _logFilterError = string.Empty;

    [ObservableProperty]
    private string _logSearchWatermark = string.Empty;

    [ObservableProperty]
    private string _logBtnClear = string.Empty;

    [ObservableProperty]
    private string _logBtnExport = string.Empty;

    [ObservableProperty]
    private string _fullscreenViewTitle = string.Empty;

    [ObservableProperty]
    private string _fullscreenEscHintText = string.Empty;

    [ObservableProperty]
    private string _fullscreenOverlayConfigName = string.Empty;

    [ObservableProperty]
    private string _fullscreenOverlayPipelineText = string.Empty;

    private double _fullscreenLastPipelineMs;

    public ObservableCollection<string> ToolbarLanguageOptions { get; } = new();

    private bool _suppressToolbarLanguageIndex;

    private readonly IAppLocalizer _appLocalizer;
    private readonly IUiCultureService _uiCulture;

    public MainWindowViewModel(
        ConfigurationService configService,
        DetectionService detectionService,
        LogService logService,
        ImageRenderService imageRenderService,
        PerformanceMonitorService performanceMonitor,
        ProductionStatisticsStore statsStore,
        ILogger<MainWindowViewModel> logger,
        IAppLocalizer appLocalizer,
        IUiCultureService uiCulture)
    {
        _configService = configService;
        _detectionService = detectionService;
        _logService = logService;
        _imageRenderService = imageRenderService;
        _performanceMonitor = performanceMonitor;
        _statsStore = statsStore;
        _logger = logger;
        _appLocalizer = appLocalizer;
        _uiCulture = uiCulture;

        // 订阅事件
        _detectionService.ResultAvailable += OnDetectionResult;
        _detectionService.SessionStarted += OnSessionStarted;
        _detectionService.SessionStopped += OnSessionStopped;
        _detectionService.SessionError += OnSessionError;
        _logService.LogReceived += OnLogReceived;
        _performanceMonitor.PerformanceUpdated += OnPerformanceUpdated;
        
        // 订阅配置变更事件
        _configService.ConfigurationChanged += OnConfigurationChanged;
        _configService.ConfigurationDeleted += OnConfigurationDeleted;

        _uiCulture.CultureChanged += (_, _) =>
            Dispatcher.UIThread.Post(RefreshLocalizedChrome);

        RefreshLocalizedChrome();

        // 定时更新统计
        _statsTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _statsTimer.Tick += UpdateStats;
        _statsTimer.Start();

        LoadExistingLogs();

        // 加载配置
        _ = LoadConfigurationsAsync();
    }

    private void LoadExistingLogs()
    {
        foreach (var logEntry in _logService.Logs)
        {
            var vm = new LogEntry
            {
                Level = logEntry.Level.ToString(),
                LevelShort = GetLogLevelShort(logEntry.Level),
                LevelColor = GetLogLevelColor(logEntry.Level),
                Timestamp = logEntry.Timestamp,
                Source = logEntry.Category,
                Message = logEntry.Message
            };

            _allLogs.Add(vm);
            if (ShouldShowLog(vm))
            {
                FilteredLogs.Add(vm);
            }
        }

        LogCount = _allLogs.Count;
    }

    private async Task LoadConfigurationsAsync()
    {
        try
        {
            var configs = await _configService.ListAsync();
            
            Dispatcher.UIThread.Post(() =>
            {
                Configurations.Clear();
                foreach (var config in configs)
                {
                    var vm = new ConfigItemViewModel(config, _detectionService, _configService, _logger, _appLocalizer, _uiCulture, _imageRenderService);
                    Configurations.Add(vm);
                }
                TotalConfigCount = Configurations.Count;
                RebuildFilteredConfigurations();
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载配置失败");
        }
    }

    private void OnDetectionResult(object? sender, DetectionResultEventArgs e)
    {
        var session = _detectionService.GetSession(e.ConfigId);
        if (session == null) return;

        // ===== 新增：解析流水线步骤数据，更新UI绑定列表 =====
        if (e.Result.ExtraData.TryGetValue("StepTexts", out var textsObj)
    && e.Result.ExtraData.TryGetValue("StepPrefixes", out var prefixesObj)
    && e.Result.ExtraData.TryGetValue("StepColorHexes", out var colorsObj))
        {
            string[] textArr = Array.Empty<string>();
            string[] preArr = Array.Empty<string>();
            string[] colorArr = Array.Empty<string>();

            // 兼容两种序列化输出：IEnumerable<string> / object[]
            if (textsObj is IEnumerable<string> tEnum)
                textArr = tEnum.ToArray();
            else if (textsObj is object[] tObjArr)
                textArr = tObjArr.Cast<string>().ToArray();

            if (prefixesObj is IEnumerable<string> pEnum)
                preArr = pEnum.ToArray();
            else if (prefixesObj is object[] pObjArr)
                preArr = pObjArr.Cast<string>().ToArray();

            if (colorsObj is IEnumerable<string> cEnum)
                colorArr = cEnum.ToArray();
            else if (colorsObj is object[] cObjArr)
                colorArr = cObjArr.Cast<string>().ToArray();

            if (textArr.Length == preArr.Length && textArr.Length == colorArr.Length && textArr.Length > 0)
            {
                var stepList = new List<ProcessStepInfo>();
                for (int i = 0; i < textArr.Length; i++)
                {
                    // 强制空值兜底，杜绝null赋值
                    var text = textArr[i]?.Trim() ?? string.Empty;
                    var pre = preArr[i]?.Trim() ?? "--";
                    var color = colorArr[i]?.Trim() ?? "#646464";

                    stepList.Add(new ProcessStepInfo
                    {
                        DisplayText = text,
                        Prefix = pre,
                        ColorHex = color
                    });
                }
                Dispatcher.UIThread.Post(() => ProcessSteps = stepList, DispatcherPriority.Normal);
            }
        }
        // ====================================================

        var isMainPreview = SelectedPreviewSource != null && session.ConfigId == SelectedPreviewSource.ConfigId;
        var hasFloating = _floatingWindows.ContainsKey(session.ConfigId);

        if (isMainPreview || hasFloating)
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (e.Result.AnnotatedFrame != null)
                {
                    var bitmap = _imageRenderService.ConvertToWriteableBitmap(e.Result.AnnotatedFrame.Mat);
                    if (bitmap != null)
                    {
                        if (isMainPreview)
                        {
                            PreviewImage = bitmap;
                            HasPreview = true;
                            if (_fullscreenWindow != null)
                            {
                                SyncFullscreenPipelineOverlay(session.ConfigName, session.AveragePipelineMs);
                                _fullscreenWindow.UpdatePreviewImage(bitmap);
                            }
                        }

                        if (_floatingWindows.TryGetValue(session.ConfigId, out var floatingWin))
                        {
                            floatingWin.UpdatePreview(bitmap, session.AveragePipelineMs, e.Result.StateMessage);
                        }
                    }
                }

                // 与「错误态」对齐：仅在 CurrentState 为 ERROR 时启用「确认已处理」等按钮
                if (isMainPreview && HasPreviewUiActions)
                {
                    var inError = string.Equals(e.Result.CurrentState, "ERROR", StringComparison.OrdinalIgnoreCase);
                    foreach (var item in EnumerateAllPreviewUiActions())
                    {
                        if (string.Equals(item.AlgorithmType, e.AlgorithmType, StringComparison.OrdinalIgnoreCase))
                            item.ApplyPreviewActionState(inError);
                    }
                }
            });
        }

        // 根据判定结果更新持久化统计
        if (e.Result.Judgement == Core.Algorithm.Results.ProductionJudgement.OK)
        {
            _statsStore.IncrementOk(session.ConfigName);
        }
        else if (e.Result.Judgement == Core.Algorithm.Results.ProductionJudgement.NG)
        {
            _statsStore.IncrementNg(session.ConfigName);
            AddRecentDetection(session.ConfigName,
                e.Result.NotificationMessage ?? e.Result.StateMessage ?? "NG", "#F44336");
        }

        // 有通知时也记录到最近检测
        if (e.Result.ShouldNotify && e.Result.Judgement != Core.Algorithm.Results.ProductionJudgement.NG)
        {
            var color = e.Result.NotificationLevel switch
            {
                Core.Algorithm.Results.NotificationLevel.Warning => "#FF9800",
                Core.Algorithm.Results.NotificationLevel.Error => "#F44336",
                Core.Algorithm.Results.NotificationLevel.Critical => "#D32F2F",
                _ => "#FFA500"
            };
            AddRecentDetection(session.ConfigName, e.Result.NotificationMessage ?? "告警", color);
        }
    }

    private void OnDetectionResult2(object? sender, DetectionResultEventArgs e)
    {
        var session = _detectionService.GetSession(e.ConfigId);
        if (session == null) return;

        // 检查是否需要更新主预览或浮动窗口
        var isMainPreview = SelectedPreviewSource != null && session.ConfigId == SelectedPreviewSource.ConfigId;
        var hasFloating = _floatingWindows.ContainsKey(session.ConfigId);

        if (isMainPreview || hasFloating)
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (e.Result.AnnotatedFrame != null)
                {
                    var bitmap = _imageRenderService.ConvertToWriteableBitmap(e.Result.AnnotatedFrame.Mat);
                    if (bitmap != null)
                    {
                        if (isMainPreview)
                        {
                            PreviewImage = bitmap;
                            HasPreview = true;
                            if (_fullscreenWindow != null)
                            {
                                SyncFullscreenPipelineOverlay(session.ConfigName, session.AveragePipelineMs);
                                _fullscreenWindow.UpdatePreviewImage(bitmap);
                            }
                        }

                        if (_floatingWindows.TryGetValue(session.ConfigId, out var floatingWin))
                        {
                            floatingWin.UpdatePreview(bitmap, session.AveragePipelineMs, e.Result.StateMessage);
                        }
                    }
                }

                // 与「错误态」对齐：仅在 CurrentState 为 ERROR 时启用「确认已处理」等按钮
                if (isMainPreview && HasPreviewUiActions)
                {
                    var inError = string.Equals(e.Result.CurrentState, "ERROR", StringComparison.OrdinalIgnoreCase);
                    foreach (var item in EnumerateAllPreviewUiActions())
                    {
                        if (string.Equals(item.AlgorithmType, e.AlgorithmType, StringComparison.OrdinalIgnoreCase))
                            item.ApplyPreviewActionState(inError);
                    }
                }
            });
        }

        // 根据判定结果更新持久化统计
        if (e.Result.Judgement == Core.Algorithm.Results.ProductionJudgement.OK)
        {
            _statsStore.IncrementOk(session.ConfigName);
        }
        else if (e.Result.Judgement == Core.Algorithm.Results.ProductionJudgement.NG)
        {
            _statsStore.IncrementNg(session.ConfigName);
            AddRecentDetection(session.ConfigName, 
                e.Result.NotificationMessage ?? e.Result.StateMessage ?? "NG", "#F44336");
        }

        // 有通知时也记录到最近检测
        if (e.Result.ShouldNotify && e.Result.Judgement != Core.Algorithm.Results.ProductionJudgement.NG)
        {
            var color = e.Result.NotificationLevel switch
            {
                Core.Algorithm.Results.NotificationLevel.Warning => "#FF9800",
                Core.Algorithm.Results.NotificationLevel.Error => "#F44336",
                Core.Algorithm.Results.NotificationLevel.Critical => "#D32F2F",
                _ => "#FFA500"
            };
            AddRecentDetection(session.ConfigName, e.Result.NotificationMessage ?? "告警", color);
        }
    }

    private void OnSessionStarted(object? sender, string configId)
    {
        UpdateRunningCount();
        var config = Configurations.FirstOrDefault(c => c.ConfigId == configId);
        if (config != null)
        {
            AddRecentDetection(config.Name, "启动成功");
        }

        Dispatcher.UIThread.Post(() =>
        {
            var item = new PreviewSourceItem { ConfigId = configId, ConfigName = config?.Name ?? configId };
            PreviewSources.Add(item);
            // 自动选中新启动的会话
            SelectedPreviewSource = item;
            // OnSelectedPreviewSourceChanged 会 Refresh；此处再调一次避免 ComboBox 未触发变更等边缘情况
            RefreshPreviewUiActions();
        });
    }

    private void OnSessionStopped(object? sender, string configId)
    {
        UpdateRunningCount();

        Dispatcher.UIThread.Post(() =>
        {
            var item = PreviewSources.FirstOrDefault(p => p.ConfigId == configId);
            if (item != null)
            {
                PreviewSources.Remove(item);
            }

            // 关闭对应的浮动窗口
            if (_floatingWindows.TryGetValue(configId, out var floatingWin))
            {
                floatingWin.Close();
                // Closed 事件中会自动 Remove
            }

            // 如果当前选中的被停止了，切换到下一个
            if (SelectedPreviewSource?.ConfigId == configId || SelectedPreviewSource == null)
            {
                SelectedPreviewSource = PreviewSources.FirstOrDefault();
                if (SelectedPreviewSource == null)
                {
                    HasPreview = false;
                    CurrentPreviewConfig = _appLocalizer.GetString(UiKeys.Main_PreviewPlaceholder);
                }
            }
        });
    }

    private void OnSessionError(object? sender, (string ConfigId, Exception Error) e)
    {
        var config = Configurations.FirstOrDefault(c => c.ConfigId == e.ConfigId);
        if (config != null)
        {
            AddRecentDetection(config.Name, $"错误: {e.Error.Message}", "#F44336");
        }
    }

    private void OnLogReceived(object? sender, Core.Logging.LogEntry logEntry)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var vm = new LogEntry
            {
                Level = logEntry.Level.ToString(),
                LevelShort = GetLogLevelShort(logEntry.Level),
                LevelColor = GetLogLevelColor(logEntry.Level),
                Timestamp = logEntry.Timestamp,
                Source = logEntry.Category,
                Message = logEntry.Message
            };

            _allLogs.Add(vm);
            while (_allLogs.Count > 1000)
                _allLogs.RemoveAt(0);

            LogCount = _allLogs.Count;

            if (ShouldShowLog(vm))
            {
                FilteredLogs.Add(vm);
            }
        });
    }

    private bool ShouldShowLog(LogEntry log)
    {
        var levelVisible = log.Level switch
        {
            "Debug" or "Trace" => ShowDebugLogs,
            "Information" => ShowInfoLogs,
            "Warning" => ShowWarningLogs,
            "Error" or "Critical" => ShowErrorLogs,
            _ => true
        };

        if (!levelVisible) return false;

        if (!string.IsNullOrWhiteSpace(LogSearchText))
        {
            return log.Message.Contains(LogSearchText, StringComparison.OrdinalIgnoreCase)
                || log.Source.Contains(LogSearchText, StringComparison.OrdinalIgnoreCase);
        }

        return true;
    }

    private void RefreshFilteredLogs()
    {
        FilteredLogs.Clear();
        foreach (var log in _allLogs)
        {
            if (ShouldShowLog(log))
                FilteredLogs.Add(log);
        }
    }

    partial void OnShowDebugLogsChanged(bool value) => RefreshFilteredLogs();
    partial void OnShowInfoLogsChanged(bool value) => RefreshFilteredLogs();
    partial void OnShowWarningLogsChanged(bool value) => RefreshFilteredLogs();
    partial void OnShowErrorLogsChanged(bool value) => RefreshFilteredLogs();
    partial void OnLogSearchTextChanged(string value) => RefreshFilteredLogs();

    partial void OnShowDetectionBoxesChanged(bool value) => SyncRenderOptions();
    partial void OnShowRegionsChanged(bool value) => SyncRenderOptions();
    partial void OnShowLabelsChanged(bool value) => SyncRenderOptions();

    partial void OnSelectedPreviewSourceChanged(PreviewSourceItem? value)
    {
        CurrentPreviewConfig = value?.ConfigName ?? _appLocalizer.GetString(UiKeys.Main_PreviewPlaceholder);
        UpdateSelectedConfigStats();
        RefreshPreviewUiActions();
    }

    /// <summary>
    /// 根据当前选中会话的配置，加载 <see cref="DetectionAlgorithmFactory.GetUiActionDefinitions"/> 中的按钮。
    /// </summary>
    private void RefreshPreviewUiActions()
    {
        foreach (var c in _previewUiByPlacement)
            c.Clear();
        HasPreviewUiActions = false;

        var src = SelectedPreviewSource;
        if (src == null)
            return;

        var session = _detectionService.GetSession(src.ConfigId);
        if (session?.Configuration?.Algorithms == null)
            return;

        foreach (var algo in session.Configuration.Algorithms.Where(a => a.Enabled))
        {
            var defs = DetectionAlgorithmFactory.GetUiActionDefinitions(algo.AlgorithmType);
            if (defs.Count == 0)
                continue;

            // 配置里为该算法启用了 PLC 通知时才渲染圆形按钮；与运行时是否成功连接 PLC 无关
            if (!algo.Notification.HasEnabledPlcNotifier())
                continue;

            var algType = algo.AlgorithmType;
            var configId = src.ConfigId;
            foreach (var d in defs)
            {
                var actionId = d.ActionId;
                var placement = d.Placement;
                if ((int)placement < 0 || (int)placement >= _previewUiByPlacement.Length)
                    placement = AlgorithmUiActionPlacement.BottomRight;

                _previewUiByPlacement[(int)placement].Add(new AlgorithmUiActionItemVm(
                    algType,
                    d.DisplayName,
                    d.Description,
                    new RelayCommand(() =>
                    {
                        if (!_detectionService.TryInvokeUiAction(configId, algType, actionId, out var err))
                            _logger.LogWarning("UI 操作失败: {Err}", err ?? "");
                        else
                            _logger.LogInformation("UI 操作已执行: {ActionId}", actionId);
                    }),
                    placement,
                    d.DisplayNameWhenOk,
                    d.DisplayNameWhenNg));
            }
        }

        HasPreviewUiActions = _previewUiByPlacement.Any(c => c.Count > 0);
    }

    private IEnumerable<AlgorithmUiActionItemVm> EnumerateAllPreviewUiActions()
    {
        foreach (var c in _previewUiByPlacement)
        {
            foreach (var item in c)
                yield return item;
        }
    }

    private void SyncRenderOptions()
    {
        _detectionService.UpdateRenderOptions(ShowDetectionBoxes, ShowRegions, ShowLabels);
    }

    private void OnPerformanceUpdated(object? sender, PerformanceData data)
    {
        Dispatcher.UIThread.Post(() =>
        {
            CpuUsage = data.CpuUsage;
            MemoryUsage = $"{data.MemoryUsageBytes / 1024.0 / 1024.0:F1} MB";
            MemoryUsagePercent = data.MemoryUsagePercent;
            HasGpu = data.HasGpu;
            GpuUsage = data.GpuUsage;
        });
    }

    private void OnConfigurationChanged(object? sender, DetectionConfiguration config)
    {
        Dispatcher.UIThread.Post(() =>
        {
            // 查找是否已存在
            var existing = Configurations.FirstOrDefault(c => c.ConfigId == config.Id);
            if (existing != null)
            {
                // 同步底层配置对象，确保启动使用最新推理参数（如 Device=GPU）
                existing.UpdateFromConfiguration(config);
                _logger.LogInformation("配置已更新: {ConfigName}", config.Name);
            }
            else
            {
                // 添加新配置
                var vm = new ConfigItemViewModel(config, _detectionService, _configService, _logger, _appLocalizer, _uiCulture, _imageRenderService);
                Configurations.Add(vm);
                TotalConfigCount = Configurations.Count;
                _logger.LogInformation("配置已添加: {ConfigName}", config.Name);
            }

            RebuildFilteredConfigurations();
        });
    }

    private void OnConfigurationDeleted(object? sender, string configId)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var config = Configurations.FirstOrDefault(c => c.ConfigId == configId);
            if (config != null)
            {
                Configurations.Remove(config);
                TotalConfigCount = Configurations.Count;
                _logger.LogInformation("配置已删除: {ConfigId}", configId);
                RebuildFilteredConfigurations();
            }
        });
    }

    private void UpdateStats(object? sender, EventArgs e)
    {
        var elapsed = DateTime.Now - _startTime;
        Uptime = $"{elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";

        var sessions = _detectionService.GetAllSessions();
        if (sessions.Count > 0)
        {
            AveragePipelineMs = sessions.Average(s => s.AveragePipelineMs);
        }

        // 从持久化存储读取：右侧面板为「所有配置 × 全部历史」累计
        var hasStats = _statsStore.HasAnyRecordedStats() || sessions.Any(s =>
            s.Algorithms.Any(a => a is IProductionStatisticsProvider));

        HasProductionStats = hasStats;
        if (hasStats)
        {
            var (ok, ng,pcount) = _statsStore.GetAllTimeTotalStats();
            TodayOkCount = ok;
            TodayNgCount = ng;
            TodayTotalCount = ok + ng;
            ProductTotalSum = pcount;
        }

        UpdateSelectedConfigStats();
    }

    private void UpdateSelectedConfigStats()
    {
        var selected = SelectedPreviewSource;
        if (selected == null)
        {
            HasSelectedConfigStats = false;
            return;
        }

        var (ok, ng) = _statsStore.GetAllTimeStatsForConfig(selected.ConfigName);
        SelectedConfigOk = ok;
        SelectedConfigNg = ng;
        var total = ok + ng;
        SelectedConfigYield = total > 0
            ? $"{(double)ok / total * 100:F1}%"
            : "—";
        HasSelectedConfigStats = total > 0;
    }

    private void UpdateRunningCount()
    {
        var sessions = _detectionService.GetAllSessions();
        RunningCount = sessions.Count(s => s.IsRunning);
    }

    private void AddRecentDetection(string configName, string message, string color = "#4CAF50")
    {
        Dispatcher.UIThread.Post(() =>
        {
            RecentDetections.Insert(0, new RecentDetection
            {
                ConfigName = configName,
                Message = message,
                SeverityColor = color,
                TimeAgo = _appLocalizer.GetString(UiKeys.Main_TimeJustNow)
            });

            // 限制数量
            while (RecentDetections.Count > 10)
            {
                RecentDetections.RemoveAt(RecentDetections.Count - 1);
            }
        });
    }

    private static string GetLogLevelColor(Microsoft.Extensions.Logging.LogLevel level) => level switch
    {
        Microsoft.Extensions.Logging.LogLevel.Debug => "#9E9E9E",
        Microsoft.Extensions.Logging.LogLevel.Information => "#2196F3",
        Microsoft.Extensions.Logging.LogLevel.Warning => "#FF9800",
        Microsoft.Extensions.Logging.LogLevel.Error => "#F44336",
        Microsoft.Extensions.Logging.LogLevel.Critical => "#D32F2F",
        _ => "#9E9E9E"
    };

    private static string GetLogLevelShort(Microsoft.Extensions.Logging.LogLevel level) => level switch
    {
        Microsoft.Extensions.Logging.LogLevel.Trace => "TRC",
        Microsoft.Extensions.Logging.LogLevel.Debug => "DBG",
        Microsoft.Extensions.Logging.LogLevel.Information => "INF",
        Microsoft.Extensions.Logging.LogLevel.Warning => "WRN",
        Microsoft.Extensions.Logging.LogLevel.Error => "ERR",
        Microsoft.Extensions.Logging.LogLevel.Critical => "CRT",
        _ => "???"
    };

    // Commands
    [RelayCommand]
    private async Task NewConfig()
    {
        var owner = GetMainWindow();
        if (owner is null)
        {
            _logger.LogWarning("无法打开新建配置窗口：主窗口不可用");
            return;
        }

        var editor = new Views.ConfigEditorWindow();
        var result = await editor.ShowDialog<DetectionConfiguration?>(owner);

        if (result != null)
        {
            try
            {
                await _configService.SaveAsync(result);
                // 配置服务会触发 ConfigurationChanged 事件，自动更新界面
                _logger.LogInformation("新建配置成功: {ConfigName}", result.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存配置失败");
            }
        }
    }

    [RelayCommand]
    private async Task ImportConfig()
    {
        var topLevel = GetMainWindow();
        if (topLevel == null) return;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "导入配置文件",
            AllowMultiple = true,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("JSON 配置文件") { Patterns = new[] { "*.json" } },
                new FilePickerFileType("所有文件") { Patterns = new[] { "*.*" } }
            }
        });

        var jsonOpts = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        foreach (var file in files)
        {
            try
            {
                var json = await File.ReadAllTextAsync(file.Path.LocalPath);
                var configs = ParseConfigFile(json, jsonOpts);

                if (configs.Count == 0)
                {
                    _logger.LogWarning("导入文件中未找到有效配置: {FileName}", file.Name);
                    continue;
                }

                var imported = 0;
                foreach (var config in configs)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(config.Id))
                            config.Id = Guid.NewGuid().ToString();

                        await _configService.ImportAsync(config);
                        imported++;
                        _logger.LogInformation("已导入配置: {ConfigName} (来自 {FileName})", config.Name, file.Name);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "导入配置 '{ConfigName}' 验证失败: {FileName}", config.Name, file.Name);
                    }
                }

                _logger.LogInformation("从 {FileName} 成功导入 {Count} 个配置", file.Name, imported);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导入配置失败: {FileName}", file.Name);
            }
        }

        await LoadConfigurationsAsync();
    }

    private static List<DetectionConfiguration> ParseConfigFile(string json, JsonSerializerOptions opts)
    {
        var trimmed = json.TrimStart();

        if (trimmed.StartsWith('['))
        {
            return JsonSerializer.Deserialize<List<DetectionConfiguration>>(json, opts)
                   ?? new List<DetectionConfiguration>();
        }

        if (trimmed.StartsWith('{'))
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("configurations", out var arr) && arr.ValueKind == JsonValueKind.Array)
            {
                return JsonSerializer.Deserialize<List<DetectionConfiguration>>(arr.GetRawText(), opts)
                       ?? new List<DetectionConfiguration>();
            }

            var single = JsonSerializer.Deserialize<DetectionConfiguration>(json, opts);
            if (single != null && !string.IsNullOrEmpty(single.Name))
                return new List<DetectionConfiguration> { single };
        }

        return new List<DetectionConfiguration>();
    }

    [RelayCommand]
    private async Task StartAll()
    {
        foreach (var config in Configurations.Where(c => !c.IsRunning))
        {
            await config.StartStopCommand.ExecuteAsync(null);
        }
    }

    [RelayCommand]
    private async Task StopAll()
    {
        var runningConfigs = Configurations.Where(c => c.IsRunning).ToList();
        foreach (var config in runningConfigs)
        {
            try
            {
                await config.StartStopCommand.ExecuteAsync(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "停止配置失败: {ConfigName}", config.Name);
            }
        }
    }

    [RelayCommand]
    private async Task OpenMinIOSync()
    {
        var owner = GetMainWindow();
        if (owner is null)
        {
            _logger.LogWarning("无法打开插件同步窗口：主窗口不可用");
            return;
        }

        try
        {
            var syncViewModel = App.Services.GetRequiredService<MinIOSyncViewModel>();
            var syncWindow = new MinIOSyncWindow
            {
                DataContext = syncViewModel
            };
            await syncWindow.ShowDialog(owner);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "打开插件同步窗口失败");
        }
    }

    private void RefreshLocalizedChrome()
    {
        var loc = _appLocalizer;
        WindowTitle = $"{loc.GetString(UiKeys.App_WindowTitle)} · v{AppMetadata.DisplayVersion}";
        ToolbarNewConfig = loc.GetString(UiKeys.Btn_NewConfig);
        ToolbarImportConfig = loc.GetString(UiKeys.Btn_ImportConfig);
        ToolbarStartAll = loc.GetString(UiKeys.Btn_StartAll);
        ToolbarStopAll = loc.GetString(UiKeys.Btn_StopAll);
        TooltipToggleTheme = loc.GetString(UiKeys.Tooltip_ToggleTheme);
        TooltipSettings = loc.GetString(UiKeys.Tooltip_Settings);

        // Keep in English so the control stays recognizable regardless of UI culture.
        ToolbarLanguageLabel = "Language";
        _suppressToolbarLanguageIndex = true;
        try
        {
            ToolbarLanguageOptions.Clear();
            ToolbarLanguageOptions.Add(UiLanguageDisplayNames.ZhCn);
            ToolbarLanguageOptions.Add(UiLanguageDisplayNames.EnUs);
            ToolbarLanguageOptions.Add(UiLanguageDisplayNames.ViVn);
            ToolbarLanguageIndex = ToolbarLanguageIndexFromCulture(_uiCulture.CurrentCultureName);
        }
        finally
        {
            _suppressToolbarLanguageIndex = false;
        }

        SearchConfigWatermark = loc.GetString(UiKeys.Main_SearchConfigWatermark);
        LivePreviewTitle = loc.GetString(UiKeys.Main_LivePreview);
        PreviewComboPlaceholder = loc.GetString(UiKeys.Main_PreviewPlaceholder);
        TooltipPreviewBoxes = loc.GetString(UiKeys.Main_TooltipShowBoxes);
        TooltipPreviewRegions = loc.GetString(UiKeys.Main_TooltipShowRegions);
        TooltipPreviewLabels = loc.GetString(UiKeys.Main_TooltipShowLabels);
        TooltipPreviewPopOut = loc.GetString(UiKeys.Main_TooltipPopOutPreview);
        TooltipPreviewFullscreen = loc.GetString(UiKeys.Main_TooltipFullscreen);
        TooltipPreviewSnapshot = loc.GetString(UiKeys.Main_TooltipSnapshot);
        PreviewEmptyTitle = loc.GetString(UiKeys.Main_PreviewEmptyTitle);
        PreviewEmptySubtitle = loc.GetString(UiKeys.Main_PreviewEmptySubtitle);
        StripYieldLabel = loc.GetString(UiKeys.Main_YieldLabel);

        SystemStatusTitle = loc.GetString(UiKeys.Main_SystemStatusTitle);
        SystemHealthyText = loc.GetString(UiKeys.Main_StatusHealthy);
        SystemLabelRunning = loc.GetString(UiKeys.Main_LabelRunningConfigs);
        SystemLabelTotal = loc.GetString(UiKeys.Main_LabelTotalConfigs);
        SystemLabelUptime = loc.GetString(UiKeys.Main_LabelUptime);

        PerformanceTitle = loc.GetString(UiKeys.Main_PerformanceTitle);
        PerfLabelCpu = loc.GetString(UiKeys.Main_CpuUsage);
        PerfLabelMemory = loc.GetString(UiKeys.Main_MemoryUsage);
        PerfLabelGpu = loc.GetString(UiKeys.Main_GpuUsage);
        PerfLabelAvgFrame = loc.GetString(UiKeys.Main_AvgFrameTime);

        RecentDetectionsTitle = loc.GetString(UiKeys.Main_RecentDetectionsTitle);
        ProductionStatsTitle = loc.GetString(UiKeys.Main_ProductionStatsTitle);
        ProductionTotalCaption = loc.GetString(UiKeys.Main_StatTotalCaption);
        ProductionTotalCaption2 = loc.GetString(UiKeys.Main_StatTotalCaption2);

        LogsTitle = loc.GetString(UiKeys.Main_LogsTitle);
        LogFilterDebug = loc.GetString(UiKeys.Main_LogFilterDebug);
        LogFilterInfo = loc.GetString(UiKeys.Main_LogFilterInfo);
        LogFilterWarning = loc.GetString(UiKeys.Main_LogFilterWarning);
        LogFilterError = loc.GetString(UiKeys.Main_LogFilterError);
        LogSearchWatermark = loc.GetString(UiKeys.Main_LogSearchWatermark);
        LogBtnClear = loc.GetString(UiKeys.Main_LogBtnClear);
        LogBtnExport = loc.GetString(UiKeys.Main_LogBtnExport);

        FullscreenViewTitle = loc.GetString(UiKeys.Main_FullscreenTitle);
        FullscreenEscHintText = loc.GetString(UiKeys.Main_FullscreenEscHint);
        FullscreenOverlayPipelineText = string.Format(
            CultureInfo.CurrentUICulture,
            loc.GetString(UiKeys.Main_FullscreenAvgPipelineFmt),
            _fullscreenLastPipelineMs);

        RefreshCountDisplays();

        if (SelectedPreviewSource == null)
            CurrentPreviewConfig = loc.GetString(UiKeys.Main_PreviewPlaceholder);
    }

    private void SyncFullscreenPipelineOverlay(string configName, double averagePipelineMs)
    {
        FullscreenOverlayConfigName = configName;
        _fullscreenLastPipelineMs = averagePipelineMs;
        FullscreenOverlayPipelineText = string.Format(
            CultureInfo.CurrentUICulture,
            _appLocalizer.GetString(UiKeys.Main_FullscreenAvgPipelineFmt),
            averagePipelineMs);
    }

    private void RefreshCountDisplays()
    {
        RunningConfigsDisplay = string.Format(
            CultureInfo.CurrentUICulture,
            _appLocalizer.GetString(UiKeys.Main_Format_RunningConfigsCount),
            RunningCount);
        TotalConfigsDisplay = string.Format(
            CultureInfo.CurrentUICulture,
            _appLocalizer.GetString(UiKeys.Main_Format_TotalConfigsCount),
            TotalConfigCount);
    }

    private static int ToolbarLanguageIndexFromCulture(string cultureName)
    {
        if (cultureName.StartsWith("zh", StringComparison.OrdinalIgnoreCase)) return 0;
        if (cultureName.StartsWith("vi", StringComparison.OrdinalIgnoreCase)) return 2;
        return 1;
    }

    private static string ToolbarCultureFromIndex(int index) => index switch
    {
        0 => "zh-CN",
        2 => "vi-VN",
        _ => "en-US"
    };

    partial void OnToolbarLanguageIndexChanged(int value)
    {
        if (_suppressToolbarLanguageIndex) return;
        if (value is < 0 or > 2)
            return;
        var culture = ToolbarCultureFromIndex(value);
        if (!string.Equals(_uiCulture.CurrentCultureName, culture, StringComparison.OrdinalIgnoreCase))
            _uiCulture.SetUiCulture(culture);
    }

    partial void OnRunningCountChanged(int value) => RefreshCountDisplays();

    partial void OnTotalConfigCountChanged(int value) => RefreshCountDisplays();

    [RelayCommand]
    private async Task OpenSettings()
    {
        var window = GetMainWindow();
        if (window == null) return;

        var loc = _appLocalizer;
        var appConfig = await _configService.GetAppConfigurationAsync();
        var s3 = appConfig.S3Storage ?? new S3StorageConfiguration();

        static CultureInfo ResolveUiCulture(string name)
        {
            try
            {
                return CultureInfo.GetCultureInfo(name);
            }
            catch (CultureNotFoundException)
            {
                return CultureInfo.GetCultureInfo("zh-CN");
            }
        }

        var dialog = new Window
        {
            Width = 480,
            Height = 760,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            CanResize = true
        };

        var panel = new StackPanel { Margin = new Thickness(24), Spacing = 12 };
        var headerTb = new TextBlock
        {
            FontSize = 20,
            FontWeight = Avalonia.Media.FontWeight.SemiBold
        };
        panel.Children.Add(headerTb);
        panel.Children.Add(new Avalonia.Controls.Separator());

        var langRow = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
        var langLabelTb = new TextBlock
        {
            Opacity = 0.7,
            Width = 120,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
        };
        langRow.Children.Add(langLabelTb);
        var langCombo = new ComboBox { MinWidth = 200 };
        var langItems = new LangPick[]
        {
            new() { Code = "zh-CN", Text = UiLanguageDisplayNames.ZhCn },
            new() { Code = "en-US", Text = UiLanguageDisplayNames.EnUs },
            new() { Code = "vi-VN", Text = UiLanguageDisplayNames.ViVn }
        };
        langCombo.ItemsSource = langItems;
        langCombo.ItemTemplate = new FuncDataTemplate<LangPick>((x, _) => new TextBlock { Text = x?.Text ?? "" });
        var suppressLangChanged = true;
        langCombo.SelectedItem = langItems.FirstOrDefault(i => i.Code == _uiCulture.CurrentCultureName) ?? langItems[0];
        langCombo.SelectionChanged += (_, e) =>
        {
            if (suppressLangChanged) return;
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is LangPick pick)
                _uiCulture.SetUiCulture(pick.Code);
        };
        Dispatcher.UIThread.Post(() => { suppressLangChanged = false; });
        langRow.Children.Add(langCombo);
        panel.Children.Add(langRow);

        var appGroupTitle = new TextBlock
        {
            Text = "应用配置",
            FontWeight = Avalonia.Media.FontWeight.SemiBold,
            Margin = new Thickness(0, 4, 0, 0)
        };
        panel.Children.Add(appGroupTitle);

        var pluginDirRow = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
        pluginDirRow.Children.Add(new TextBlock
        {
            Text = "插件目录:",
            Opacity = 0.7,
            Width = 120,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
        });
        var pluginDirInput = new TextBox { MinWidth = 280, Text = appConfig.PluginDirectory };
        pluginDirRow.Children.Add(pluginDirInput);
        panel.Children.Add(pluginDirRow);

        var modelDirRow = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
        modelDirRow.Children.Add(new TextBlock
        {
            Text = "模型目录:",
            Opacity = 0.7,
            Width = 120,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
        });
        var modelDirInput = new TextBox { MinWidth = 280, Text = appConfig.ModelDirectory };
        modelDirRow.Children.Add(modelDirInput);
        panel.Children.Add(modelDirRow);

        var s3GroupTitle = new TextBlock
        {
            Text = "对象存储同步（S3/MinIO）",
            FontWeight = Avalonia.Media.FontWeight.SemiBold,
            Margin = new Thickness(0, 4, 0, 0)
        };
        panel.Children.Add(s3GroupTitle);

        var s3Enabled = new CheckBox { Content = "启用 S3 同步", IsChecked = s3.Enabled };
        panel.Children.Add(s3Enabled);

        StackPanel CreateInputRow(string label, string value)
        {
            var row = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
            row.Children.Add(new TextBlock
            {
                Text = label,
                Opacity = 0.7,
                Width = 120,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
            });
            row.Children.Add(new TextBox { MinWidth = 280, Text = value });
            return row;
        }

        var endpointRow = CreateInputRow("Endpoint:", s3.Endpoint);
        var bucketRow = CreateInputRow("Bucket:", s3.BucketName);
        var accessKeyRow = CreateInputRow("AccessKey:", s3.AccessKey);
        var secretKeyRow = CreateInputRow("SecretKey:", s3.SecretKey);
        var remotePluginRow = CreateInputRow("远程插件路径:", s3.RemotePluginPath);
        var remoteModelRow = CreateInputRow("远程模型路径:", s3.RemoteModelPath);
        var autoSyncRow = CreateInputRow("自动同步间隔(分):", s3.AutoSyncIntervalMinutes.ToString(CultureInfo.InvariantCulture));

        panel.Children.Add(endpointRow);
        panel.Children.Add(bucketRow);
        panel.Children.Add(accessKeyRow);
        panel.Children.Add(secretKeyRow);
        panel.Children.Add(remotePluginRow);
        panel.Children.Add(remoteModelRow);
        panel.Children.Add(autoSyncRow);

        var useSsl = new CheckBox { Content = "使用 HTTPS", IsChecked = s3.UseSsl };
        var syncOnStartup = new CheckBox { Content = "启动时自动同步", IsChecked = s3.SyncOnStartup };
        panel.Children.Add(useSsl);
        panel.Children.Add(syncOnStartup);

        var saveRow = new StackPanel
        {
            Orientation = Avalonia.Layout.Orientation.Horizontal,
            Spacing = 8,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right
        };
        var saveBtn = new Button { Content = "保存配置", Padding = new Thickness(16, 6) };
        saveBtn.Click += async (_, _) =>
        {
            try
            {
                appConfig.PluginDirectory = string.IsNullOrWhiteSpace(pluginDirInput.Text) ? "plugins" : pluginDirInput.Text.Trim();
                appConfig.ModelDirectory = string.IsNullOrWhiteSpace(modelDirInput.Text) ? "resources/models" : modelDirInput.Text.Trim();
                appConfig.S3Storage ??= new S3StorageConfiguration();
                appConfig.S3Storage.Enabled = s3Enabled.IsChecked == true;
                appConfig.S3Storage.Endpoint = ((endpointRow.Children[1] as TextBox)?.Text ?? string.Empty).Trim();
                appConfig.S3Storage.BucketName = ((bucketRow.Children[1] as TextBox)?.Text ?? string.Empty).Trim();
                appConfig.S3Storage.AccessKey = ((accessKeyRow.Children[1] as TextBox)?.Text ?? string.Empty).Trim();
                appConfig.S3Storage.SecretKey = ((secretKeyRow.Children[1] as TextBox)?.Text ?? string.Empty).Trim();
                appConfig.S3Storage.RemotePluginPath = ((remotePluginRow.Children[1] as TextBox)?.Text ?? "plugins/").Trim();
                appConfig.S3Storage.RemoteModelPath = ((remoteModelRow.Children[1] as TextBox)?.Text ?? "models/").Trim();
                appConfig.S3Storage.UseSsl = useSsl.IsChecked == true;
                appConfig.S3Storage.SyncOnStartup = syncOnStartup.IsChecked == true;
                var autoSyncText = ((autoSyncRow.Children[1] as TextBox)?.Text ?? "60").Trim();
                if (!int.TryParse(autoSyncText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var autoSyncMinutes))
                    autoSyncMinutes = 60;
                appConfig.S3Storage.AutoSyncIntervalMinutes = Math.Max(0, autoSyncMinutes);

                await _configService.SetAppConfigurationAsync(appConfig);
                _logger.LogInformation("应用/S3 配置保存成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存应用/S3 配置失败");
            }
        };
        saveRow.Children.Add(saveBtn);
        panel.Children.Add(saveRow);

        var rowRunMode = CreateInfoRowPlaceholder();
        var rowFramework = CreateInfoRowPlaceholder();
        var rowVersion = CreateInfoRowPlaceholder();
        var rowOs = CreateInfoRowPlaceholder();
        var rowCpu = CreateInfoRowPlaceholder();
        var rowMemory = CreateInfoRowPlaceholder();
        var rowGpu = CreateInfoRowPlaceholder();
        var rowConfigDir = CreateInfoRowPlaceholder();
        panel.Children.Add(rowRunMode);
        panel.Children.Add(rowFramework);
        panel.Children.Add(rowVersion);
        panel.Children.Add(rowOs);
        panel.Children.Add(rowCpu);
        panel.Children.Add(rowMemory);
        panel.Children.Add(rowGpu);
        panel.Children.Add(rowConfigDir);

        void ApplySettingsTexts(CultureInfo ui)
        {
            var perf = _performanceMonitor.GetCurrentData();
            dialog.Title = loc.GetString(UiKeys.Settings_Title, ui);
            headerTb.Text = loc.GetString(UiKeys.Settings_Header, ui);
            langLabelTb.Text = $"{loc.GetString(UiKeys.Settings_Language, ui)}:";
            SetInfoRowTexts(rowRunMode, loc.GetString(UiKeys.Settings_RunMode, ui), loc.GetString(UiKeys.Settings_RunModeValue, ui));
            SetInfoRowTexts(rowFramework, loc.GetString(UiKeys.Settings_Framework, ui), $".NET {Environment.Version}");
            SetInfoRowTexts(rowVersion, loc.GetString(UiKeys.Settings_Version, ui), AppMetadata.DisplayVersion);
            SetInfoRowTexts(rowOs, loc.GetString(UiKeys.Settings_OS, ui), Environment.OSVersion.ToString());
            SetInfoRowTexts(rowCpu, loc.GetString(UiKeys.Settings_CPU, ui), $"{Environment.ProcessorCount}");
            SetInfoRowTexts(rowMemory, loc.GetString(UiKeys.Settings_Memory, ui), MemoryUsage);
            SetInfoRowTexts(
                rowGpu,
                loc.GetString(UiKeys.Settings_GPURow, ui),
                perf.HasGpu ? loc.GetString(UiKeys.Settings_GPUAvailable, ui) : loc.GetString(UiKeys.Settings_GPUNotAvailable, ui));
            SetInfoRowTexts(rowConfigDir, loc.GetString(UiKeys.Settings_ConfigDir, ui), Path.GetFullPath("configs"));
        }

        ApplySettingsTexts(ResolveUiCulture(_uiCulture.CurrentCultureName));

        EventHandler? onCultureChanged = null;
        onCultureChanged = (_, _) =>
        {
            suppressLangChanged = true;
            try
            {
                langCombo.SelectedItem = langItems.FirstOrDefault(i => i.Code == _uiCulture.CurrentCultureName) ?? langItems[0];
            }
            finally
            {
                suppressLangChanged = false;
            }

            ApplySettingsTexts(ResolveUiCulture(_uiCulture.CurrentCultureName));
        };
        _uiCulture.CultureChanged += onCultureChanged;
        dialog.Closed += (_, _) => _uiCulture.CultureChanged -= onCultureChanged;

        dialog.Content = new ScrollViewer
        {
            Content = panel,
            VerticalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto
        };
        await dialog.ShowDialog(window);
    }

    private static StackPanel CreateInfoRowPlaceholder()
    {
        var row = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
        row.Children.Add(new TextBlock { Opacity = 0.7, Width = 120 });
        row.Children.Add(new TextBlock { FontWeight = Avalonia.Media.FontWeight.SemiBold });
        return row;
    }

    private static void SetInfoRowTexts(StackPanel row, string label, string value)
    {
        if (row.Children[0] is TextBlock l)
            l.Text = $"{label}:";
        if (row.Children.Count > 1 && row.Children[1] is TextBlock v)
            v.Text = value;
    }

    private static Window? GetMainWindow()
    {
        return Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;
    }

    private static async Task ShowMessageDialogAsync(Window owner, string title, string message)
    {
        var dialog = new Window
        {
            Title = title,
            Width = 520,
            MinHeight = 120,
            SizeToContent = SizeToContent.Height,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            CanResize = false
        };

        var panel = new StackPanel { Margin = new Thickness(24), Spacing = 16 };
        panel.Children.Add(new TextBlock
        {
            Text = message,
            TextWrapping = TextWrapping.Wrap
        });
        var ok = new Button
        {
            Content = "确定",
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
            Padding = new Thickness(24, 8)
        };
        ok.Click += (_, _) => dialog.Close();
        panel.Children.Add(ok);
        dialog.Content = panel;
        await dialog.ShowDialog(owner);
    }

    [RelayCommand]
    private void PopOutPreview()
    {
        if (SelectedPreviewSource == null) return;

        var configId = SelectedPreviewSource.ConfigId;
        var configName = SelectedPreviewSource.ConfigName;

        // 如果已有该配置的浮动窗口，激活它
        if (_floatingWindows.TryGetValue(configId, out var existing))
        {
            existing.Activate();
            return;
        }

        var window = new Views.FloatingPreviewWindow(configId, configName, _appLocalizer, _uiCulture);
        _floatingWindows[configId] = window;
        window.Closed += (_, _) => _floatingWindows.Remove(configId);
        window.Show();
        _logger.LogInformation("打开浮动预览: {ConfigName}", configName);
    }

    [RelayCommand]
    private void Fullscreen()
    {
        if (_fullscreenWindow != null)
        {
            _fullscreenWindow.Close();
            _fullscreenWindow = null;
            return;
        }

        _fullscreenWindow = new Views.FullscreenPreviewWindow
        {
            DataContext = this
        };
        _fullscreenWindow.Closed += (s, e) => _fullscreenWindow = null;

        if (SelectedPreviewSource != null)
        {
            var session = _detectionService.GetSession(SelectedPreviewSource.ConfigId);
            var name = session?.ConfigName ?? SelectedPreviewSource.ConfigName;
            var ms = session?.AveragePipelineMs ?? 0;
            SyncFullscreenPipelineOverlay(name, ms);
            _fullscreenWindow.UpdatePreviewImage(PreviewImage);
        }

        _fullscreenWindow.Show();
        _logger.LogInformation("打开全屏预览");
    }

    partial void OnIsDarkThemeChanged(bool value)
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (Avalonia.Application.Current != null)
            {
                Avalonia.Application.Current.RequestedThemeVariant = value 
                    ? Avalonia.Styling.ThemeVariant.Dark 
                    : Avalonia.Styling.ThemeVariant.Light;
            }
        });
    }

    [RelayCommand]
    private async Task Snapshot()
    {
        if (PreviewImage == null)
        {
            _logger.LogWarning("没有可截图的预览画面");
            return;
        }

        var topLevel = GetMainWindow();
        if (topLevel == null) return;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "保存截图",
            SuggestedFileName = $"snapshot_{DateTime.Now:yyyyMMdd_HHmmss}.png",
            FileTypeChoices = new[]
            {
                new FilePickerFileType("PNG 图片") { Patterns = new[] { "*.png" } },
                new FilePickerFileType("JPEG 图片") { Patterns = new[] { "*.jpg" } }
            }
        });

        if (file != null)
        {
            try
            {
                await using var stream = await file.OpenWriteAsync();
                PreviewImage.Save(stream);
                _logger.LogInformation("截图已保存: {FilePath}", file.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存截图失败");
            }
        }
    }

    [RelayCommand]
    private void ClearLogs()
    {
        _logService.Clear();
        _allLogs.Clear();
        FilteredLogs.Clear();
        LogCount = 0;
    }

    [RelayCommand]
    private async Task ExportLogs()
    {
        var owner = GetMainWindow();
        try
        {
            var logsDir = Path.Combine(Environment.CurrentDirectory, "logs");
            Directory.CreateDirectory(logsDir);
            var fileName = $"ui-export_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            var path = Path.Combine(logsDir, fileName);
            await _logService.ExportAsync(path);
            var fullPath = Path.GetFullPath(path);
            _logger.LogInformation("日志已导出: {Path}", fullPath);
            if (owner != null)
                await ShowMessageDialogAsync(owner, "导出成功", $"日志已导出到：\n{fullPath}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "导出日志失败");
            if (owner != null)
                await ShowMessageDialogAsync(owner, "导出失败", ex.Message);
        }
    }

    partial void OnIsLogPanelExpandedChanged(bool value)
    {
        LogPanelExpandIcon = value 
            ? "M7.41,15.41L12,10.83L16.59,15.41L18,14L12,8L6,14L7.41,15.41Z"
            : "M7.41,8.58L12,13.17L16.59,8.58L18,10L12,16L6,10L7.41,8.58Z";
    }

    partial void OnSearchTextChanged(string value)
    {
        RebuildFilteredConfigurations();
    }

    private void RebuildFilteredConfigurations()
    {
        var query = SearchText?.Trim() ?? string.Empty;
        FilteredConfigurations.Clear();

        if (string.IsNullOrWhiteSpace(query))
        {
            foreach (var config in Configurations)
            {
                FilteredConfigurations.Add(config);
            }
            return;
        }

        var matches = Configurations
            .Select(config => new
            {
                Config = config,
                Score = CalculateSimilarityScore(config.Name, query)
            })
            .Where(x => x.Score >= 0.45)
            .OrderByDescending(x => x.Score)
            .ThenBy(x => x.Config.Name, StringComparer.CurrentCultureIgnoreCase)
            .Select(x => x.Config);

        foreach (var config in matches)
        {
            FilteredConfigurations.Add(config);
        }
    }

    private static double CalculateSimilarityScore(string source, string query)
    {
        if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(query))
            return 0;

        var normalizedSource = source.Trim().ToLower(CultureInfo.InvariantCulture);
        var normalizedQuery = query.Trim().ToLower(CultureInfo.InvariantCulture);

        if (normalizedSource == normalizedQuery)
            return 1.0;

        if (normalizedSource.Contains(normalizedQuery, StringComparison.Ordinal))
            return Math.Max(0.9, (double)normalizedQuery.Length / normalizedSource.Length);

        var distance = LevenshteinDistance(normalizedSource, normalizedQuery);
        var maxLen = Math.Max(normalizedSource.Length, normalizedQuery.Length);
        var editSimilarity = maxLen == 0 ? 1.0 : 1.0 - (double)distance / maxLen;

        var subsequenceSimilarity = CalculateSubsequenceSimilarity(normalizedSource, normalizedQuery);

        // 编辑距离 + 子序列匹配加权：兼顾错字与缩写输入场景
        return editSimilarity * 0.7 + subsequenceSimilarity * 0.3;
    }

    private static double CalculateSubsequenceSimilarity(string source, string query)
    {
        if (query.Length == 0) return 1.0;
        if (source.Length == 0) return 0.0;

        var matchCount = 0;
        var sourceIndex = 0;

        for (var i = 0; i < query.Length && sourceIndex < source.Length; i++)
        {
            while (sourceIndex < source.Length && source[sourceIndex] != query[i])
            {
                sourceIndex++;
            }

            if (sourceIndex < source.Length)
            {
                matchCount++;
                sourceIndex++;
            }
        }

        return (double)matchCount / query.Length;
    }

    private static int LevenshteinDistance(string a, string b)
    {
        var n = a.Length;
        var m = b.Length;
        if (n == 0) return m;
        if (m == 0) return n;

        var dp = new int[n + 1, m + 1];
        for (var i = 0; i <= n; i++) dp[i, 0] = i;
        for (var j = 0; j <= m; j++) dp[0, j] = j;

        for (var i = 1; i <= n; i++)
        {
            for (var j = 1; j <= m; j++)
            {
                var cost = a[i - 1] == b[j - 1] ? 0 : 1;
                dp[i, j] = Math.Min(
                    Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                    dp[i - 1, j - 1] + cost);
            }
        }

        return dp[n, m];
    }
}
