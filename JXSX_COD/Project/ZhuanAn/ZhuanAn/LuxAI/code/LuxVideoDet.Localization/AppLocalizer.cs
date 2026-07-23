using System.Globalization;
using System.Resources;

namespace LuxVideoDet.Localization;

public sealed class AppLocalizer : IAppLocalizer
{
    private static readonly ResourceManager Rm = new(
        "LuxVideoDet.Localization.Resources.Strings",
        typeof(AppLocalizer).Assembly);

    public string GetString(string key) =>
        GetString(key, CultureInfo.CurrentUICulture);

    public string GetString(string key, CultureInfo uiCulture) =>
        string.IsNullOrEmpty(key) ? string.Empty : Rm.GetString(key, uiCulture) ?? key;
}
