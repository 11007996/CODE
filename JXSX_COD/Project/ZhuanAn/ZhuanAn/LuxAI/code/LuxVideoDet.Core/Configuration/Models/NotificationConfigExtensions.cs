using System.Linq;

namespace LuxVideoDet.Core.Configuration.Models;

/// <summary>
/// <see cref="NotificationConfig"/> 查询辅助（Desktop/Web 与算法 UI 门控共用）。
/// </summary>
public static class NotificationConfigExtensions
{
    /// <summary>
    /// 总开关打开且存在已启用的 PLC（Modbus）通知渠道（仅看配置 JSON，不表示已成功连上设备）。
    /// </summary>
    public static bool HasEnabledPlcNotifier(this NotificationConfig? notification)
    {
        if (notification is not { Enabled: true })
            return false;

        return notification.Notifiers.Any(n =>
            n.Enabled && string.Equals(n.Type, "plc", StringComparison.OrdinalIgnoreCase));
    }
}
