using Avalonia.Input;
using LuxVideoDet.Desktop.ViewModels;

namespace LuxVideoDet.Desktop.Views;

public partial class MainWindow : ScreenFitWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void LogHeader_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.IsLogPanelExpanded = !viewModel.IsLogPanelExpanded;
        }
    }
}