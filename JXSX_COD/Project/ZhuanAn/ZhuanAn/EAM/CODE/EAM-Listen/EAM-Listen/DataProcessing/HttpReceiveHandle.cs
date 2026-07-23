using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.Communication;
using EAM.Listen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAM.Listen.DataProcessing
{
    public class HttpReceiveHandle
    {
        /// <summary>
        /// 处理HTTP请求
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static string HandleHttpMsg(HttpStateEventArgs arg)
        {
            int deviceId = 0;
            string traceId = Guid.NewGuid().ToString();
            string logContent = string.Empty;
            string url = arg.url;
            string postData = arg.postData;
            try
            {
                IotCommMsgDto param = new IotCommMsgDto();
                logContent += DateTime.Now + "  URL:" + url + " ;" + postData.Length + Environment.NewLine;
                if (url.Contains("?parser=default"))
                {
                    param.Payload = SoftBasic.HexStringToBytes(postData);
                    logContent += "原始数据:" + Environment.NewLine + postData + Environment.NewLine;
                }
                else
                {
                    param.Payload = Encoding.UTF8.GetBytes(postData);
                    logContent += "原始数据:" + Environment.NewLine + postData + Environment.NewLine;
                }

                //1.url解析, 通用格式:  /iot/${factoryId}/${deviceKey}/${function}/${method}
                List<string> topicItems = url.Split('/').ToList();
                if (url.StartsWith("/iot/"))
                {
                    param.FactoryId = topicItems[topicItems.IndexOf("iot") + 1];
                    param.DeviceKey = topicItems[topicItems.IndexOf("iot") + 2];
                    param.Function = topicItems[topicItems.IndexOf("iot") + 3];
                }
                else
                {
                    logContent += DateTime.Now + "  url非法，需要以'/iot/'开始" + url + Environment.NewLine;
                    goto errorHandle;
                }

                param.TtraceId = traceId;
                param.Protocol = "http";
                var device = IotProductConfig.GetIotDeviceByKey(param.DeviceKey);
                if (device == null)
                {
                    logContent += DateTime.Now + "  未找到指定设备Key" + param.DeviceKey + Environment.NewLine;
                    goto errorHandle;
                }
                deviceId =  device.DeviceId ;
                param.DeviceId = deviceId;

                IotService.UpdateDeviceStatus(deviceId, null, true);
                IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据接收, true, logContent);

                RespMsgDto res = IotService.HandleMsg(param);
                if (res != null)
                {
                    logContent = DateTime.Now + " 返回数据：" + res.Content + Environment.NewLine;
                    IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据发送, true, logContent);
                    return res.Content;
                }
            }
            catch (Exception ex)
            {
                logContent += "http异常：" + ex.Message + Environment.NewLine;
                goto errorHandle;
            }
            return null;

        errorHandle:
            IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据接收, false, logContent);
            return null;
        }
    }
}