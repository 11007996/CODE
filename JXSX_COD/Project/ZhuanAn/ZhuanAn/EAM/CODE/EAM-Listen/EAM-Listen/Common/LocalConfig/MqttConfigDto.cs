using System.Collections.Generic;

namespace EAM.Listen.Common.Config
{
    /// <summary>
    /// MQTT连接配置
    /// </summary>
    public class MqttConfigDto
    {
        /// <summary>
        /// 订阅开关
        /// </summary>
        public bool MqttSubFlag { get; set; } = false;

        /// <summary>
        /// MQTT Broder 主机
        /// </summary>
        public string Host { get; set; } = "127.0.0.1";

        /// <summary>
        /// MQTT Broder 主机端口
        /// </summary>
        public int Port { get; set; } = 1883;

        /// <summary>
        /// MQTT协议版本
        /// </summary>
        public int MqttProtocolVersion { get; set; } = 4;

        /// <summary>
        /// 登入用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 登入密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 是否清空会话
        /// </summary>
        public bool CleanSession { get; set; }

        /// <summary>
        /// 保活时间
        /// </summary>
        public int KeepAliveSeconds { get; set; } = 60;

        /// <summary>
        /// 订阅的主题列表
        /// </summary>
        public IList<string> SubTopicList { get; set; } = new List<string>();
    }
}