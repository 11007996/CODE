using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;

namespace LuxVideoDet.Desktop.Views;

public partial class FullscreenPreviewWindow : ScreenFitWindow
{
    public FullscreenPreviewWindow()
    {
        InitializeComponent();
        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Close();
        }
    }

    public void UpdatePreviewImage(WriteableBitmap? image)
    {
        if (PreviewImage != null)
            PreviewImage.Source = image;
    }
}
