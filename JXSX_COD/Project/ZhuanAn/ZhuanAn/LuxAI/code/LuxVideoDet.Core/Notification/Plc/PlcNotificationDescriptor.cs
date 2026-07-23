using System.Globalization;
using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.Plc;

/// <summary>
/// PLC（Modbus TCP 写保持寄存器）通知 — 注册元数据与实例创建（供工厂反射发现）。
/// 说明见同目录 <c>README.md</c>。
/// </summary>
public sealed class PlcNotificationDescriptor : INotificationDescriptor
{
    public string TypeKey => "plc";

    public string DisplayName => "PLC（Modbus TCP）";

    public IReadOnlyList<NotificationParameterDefinition> ParameterDefinitions => Definitions;

    public static readonly List<NotificationParameterDefinition> Definitions =
    [
        new NotificationParameterDefinition
        {
            Name = "plcIp",
            DisplayName = "PLC IP",
            Description =
                "必填。PLC 或 Modbus 网关的 IPv4 地址。配置键也可使用 ip、plc_ip。默认留空，避免误连现场设备；新渠道在 Desktop 中需自行填写。",
            ParameterType = "string",
            DefaultValue = "",
            Required = true,
            Example = "192.168.250.100"
        },
        new NotificationParameterDefinition
        {
            Name = "modbusPort",
            DisplayName = "Modbus TCP 端口",
            Description = "工业常用 502；串口服务器映射时以设备手册为准。",
            ParameterType = "int",
            DefaultValue = 502,
            Required = false,
            Example = "502"
        },
        new NotificationParameterDefinition
        {
            Name = "modbusUnitId",
            DisplayName = "Modbus 站号",
            Description = "TCP 帧中的单元标识符（1～247），与 PLC 工程站号一致；多数单机为 1。",
            ParameterType = "int",
            DefaultValue = 1,
            Required = false,
            Example = "1"
        },
        new NotificationParameterDefinition
        {
            Name = "registerAddress",
            DisplayName = "寄存器地址（字偏移）",
            Description =
                "写单个保持寄存器的地址偏移，需与 PLC 映射表一致。常见业务：0 对应映射到 D 区首字。",
            ParameterType = "int",
            DefaultValue = 0,
            Required = false,
            Example = "0"
        },
        new NotificationParameterDefinition
        {
            Name = "registerValue",
            DisplayName = "写入值（16 位有符号）",
            Description =
                "告警触发时写入的值。常见约定：1 表示请求停线/NG，0 表示允许运行（本通知仅在发送告警时写入，一般为 1）。",
            ParameterType = "int",
            DefaultValue = 1,
            Required = false,
            Example = "1"
        },
        new NotificationParameterDefinition
        {
            Name = "cooldownSeconds",
            DisplayName = "冷却时间（秒）",
            Description = "两次写入之间的最小间隔，避免连续触发打满 PLC。",
            ParameterType = "double",
            DefaultValue = 1.0,
            Required = false,
            Example = "1.0"
        },
        new NotificationParameterDefinition
        {
            Name = "connectTimeoutMs",
            DisplayName = "连接/发送超时（毫秒）",
            Description = "套接字发送侧超时；一般无需修改，网络极差时可略增大。",
            ParameterType = "int",
            DefaultValue = 5000,
            Required = false,
            Example = "5000"
        },
        new NotificationParameterDefinition
        {
            Name = "receiveTimeoutMs",
            DisplayName = "接收响应超时（毫秒）",
            Description = "等待 PLC 返回 Modbus 响应的最长时间。",
            ParameterType = "int",
            DefaultValue = 5000,
            Required = false,
            Example = "5000"
        }
    ];

    /// <summary>
    /// 与 <see cref="Definitions"/> 中 <see cref="NotificationParameterDefinition.DefaultValue"/> 对应的字符串表，
    /// 供算法描述符在 <see cref="IAlgorithmDescriptor.GetDefaultNotifierParameters"/> 中按需返回（与 Desktop 预填、运行时合并一致）。
    /// </summary>
    public static IReadOnlyDictionary<string, string> GetDefaultParameterStringsForAlgorithms()
    {
        var d = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var def in Definitions)
            d[def.Name] = FormatDefaultForConfig(def.DefaultValue);
        return d;
    }

    private static string FormatDefaultForConfig(object? o)
    {
        if (o == null)
            return string.Empty;
        if (o is bool b)
            return b ? "true" : "false";
        if (o is IFormattable f)
            return f.ToString(null, CultureInfo.InvariantCulture) ?? string.Empty;
        return o.ToString() ?? string.Empty;
    }

    public INotificationService Create(NotificationServiceFactoryContext context)
    {
        return new PlcNotificationService(
            context.LoggerFactory.CreateLogger<PlcNotificationService>());
    }
}
