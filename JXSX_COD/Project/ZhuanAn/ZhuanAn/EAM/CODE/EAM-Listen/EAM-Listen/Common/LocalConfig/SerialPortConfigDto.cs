using System.IO.Ports;

namespace EAM.Listen.Common.Config
{
    /// <summary>
    /// 串口连接配置
    /// </summary>
    public class SerialPortConfigDto
    {
        /// <summary>
        /// 是否开启串口监听服务
        /// </summary>
        public bool SerialListenFlag { get; set; } = false;

        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName { get; set; } = "COM3";

        /// <summary>
        /// 比特率
        /// </summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; } = 8;

        /// <summary>
        /// 停止位(枚举StopBits的值)
        /// </summary>
        public StopBits StopBits { get; set; } = StopBits.One;

        /// <summary>
        /// 奇偶校验(枚举Parity的值)
        /// </summary>
        public Parity Parity { get; set; } = Parity.Odd;
    }
}