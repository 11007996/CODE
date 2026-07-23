using LuxVideoDet.Core.Notification;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.WeChat;

/// <summary>
/// 企业微信通知 — 注册元数据与实例创建（供工厂反射发现）。
/// </summary>
public sealed class WeChatNotificationDescriptor : INotificationDescriptor
{
    public string TypeKey => "wechat";

    public string DisplayName => "企业微信";

    public IReadOnlyList<NotificationParameterDefinition> ParameterDefinitions => Definitions;

    /// <summary>供 <see cref="WeChatNotificationService.GetRequiredParameters"/> 与工厂共用同一份定义。</summary>
    public static readonly List<NotificationParameterDefinition> Definitions =
    [
        new NotificationParameterDefinition
        {
            Name = "webhook_url",
            DisplayName = "企业微信 Webhook URL",
            Description = "企业微信机器人的 Webhook 地址",
            ParameterType = "string",
            DefaultValue = "https://qyapi.weixin.qq.com/cgi-bin/webhook/send?key=YOUR_KEY",
            Required = true,
            Example = "https://qyapi.weixin.qq.com/cgi-bin/webhook/send?key=xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
        }
    ];

    public INotificationService Create(NotificationServiceFactoryContext context)
    {
        return new WeChatNotificationService(
            context.LoggerFactory.CreateLogger<WeChatNotificationService>(),
            context.HttpClientFactory.CreateClient());
    }
}
