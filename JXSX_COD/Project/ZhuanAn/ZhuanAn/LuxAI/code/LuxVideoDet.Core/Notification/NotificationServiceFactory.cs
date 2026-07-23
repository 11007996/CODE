using System.Reflection;
using LuxVideoDet.Core;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification;

/// <summary>
/// 通知服务工厂 — 通过反射自动发现所有 <see cref="INotificationDescriptor"/> 实现，
/// 新增通知渠道只需添加 Descriptor 与对应 Service，无需修改本类。
/// <para>
/// 各 <see cref="INotificationService.Initialize"/> 若因必填参数缺失抛出 <see cref="ArgumentException"/>，
/// 由调用方（如 <see cref="LuxVideoDet.Core.Algorithm.DetectionAlgorithmFactory"/>）捕获后跳过该渠道。
/// </para>
/// </summary>
public class NotificationServiceFactory
{
    private static readonly Dictionary<string, INotificationDescriptor> Descriptors;

    static NotificationServiceFactory()
    {
        Descriptors = DiscoverDescriptors();
    }

    private readonly NotificationServiceFactoryContext _context;

    public NotificationServiceFactory(
        ILoggerFactory loggerFactory,
        IHttpClientFactory httpClientFactory)
    {
        _context = new NotificationServiceFactoryContext
        {
            LoggerFactory = loggerFactory,
            HttpClientFactory = httpClientFactory
        };
    }

    /// <summary>
    /// 创建通知服务实例
    /// </summary>
    public INotificationService CreateNotificationService(string type, Dictionary<string, object> config)
    {
        if (!Descriptors.TryGetValue(type.ToLower(), out var descriptor))
            throw new ArgumentException($"不支持的通知服务类型: {type}");

        var service = descriptor.Create(_context);
        service.Initialize(config);
        return service;
    }

    /// <summary>
    /// 获取所有支持的通知服务类型键
    /// </summary>
    public static string[] GetSupportedTypes() =>
        Descriptors.Keys.OrderBy(k => k, StringComparer.Ordinal).ToArray();

    /// <summary>
    /// 获取通知服务类型的显示名称
    /// </summary>
    public static string GetDisplayName(string type) =>
        Descriptors.TryGetValue(type.ToLower(), out var d) ? d.DisplayName : type;

    /// <summary>
    /// 检查通知服务类型是否支持
    /// </summary>
    public static bool IsSupported(string type) =>
        Descriptors.ContainsKey(type.ToLower());

    /// <summary>
    /// 获取通知服务需要的参数定义（不需要实例化服务）
    /// </summary>
    public static List<NotificationParameterDefinition> GetRequiredParameters(string type)
    {
        if (!Descriptors.TryGetValue(type.ToLower(), out var d))
            return new List<NotificationParameterDefinition>();

        return d.ParameterDefinitions.ToList();
    }

    private static Dictionary<string, INotificationDescriptor> DiscoverDescriptors()
    {
        var result = new Dictionary<string, INotificationDescriptor>(StringComparer.OrdinalIgnoreCase);

        var descriptorTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                        && typeof(INotificationDescriptor).IsAssignableFrom(t)
                        && t.GetCustomAttribute<ExampleTemplateAttribute>(inherit: false) is null);

        foreach (var type in descriptorTypes)
        {
            if (Activator.CreateInstance(type) is not INotificationDescriptor descriptor)
                continue;

            var key = descriptor.TypeKey.ToLowerInvariant();
            result[key] = descriptor;
        }

        return result;
    }
}
