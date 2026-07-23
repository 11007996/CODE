using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using LuxVideoDet.Desktop.ViewModels;

namespace LuxVideoDet.Desktop.Views;

public partial class NgReplayWindow : ScreenFitWindow
{
    private NgReplayViewModel? _viewModel;

    public NgReplayWindow()
    {
        InitializeComponent();
    }

    public NgReplayWindow(NgReplayViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = viewModel;

        Closed += (_, _) => _viewModel?.Dispose();

        var slider = this.FindControl<Slider>("ProgressSlider");
        if (slider != null)
        {
            slider.AddHandler(Thumb.DragStartedEvent, OnSliderDragStarted, handledEventsToo: true);
            slider.AddHandler(Thumb.DragCompletedEvent, OnSliderDragCompleted, handledEventsToo: true);
        }
    }

    private bool _wasPausedBeforeDrag;

    private void OnSliderDragStarted(object? sender, object e)
    {
        if (_viewModel == null) return;
        _wasPausedBeforeDrag = !_viewModel.IsPlaying;
        if (_viewModel.IsPlaying)
            _viewModel.PlayPauseCommand.Execute(null);
    }

    private void OnSliderDragCompleted(object? sender, object e)
    {
        if (_viewModel == null) return;
        _viewModel.SeekTo(_viewModel.CurrentPosition);
        if (!_wasPausedBeforeDrag)
            _viewModel.PlayPauseCommand.Execute(null);
    }
}
