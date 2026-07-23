using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.Example;

/// <summary>
/// 自定义通知示例：实现 <see cref="INotificationService"/>。
/// </summary>
/// <remarks>
/// <para><b>实现 INotificationService 时：</b>接口中 <b>全部为必须实现</b>，无 virtual 可选层。</para>
/// <list type="bullet">
/// <item><description><see cref="NotificationType"/>：类型展示名（可与配置 <c>type</c> 对应）。</description></item>
/// <item><description><see cref="GetRequiredParameters"/>：参数元数据（表单/文档）；无参数可返回空列表。</description></item>
/// <item><description><see cref="Initialize"/>：从配置字典读取参数。</description></item>
/// <item><description><see cref="SendAsync"/>：真正发送告警；失败返回 <c>false</c>。</description></item>
/// </list>
/// <para>另需配套 <see cref="ExampleNotificationDescriptor"/>（<see cref="INotificationDescriptor"/>）；示例带 <see cref="ExampleTemplateAttribute"/> 不注册。</para>
/// </remarks>
public sealed class ExampleNotificationService : INotificationService
{
    private readonly ILogger<ExampleNotificationService> _logger;
    private string _prefix = "[示例]";

    // 必须：与 Descriptor.TypeKey 对应（展示用）
    public string NotificationType => "Example";

    public ExampleNotificationService(ILogger<ExampleNotificationService> logger)
    {
        _logger = logger;
    }

    // 必须：声明可配置参数（无则返回空列表）
    public List<NotificationParameterDefinition> GetRequiredParameters() =>
        [.. ExampleNotificationDescriptor.Definitions];

    // 必须：读取配置中的参数
    public void Initialize(Dictionary<string, object> config)
    {
        if (config.TryGetValue("prefix", out var p) && p != null)
            _prefix = p.ToString() ?? _prefix;
    }

    // 必须：执行一次通知发送
    public Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("{Prefix} {Title} | {Content}", _prefix, message.Title, message.Content);
        return Task.FromResult(true);
    }
}
