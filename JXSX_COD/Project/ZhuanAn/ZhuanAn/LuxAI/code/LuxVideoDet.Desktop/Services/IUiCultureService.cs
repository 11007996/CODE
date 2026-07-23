using System;

namespace LuxVideoDet.Desktop.Services;

/// <summary>桌面端 UI 文化：持久化至本机，切换时通知界面刷新。</summary>
public interface IUiCultureService
{
    string CurrentCultureName { get; }

    void ApplySavedCulture();

    void SetUiCulture(string cultureName);

    event EventHandler? CultureChanged;
}
