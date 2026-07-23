namespace EAM.Listen.Common.Config
{
    /// <summary>
    /// Tcp连接配置
    /// </summary>
    public class TCPConfigDto
    {
        /// <summary>
        /// 是否开启TCP监听服务
        /// </summary>
        public bool TCPListenFlag { get; set; } = false;

        /// <summary>
        /// 开启服务的IP
        /// </summary>
        public string ListenIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// 服务端口号
        /// </summary>
        public int Port { get; set; } = 10409;

        /// <summary>
        /// 接收超时(秒)
        /// </summary>
        public int ReceiveTimeout { get; set; } = 5;

        /// <summary>
        /// 监听限制
        /// </summary>
        public int ListenLimit { get; set; } = 100;
    }
}