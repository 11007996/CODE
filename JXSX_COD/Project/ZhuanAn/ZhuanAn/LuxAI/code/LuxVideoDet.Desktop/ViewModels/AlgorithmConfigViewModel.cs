using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Configuration;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Notification;
using LuxVideoDet.Localization;

namespace LuxVideoDet.Desktop.ViewModels;

/// <summary>
/// 单个算法的配置 ViewModel，支持推理/区域/存储/通知参数编辑。
/// </summary>
public partial class AlgorithmConfigViewModel : ViewModelBase
{
    private readonly IAppLocalizer _appLocalizer;

    public IAppLocalizer AppLocalizer => _appLocalizer;

    public AlgorithmConfigViewModel(IAppLocalizer appLocalizer)
    {
        _appLocalizer = appLocalizer;
    }
    [ObservableProperty]
    private string _algorithmType = string.Empty;

    [ObservableProperty]
    private string _displayName = string.Empty;

    [ObservableProperty]
    private string _modelPath = string.Empty;

    [ObservableProperty]
    private InferenceDevice _device = InferenceDevice.CPU;

    [ObservableProperty]
    private float _confidenceThreshold = 0.5f;

    [ObservableProperty]
    private float _iouThreshold = 0.5f;

    [ObservableProperty]
    private int _inputSize = 640;

    [ObservableProperty]
    private string _classes = string.Empty;

    [ObservableProperty]
    private ModelType _modelType = ModelType.Auto;

    [ObservableProperty]
    private string _regionsJson = "[]";

    /// <summary>
    /// 当前算法需要的功能区定义（来自 Core 层）
    /// </summary>
    public List<RegionDefinition> RequiredRegions
        => DetectionAlgorithmFactory.GetRequiredRegions(AlgorithmType);

    public string ClassesHint => _appLocalizer.GetString(UiKeys.Editor_ClassesHint);

    [ObservableProperty]
    private bool _saveErrorImage = true;

    [ObservableProperty]
    private bool _saveVideo;

    [ObservableProperty]
    private int _videoDuration = 10;

    /// <summary>NG 录像是否按源分辨率保存（不缩放）；否则为自动≤2K 或 <see cref="RecordingMaxWidth"/>。</summary>
    [ObservableProperty]
    private bool _ngVideoUseSourceResolution;

    [ObservableProperty]
    private bool _notificationEnabled;

    /// <summary>
    /// 通知渠道列表（与 <see cref="NotificationConfig.Notifiers"/> 对应）。
    /// </summary>
    public ObservableCollection<NotifierConfigItemViewModel> NotificationNotifiers { get; } = new();

    /// <summary>
    /// AOI 相关 <c>args</c> 表单（来自 <see cref="DetectionAlgorithmFactory.GetAoiParameterSections"/>）。
    /// </summary>
    public ObservableCollection<AlgorithmArgsFormSectionViewModel> AoiParameterGroups { get; } = new();

    /// <summary>当前算法是否声明了 AOI 参数表单。</summary>
    public bool HasAoiParameterSections => AoiParameterGroups.Count > 0;

    /// <summary>
    /// 纯算法逻辑 <c>args</c> 表单（来自 <see cref="DetectionAlgorithmFactory.GetAlgorithmParameterSections"/>）。
    /// </summary>
    public ObservableCollection<AlgorithmArgsFormSectionViewModel> AlgorithmParameterGroups { get; } = new();

    /// <summary>当前算法是否声明了算法参数表单。</summary>
    public bool HasAlgorithmParameterSections => AlgorithmParameterGroups.Count > 0;

    /// <summary>与 <see cref="AlgorithmConfig.Args"/> 对齐；其它键（如 <c>messages</c>）也会保留。</summary>
    private Dictionary<string, JsonElement> _args = new(StringComparer.OrdinalIgnoreCase);

    private IReadOnlyList<NotifierTypePick>? _notifierTypeChoicesCache;

    /// <summary>
    /// 当前算法允许选择的通知类型（来自 <see cref="IAlgorithmDescriptor.SupportedNotifierTypes"/> 与系统注册渠道的交集）。
    /// </summary>
    public IReadOnlyList<NotifierTypePick> NotifierTypeChoices =>
        _notifierTypeChoicesCache ??= BuildNotifierTypeChoices();

    private IReadOnlyList<NotifierTypePick> BuildNotifierTypeChoices() =>
        DetectionAlgorithmFactory.GetAllowedNotifierTypeKeys(AlgorithmType)
            .Select(k => new NotifierTypePick(k, NotificationServiceFactory.GetDisplayName(k)))
            .ToArray();

    partial void OnAlgorithmTypeChanged(string value)
    {
        _notifierTypeChoicesCache = null;
        OnPropertyChanged(nameof(NotifierTypeChoices));
        RefreshClassesFromDescriptor();
        RebuildAllArgsFormSections();
    }

    /// <summary>
    /// 与 <see cref="DetectionAlgorithmFactory.GetDefaultClasses"/> 同步；保存时亦只写该列表。
    /// </summary>
    private void RefreshClassesFromDescriptor()
    {
        var defaults = DetectionAlgorithmFactory.GetDefaultClasses(AlgorithmType);
        Classes = defaults.Count > 0 ? string.Join("\n", defaults) : string.Empty;
    }

    [RelayCommand]
    private void AddNotificationChannel()
    {
        NotificationNotifiers.Add(new NotifierConfigItemViewModel(this));
    }

    public void RemoveNotifierItem(NotifierConfigItemViewModel item) => NotificationNotifiers.Remove(item);

    [ObservableProperty]
    private bool _enabled = true;

    // --- ComboBox 绑定（Device，选项来自 <see cref="InferenceDeviceRegistry"/>） ---

    private IReadOnlyList<InferenceDeviceDescriptor>? _inferenceDeviceChoices;

    /// <summary>
    /// 当前算法可选的推理设备（随运行环境变化；不随当前 <see cref="Device"/> 每变一次就换列表引用）。
    /// 若在 <see cref="OnDeviceChanged"/> 里清空并刷新本列表，Avalonia <c>ComboBox</c> 会在 <c>ItemsSource</c> 替换时把
    /// <c>SelectedIndex</c> 置为 0 并回写绑定，把设备改回列表第一项（常为 CPU），出现「要点两次」且保存仍为 GPU 等问题。
    /// </summary>
    public IReadOnlyList<InferenceDeviceDescriptor> InferenceDeviceChoices =>
        _inferenceDeviceChoices ??= InferenceDeviceRegistry.GetDescriptorsForUi(Device);

    /// <summary>
    /// 与 <see cref="Device"/> 对应的下拉索引。
    /// Avalonia <c>ComboBox.SelectedItem</c> 对值类型/结构体用引用相等，选项列表刷新后无法与 <see cref="InferenceDeviceDescriptor"/> 匹配，
    /// 会导致无法切换设备；因此用 <c>SelectedIndex</c> 绑定本属性。
    /// </summary>
    public int InferenceDeviceSelectedIndex
    {
        get
        {
            var choices = InferenceDeviceChoices;
            for (var i = 0; i < choices.Count; i++)
            {
                if (choices[i].Device == Device)
                    return i;
            }

            return 0;
        }
        set
        {
            var choices = InferenceDeviceChoices;
            if (value < 0 || value >= choices.Count)
                return;
            var next = choices[value].Device;
            if (Device != next)
                Device = next;
        }
    }

    partial void OnDeviceChanged(InferenceDevice value)
    {
        // 禁止在此刷新 InferenceDeviceChoices：见该类上对 ItemsSource 的说明。
        OnPropertyChanged(nameof(InferenceDeviceSelectedIndex));
    }

    /// <summary>模型类型下拉行（含不可选的预留项 Track）。</summary>
    public ModelTypePickerItem[] ModelTypePickerItems => ModelTypeYoloLabels.PickerOrdered;

    public void RefreshLocalization()
    {
        OnPropertyChanged(nameof(ClassesHint));
        foreach (var n in NotificationNotifiers)
            n.ApplyNotifierChrome();
        OnPropertyChanged(nameof(AoiSectionTitle));
        OnPropertyChanged(nameof(AoiSectionHint));
        OnPropertyChanged(nameof(AlgorithmParameterSectionTitle));
        OnPropertyChanged(nameof(AlgorithmParameterSectionHint));
    }

    /// <summary>AOI 参数区块标题（本地化）。</summary>
    public string AoiSectionTitle => _appLocalizer.GetString(UiKeys.Editor_SectionAoi);

    /// <summary>AOI 参数脚注。</summary>
    public string AoiSectionHint => _appLocalizer.GetString(UiKeys.Editor_HintAoiArgs);

    /// <summary>纯算法参数区块标题（本地化）。</summary>
    public string AlgorithmParameterSectionTitle => _appLocalizer.GetString(UiKeys.Editor_SectionAlgorithmParameters);

    /// <summary>纯算法参数脚注。</summary>
    public string AlgorithmParameterSectionHint => _appLocalizer.GetString(UiKeys.Editor_HintAlgorithmParameters);

    public int ModelTypeIndex
    {
        get => (int)ModelType;
        set
        {
            var items = ModelTypePickerItems;
            if (value < 0 || value >= items.Length)
                return;

            var next = items[value].Type;
            if (!ModelTypeYoloLabels.IsSelectable(next))
            {
                OnPropertyChanged();
                return;
            }

            ModelType = next;
            OnPropertyChanged();
        }
    }

    partial void OnModelTypeChanged(ModelType value)
        => OnPropertyChanged(nameof(ModelTypeIndex));

    // --- Args / AOI ---

    internal bool TryGetArgElement(string key, out JsonElement element) =>
        _args.TryGetValue(key, out element);

    internal static string ResolveArgDisplayRaw(AlgorithmConfigViewModel owner, NotificationParameterDefinition def)
    {
        if (!owner.TryGetArgElement(def.Name, out var el))
            return FormatArgDefaultForEditor(def);

        return JsonElementToDisplayString(el, def);
    }

    private static string FormatArgDefaultForEditor(NotificationParameterDefinition def)
    {
        if (def.ParameterType.Equals("string", StringComparison.OrdinalIgnoreCase) &&
            def.DefaultValue is string s)
            return s;

        return FormatDefaultStatic(def.DefaultValue);
    }

    private static string FormatDefaultStatic(object? o)
    {
        if (o == null) return string.Empty;
        if (o is bool b) return b ? "true" : "false";
        if (o is IFormattable f) return f.ToString(null, CultureInfo.InvariantCulture) ?? string.Empty;
        return o.ToString() ?? string.Empty;
    }

    private static string JsonElementToDisplayString(JsonElement el, NotificationParameterDefinition def)
    {
        var t = def.ParameterType?.ToLowerInvariant() ?? "string";
        return el.ValueKind switch
        {
            JsonValueKind.String => el.GetString() ?? "",
            JsonValueKind.True => "true",
            JsonValueKind.False => "false",
            JsonValueKind.Number when t == "int" || t == "double" => el.GetRawText(),
            JsonValueKind.Number => el.GetRawText(),
            _ => el.ToString()
        };
    }

    private void RebuildAllArgsFormSections()
    {
        AoiParameterGroups.Clear();
        foreach (var section in DetectionAlgorithmFactory.GetAoiParameterSections(AlgorithmType))
            AoiParameterGroups.Add(new AlgorithmArgsFormSectionViewModel(this, section));
        OnPropertyChanged(nameof(HasAoiParameterSections));

        AlgorithmParameterGroups.Clear();
        foreach (var section in DetectionAlgorithmFactory.GetAlgorithmParameterSections(AlgorithmType))
            AlgorithmParameterGroups.Add(new AlgorithmArgsFormSectionViewModel(this, section));
        OnPropertyChanged(nameof(HasAlgorithmParameterSections));
    }

    /// <summary>
    /// 将 AOI + 算法参数表单行写回 <see cref="_args"/>（仅更新描述符声明的键，其它 <c>args</c> 键保留）。
    /// </summary>
    private void ApplyArgsFromFormGroups()
    {
        foreach (var group in AoiParameterGroups)
        {
            foreach (var row in group.ParameterRows)
            {
                var def = row.Definition;
                var raw = row.RawValue.Trim();

                if (string.IsNullOrEmpty(raw) && !def.Required)
                {
                    _args.Remove(def.Name);
                    continue;
                }

                try
                {
                    _args[def.Name] = RawToJsonElement(raw, def);
                }
                catch
                {
                    // 解析失败时保留原值或跳过
                }
            }
        }

        foreach (var group in AlgorithmParameterGroups)
        {
            foreach (var row in group.ParameterRows)
            {
                var def = row.Definition;
                var raw = row.RawValue.Trim();

                if (string.IsNullOrEmpty(raw) && !def.Required)
                {
                    _args.Remove(def.Name);
                    continue;
                }

                try
                {
                    _args[def.Name] = RawToJsonElement(raw, def);
                }
                catch
                {
                    // 解析失败时保留原值或跳过
                }
            }
        }
    }

    private static JsonElement RawToJsonElement(string raw, NotificationParameterDefinition def)
    {
        var t = def.ParameterType?.ToLowerInvariant() ?? "string";
        return t switch
        {
            "bool" => JsonSerializer.SerializeToElement(bool.Parse(raw)),
            "int" => JsonSerializer.SerializeToElement(int.Parse(raw, CultureInfo.InvariantCulture)),
            "double" => JsonSerializer.SerializeToElement(double.Parse(raw, CultureInfo.InvariantCulture)),
            "string" => StringRawToJsonElement(raw),
            _ => JsonSerializer.SerializeToElement(raw)
        };
    }

    /// <summary>
    /// 可选字符串字段：空则不写入；纯数字则写入 JSON 数字（供 U 型检测器参数等）。
    /// </summary>
    private static JsonElement StringRawToJsonElement(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return JsonSerializer.SerializeToElement("");

        if (int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i))
            return JsonSerializer.SerializeToElement(i);

        if (double.TryParse(raw, NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture, out var d))
            return JsonSerializer.SerializeToElement(d);

        return JsonSerializer.SerializeToElement(raw);
    }

    // --- 转换方法 ---

    public AlgorithmConfig ToAlgorithmConfig()
    {
        ApplyArgsFromFormGroups();

        var config = new AlgorithmConfig
        {
            AlgorithmType = AlgorithmType,
            DisplayName = DisplayName,
            Enabled = Enabled,
            Inference = new InferenceConfig
            {
                ModelPath = ModelPath,
                Device = Device,
                ModelType = ModelType,
                ConfidenceThreshold = ConfidenceThreshold,
                IouThreshold = IouThreshold,
                InputSize = new ImageSize { Width = InputSize, Height = InputSize },
                Classes = DetectionAlgorithmFactory.GetDefaultClasses(AlgorithmType).ToList()
            },
            Storage = new StorageConfig
            {
                SaveErrorImage = SaveErrorImage,
                SaveVideo = SaveVideo,
                VideoDuration = VideoDuration,
                NgVideoUseSourceResolution = NgVideoUseSourceResolution
            },
            Notification = new NotificationConfig
            {
                Enabled = NotificationEnabled,
                Notifiers = NotificationNotifiers
                    .Select(n => new NotifierConfig
                    {
                        Enabled = n.Enabled,
                        Type = n.TypeKey,
                        Parameters = n.CollectParameters()
                    })
                    .ToList()
            }
        };

        try
        {
            config.Regions = System.Text.Json.JsonSerializer.Deserialize<List<RegionConfig>>(RegionsJson)
                ?? new List<RegionConfig>();
        }
        catch
        {
            config.Regions = new List<RegionConfig>();
        }

        if (_args.Count > 0)
            config.Args = new Dictionary<string, JsonElement>(_args, StringComparer.OrdinalIgnoreCase);

        return config;
    }

    public static AlgorithmConfigViewModel FromAlgorithmConfig(AlgorithmConfig config, IAppLocalizer appLocalizer)
    {
        var defaultClasses = DetectionAlgorithmFactory.GetDefaultClasses(config.AlgorithmType);
        var classesText = defaultClasses.Count > 0 ? string.Join("\n", defaultClasses) : string.Empty;

        var vm = new AlgorithmConfigViewModel(appLocalizer)
        {
            AlgorithmType = config.AlgorithmType,
            DisplayName = config.DisplayName,
            Enabled = config.Enabled,
            ModelPath = config.Inference.ModelPath,
            Device = config.Inference.Device,
            ModelType = config.Inference.ModelType,
            ConfidenceThreshold = config.Inference.ConfidenceThreshold,
            IouThreshold = config.Inference.IouThreshold,
            InputSize = config.Inference.InputSize.Width,
            Classes = classesText,
            RegionsJson = System.Text.Json.JsonSerializer.Serialize(
                config.Regions, ConfigurationJsonOptions.ForRegionsJsonCompact),
            SaveErrorImage = config.Storage.SaveErrorImage,
            SaveVideo = config.Storage.SaveVideo,
            VideoDuration = config.Storage.VideoDuration,
            NgVideoUseSourceResolution = config.Storage.NgVideoUseSourceResolution,
            NotificationEnabled = config.Notification.Enabled
        };

        foreach (var nc in config.Notification.Notifiers ?? new List<NotifierConfig>())
            vm.NotificationNotifiers.Add(new NotifierConfigItemViewModel(vm, nc));

        if (config.Args != null)
        {
            foreach (var kv in config.Args)
                vm._args[kv.Key] = kv.Value;
        }

        vm.RebuildAllArgsFormSections();
        return vm;
    }
}
