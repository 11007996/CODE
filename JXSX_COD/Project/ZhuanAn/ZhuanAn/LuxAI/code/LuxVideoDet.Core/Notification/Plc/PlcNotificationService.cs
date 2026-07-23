using Microsoft.Extensions.Logging;

namespace LuxVideoDet.Core.Notification.Plc;

/// <summary>
/// 通过 Modbus TCP 向 PLC 写单个保持寄存器，用作产线停线/告警等数字量通知（与 Webhook/Speaker 等并列）。
/// </summary>
public sealed class PlcNotificationService : INotificationService
{
    private readonly ILogger<PlcNotificationService> _logger;

    private string _plcIp = string.Empty;
    private int _modbusPort = 502;
    private int _registerAddress;
    private short _registerValue = 1;
    private double _cooldownSeconds = 1.0;
    private int _connectTimeoutMs = 5000;
    private int _receiveTimeoutMs = 5000;
    private byte _modbusUnitId = 1;
    private DateTime _lastWriteUtc = DateTime.MinValue;

    public string NotificationType => "Plc";

    public PlcNotificationService(ILogger<PlcNotificationService> logger)
    {
        _logger = logger;
    }

    public List<NotificationParameterDefinition> GetRequiredParameters() =>
        [.. PlcNotificationDescriptor.Definitions];

    public void Initialize(Dictionary<string, object> config)
    {
        _plcIp = ReadIp(config);
        NotificationChannelInitRules.ThrowIfNullOrWhiteSpace(
            _plcIp,
            "PLC 通知缺少必填参数：请在 parameters 中填写 plcIp 或 ip（PLC/网关的 IPv4 地址）。当前为空。");

        _modbusPort = ReadInt(config, "modbusPort", 502);
        _registerAddress = ReadInt(config, "registerAddress", 0);
        var rawVal = ReadInt(config, "registerValue", 1);
        if (rawVal is < short.MinValue or > short.MaxValue)
            throw new ArgumentException($"registerValue 必须在 {short.MinValue}～{short.MaxValue} 范围内。");
        _registerValue = (short)rawVal;

        _cooldownSeconds = ReadDouble(config, "cooldownSeconds", 1.0);
        _connectTimeoutMs = ReadInt(config, "connectTimeoutMs", 5000);
        _receiveTimeoutMs = ReadInt(config, "receiveTimeoutMs", 5000);
        var uid = ReadInt(config, "modbusUnitId", 1);
        _modbusUnitId = (byte)Math.Clamp(uid, 1, 247);

        _logger.LogInformation(
            "PLC 通知已初始化：IP={Ip}, Port={Port}, UnitId={Unit}, Address={Addr}, Value={Val}, Cooldown={Cd}s",
            _plcIp,
            _modbusPort,
            _modbusUnitId,
            _registerAddress,
            _registerValue,
            _cooldownSeconds);
    }

    public Task<bool> SendAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        if ((now - _lastWriteUtc).TotalSeconds < _cooldownSeconds)
        {
            _logger.LogInformation(
                "[通知·PLC] 冷却中，本轮未写入寄存器（避免重复触发），距上次成功未满 {Cooldown}s",
                _cooldownSeconds);
            return Task.FromResult(true);
        }

        _logger.LogInformation(
            "[通知·PLC] 开始写入：Level={Level}, Title={Title}, Machine={Machine}",
            message.Level,
            message.Title,
            message.MachineName);

        return Task.Run(() =>
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var writer = new ModbusTcpPlcWriter();
                try
                {
                    if (!writer.Connect(_plcIp, _modbusPort, _connectTimeoutMs, _receiveTimeoutMs))
                    {
                        _logger.LogWarning(
                            "[通知·PLC] TCP 连接失败：{Ip}:{Port}（检查网络、防火墙、PLC 是否上电）",
                            _plcIp,
                            _modbusPort);
                        return false;
                    }

                    var ok = writer.WriteSingleRegister(_registerAddress, _registerValue, _modbusUnitId);
                    if (ok)
                    {
                        _lastWriteUtc = DateTime.UtcNow;
                        _logger.LogInformation(
                            "[通知·PLC] Modbus 写入成功：{Ip}:{Port} 站={Unit} 地址={Addr} 值={Val}",
                            _plcIp,
                            _modbusPort,
                            _modbusUnitId,
                            _registerAddress,
                            _registerValue);
                    }
                    else
                    {
                        _logger.LogWarning(
                            "[通知·PLC] 未得到有效 Modbus 响应或连接异常：{Ip}:{Port} 站={Unit} 地址={Addr} 值={Val}",
                            _plcIp,
                            _modbusPort,
                            _modbusUnitId,
                            _registerAddress,
                            _registerValue);
                    }

                    return ok;
                }
                finally
                {
                    writer.Disconnect();
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PLC 通知执行异常");
                return false;
            }
        }, cancellationToken);
    }

    private static string ReadIp(Dictionary<string, object> config)
    {
        foreach (var key in new[] { "plcIp", "ip", "plc_ip" })
        {
            if (config.TryGetValue(key, out var v) && v != null)
            {
                var s = v.ToString()?.Trim();
                if (!string.IsNullOrEmpty(s))
                    return s;
            }
        }

        return string.Empty;
    }

    private static int ReadInt(Dictionary<string, object> config, string key, int defaultValue)
    {
        if (!config.TryGetValue(key, out var v) || v == null)
            return defaultValue;
        try
        {
            return Convert.ToInt32(v, System.Globalization.CultureInfo.InvariantCulture);
        }
        catch
        {
            return defaultValue;
        }
    }

    private static double ReadDouble(Dictionary<string, object> config, string key, double defaultValue)
    {
        if (!config.TryGetValue(key, out var v) || v == null)
            return defaultValue;
        try
        {
            return Convert.ToDouble(v, System.Globalization.CultureInfo.InvariantCulture);
        }
        catch
        {
            return defaultValue;
        }
    }
}
