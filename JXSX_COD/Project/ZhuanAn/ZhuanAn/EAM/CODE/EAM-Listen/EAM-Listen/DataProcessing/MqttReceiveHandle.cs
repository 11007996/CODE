using EAM.Listen.Common.Config;
using EAM.Listen.Communication;
using EAM.Listen.Model;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace EAM.Listen.DataProcessing
{
    public class MqttReceiveHandle
    {
        /// <summary>
        /// 处理订阅消息
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="payload"></param>
        /// <param name="retain"></param>
        /// <param name="qos"></param>
        public static void HandleMqttMsg(string topic, byte[] payload, bool retain, MqttQualityOfServiceLevel qos)
        {
            //不处理保留信息
            if (retain) return;
            //防止答复的消息被处理
            if (topic.Split('?')[0].EndsWith("/reply"))
                return;

            int deviceId = 0;
            string traceId = Guid.NewGuid().ToString();
            string logContent = string.Empty;

            try
            {
                logContent += DateTime.Now + "  Topic:" + topic + " ;数据大小(bytes):" + payload.Length + Environment.NewLine;
                if (topic.Contains("?parser=default"))
                {
                    logContent += "原始数据:" + Environment.NewLine + BitConverter.ToString(payload) + Environment.NewLine;
                }
                else
                {
                    logContent += "原始数据:" + Environment.NewLine + Encoding.UTF8.GetString(payload) + Environment.NewLine;
                }

                IotCommMsgDto param = new IotCommMsgDto();
                //1.topic解析, 通用格式:  /iot/${factoryId}/${deviceKey}/${function}/${method}
                List<string> topicItems = topic.Split('/').ToList();
                if (topic.StartsWith("/iot/"))
                {
                    param.FactoryId = topicItems[topicItems.IndexOf("iot") + 1];
                    param.DeviceKey = topicItems[topicItems.IndexOf("iot") + 2];
                    param.Function = topicItems[topicItems.IndexOf("iot") + 3];
                }
                else
                {
                    logContent += DateTime.Now + "  topic非法，需要以'/iot/'开始" + topic + Environment.NewLine;
                    goto errorHandle;
                }

                param.TtraceId = traceId;
                param.Payload = payload;
                param.Protocol = "mqtt";
                param.Topic = topic;
                var device = IotProductConfig.GetIotDeviceByKey(param.DeviceKey);
                if (device == null)
                {
                    logContent += DateTime.Now + "  未找到指定设备Key" + param.DeviceKey + Environment.NewLine;
                    goto errorHandle;
                }
                deviceId = device.DeviceId;
                param.DeviceId = deviceId;

                IotService.UpdateDeviceStatus(deviceId, null, true);
                IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据接收, true, logContent);

                RespMsgDto res = IotService.HandleMsg(param);
                if (res != null)
                {
                    string resTopic = topic.Split('?')[0] + "/reply";//以/reply/结尾的为对应topic的答复topic
                    MqttProxy.PubMessage(resTopic, res.Payload);
                    logContent = DateTime.Now + " 返回数据：" + res.Content + Environment.NewLine;
                    IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据发送, true, logContent);
                }
            }
            catch (Exception ex)
            {
                logContent += "mqtt异常：" + ex.Message + Environment.NewLine;
                goto errorHandle;
            }
            return;

        errorHandle:
            IotService.AddLog(deviceId, traceId, IotLogBusinessTypeConstant.设备上报, IotLogOperationConstant.数据接收, false, logContent);
        }
    }
}