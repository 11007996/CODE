using EAM.Listen.Common.Config;
using EAM.Listen.DataProcessing;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EAM.Listen.Communication
{
    public class MqttProxy
    {
        private static readonly MqttFactory mqttFactory = new MqttFactory();

        private static IMqttClient mqttClient;

        public static void Start()
        {
            if (mqttClient != null)
                return;

            mqttClient = mqttFactory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(Setting.MqttConfig.Host, Setting.MqttConfig.Port) // 替换为你的 Broker 地址
                .WithCredentials(Setting.MqttConfig.Username, Setting.MqttConfig.Password) // 设置用户名密码
                .WithCleanSession(Setting.MqttConfig.CleanSession) // 清理会话
                .WithClientId(Setting.MqttConfig.ClientId)
                .WithKeepAlivePeriod(new TimeSpan(0, 0, Setting.MqttConfig.KeepAliveSeconds))//保活间隔
                .WithProtocolVersion((MQTTnet.Formatter.MqttProtocolVersion)Setting.MqttConfig.MqttProtocolVersion)
                .WithTimeout(new TimeSpan(0, 0, 10)) //连接超时10秒
                .Build();

            mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;

            mqttClient.ConnectAsync(options).Wait();

            Console.WriteLine($"Mqtt客户端已启动：{Setting.MqttConfig.Host}:{Setting.MqttConfig.Port}");

            //订阅
            if (Setting.MqttConfig.SubTopicList != null)
            {
                foreach (string topic in Setting.MqttConfig.SubTopicList)
                {
                    var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                     .WithTopicFilter(topic, MqttQualityOfServiceLevel.AtMostOnce)
                     .Build();
                    mqttClient.SubscribeAsync(subscribeOptions).Wait();
                }
                Console.WriteLine($"Mqtt客户端已订阅：{string.Join(",", Setting.MqttConfig.SubTopicList.ToArray())}");
            }
        }

        /// <summary>
        /// 终止连接
        /// </summary>
        public static void Stop()
        {
            mqttClient.DisconnectAsync().Wait();
            mqttClient = null;
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        public static void PubMessage(string topic, string payload)
        {
            if (mqttClient == null || string.IsNullOrEmpty(topic) || string.IsNullOrEmpty(payload))
                return;

            //发布消息
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                .WithRetainFlag(false)
                .Build();

            mqttClient.PublishAsync(message).Wait();
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        public static void PubMessage(string topic, byte[] payload)
        {
            if (mqttClient == null || string.IsNullOrEmpty(topic) || payload == null || payload.Length <= 0)
                return;

            //发布消息
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                .WithRetainFlag(false)
                .Build();

            mqttClient.PublishAsync(message).Wait();
        }

        public static async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs args)
        {
            try
            {
                // 处理接收到的消息
                ExecCallBackFunc(args.ApplicationMessage);
                // Console.WriteLine($"收到消息: {  JsonConvert.SerializeObject(args.ApplicationMessage)}");
                MqttReceiveHandle.HandleMqttMsg(args.ApplicationMessage.Topic, args.ApplicationMessage.PayloadSegment.ToArray(), args.ApplicationMessage.Retain, args.ApplicationMessage.QualityOfServiceLevel);
                // 异步操作示例（如保存到数据库）
                // await SaveToDatabaseAsync(args.ApplicationMessage.PayloadSegment);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
            }
        }

        #region 回调方法

        //回调方法（给外部UI线程调用）
        private static Func<MqttApplicationMessage, bool> CallBackFunc;

        /// <summary>
        /// 设置回调方法
        /// </summary>
        /// <param name="func"></param>
        public static void SetCallBackFunc(Func<MqttApplicationMessage, bool> func)
        {
            CallBackFunc = func;
        }

        private static void ExecCallBackFunc(MqttApplicationMessage args)
        {
            if (CallBackFunc != null)
            {
                try
                {
                    CallBackFunc.Invoke(args);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion 回调方法
    }
}