using System.Collections.Concurrent;
using LuxVideoDet.Core.Configuration.Models;

namespace LuxVideoDet.Core.Inference;

/// <summary>
/// 单点注册：推理设备在 UI 中的展示名与当前运行环境下是否作为「可选」列出。
/// 新增设备时：在 <see cref="InferenceDevice"/> 增加枚举值后，将对应条目加入 <see cref="All"/>，
/// 并在 <see cref="IsDeviceListedForCurrentRuntime"/> 中补充可用性规则。
/// </summary>
public static class InferenceDeviceRegistry
{
    private static readonly InferenceDeviceDescriptor[] All =
    {
        new(InferenceDevice.CPU, "CPU"),
        new(InferenceDevice.OpenVINO, "OpenVINO"),
        new(InferenceDevice.GPU, "GPU"),
        new(InferenceDevice.QNN, "QNN"),
        new(InferenceDevice.CoreML, "CoreML"),
        new(InferenceDevice.TensorRT, "TensorRT")
    };

    private static readonly ConcurrentDictionary<InferenceDevice, IReadOnlyList<InferenceDeviceDescriptor>> UiCache = new();

    /// <summary>全部已注册设备（按枚举顺序）。</summary>
    public static IReadOnlyList<InferenceDeviceDescriptor> AllDescriptors => All;

    /// <summary>
    /// 当前运行环境下可作为默认选项列出的设备（与 <see cref="OnnxInferenceEngine"/> 等平台逻辑一致）。
    /// </summary>
    public static bool IsDeviceListedForCurrentRuntime(InferenceDevice device) => device switch
    {
        InferenceDevice.CoreML => OperatingSystem.IsMacOS(),
        InferenceDevice.OpenVINO => OperatingSystem.IsWindows() || OperatingSystem.IsLinux(),
        InferenceDevice.TensorRT => OperatingSystem.IsWindows() || OperatingSystem.IsLinux(),
        _ => true
    };

    /// <summary>
    /// 当前运行环境下应出现在下拉框中的设备（不含「仅因配置已保存而需显示」的例外）。
    /// </summary>
    public static IReadOnlyList<InferenceDeviceDescriptor> GetDescriptorsForCurrentRuntime()
    {
        return All.Where(d => IsDeviceListedForCurrentRuntime(d.Device)).ToArray();
    }

    /// <summary>
    /// 用于配置编辑 UI：包含当前运行环境下可选设备；若 <paramref name="currentDevice"/> 不在可选集中（例如从 macOS 拷到 Linux 的配置仍为 CoreML），则追加该项以便展示与修改。
    /// </summary>
    public static IReadOnlyList<InferenceDeviceDescriptor> GetDescriptorsForUi(InferenceDevice currentDevice) =>
        UiCache.GetOrAdd(currentDevice, BuildUiList);

    private static IReadOnlyList<InferenceDeviceDescriptor> BuildUiList(InferenceDevice current)
    {
        var list = GetDescriptorsForCurrentRuntime().ToList();
        var set = new HashSet<InferenceDevice>(list.Select(d => d.Device));
        if (!set.Contains(current))
        {
            list.Add(GetDescriptor(current));
            list.Sort((a, b) => ((int)a.Device).CompareTo((int)b.Device));
        }

        return list;
    }

    /// <summary>按枚举值取注册项（必须已在 <see cref="All"/> 中注册）。</summary>
    public static InferenceDeviceDescriptor GetDescriptor(InferenceDevice device) =>
        All.First(d => d.Device == device);
}

/// <summary>推理设备在界面上的展示项。</summary>
public readonly record struct InferenceDeviceDescriptor(InferenceDevice Device, string DisplayName);
