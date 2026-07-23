using System.Globalization;

namespace LuxVideoDet.Localization;

/// <summary>
/// 读取 <see cref="Resources.Strings"/> 资源。
/// </summary>
public interface IAppLocalizer
{
    /// <summary>使用 <see cref="CultureInfo.CurrentUICulture"/> 查找。</summary>
    string GetString(string key);

    /// <summary>使用指定的 UI 区域查找（不依赖当前线程）。</summary>
    string GetString(string key, CultureInfo uiCulture);

    string this[string key] => GetString(key);
}
