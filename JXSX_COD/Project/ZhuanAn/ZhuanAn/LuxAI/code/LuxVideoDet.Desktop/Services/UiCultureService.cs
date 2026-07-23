using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace LuxVideoDet.Desktop.Services;

public sealed class UiCultureService : IUiCultureService
{
    private static readonly JsonSerializerOptions JsonWriteOptions = new() { WriteIndented = true };
    private readonly string _path;
    private string _currentCultureName = "zh-CN";

    public UiCultureService()
    {
        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LuxVideoDet");
        Directory.CreateDirectory(dir);
        _path = Path.Combine(dir, "ui-settings.json");
    }

    public string CurrentCultureName => _currentCultureName;

    public event EventHandler? CultureChanged;

    public void ApplySavedCulture()
    {
        try
        {
            if (File.Exists(_path))
            {
                using var doc = JsonDocument.Parse(File.ReadAllText(_path));
                if (doc.RootElement.TryGetProperty("uiCulture", out var el))
                {
                    var name = el.GetString();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        ApplyCore(name.Trim(), persist: false);
                        return;
                    }
                }
            }
        }
        catch
        {
            // fall through to default
        }

        ApplyCore("zh-CN", persist: false);
    }

    public void SetUiCulture(string cultureName)
    {
        if (string.IsNullOrWhiteSpace(cultureName))
            return;
        ApplyCore(cultureName.Trim(), persist: true);
    }

    private void ApplyCore(string cultureName, bool persist)
    {
        try
        {
            var c = CultureInfo.GetCultureInfo(cultureName);
            CultureInfo.DefaultThreadCurrentUICulture = c;
            CultureInfo.DefaultThreadCurrentCulture = c;
            Thread.CurrentThread.CurrentUICulture = c;
            Thread.CurrentThread.CurrentCulture = c;
            _currentCultureName = c.Name;
            if (persist)
            {
                File.WriteAllText(
                    _path,
                    JsonSerializer.Serialize(new Dictionary<string, string> { ["uiCulture"] = c.Name }, JsonWriteOptions));
            }

            CultureChanged?.Invoke(this, EventArgs.Empty);
        }
        catch (CultureNotFoundException)
        {
            // ignore invalid culture
        }
    }
}
