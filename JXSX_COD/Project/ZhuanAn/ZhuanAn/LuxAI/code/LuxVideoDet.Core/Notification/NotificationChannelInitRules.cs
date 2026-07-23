namespace LuxVideoDet.Core.Notification;

/// <summary>
/// 各 <see cref="INotificationService.Initialize"/> 共用的必填校验。
/// 校验失败应抛 <see cref="ArgumentException"/>，由 <see cref="DetectionAlgorithmFactory"/> 捕获后跳过该渠道。
/// </summary>
public static class NotificationChannelInitRules
{
    /// <summary>若 <paramref name="value"/> 为 null、空或仅空白，则抛出 <see cref="ArgumentException"/>。</summary>
    public static void ThrowIfNullOrWhiteSpace(string? value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(message);
    }
}
