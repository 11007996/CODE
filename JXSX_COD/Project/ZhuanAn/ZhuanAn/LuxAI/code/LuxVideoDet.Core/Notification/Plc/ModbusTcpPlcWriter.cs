using System.Net;
using System.Net.Sockets;

namespace LuxVideoDet.Core.Notification.Plc;

/// <summary>
/// Modbus TCP 写单个保持寄存器（功能码 0x06），与现场 PLC 映射一致时使用。
/// 设计为「一次连接 → 一次写入 → 断开」，避免长时间占用连接。
/// </summary>
public sealed class ModbusTcpPlcWriter
{
    private Socket? _socket;

    /// <summary>连接到 PLC 或协议网关。</summary>
    public bool Connect(string ip, int port, int connectTimeoutMs, int receiveTimeoutMs)
    {
        try
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = Math.Clamp(connectTimeoutMs, 1000, 60_000),
                ReceiveTimeout = Math.Clamp(receiveTimeoutMs, 1000, 60_000)
            };

            var endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _socket.Connect(endPoint);
            return _socket.Connected;
        }
        catch
        {
            Disconnect();
            return false;
        }
    }

    /// <summary>写入保持寄存器（16 位有符号，大端序）。<paramref name="unitId"/> 为 Modbus 站号（1～247）。</summary>
    public bool WriteSingleRegister(int address, short value, byte unitId)
    {
        if (_socket is not { Connected: true })
            return false;

        try
        {
            var command = BuildModbusWriteCommand(address, value, unitId);
            _ = _socket.Send(command);

            var response = new byte[1024];
            var received = _socket.Receive(response);
            if (received < 12 || response[6] != unitId || response[7] != 0x06)
                return false;

            var respAddress = (ushort)((response[8] << 8) | response[9]);
            var respValue = (ushort)((response[10] << 8) | response[11]);
            var expectedAddr = (ushort)(address & 0xFFFF);
            return respAddress == expectedAddr && unchecked((short)respValue) == value;
        }
        catch
        {
            return false;
        }
    }

    public void Disconnect()
    {
        try
        {
            if (_socket is { Connected: true })
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
        }
        catch
        {
            // ignore
        }
        finally
        {
            _socket = null;
        }
    }

    private static byte[] BuildModbusWriteCommand(int address, short value, byte unitId)
    {
        var command = new byte[12];
        command[0] = 0x00;
        command[1] = 0x01;
        command[2] = 0x00;
        command[3] = 0x00;
        command[4] = 0x00;
        command[5] = 0x06;
        command[6] = unitId;
        command[7] = 0x06;
        command[8] = (byte)((address >> 8) & 0xFF);
        command[9] = (byte)(address & 0xFF);
        command[10] = (byte)((value >> 8) & 0xFF);
        command[11] = (byte)(value & 0xFF);
        return command;
    }
}
