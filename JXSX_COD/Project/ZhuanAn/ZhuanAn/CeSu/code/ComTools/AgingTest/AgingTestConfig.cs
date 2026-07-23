using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace ComTools.AgingTest
{
    public class AgingTestConfig
    {
        /// <summary>
        /// 比特率
        /// </summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; } = 8;

        /// <summary>
        /// 测试设备总数
        /// </summary>
        public int DeviceCount { get; set; } = 1;

        /// <summary>
        /// 测试设备布局列数
        /// </summary>
        public int LayoutColCount { get; set; } = 1;

        /// <summary>
        /// 奇偶校验(枚举Parity的值)
        /// </summary>
        public Parity Parity { get; set; } = Parity.Odd;

        /// <summary>
        /// Pass达标次数
        /// </summary>
        public int PassCount { get; set; }

        /// <summary>
        /// 测试设备串口名称集合
        /// </summary>
        public List<String> SerialPortNames { get; set; } = new List<String>();

        /// <summary>
        /// 停止位(枚举StopBits的值)
        /// </summary>
        public StopBits StopBits { get; set; } = StopBits.One;

        /// <summary>
        /// 测试次数
        /// </summary>
        public int TestCount { get; set; }

        /// <summary>
        /// 测试项目配置
        /// </summary>
        public List<AgingTestItemConfig> TestItems { get; set; } = new List<AgingTestItemConfig>();
    }

    //测试项目配置
    public class AgingTestItemConfig
    {
        /// <summary>
        /// 响应失败
        /// </summary>
        public string FailHexCode { get; set; }

        /// <summary>
        /// 启动测试超时(秒)
        /// </summary>
        public int OverTime { get; set; }

        /// <summary>
        /// 响应成功 与PLC通信指令的16进制指令
        /// </summary>
        public string PassHexCode { get; set; }

        /// <summary>
        /// 请求总字节大小,不足补0
        /// </summary>
        public int RequestByteSize { get; set; }

        /// <summary>
        /// 开始测试指令 与PLC通信指令的16进制指令
        /// </summary>
        public string RequestHexCode { get; set; }

        /// <summary>
        /// 响应总字节数据,不足补0
        /// </summary>
        public int ResponseByteSize { get; set; }

        /// <summary>
        /// 结束测试返回给PLC的指令 与PLC通信指令的16进制指令
        /// </summary>
        public string ResponseHexCode { get; set; }

        /// <summary>
        /// 测试项目枚举
        /// </summary>
        public string TestItem { get; set; }

        /// <summary>
        /// 响应等待
        /// </summary>
        public string WaitHexCode { get; set; }
    }
}