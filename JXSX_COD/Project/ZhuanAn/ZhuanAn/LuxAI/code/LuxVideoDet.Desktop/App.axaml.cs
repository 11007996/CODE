using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Linq;
using System.Threading.Tasks;
using LuxVideoDet.Desktop.Services;
using LuxVideoDet.Desktop.ViewModels;
using LuxVideoDet.Desktop.Views;
using LuxVideoDet.Core.S3Storage;
using Microsoft.Extensions.DependencyInjection;

namespace LuxVideoDet.Desktop;

public partial class App : Application
{
    private IServiceProvider? _serviceProvider;
    private bool _macOsAppMenuAttached;

    /// <summary>桌面进程 DI 根；在启动完成后可用（供无宿主注入的对话框等使用）。</summary>
    public static IServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            try
            {
                // 配置依赖注入
                _serviceProvider = ServiceConfiguration.ConfigureServices();
                Services = _serviceProvider;

                _serviceProvider.GetRequiredService<IUiCultureService>().ApplySavedCulture();

                // 创建主窗口
                var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = viewModel,
                };

                _ = Task.Run(async () =>
                {
                    try
                    {
                        var s3Sync = _serviceProvider.GetRequiredService<IS3StorageSyncService>();
                        await s3Sync.InitializeAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"S3 同步服务初始化失败: {ex.Message}");
                    }
                });

                desktop.MainWindow.Opened += (_, _) => TryAttachMacOsApplicationMenu(desktop);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"启动失败: {ex}");
                throw;
            }
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// 须在窗口进入原生层之后设置；在 App.axaml 里写 NativeMenu + Click 绑定在部分 .app 启动路径下会闪退。
    /// </summary>
    private void TryAttachMacOsApplicationMenu(IClassicDesktopStyleApplicationLifetime desktop)
    {
        if (!OperatingSystem.IsMacOS() || _macOsAppMenuAttached)
            return;

        try
        {
            var about = new NativeMenuItem("关于 LuxVideoDet…");
            about.Click += (_, _) =>
            {
                _ = ShowAboutDialogSafeAsync(desktop);
            };

            var prefs = new NativeMenuItem("偏好设置…")
            {
                Gesture = new KeyGesture(Key.OemComma, KeyModifiers.Meta)
            };
            prefs.Click += (_, _) =>
            {
                Dispatcher.UIThread.Post(() =>
                {
                    try
                    {
                        if (desktop.MainWindow?.DataContext is MainWindowViewModel vm
                            && vm.OpenSettingsCommand.CanExecute(null))
                            vm.OpenSettingsCommand.Execute(null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"偏好设置菜单: {ex}");
                    }
                });
            };

            var menu = new NativeMenu();
            menu.Add(about);
            menu.Add(prefs);
            NativeMenu.SetMenu(this, menu);
            _macOsAppMenuAttached = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"macOS 应用菜单挂载失败（可忽略）: {ex}");
        }
    }

    private static async Task ShowAboutDialogSafeAsync(IClassicDesktopStyleApplicationLifetime desktop)
    {
        try
        {
            await Dispatcher.UIThread.InvokeAsync(async () =>
            {
                var owner = desktop.MainWindow;
                if (owner is null)
                    return;

                var ver = typeof(App).Assembly.GetName().Version?.ToString() ?? "—";
                var panel = new StackPanel { Margin = new Thickness(24), Spacing = 10 };
                panel.Children.Add(new TextBlock { Text = "LuxVideoDet", FontSize = 18, FontWeight = FontWeight.Bold });
                panel.Children.Add(new TextBlock { Text = "视频检测系统", Opacity = 0.9 });
                panel.Children.Add(new TextBlock { Text = $"版本 {ver}", Opacity = 0.55, FontSize = 12 });

                var w = new Window
                {
                    Title = "关于 LuxVideoDet",
                    Width = 400,
                    Height = 220,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    CanResize = false,
                    Content = panel
                };
                await w.ShowDialog(owner);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"关于对话框: {ex}");
        }
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
