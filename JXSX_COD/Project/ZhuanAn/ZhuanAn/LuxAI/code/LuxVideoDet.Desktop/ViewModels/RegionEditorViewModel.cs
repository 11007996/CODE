using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.VideoSource;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Localization;
using OpenCvSharp;
using Avalonia.Threading;
using Serilog;
using Point = LuxVideoDet.Core.Configuration.Models.Point;
using AvaloniaWindow = Avalonia.Controls.Window;

namespace LuxVideoDet.Desktop.ViewModels;

/// <summary>
/// 目标区域项 - ComboBox 中供用户选择"要绘制哪个区域"
/// </summary>
public partial class RegionTargetItem : ObservableObject
{
    public string Name { get; init; } = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Label))]
    private string _displayName = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Label))]
    private string _description = string.Empty;

    public bool IsPredefined { get; init; }
    public bool Required { get; init; }
    public Scalar DefaultColor { get; init; } = new(0, 255, 0);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Label))]
    private bool _isDrawn;

    public string Label => IsPredefined
        ? (IsDrawn ? $"✓ {DisplayName}" : (Required ? $"* {DisplayName}" : DisplayName))
        : DisplayName;

    public override string ToString() => Label;
}

/// <summary>
/// 已绘制区域列表项
/// </summary>
public class DrawnRegionItem
{
    public string Key { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
    public override string ToString() => Label;
}

public partial class RegionEditorViewModel : ViewModelBase
{
    private static readonly Serilog.ILogger Logger = Log.ForContext<RegionEditorViewModel>();
    private readonly AvaloniaWindow _window;
    private readonly IAppLocalizer _appLocalizer;
    private readonly IUiCultureService _uiCulture;
    private string? _statusMessageKey;
    private object[] _statusFormatArgs = Array.Empty<object>();
    private Mat? _originalImage;
    private Mat? _displayMat;

    private DrawMode _currentMode = DrawMode.None;
    private bool _isDrawing;
    private System.Drawing.Point _startPoint;
    private System.Drawing.Point? _currentRectanglePoint;
    private readonly List<System.Drawing.Point> _polygonPoints = new();
    private readonly Dictionary<string, RegionConfig> _regions = new();
    private readonly Dictionary<string, Scalar> _regionColors = new();
    private readonly List<RegionDefinition> _regionDefinitions;

    [ObservableProperty]
    private Bitmap? _displayImage;

    [ObservableProperty]
    private double _canvasWidth = 800;

    [ObservableProperty]
    private double _canvasHeight = 600;

    // --- 目标区域选择 ---

    [ObservableProperty]
    private ObservableCollection<RegionTargetItem> _targetRegions = new();

    [ObservableProperty]
    private RegionTargetItem? _selectedTarget;

    [ObservableProperty]
    private bool _hasPredefinedRegions;

    [ObservableProperty]
    private string _customRegionName = string.Empty;

    [ObservableProperty]
    private bool _showCustomNameInput;

    [ObservableProperty]
    private string _targetDescription = string.Empty;

    // --- 已绘制区域列表 ---

    [ObservableProperty]
    private ObservableCollection<DrawnRegionItem> _drawnRegions = new();

    [ObservableProperty]
    private DrawnRegionItem? _selectedDrawnRegion;

    [ObservableProperty]
    private string _windowTitle = string.Empty;

    [ObservableProperty]
    private string _lblSectionTarget = string.Empty;

    [ObservableProperty]
    private string _wmCustomRegionName = string.Empty;

    [ObservableProperty]
    private string _lblSectionDraw = string.Empty;

    [ObservableProperty]
    private string _btnRectText = string.Empty;

    [ObservableProperty]
    private string _btnPolygonText = string.Empty;

    [ObservableProperty]
    private string _btnClearCurrentText = string.Empty;

    [ObservableProperty]
    private string _btnDeleteSelectedText = string.Empty;

    [ObservableProperty]
    private string _lblDrawnRegions = string.Empty;

    [ObservableProperty]
    private string _btnSaveText = string.Empty;

    [ObservableProperty]
    private string _btnCancelText = string.Empty;

    [ObservableProperty]
    private string _statusText = string.Empty;

    public enum DrawMode
    {
        None,
        Rectangle,
        Polygon
    }

    public RegionEditorViewModel(
        string sourcePath,
        VideoSourceType sourceType,
        AvaloniaWindow window,
        IAppLocalizer appLocalizer,
        IUiCultureService uiCulture,
        List<RegionDefinition> regionDefinitions,
        List<RegionConfig>? existingRegions = null)
    {
        _window = window;
        _appLocalizer = appLocalizer;
        _uiCulture = uiCulture;
        _regionDefinitions = regionDefinitions ?? new();

        _uiCulture.CultureChanged += (_, _) =>
            Dispatcher.UIThread.Post(ApplyRegionChrome);

        Logger.Debug("打开区域编辑器，视频源类型={SourceType}，视频源={SourcePath}，预定义区域数={RegionDefinitionCount}，已有区域数={ExistingRegionCount}",
            sourceType, sourcePath, _regionDefinitions.Count, existingRegions?.Count ?? 0);

        InitializeTargetRegions();
        LoadExistingRegions(existingRegions);
        LoadFrame(sourcePath, sourceType);
        ApplyRegionChrome();
    }

    private void ApplyRegionChrome()
    {
        Func<string, string> L = k => _appLocalizer.GetString(k);
        WindowTitle = L(UiKeys.Region_WindowTitle);
        LblSectionTarget = L(UiKeys.Region_SectionTarget);
        WmCustomRegionName = L(UiKeys.Region_WatermarkCustomName);
        LblSectionDraw = L(UiKeys.Region_SectionDraw);
        BtnRectText = L(UiKeys.Region_BtnRect);
        BtnPolygonText = L(UiKeys.Region_BtnPolygon);
        BtnClearCurrentText = L(UiKeys.Region_BtnClearCurrent);
        BtnDeleteSelectedText = L(UiKeys.Region_BtnDeleteSelected);
        LblDrawnRegions = L(UiKeys.Region_LblDrawn);
        BtnSaveText = L(UiKeys.Region_BtnSave);
        BtnCancelText = L(UiKeys.Region_BtnCancel);
        RefreshCustomTargetLabels();
        RefreshStatusText();
    }

    private void RefreshCustomTargetLabels()
    {
        var custom = TargetRegions.FirstOrDefault(t => t.Name == "__custom__");
        if (custom == null) return;
        custom.DisplayName = _appLocalizer.GetString(UiKeys.Region_CustomDisplayName);
        custom.Description = _appLocalizer.GetString(UiKeys.Region_CustomDescription);
    }

    private void SetRegionStatus(string key, params object[] args)
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

    private void InitializeTargetRegions()
    {
        HasPredefinedRegions = _regionDefinitions.Count > 0;

        foreach (var def in _regionDefinitions)
        {
            TargetRegions.Add(new RegionTargetItem
            {
                Name = def.Name,
                DisplayName = def.DisplayName,
                Description = def.Description,
                IsPredefined = true,
                Required = def.Required,
                DefaultColor = def.DefaultColor
            });
        }

        TargetRegions.Add(new RegionTargetItem
        {
            Name = "__custom__",
            DisplayName = _appLocalizer.GetString(UiKeys.Region_CustomDisplayName),
            Description = _appLocalizer.GetString(UiKeys.Region_CustomDescription),
            IsPredefined = false,
            Required = false,
            DefaultColor = new Scalar(0, 255, 0)
        });

        SelectedTarget = HasPredefinedRegions ? TargetRegions.First() : TargetRegions.Last();
        Logger.Debug("初始化区域目标完成，预定义区域模式={HasPredefinedRegions}，目标总数={TargetCount}", HasPredefinedRegions, TargetRegions.Count);
    }

    partial void OnSelectedTargetChanged(RegionTargetItem? value)
    {
        ShowCustomNameInput = value is { IsPredefined: false };

        if (value == null)
        {
            TargetDescription = string.Empty;
            return;
        }

        Logger.Debug("切换目标区域：Name={TargetName}，DisplayName={DisplayName}，IsPredefined={IsPredefined}，IsDrawn={IsDrawn}",
            value.Name, value.DisplayName, value.IsPredefined, value.IsDrawn);

        if (value.IsPredefined)
        {
            TargetDescription = value.Description;
            if (value.IsDrawn)
                SetRegionStatus(UiKeys.Region_StatusTargetRedrawFmt, value.DisplayName);
            else
                SetRegionStatus(UiKeys.Region_StatusTargetPickModeFmt, value.DisplayName);
        }
        else
        {
            TargetDescription = _appLocalizer.GetString(UiKeys.Region_StatusCustomTargetDesc);
            SetRegionStatus(UiKeys.Region_StatusCustomEnterName);
        }
    }

    private void LoadExistingRegions(List<RegionConfig>? existingRegions)
    {
        if (existingRegions == null || existingRegions.Count == 0) return;

        foreach (var region in existingRegions)
        {
            _regions[region.Name] = region;

            var def = _regionDefinitions.FirstOrDefault(d => d.Name == region.Name);
            var color = def?.DefaultColor ?? new Scalar(0, 255, 0);
            _regionColors[region.Name] = color;

            var label = def != null ? def.DisplayName : region.DisplayName;
            DrawnRegions.Add(new DrawnRegionItem { Key = region.Name, Label = label });

            var target = TargetRegions.FirstOrDefault(t => t.Name == region.Name);
            if (target != null) target.IsDrawn = true;

            Logger.Debug("加载已有区域：Name={RegionName}，DisplayName={DisplayName}，PointCount={PointCount}",
                region.Name, region.DisplayName, region.Points.Count);
        }

        AutoSelectNextTarget();
    }

    // --- 帧加载 ---

    private void LoadFrame(string sourcePath, VideoSourceType sourceType)
    {
        try
        {
            SetRegionStatus(UiKeys.Region_StatusLoadingImage);
            Logger.Debug("开始加载区域编辑器底图");

            if (sourceType == VideoSourceType.LocalVideo || sourceType == VideoSourceType.Rtsp)
            {
                using var capture = VideoFrameCapture.OpenVideoCaptureForFile(sourcePath);
                if (!capture.IsOpened())
                {
                    SetRegionStatus(UiKeys.Region_StatusCannotOpenVideo);
                    return;
                }
                _originalImage = new Mat();
                if (!VideoFrameCapture.ReadRepresentativeFrame(capture, _originalImage, null))
                {
                    SetRegionStatus(UiKeys.Region_StatusVideoReadFailed);
                    return;
                }
            }
            else if (sourceType == VideoSourceType.Camera)
            {
                if (!int.TryParse(sourcePath, out var cameraId))
                {
                    SetRegionStatus(UiKeys.Region_StatusInvalidCameraId);
                    Logger.Warning("区域编辑器: 摄像头源非数字 ID，Source={Source}", sourcePath);
                    return;
                }

                Logger.Information("区域编辑器: 正在打开摄像头 CameraId={CameraId}", cameraId);
                using var capture = VideoFrameCapture.OpenCameraVideoCapture(cameraId, null);
                if (capture == null)
                {
                    var hint = VideoFrameCapture.GetCameraPermissionHint();
                    SetRegionStatus(UiKeys.Region_StatusCannotOpenCamera);
                    Logger.Error("区域编辑器: 无法打开摄像头 CameraId={CameraId}。{Hint}", cameraId, hint);
                    return;
                }

                _originalImage = new Mat();
                if (!VideoFrameCapture.ReadRepresentativeFrame(capture, _originalImage, null))
                {
                    SetRegionStatus(UiKeys.Region_StatusCameraReadFailed);
                    Logger.Error(
                        "区域编辑器: 摄像头已打开但无法读取有效帧 CameraId={CameraId}",
                        cameraId);
                    return;
                }
            }

            if (_originalImage == null || _originalImage.Empty())
            {
                SetRegionStatus(UiKeys.Region_StatusCannotReadImage);
                return;
            }

            CanvasWidth = _originalImage.Width;
            CanvasHeight = _originalImage.Height;

            UpdateDisplay();
            SetRegionStatus(UiKeys.Region_StatusLoadedFmt, _originalImage.Width, _originalImage.Height);
            Logger.Information("区域编辑器底图加载成功：Width={Width}，Height={Height}", _originalImage.Width, _originalImage.Height);
        }
        catch (Exception ex)
        {
            SetRegionStatus(UiKeys.Region_StatusLoadFailedFmt, ex.Message);
            Logger.Error(ex, "区域编辑器加载图像失败");
        }
    }

    // --- 显示更新 ---

    private void UpdateDisplay()
    {
        if (_originalImage == null || _originalImage.Empty()) return;

        try
        {
            _displayMat?.Dispose();
            _displayMat = _originalImage.Clone();

            foreach (var (name, region) in _regions)
            {
                var color = _regionColors.GetValueOrDefault(name, new Scalar(0, 255, 0));
                DrawRegionOnMat(_displayMat, region, GetRenderableRegionLabel(region, name), color);
            }

            if (_currentMode == DrawMode.Polygon && _polygonPoints.Count > 0)
            {
                DrawPolygonPreview(_displayMat);
            }

            if (_currentMode == DrawMode.Rectangle && _isDrawing && _currentRectanglePoint.HasValue)
            {
                DrawRectanglePreview(_displayMat, _currentRectanglePoint.Value);
            }

            var bmp = MatToBitmap(_displayMat);
            DisplayImage = bmp;
            if (bmp == null)
            {
                SetRegionStatus(UiKeys.Region_StatusBitmapFailed);
                Logger.Warning("区域编辑器 Mat→Bitmap 返回 null");
            }
        }
        catch (Exception ex)
        {
            SetRegionStatus(UiKeys.Region_StatusUpdateFailedFmt, ex.Message);
            Logger.Error(ex, "刷新区域编辑器画面失败");
        }
    }

    private static void DrawRegionOnMat(Mat mat, RegionConfig region, string label, Scalar color)
    {
        if (region.Points.Count < 2) return;

        var cvPoints = region.Points.Select(p => new OpenCvSharp.Point(p.X, p.Y)).ToArray();

        if (region.Points.Count >= 3)
        {
            Cv2.Polylines(mat, [cvPoints], true, color, 3);

            var overlay = mat.Clone();
            Cv2.FillPoly(overlay, [cvPoints], new Scalar(color.Val0, color.Val1, color.Val2, 50));
            Cv2.AddWeighted(overlay, 0.15, mat, 0.85, 0, mat);
            overlay.Dispose();
        }
        else if (region.Points.Count == 2)
        {
            Cv2.Rectangle(mat, new Rect(cvPoints[0].X, cvPoints[0].Y,
                cvPoints[1].X - cvPoints[0].X, cvPoints[1].Y - cvPoints[0].Y), color, 3);
        }

        var textPos = cvPoints[0];
        textPos.Y -= 10;
        if (textPos.Y < 20) textPos.Y = cvPoints[0].Y + 25;
        Cv2.PutText(mat, label, textPos, HersheyFonts.HersheySimplex, 0.7, color, 2);
    }

    private static string GetRenderableRegionLabel(RegionConfig region, string fallbackName)
    {
        var preferred = string.IsNullOrWhiteSpace(region.DisplayName) ? fallbackName : region.DisplayName;

        // OpenCV 内置字体不支持中文，遇到非 ASCII 文本时回退到内部名称，避免显示为问号。
        return preferred.All(c => c <= 127) ? preferred : fallbackName;
    }

    private void DrawPolygonPreview(Mat mat)
    {
        if (_polygonPoints.Count == 0) return;

        var color = SelectedTarget?.DefaultColor ?? new Scalar(0, 0, 255);
        var cvPoints = _polygonPoints.Select(p => new OpenCvSharp.Point(p.X, p.Y)).ToArray();

        foreach (var pt in cvPoints)
        {
            Cv2.Circle(mat, pt, 5, color, -1);
        }

        if (cvPoints.Length >= 2)
        {
            Cv2.Polylines(mat, [cvPoints], false, color, 2);
        }
    }

    private void DrawRectanglePreview(Mat mat, System.Drawing.Point endPoint)
    {
        var color = SelectedTarget?.DefaultColor ?? new Scalar(0, 0, 255);
        var x1 = Math.Min(_startPoint.X, endPoint.X);
        var y1 = Math.Min(_startPoint.Y, endPoint.Y);
        var x2 = Math.Max(_startPoint.X, endPoint.X);
        var y2 = Math.Max(_startPoint.Y, endPoint.Y);

        if (x2 <= x1 || y2 <= y1)
        {
            return;
        }

        var rect = new Rect(x1, y1, x2 - x1, y2 - y1);
        Cv2.Rectangle(mat, rect, color, 2);

        var overlay = mat.Clone();
        Cv2.Rectangle(overlay, rect, color, -1);
        Cv2.AddWeighted(overlay, 0.12, mat, 0.88, 0, mat);
        overlay.Dispose();
    }

    // --- 鼠标事件 ---

    public void OnCanvasLeftClick(double x, double y)
    {
        if (_originalImage == null) return;

        var point = new System.Drawing.Point((int)x, (int)y);

        if (_currentMode == DrawMode.Rectangle)
        {
            _isDrawing = true;
            _startPoint = point;
            _currentRectanglePoint = point;
            Logger.Debug("开始绘制矩形，起点=({X},{Y})", point.X, point.Y);
        }
        else if (_currentMode == DrawMode.Polygon)
        {
            _polygonPoints.Add(point);
            UpdateDisplay();
            SetRegionStatus(UiKeys.Region_StatusPolygonPointsFmt, _polygonPoints.Count);
            Logger.Debug("添加多边形顶点，点=({X},{Y})，当前顶点数={PointCount}", point.X, point.Y, _polygonPoints.Count);
        }
    }

    public void OnCanvasRightClick(double x, double y)
    {
        if (_currentMode == DrawMode.Polygon && _polygonPoints.Count >= 3)
        {
            FinishPolygon();
        }
    }

    public void OnCanvasMouseMove(double x, double y)
    {
        if (_currentMode == DrawMode.Rectangle && _isDrawing)
        {
            _currentRectanglePoint = new System.Drawing.Point((int)x, (int)y);
            UpdateDisplay();
        }
    }

    public void OnCanvasMouseUp(double x, double y)
    {
        if (_originalImage == null || !_isDrawing) return;

        if (_currentMode == DrawMode.Rectangle)
        {
            _isDrawing = false;
            var endPoint = new System.Drawing.Point((int)x, (int)y);
            _currentRectanglePoint = null;
            FinishRectangle(endPoint);
        }
    }

    // --- 区域名称解析 ---

    private string? ResolveTargetRegion(out string displayName, out Scalar color)
    {
        displayName = string.Empty;
        color = new Scalar(0, 255, 0);

        if (SelectedTarget == null)
        {
            SetRegionStatus(UiKeys.Region_StatusSelectTargetFirst);
            return null;
        }

        if (SelectedTarget.IsPredefined)
        {
            displayName = SelectedTarget.DisplayName;
            color = SelectedTarget.DefaultColor;
            return SelectedTarget.Name;
        }

        if (string.IsNullOrWhiteSpace(CustomRegionName))
        {
            SetRegionStatus(UiKeys.Region_StatusEnterCustomName);
            return null;
        }

        var name = CustomRegionName.Trim();
        displayName = name;

        var conflictPredefined = TargetRegions.FirstOrDefault(t => t.IsPredefined && t.Name == name);
        if (conflictPredefined != null)
        {
            SetRegionStatus(UiKeys.Region_StatusNameConflictFmt, name);
            return null;
        }

        return name;
    }

    private void AddOrReplaceRegion(string name, string displayName, Scalar color, RegionConfig region)
    {
        bool isReplace = _regions.ContainsKey(name);

        _regions[name] = region;
        _regionColors[name] = color;

        if (isReplace)
        {
            var oldItem = DrawnRegions.FirstOrDefault(d => d.Key == name);
            if (oldItem != null) DrawnRegions.Remove(oldItem);
        }

        DrawnRegions.Add(new DrawnRegionItem { Key = name, Label = displayName });

        var target = TargetRegions.FirstOrDefault(t => t.Name == name);
        if (target != null) target.IsDrawn = true;

        if (SelectedTarget is { IsPredefined: false })
            CustomRegionName = string.Empty;

        Logger.Information("{Action}区域成功：Name={RegionName}，DisplayName={DisplayName}，PointCount={PointCount}",
            isReplace ? "替换" : "新增", name, displayName, region.Points.Count);

        UpdateDisplay();
    }

    private void AutoSelectNextTarget()
    {
        if (!HasPredefinedRegions) return;

        var nextUndrawn = TargetRegions.FirstOrDefault(t => t.IsPredefined && !t.IsDrawn);
        if (nextUndrawn != null)
        {
            SelectedTarget = nextUndrawn;
        }
    }

    // --- 完成绘制 ---

    private void FinishRectangle(System.Drawing.Point endPoint)
    {
        var x1 = Math.Min(_startPoint.X, endPoint.X);
        var y1 = Math.Min(_startPoint.Y, endPoint.Y);
        var x2 = Math.Max(_startPoint.X, endPoint.X);
        var y2 = Math.Max(_startPoint.Y, endPoint.Y);

        if (x2 - x1 < 10 || y2 - y1 < 10)
        {
            SetRegionStatus(UiKeys.Region_StatusRectTooSmall);
            Logger.Warning("矩形区域过小，起点=({StartX},{StartY})，终点=({EndX},{EndY})", _startPoint.X, _startPoint.Y, endPoint.X, endPoint.Y);
            return;
        }

        var name = ResolveTargetRegion(out var displayName, out var color);
        if (name == null) return;

        var region = new RegionConfig
        {
            Name = name,
            DisplayName = displayName,
            Description = _appLocalizer.GetString(UiKeys.Region_DescRectangle),
            Points = new List<Point>
            {
                new(x1, y1), new(x2, y1), new(x2, y2), new(x1, y2)
            }
        };

        AddOrReplaceRegion(name, displayName, color, region);
        SetRegionStatus(UiKeys.Region_StatusAddedRectFmt, displayName);
        AutoSelectNextTarget();
    }

    private void FinishPolygon()
    {
        if (_polygonPoints.Count < 3)
        {
            SetRegionStatus(UiKeys.Region_StatusPolygonMinPoints);
            Logger.Warning("多边形顶点不足，当前点数={PointCount}", _polygonPoints.Count);
            return;
        }

        var name = ResolveTargetRegion(out var displayName, out var color);
        if (name == null)
        {
            _polygonPoints.Clear();
            UpdateDisplay();
            return;
        }

        var region = new RegionConfig
        {
            Name = name,
            DisplayName = displayName,
            Description = _appLocalizer.GetString(UiKeys.Region_DescPolygon),
            Points = _polygonPoints.Select(p => new Point(p.X, p.Y)).ToList()
        };

        _polygonPoints.Clear();
        AddOrReplaceRegion(name, displayName, color, region);
        SetRegionStatus(UiKeys.Region_StatusAddedPolygonFmt, displayName);
        AutoSelectNextTarget();
    }

    // --- 命令 ---

    [RelayCommand]
    private void SetRectangleMode()
    {
        _currentMode = DrawMode.Rectangle;
        _isDrawing = false;
        _currentRectanglePoint = null;
        _polygonPoints.Clear();
        SetRegionStatus(UiKeys.Region_StatusRectModeHint);
        Logger.Debug("切换到矩形绘制模式");
        UpdateDisplay();
    }

    [RelayCommand]
    private void SetPolygonMode()
    {
        _currentMode = DrawMode.Polygon;
        _isDrawing = false;
        _currentRectanglePoint = null;
        _polygonPoints.Clear();
        SetRegionStatus(UiKeys.Region_StatusPolyModeHint);
        Logger.Debug("切换到多边形绘制模式");
        UpdateDisplay();
    }

    [RelayCommand]
    private void ClearCurrent()
    {
        _polygonPoints.Clear();
        _isDrawing = false;
        _currentRectanglePoint = null;
        UpdateDisplay();
        SetRegionStatus(UiKeys.Region_StatusClearedDraw);
        Logger.Debug("清除当前未完成绘制");
    }

    [RelayCommand]
    private void DeleteSelected()
    {
        var selected = SelectedDrawnRegion;
        if (selected == null)
        {
            SetRegionStatus(UiKeys.Region_StatusSelectDrawnToDelete);
            return;
        }

        var key = selected.Key;
        var label = selected.Label;
        _regions.Remove(key);
        _regionColors.Remove(key);

        var target = TargetRegions.FirstOrDefault(t => t.Name == key);
        if (target != null) target.IsDrawn = false;

        DrawnRegions.Remove(selected);
        SelectedDrawnRegion = null;
        if (target != null)
        {
            SelectedTarget = target;
        }

        UpdateDisplay();
        SetRegionStatus(UiKeys.Region_StatusDeletedFmt, label);
        Logger.Information("删除区域成功：Name={RegionName}，DisplayName={DisplayName}", key, label);
    }

    [RelayCommand]
    private void Save()
    {
        var missingRequired = TargetRegions
            .Where(t => t.IsPredefined && t.Required && !t.IsDrawn)
            .ToList();

        if (missingRequired.Count > 0)
        {
            var names = string.Join("、", missingRequired.Select(t => t.DisplayName));
            SetRegionStatus(UiKeys.Region_StatusMissingRequiredFmt, names);
            Logger.Warning("保存区域失败，缺少必需区域：{MissingRegions}", names);
            return;
        }

        Logger.Information("保存区域编辑结果，区域总数={RegionCount}", _regions.Count);
        _window.Close(_regions.Values.ToList());
    }

    [RelayCommand]
    private void Cancel()
    {
        Logger.Information("取消区域编辑");
        _window.Close(null);
    }

    /// <summary>
    /// 使用 BGR→BGRA 直拷，避免 <see cref="Mat.ToBytes"/> / ImEncode（macOS 上 OpenCvSharp 常缺少 imgcodecs 导出）。
    /// </summary>
    private static Bitmap? MatToBitmap(Mat mat)
        => ImageRenderService.MatToWriteableBitmap(mat);
}
