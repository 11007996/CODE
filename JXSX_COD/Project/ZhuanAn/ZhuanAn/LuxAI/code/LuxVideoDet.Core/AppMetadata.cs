using System.Reflection;

namespace LuxVideoDet.Core;

/// <summary>应用版本信息（由 Directory.Build.props 中的 Version 生成到程序集）。</summary>
public static class AppMetadata
{
    private static readonly Assembly CoreAssembly = typeof(AppMetadata).Assembly;

    /// <summary>
    /// 完整 InformationalVersion。在 Git 仓库内构建时 SDK 常会追加 <c>+&lt;commit&gt;</c>，用于日志与排障。
    /// </summary>
    public static string InformationalVersion =>
        CoreAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
        ?? CoreAssembly.GetName().Version?.ToString()
        ?? "0.0.0";

    /// <summary>界面/对外展示：仅 SemVer 核心与预发布标识，不含 <c>+</c> 后的构建元数据（Git 哈希等）。</summary>
    public static string DisplayVersion
    {
        get
        {
            var s = InformationalVersion;
            var plus = s.IndexOf('+');
            return plus >= 0 ? s[..plus] : s;
        }
    }
}
