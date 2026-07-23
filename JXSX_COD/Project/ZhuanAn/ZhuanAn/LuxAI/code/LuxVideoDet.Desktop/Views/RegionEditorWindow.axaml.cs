using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Desktop.ViewModels;
using LuxVideoDet.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace LuxVideoDet.Desktop.Views;

public partial class RegionEditorWindow : ScreenFitWindow
{
    public RegionEditorWindow()
    {
        InitializeComponent();
    }

    public RegionEditorWindow(string sourcePath, VideoSourceType sourceType,
        List<RegionDefinition> regionDefinitions, List<RegionConfig>? existingRegions = null) : this()
    {
        var loc = App.Services.GetRequiredService<IAppLocalizer>();
        var ui = App.Services.GetRequiredService<IUiCultureService>();
        DataContext = new RegionEditorViewModel(sourcePath, sourceType, this, loc, ui, regionDefinitions, existingRegions);
    }

    private void Canvas_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is not RegionEditorViewModel vm) return;

        var point = e.GetPosition(DrawingCanvas);
        var props = e.GetCurrentPoint(DrawingCanvas).Properties;

        if (props.IsLeftButtonPressed)
        {
            vm.OnCanvasLeftClick(point.X, point.Y);
        }
        else if (props.IsRightButtonPressed)
        {
            vm.OnCanvasRightClick(point.X, point.Y);
        }
    }

    private void Canvas_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (DataContext is not RegionEditorViewModel vm) return;

        var point = e.GetPosition(DrawingCanvas);
        vm.OnCanvasMouseMove(point.X, point.Y);
    }

    private void Canvas_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (DataContext is not RegionEditorViewModel vm) return;

        var point = e.GetPosition(DrawingCanvas);
        vm.OnCanvasMouseUp(point.X, point.Y);
    }
}
