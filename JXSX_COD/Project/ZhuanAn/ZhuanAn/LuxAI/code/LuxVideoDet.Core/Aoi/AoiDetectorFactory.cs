using System.Reflection;
using LuxVideoDet.Core;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Aoi;

/// <summary>
/// AOI 检测器工厂 — 反射发现 <see cref="IAoiDetectorDescriptor"/>；亦支持 <see cref="Register"/>。
/// 标记 <see cref="ExampleTemplateAttribute"/> 的描述符不参与注册（见 <c>Aoi/Example</c>）。
/// </summary>
public class AoiDetectorFactory
{
    private static readonly Dictionary<string, IAoiDetectorDescriptor> DiscoveredDescriptors;

    static AoiDetectorFactory()
    {
        DiscoveredDescriptors = DiscoverDescriptors();
    }

    private readonly ILoggerFactory _loggerFactory;
    private readonly Dictionary<string, IAoiDetectorDescriptor> _descriptors;

    public AoiDetectorFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _descriptors = new Dictionary<string, IAoiDetectorDescriptor>(
            DiscoveredDescriptors,
            StringComparer.OrdinalIgnoreCase);
    }

    public IAoiDetector CreateDetector(string detectorName)
    {
        if (string.IsNullOrWhiteSpace(detectorName))
            throw new ArgumentException("检测器名称不能为空", nameof(detectorName));

        var normalizedName = detectorName.ToLowerInvariant();

        if (!_descriptors.TryGetValue(normalizedName, out var descriptor))
        {
            var availableTypes = string.Join(", ", _descriptors.Keys.OrderBy(k => k, StringComparer.Ordinal));
            throw new ArgumentException($"未知的 AOI 检测器: {detectorName}。可用类型: {availableTypes}");
        }

        var logger = _loggerFactory.CreateLogger($"LuxVideoDet.Core.Aoi.{descriptor.TypeKey}");
        return descriptor.Create(logger);
    }

    public void Register(string detectorName, Func<ILogger, IAoiDetector> factory)
    {
        if (string.IsNullOrWhiteSpace(detectorName))
            throw new ArgumentException("检测器名称不能为空", nameof(detectorName));

        ArgumentNullException.ThrowIfNull(factory);

        var key = detectorName.ToLowerInvariant();
        _descriptors[key] = new DelegateAoiDetectorDescriptor(detectorName.Trim(), factory);
    }

    public string[] GetRegisteredDetectors() =>
        _descriptors.Keys.OrderBy(k => k, StringComparer.Ordinal).ToArray();

    public static string GetDisplayName(string detectorKey)
    {
        if (string.IsNullOrWhiteSpace(detectorKey))
            return detectorKey;

        var k = detectorKey.ToLowerInvariant();
        return DiscoveredDescriptors.TryGetValue(k, out var d) ? d.DisplayName : detectorKey;
    }

    private static Dictionary<string, IAoiDetectorDescriptor> DiscoverDescriptors()
    {
        var result = new Dictionary<string, IAoiDetectorDescriptor>(StringComparer.OrdinalIgnoreCase);

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                     .Where(t => t is { IsClass: true, IsAbstract: false }
                                 && typeof(IAoiDetectorDescriptor).IsAssignableFrom(t)
                                 && t.IsPublic
                                 && t.GetConstructor(Type.EmptyTypes) is not null
                                 && t.GetCustomAttribute<ExampleTemplateAttribute>(inherit: false) is null))
        {
            if (Activator.CreateInstance(type) is not IAoiDetectorDescriptor descriptor)
                continue;

            foreach (var key in EnumerateRegistrationKeys(descriptor))
                result[key.ToLowerInvariant()] = descriptor;
        }

        return result;
    }

    private static IEnumerable<string> EnumerateRegistrationKeys(IAoiDetectorDescriptor descriptor)
    {
        if (!string.IsNullOrWhiteSpace(descriptor.TypeKey))
            yield return descriptor.TypeKey.Trim();

        foreach (var a in descriptor.Aliases)
        {
            if (!string.IsNullOrWhiteSpace(a))
                yield return a.Trim();
        }
    }

    private sealed class DelegateAoiDetectorDescriptor : IAoiDetectorDescriptor
    {
        private readonly Func<ILogger, IAoiDetector> _factory;

        public DelegateAoiDetectorDescriptor(string typeKey, Func<ILogger, IAoiDetector> factory)
        {
            TypeKey = typeKey;
            _factory = factory;
        }

        public string TypeKey { get; }
        public IReadOnlyList<string> Aliases => Array.Empty<string>();
        public string DisplayName => TypeKey;
        public IAoiDetector Create(ILogger logger) => _factory(logger);
    }
}
