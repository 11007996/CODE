using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.Communication;
using EAM.Listen.Model;
using System;
using System.Text;

namespace EAM.Listen.DataProcessing
{
    public class TcpIotReceiveHandle
    {
        /// <summary>
        /// 处理TCP报文
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static byte[] HandleTcpMsg(TcpStateEventArgs arg)
        {
            string traceId = Guid.NewGuid().ToString();
            string logContent = string.Empty;
            int deviceId = arg.deviceId.Value;
            byte[] payload = arg.buffer;
            string ip = arg.ip.ToString();
            try
            {
                IotCommMsgDto param = new IotCommMsgDto();
                param.FactoryId = Setting.DbConfig.ConfigId;
                param.DeviceId = deviceId;
                param.TtraceId = traceId;
                param.Payload = payload;
                param.Protocol = "tcp";
                //获取设备配置
                IotDevice device = IotProductConfig.GetIotDeviceById(deviceId);

                if (device == null)
                {
                    logContent += $"检查设备：未找到有效的设备Id【{deviceId}】" + Environment.NewLine;
                    goto errorHandle;
                }
                param.DeviceKey = device.DeviceKey;

                IotService.UpdateDeviceStatus(deviceId, arg.ip, true);

                string content = string.Empty;
                //判断是否是心跳包(注册包)
                if (device.RegisterPacket == Encoding.UTF8.GetString(payload))
                {
                    logContent += DateTime.Now + " 当前数据为注册包数据:" + device.RegisterPacket;
                    IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据接收, true, logContent);
                    return null;
                }
                logContent += DateTime.Now + " 字节大小：" + payload.Length;
                IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据接收, true, logContent);

                RespMsgDto res = IotService.HandleMsg(param);
                if (res != null)
                {
                    logContent = DateTime.Now + " 返回数据：" + res.Content + Environment.NewLine;
                    IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据发送, true, logContent);
                    return res.Payload;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(TcpReceiveHandle), ex.Message);
                logContent += "tcp异常：" + ex.Message + Environment.NewLine;
                goto errorHandle;
            }
            return null;

        errorHandle:
            IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据接收, false, logContent);
            return null;
        }
    }
}