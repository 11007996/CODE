using System;

namespace LuxVideoDet.Desktop.ViewModels;

/// <summary>
/// 通知渠道下拉项：内部键 + 显示名。
/// </summary>
public sealed class NotifierTypePick : IEquatable<NotifierTypePick>
{
    public NotifierTypePick(string key, string displayName)
    {
        Key = key;
        DisplayName = displayName;
    }

    public string Key { get; }
    public string DisplayName { get; }

    public override string ToString() => DisplayName;

    public bool Equals(NotifierTypePick? other) =>
        other is not null && string.Equals(Key, other.Key, StringComparison.OrdinalIgnoreCase);

    public override bool Equals(object? obj) => obj is NotifierTypePick o && Equals(o);

    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Key);
}
