using LuxVideoDet.Core.Notification;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.Webhook;

/// <summary>
/// Webhook 通知 — 注册元数据与实例创建（供工厂反射发现）。
/// </summary>
public sealed class WebhookNotificationDescriptor : INotificationDescriptor
{
    public string TypeKey => "webhook";

    public string DisplayName => "Webhook";

    public IReadOnlyList<NotificationParameterDefinition> ParameterDefinitions => Definitions;

    /// <summary>供 <see cref="WebhookNotificationService.GetRequiredParameters"/> 与工厂共用同一份定义。</summary>
    public static readonly List<NotificationParameterDefinition> Definitions =
    [
        new NotificationParameterDefinition
        {
            Name = "businessWebhook",
            DisplayName = "业务回调地址",
            Description = "接收通知的业务系统 HTTP 端点地址",
            ParameterType = "string",
            DefaultValue = "https://example.com/api/notification",
            Required = true,
            Example = "https://example.com/api/notification"
        },
        new NotificationParameterDefinition
        {
            Name = "webhookCooldown",
            DisplayName = "冷却时间（秒）",
            Description = "两次通知之间的最小间隔时间，防止频繁发送",
            ParameterType = "double",
            DefaultValue = 1.0,
            Required = false,
            Example = "1.0"
        }
    ];

    public INotificationService Create(NotificationServiceFactoryContext context)
    {
        return new WebhookNotificationService(
            context.LoggerFactory.CreateLogger<WebhookNotificationService>(),
            context.HttpClientFactory.CreateClient());
    }
}
