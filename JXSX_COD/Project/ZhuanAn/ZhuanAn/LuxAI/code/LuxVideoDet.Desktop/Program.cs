using Avalonia;
using LuxVideoDet.Core;
using System;
using System.IO;

namespace LuxVideoDet.Desktop;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // macOS 从 .app 双击启动时，工作目录常为 /，相对路径（logs/、configs.json 等）会失败并直接导致进程退出。
        try
        {
            var dir = AppContext.BaseDirectory;
            if (!string.IsNullOrEmpty(dir))
                Directory.SetCurrentDirectory(dir);
        }
        catch
        {
            // 若设置失败则沿用系统默认工作目录
        }

        PrintBanner();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    static void PrintBanner()
    {
        const string banner = @"
  ██╗     ██╗   ██╗██╗  ██╗██╗   ██╗██╗██████╗ ███████╗ ██████╗ ██████╗ ███████╗████████╗
  ██║     ██║   ██║╚██╗██╔╝██║   ██║██║██╔══██╗██╔════╝██╔═══██╗██╔══██╗██╔════╝╚══██╔══╝
  ██║     ██║   ██║ ╚███╔╝ ██║   ██║██║██║  ██║█████╗  ██║   ██║██║  ██║█████╗     ██║
  ██║     ██║   ██║ ██╔██╗ ╚██╗ ██╔╝██║██║  ██║██╔══╝  ██║   ██║██║  ██║██╔══╝     ██║
  ███████╗╚██████╔╝██╔╝ ██╗ ╚████╔╝ ██║██████╔╝███████╗╚██████╔╝██████╔╝███████╗   ██║
  ╚══════╝ ╚═════╝ ╚═╝  ╚═╝  ╚═══╝  ╚═╝╚═════╝ ╚══════╝ ╚═════╝ ╚═════╝ ╚══════╝   ╚═╝
";

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(banner);
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  ──────────────────────────────────────────────────────────────────────────────────");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("  跨平台视频检测系统 — Desktop 桌面应用");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  v{AppMetadata.DisplayVersion} | .NET {Environment.Version} | {Environment.OSVersion}");
        Console.WriteLine();
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  ✓ 桌面应用已启动");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  ──────────────────────────────────────────────────────────────────────────────────");
        Console.ResetColor();
        Console.WriteLine();
    }
}
