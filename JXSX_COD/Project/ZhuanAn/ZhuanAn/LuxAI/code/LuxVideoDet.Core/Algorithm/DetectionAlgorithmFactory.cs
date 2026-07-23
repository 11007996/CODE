using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using LuxVideoDet.Core.Algorithm.Ui;
using LuxVideoDet.Core.Configuration.Models;
using LuxVideoDet.Core;
using LuxVideoDet.Core.Configuration.Validation;
using LuxVideoDet.Core.Inference;
using LuxVideoDet.Core.Notification;
using LuxVideoDet.Core.Region;
using Microsoft.Extensions.Logging;
using OpenCvSharp;

namespace LuxVideoDet.Core.Algorithm;

/// <summary>
/// 检测算法工厂 — 通过反射自动发现所有 <see cref="IAlgorithmDescriptor"/> 实现，
/// 新增算法只需在对应文件夹添加 Descriptor 即可，无需修改此文件。
/// </summary>
public class DetectionAlgorithmFactory
{
    private static readonly Dictionary<string, IAlgorithmDescriptor> _descriptors;

    static DetectionAlgorithmFactory()
    {
        _descriptors = DiscoverDescriptors();
    }

    private readonly IServiceProvider _serviceProvider;
    private readonly InferenceEngineFactory _inferenceEngineFactory;
    private readonly NotificationServiceFactory _notificationServiceFactory;
    private readonly ILoggerFactory _loggerFactory;

    public DetectionAlgorithmFactory(
        IServiceProvider serviceProvider,
        InferenceEngineFactory inferenceEngineFactory,
        NotificationServiceFactory notificationServiceFactory,
        ILoggerFactory loggerFactory)
    {
        _serviceProvider = serviceProvider;
        _inferenceEngineFactory = inferenceEngineFactory;
        _notificationServiceFactory = notificationServiceFactory;
        _loggerFactory = loggerFactory;
    }

    /// <summary>
    /// 从算法配置创建检测算法实例（异步，含模型加载）。
    /// </summary>
    public async Task<IDetectionAlgorithm> CreateAlgorithmAsync(AlgorithmConfig config)
    {
        var log = _loggerFactory.CreateLogger<DetectionAlgorithmFactory>();

        log.LogInformation(
            "[配置·算法] 实例化 [{AlgorithmType}] 显示名={DisplayName}",
            config.AlgorithmType, config.DisplayName);

        var typeKey = config.AlgorithmType.ToLower();
        if (!_descriptors.TryGetValue(typeKey, out var descriptor))
            throw new ArgumentException($"不支持的算法类型: {config.AlgorithmType}");

        AlgorithmRegionRequirements.EnsureRequiredRegionsPresent(config);

        // 1. 推理引擎（[推理运行时] 见引擎内单条；此处保留路径便于与流水线日志对照）
        var modelPathResolved = string.IsNullOrWhiteSpace(config.Inference.ModelPath)
            ? config.Inference.ModelPath
            : Path.GetFullPath(config.Inference.ModelPath);
        log.LogInformation(
            "[配置·推理] 正在加载模型: {ModelPath} | 枚举设备={Device} | 输入={InputSize} | 配置类别数={ClassCount} | ModelType={ModelType}",
            modelPathResolved,
            config.Inference.Device,
            $"{config.Inference.InputSize.Width}x{config.Inference.InputSize.Height}",
            config.Inference.Classes.Count,
            ModelTypeYoloLabels.Format(config.Inference.ModelType));

        var inferenceEngine = await _inferenceEngineFactory
            .CreateEngineAsync(config.Inference, CancellationToken.None)
            .ConfigureAwait(false);

        var modelInfo = inferenceEngine.GetModelInfo();
        log.LogInformation(
            "[配置·推理] 模型已载入: {ModelPath} | 任务={TaskType} | 类别数={ClassCount}",
            modelPathResolved,
            ModelTypeYoloLabels.Format(modelInfo.Type), modelInfo.ClassCount);

        LogClassAlignmentForRun(log, config, inferenceEngine, modelInfo, descriptor);

        // 2. 区域管理器
        var regionManager = new RegionManager(_loggerFactory.CreateLogger<RegionManager>());
        if (config.Regions is { Count: > 0 })
            regionManager.LoadFromConfig(config.Regions, config.AlgorithmType);

        // 3. 通知器
        var notifiers = CreateNotifiers(config, log);
        if (notifiers.Count > 0)
            log.LogInformation("[配置·通知] 已创建通知渠道 {Count} 个", notifiers.Count);

        // 4. 通过 Descriptor 创建算法实例
        var algorithmLogger = _loggerFactory.CreateLogger(
            $"LuxVideoDet.Core.Algorithm.{config.AlgorithmType}");

        var algorithm = descriptor.Create(
            inferenceEngine, regionManager, notifiers, algorithmLogger, _serviceProvider);

        // 5. 初始化（存储、颜色等均在此完成）
        algorithm.Initialize(config);

        log.LogInformation("[配置·算法] [{AlgorithmType}] 实例创建完成并已 Initialize", config.AlgorithmType);
        return algorithm;
    }

    /// <summary>
    /// 在单条算法实例创建、模型已加载后，输出配置类别 / 解析后类别 / 算法期望类别的对照日志，便于排查跨平台 ClassName 错位。
    /// </summary>
    private static void LogClassAlignmentForRun(
        ILogger log,
        AlgorithmConfig config,
        IInferenceEngine inferenceEngine,
        ModelInfo modelInfo,
        IAlgorithmDescriptor descriptor)
    {
        var cfgList = config.Inference.Classes;
        var resolved = inferenceEngine.GetClassNames();
        var expected = descriptor.DefaultClasses;
        var numClasses = modelInfo.ClassCount;
        var take = Math.Min(resolved.Count, numClasses);
        var resolvedPrefix = take > 0 ? resolved.GetRange(0, take) : [];

        var indexMap = inferenceEngine.GetClassIndexMap();

        var cfgSummary = cfgList.Count > 0
            ? string.Join(", ", cfgList)
            : "（空，依赖模型元数据或占位名）";
        var resolvedSummary = resolvedPrefix.Count > 0
            ? string.Join(", ", resolvedPrefix.Select((n, i) => $"{i}:{n}"))
            : "（无）";
        var expectedSummary = expected.Count > 0
            ? string.Join(", ", expected)
            : "（无）";
        var mapSummary = indexMap.Count > 0
            ? string.Join(", ", indexMap.OrderBy(kv => kv.Value).Select(kv => $"{kv.Key}={kv.Value}"))
            : "（无）";

        log.LogInformation(
            "[配置·类别] [{AlgorithmType}] {DisplayName} | 模型类数={ModelClassCount} | 配置类数={ConfigCount}",
            config.AlgorithmType,
            config.DisplayName,
            numClasses,
            cfgList.Count);
        log.LogInformation(
            "[配置·类别] 配置顺序=[{Cfg}] | 后处理用=[{Res}] | 算法默认期望=[{Exp}] | 名称→索引=[{Map}]",
            cfgSummary,
            resolvedSummary,
            expectedSummary,
            mapSummary);

        if (indexMap.Count == 0)
            log.LogWarning("[配置·类别] 名称→索引映射为空，请确认模型已正确加载。");

        var missingForAlgorithm = expected
            .Where(n => !string.IsNullOrWhiteSpace(n) && !indexMap.ContainsKey(n))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
        if (missingForAlgorithm.Count > 0)
        {
            log.LogWarning(
                "[配置·类别] 算法期望的下列类别在解析结果中不存在（按名匹配将失效）: [{Missing}]",
                string.Join(", ", missingForAlgorithm));
        }

        if (cfgList.Count > 0 && resolved.Count >= numClasses)
        {
            var divergeAt = -1;
            var max = Math.Min(cfgList.Count, numClasses);
            for (var i = 0; i < max; i++)
            {
                if (!cfgList[i].Equals(resolved[i], StringComparison.OrdinalIgnoreCase))
                {
                    divergeAt = i;
                    break;
                }
            }

            if (divergeAt >= 0)
            {
                log.LogWarning(
                    "[配置·类别] 配置与解析顺序自索引 {Index} 起不一致（后处理以解析为准）。前 {N} 项 配置=[{Cfg}] 解析=[{Res}]",
                    divergeAt,
                    numClasses,
                    string.Join(", ", cfgList.Take(numClasses)),
                    string.Join(", ", resolved.Take(numClasses)));
            }
        }
        else if (cfgList.Count > 0 && resolved.Count < numClasses)
        {
            log.LogWarning(
                "[配置·类别] 解析类别名数量 ({ResolvedCount}) 小于模型类别数 ({NumClasses})，可能导致 ClassName 错位",
                resolved.Count,
                numClasses);
        }
    }

    private List<INotificationService> CreateNotifiers(AlgorithmConfig config, ILogger log)
    {
        var notifiers = new List<INotificationService>();
        if (config.Notification?.Notifiers == null) return notifiers;
        if (!config.Notification.Enabled) return notifiers;

        var typeKey = config.AlgorithmType.ToLowerInvariant();
        _descriptors.TryGetValue(typeKey, out var algorithmDescriptor);

        foreach (var nc in config.Notification.Notifiers)
        {
            if (!nc.Enabled) continue;
            try
            {
                var descriptorDefaults = algorithmDescriptor?.GetDefaultNotifierParameters(nc.Type);
                var parameters = MergeNotifierParameters(descriptorDefaults, nc.Parameters);
                notifiers.Add(
                    _notificationServiceFactory.CreateNotificationService(nc.Type, parameters));
            }
            catch (Exception ex)
            {
                log.LogError(
                    ex,
                    "[配置·通知] 通知器「{Type}」未加入列表，原因：{Reason}（该渠道在会话内不可用）",
                    nc.Type,
                    ex.Message);
            }
        }

        var enabledEntries = config.Notification.Notifiers.Where(n => n.Enabled).ToList();
        if (notifiers.Count == 0 && enabledEntries.Count > 0)
        {
            var types = string.Join(", ", enabledEntries.Select(n => n.Type));
            log.LogWarning(
                "[配置·通知] notification.enabled=true，但 {Count} 条已启用的渠道均未创建成功（类型：{Types}）。运行中 NG 时将提示未加载通知渠道。请根据上方面每条「未加入列表」日志修正参数（例如 PLC 必填 plcIp/ip）后重载会话。",
                enabledEntries.Count,
                types);
        }

        return notifiers;
    }

    /// <summary>
    /// 合并顺序：描述符默认 &lt; 配置文件（同键以配置为准）。
    /// </summary>
    private static Dictionary<string, object> MergeNotifierParameters(
        IReadOnlyDictionary<string, string>? descriptorDefaults,
        Dictionary<string, string> configParams)
    {
        var merged = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        if (descriptorDefaults != null)
        {
            foreach (var kv in descriptorDefaults)
                merged[kv.Key] = kv.Value;
        }

        foreach (var kv in configParams)
            merged[kv.Key] = kv.Value;

        return merged;
    }

    // ──────────── 算法元数据（静态查询，从 Descriptor 自动获取） ────────────

    public static string[] GetSupportedAlgorithms() =>
        _descriptors.Keys.ToArray();

    public static bool IsSupported(string algorithmType) =>
        _descriptors.ContainsKey(algorithmType.ToLower());

    public static string GetDisplayName(string algorithmType) =>
        _descriptors.TryGetValue(algorithmType.ToLower(), out var d) ? d.DisplayName : algorithmType;

    public static List<string> GetDefaultClasses(string algorithmType) =>
        _descriptors.TryGetValue(algorithmType.ToLower(), out var d) ? d.DefaultClasses : new();

    public static List<RegionDefinition> GetRequiredRegions(string algorithmType) =>
        _descriptors.TryGetValue(algorithmType.ToLower(), out var d) ? d.RequiredRegions : new();

    /// <summary>
    /// 与 <see cref="GetDefaultClasses"/> 顺序对齐的默认检测框颜色（BGR）；无描述符或描述符未配置时返回 <c>null</c>。
    /// </summary>
    public static IReadOnlyList<Scalar>? GetDefaultClassColors(string algorithmType)
    {
        if (!_descriptors.TryGetValue(algorithmType.ToLower(), out var d))
            return null;
        return d.DefaultClassColors;
    }

    /// <summary>
    /// 配置编辑 UI：返回本算法允许选择的通知 type 键（已过滤为系统已注册渠道）。
    /// 描述符未限制时返回全部已注册渠道。
    /// </summary>
    public static IReadOnlyList<string> GetAllowedNotifierTypeKeys(string algorithmType)
    {
        var registered = NotificationServiceFactory.GetSupportedTypes();
        if (!_descriptors.TryGetValue(algorithmType.ToLowerInvariant(), out var d))
            return registered.ToList();

        var supported = d.SupportedNotifierTypes;
        if (supported == null || supported.Count == 0)
            return registered.ToList();

        var regSet = new HashSet<string>(registered, StringComparer.OrdinalIgnoreCase);
        var filtered = new List<string>();
        foreach (var t in supported)
        {
            var key = t.ToLowerInvariant();
            if (regSet.Contains(key))
                filtered.Add(key);
        }

        return filtered.Count > 0 ? filtered : registered.ToList();
    }

    /// <summary>
    /// 合并通知参数时与 <see cref="IAlgorithmDescriptor.GetDefaultNotifierParameters"/> 一致，供 UI 预填。
    /// </summary>
    public static IReadOnlyDictionary<string, string>? GetDefaultNotifierParameters(
        string algorithmType,
        string notifierType)
    {
        if (!_descriptors.TryGetValue(algorithmType.ToLowerInvariant(), out var d))
            return null;
        return d.GetDefaultNotifierParameters(notifierType);
    }

    /// <summary>
    /// Desktop/Web 实时画面：某算法类型在 UI 上可展示的操作按钮元数据（无则空列表）。
    /// </summary>
    public static IReadOnlyList<AlgorithmUiActionDefinition> GetUiActionDefinitions(string algorithmType)
    {
        if (!_descriptors.TryGetValue(algorithmType.ToLowerInvariant(), out var d))
            return Array.Empty<AlgorithmUiActionDefinition>();
        return d.GetUiActionDefinitions();
    }

    /// <summary>
    /// 配置编辑 UI：某算法声明的<strong>纯算法</strong>参数表单（<c>args</c>），无则空列表。
    /// </summary>
    public static IReadOnlyList<AlgorithmArgsFormSection> GetAlgorithmParameterSections(string algorithmType)
    {
        if (string.IsNullOrWhiteSpace(algorithmType))
            return Array.Empty<AlgorithmArgsFormSection>();
        if (!_descriptors.TryGetValue(algorithmType.ToLowerInvariant(), out var d))
            return Array.Empty<AlgorithmArgsFormSection>();
        return d.AlgorithmParameterSections;
    }

    /// <summary>
    /// 配置编辑 UI：某算法声明的 <strong>AOI</strong> 相关参数表单（<c>args</c>），无则空列表。
    /// </summary>
    public static IReadOnlyList<AlgorithmArgsFormSection> GetAoiParameterSections(string algorithmType)
    {
        if (string.IsNullOrWhiteSpace(algorithmType))
            return Array.Empty<AlgorithmArgsFormSection>();
        if (!_descriptors.TryGetValue(algorithmType.ToLowerInvariant(), out var d))
            return Array.Empty<AlgorithmArgsFormSection>();
        return d.AoiParameterSections;
    }

    /// <inheritdoc cref="GetAoiParameterSections"/>
    [Obsolete("Use GetAoiParameterSections.")]
    public static IReadOnlyList<AlgorithmArgsFormSection> GetAlgorithmAoiUsages(string algorithmType) =>
        GetAoiParameterSections(algorithmType);

    // ──────────── 反射自动发现 ────────────

    private static Dictionary<string, IAlgorithmDescriptor> DiscoverDescriptors()
    {
        var result = new Dictionary<string, IAlgorithmDescriptor>(StringComparer.OrdinalIgnoreCase);

        var descriptorTypes = CollectDescriptorTypes();

        foreach (var type in descriptorTypes)
        {
            try
            {
                if (Activator.CreateInstance(type) is IAlgorithmDescriptor descriptor)
                {
                    result[descriptor.TypeKey.ToLower()] = descriptor;
                }
            }
            catch (Exception ex)
            {
                // 某个插件版本与当前 Core 不兼容时，跳过该描述符，不中断整个工厂初始化
                Console.WriteLine($"[DetectionAlgorithmFactory] 跳过不兼容描述符 {type.FullName}: {ex.GetType().Name} - {ex.Message}");
            }
        }

        return result;
    }

    private static IEnumerable<Type> CollectDescriptorTypes()
    {
        var assemblies = new List<Assembly> { Assembly.GetExecutingAssembly() };
        assemblies.AddRange(LoadPluginAssemblies());

        foreach (var asm in assemblies.Distinct())
        {
            Type[] types;
            try
            {
                types = asm.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.OfType<Type>().ToArray();
            }

            foreach (var type in types)
            {
                if (type is { IsClass: true, IsAbstract: false }
                    && typeof(IAlgorithmDescriptor).IsAssignableFrom(type)
                    && type.GetCustomAttribute<ExampleTemplateAttribute>(inherit: false) is null)
                {
                    yield return type;
                }
            }
        }
    }

    private static IEnumerable<Assembly> LoadPluginAssemblies()
    {
        var pluginDirectory = ResolvePluginDirectory();
        if (!Directory.Exists(pluginDirectory))
            yield break;

        foreach (var dll in Directory.EnumerateFiles(pluginDirectory, "*.dll", SearchOption.TopDirectoryOnly))
        {
            var fileName = Path.GetFileName(dll);
            if (!fileName.StartsWith("LuxVideoDet.Algorithm.", StringComparison.OrdinalIgnoreCase))
                continue;
            if (fileName.StartsWith("LuxVideoDet.Algorithm.Example", StringComparison.OrdinalIgnoreCase))
                continue;

            Assembly? asm = null;
            try
            {
                asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(dll));
            }
            catch
            {
                // ignore invalid/non-managed dll
            }

            if (asm != null)
                yield return asm;
        }
    }

    private static string ResolvePluginDirectory()
        => Path.Combine(AppContext.BaseDirectory, "plugins");
}
