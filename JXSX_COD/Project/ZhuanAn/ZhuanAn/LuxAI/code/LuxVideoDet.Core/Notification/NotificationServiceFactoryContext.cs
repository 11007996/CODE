using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification;

/// <summary>
/// 创建通知服务时由工厂注入的依赖（与具体通知实现所需一致）。
/// </summary>
public sealed class NotificationServiceFactoryContext
{
    public required ILoggerFactory LoggerFactory { get; init; }

    public required IHttpClientFactory HttpClientFactory { get; init; }
}
