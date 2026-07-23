using System;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using LuxVideoDet.Core.Notification;

namespace LuxVideoDet.Desktop.ViewModels;

/// <summary>
/// 单个通知参数行：按 <see cref="NotificationParameterDefinition.ParameterType"/> 选择编辑器。
/// </summary>
public partial class NotifierParameterRowViewModel : ViewModelBase
{
    public NotifierParameterRowViewModel(NotificationParameterDefinition definition, string rawValue)
    {
        Definition = definition;
        RawValue = rawValue;
    }

    public NotificationParameterDefinition Definition { get; }

    [ObservableProperty]
    private string _rawValue;

    public bool IsBool => string.Equals(Definition.ParameterType, "bool", StringComparison.OrdinalIgnoreCase);

    public bool IsInt => string.Equals(Definition.ParameterType, "int", StringComparison.OrdinalIgnoreCase);

    public bool IsDouble => string.Equals(Definition.ParameterType, "double", StringComparison.OrdinalIgnoreCase);

    public bool IsString => !(IsBool || IsInt || IsDouble);

    public bool BoolEditor
    {
        get => bool.TryParse(RawValue, out var b) && b;
        set
        {
            var s = value ? "true" : "false";
            if (RawValue == s) return;
            RawValue = s;
            OnPropertyChanged(nameof(BoolEditor));
        }
    }

    public int IntEditor
    {
        get
        {
            if (int.TryParse(RawValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i))
                return i;
            return CoerceIntDefault(Definition.DefaultValue);
        }
        set
        {
            var s = value.ToString(CultureInfo.InvariantCulture);
            if (RawValue == s) return;
            RawValue = s;
            OnPropertyChanged(nameof(IntEditor));
        }
    }

    public double DoubleEditor
    {
        get
        {
            if (double.TryParse(RawValue, NumberStyles.Float | NumberStyles.AllowThousands,
                    CultureInfo.InvariantCulture, out var d))
                return d;
            return CoerceDoubleDefault(Definition.DefaultValue);
        }
        set
        {
            var s = value.ToString(CultureInfo.InvariantCulture);
            if (RawValue == s) return;
            RawValue = s;
            OnPropertyChanged(nameof(DoubleEditor));
        }
    }

    partial void OnRawValueChanged(string value)
    {
        OnPropertyChanged(nameof(BoolEditor));
        OnPropertyChanged(nameof(IntEditor));
        OnPropertyChanged(nameof(DoubleEditor));
    }

    private static int CoerceIntDefault(object? o)
    {
        if (o is int i) return i;
        if (o != null && int.TryParse(o.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var p))
            return p;
        return 0;
    }

    private static double CoerceDoubleDefault(object? o)
    {
        if (o is double d) return d;
        if (o is float f) return f;
        if (o != null && double.TryParse(o.ToString(), NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture, out var p))
            return p;
        return 0;
    }
}
