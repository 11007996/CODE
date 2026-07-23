using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Notification;
using LuxVideoDet.Localization;

namespace LuxVideoDet.Desktop.ViewModels;

/// <summary>
/// 算法配置中一条通知渠道：类型、启用、动态参数行。
/// </summary>
public partial class NotifierConfigItemViewModel : ViewModelBase
{
    private readonly AlgorithmConfigViewModel _owner;
    private bool _loading;
    private bool _syncingTypeKey;

    public NotifierConfigItemViewModel(AlgorithmConfigViewModel owner, NotifierConfig? cfg = null)
    {
        _owner = owner;

        var saved = cfg?.Parameters != null
            ? new Dictionary<string, string>(cfg.Parameters, StringComparer.OrdinalIgnoreCase)
            : new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        _loading = true;
        try
        {
            Enabled = cfg?.Enabled ?? true;
            var key = !string.IsNullOrWhiteSpace(cfg?.Type)
                ? cfg!.Type.Trim()
                : owner.NotifierTypeChoices.FirstOrDefault()?.Key ?? "speaker";
            TypeKey = key;

            var pick = TypeChoices.FirstOrDefault(t =>
                           string.Equals(t.Key, TypeKey, StringComparison.OrdinalIgnoreCase))
                       ?? TypeChoices.FirstOrDefault();
            if (pick != null)
            {
                if (!string.Equals(TypeKey, pick.Key, StringComparison.OrdinalIgnoreCase))
                    TypeKey = pick.Key;
                SelectedTypePick = pick;
            }
        }
        finally
        {
            _loading = false;
        }

        RebuildParameters(saved);
        ApplyNotifierChrome();
    }

    [ObservableProperty]
    private bool _enabled = true;

    [ObservableProperty]
    private string _typeKey = "speaker";

    [ObservableProperty]
    private NotifierTypePick? _selectedTypePick;

    [ObservableProperty]
    private ObservableCollection<NotifierParameterRowViewModel> _parameterRows = new();

    [ObservableProperty]
    private string _notifierDeleteButtonText = string.Empty;

    [ObservableProperty]
    private string _notifierEnableChannelTooltip = string.Empty;

    public IReadOnlyList<NotifierTypePick> TypeChoices => _owner.NotifierTypeChoices;

    public void ApplyNotifierChrome()
    {
        var loc = _owner.AppLocalizer;
        NotifierDeleteButtonText = loc.GetString(UiKeys.Editor_BtnDeleteNotifier);
        NotifierEnableChannelTooltip = loc.GetString(UiKeys.Editor_NotifierEnableTooltip);
    }

    [RelayCommand]
    private void RemoveSelf() => _owner.RemoveNotifierItem(this);

    partial void OnTypeKeyChanged(string value)
    {
        if (_loading || _syncingTypeKey) return;

        var pick = TypeChoices.FirstOrDefault(t =>
                       string.Equals(t.Key, value, StringComparison.OrdinalIgnoreCase))
                   ?? TypeChoices.FirstOrDefault();
        if (pick == null) return;

        _syncingTypeKey = true;
        try
        {
            if (!string.Equals(TypeKey, pick.Key, StringComparison.OrdinalIgnoreCase))
                TypeKey = pick.Key;
            SelectedTypePick = pick;
        }
        finally
        {
            _syncingTypeKey = false;
        }

        RebuildParameters(null);
    }

    partial void OnSelectedTypePickChanged(NotifierTypePick? value)
    {
        if (_loading || _syncingTypeKey || value == null) return;
        if (!string.Equals(TypeKey, value.Key, StringComparison.OrdinalIgnoreCase))
            TypeKey = value.Key;
    }

    private void RebuildParameters(IReadOnlyDictionary<string, string>? saved)
    {
        var defs = NotificationServiceFactory.GetRequiredParameters(TypeKey);
        var algoDef = DetectionAlgorithmFactory.GetDefaultNotifierParameters(_owner.AlgorithmType, TypeKey);

        ParameterRows.Clear();
        foreach (var def in defs)
        {
            string resolved;
            if (saved != null && saved.TryGetValue(def.Name, out var sv))
                resolved = sv;
            else if (algoDef != null && algoDef.TryGetValue(def.Name, out var av))
                resolved = av;
            else
                resolved = FormatDefault(def.DefaultValue);

            ParameterRows.Add(new NotifierParameterRowViewModel(def, resolved));
        }
    }

    private static string FormatDefault(object? o)
    {
        if (o == null) return string.Empty;
        if (o is bool b) return b ? "true" : "false";
        if (o is IFormattable f) return f.ToString(null, CultureInfo.InvariantCulture) ?? string.Empty;
        return o.ToString() ?? string.Empty;
    }

    public Dictionary<string, string> CollectParameters()
    {
        return ParameterRows.ToDictionary(
            r => r.Definition.Name,
            r => r.RawValue,
            StringComparer.OrdinalIgnoreCase);
    }
}
