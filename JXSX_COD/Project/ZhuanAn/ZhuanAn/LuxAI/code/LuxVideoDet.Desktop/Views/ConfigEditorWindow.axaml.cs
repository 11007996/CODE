using Avalonia.Controls;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Desktop.ViewModels;
using LuxVideoDet.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace LuxVideoDet.Desktop.Views;

public partial class ConfigEditorWindow : ScreenFitWindow
{
    public ConfigEditorWindow() : this(null) { }

    public ConfigEditorWindow(DetectionConfiguration? config)
        : this(
            config,
            App.Services.GetRequiredService<IAppLocalizer>(),
            App.Services.GetRequiredService<IUiCultureService>())
    {
    }

    private ConfigEditorWindow(
        DetectionConfiguration? config,
        IAppLocalizer appLocalizer,
        IUiCultureService uiCulture)
    {
        InitializeComponent();

        var vm = new ConfigEditorViewModel(appLocalizer, uiCulture, config);
        DataContext = vm;

        vm.SaveRequested += (_, _) =>
        {
            var result = vm.GetConfiguration();
            Close(result);
        };
        vm.CancelRequested += (_, _) => Close(null);
    }
}
