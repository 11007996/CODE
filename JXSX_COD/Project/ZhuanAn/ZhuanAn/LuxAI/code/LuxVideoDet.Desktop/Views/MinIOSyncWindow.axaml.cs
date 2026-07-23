using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
#if DEBUG
using Avalonia.Diagnostics;
#endif
using LuxVideoDet.Desktop.ViewModels;

namespace LuxVideoDet.Desktop.Views;

public partial class MinIOSyncWindow : Window
{
    public MinIOSyncWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void PluginDataGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is MinIOSyncViewModel vm)
            await vm.DownloadSelectedPluginCommand.ExecuteAsync(null);
    }

    private async void ModelDataGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is MinIOSyncViewModel vm)
            await vm.DownloadSelectedModelCommand.ExecuteAsync(null);
    }

    private async void VideoDataGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is MinIOSyncViewModel vm)
            await vm.DownloadSelectedVideoCommand.ExecuteAsync(null);
    }

    private async void ZipDataGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is MinIOSyncViewModel vm)
            await vm.DownloadSelectedZipCommand.ExecuteAsync(null);
    }
}