using LuxVideoDet.Core;
using LuxVideoDet.Core.Notification;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.Example;

/// <summary>
/// 示例 Descriptor：与 <see cref="ExampleNotificationService"/> 配对。
/// 带 <see cref="ExampleTemplateAttribute"/>，不会被 <see cref="NotificationServiceFactory"/> 注册。
/// </summary>
/// <remarks>
/// 实现 <see cref="INotificationDescriptor"/> 时接口成员 <b>全部为必须</b>（<c>TypeKey</c>、<c>DisplayName</c>、
/// <c>ParameterDefinitions</c>、<c>Create</c>）。扩展时去掉 <see cref="ExampleTemplateAttribute"/>。
/// </remarks>
[ExampleTemplate]
public sealed class ExampleNotificationDescriptor : INotificationDescriptor
{
    public string TypeKey => "example_notify";

    public string DisplayName => "（示例）自定义通知";

    public IReadOnlyList<NotificationParameterDefinition> ParameterDefinitions => Definitions;

    internal static readonly List<NotificationParameterDefinition> Definitions =
    [
        new NotificationParameterDefinition
        {
            Name = "prefix",
            DisplayName = "日志前缀",
            ParameterType = "string",
            DefaultValue = "[示例]",
            Required = false
        }
    ];

    public INotificationService Create(NotificationServiceFactoryContext context)
    {
        return new ExampleNotificationService(
            context.LoggerFactory.CreateLogger<ExampleNotificationService>());
    }
}
