using ComTools.SerialPortService;
using System.Collections.Generic;

namespace ComTools.Automation
{
    public class AutomationTestConfig
    {
        /// <summary>
        /// 测试模式（Auto:自动化监听，PLC:与PLC）
        /// </summary>
        public TestStartModelEnum Model { get; set; }

        /// <summary>
        /// 串口开关标记
        /// </summary>
        public bool SerailListenFlag { get; set; }

        public SerialPortConfig SerialProt { get; set; } = new SerialPortConfig();

        /// <summary>
        /// 开始测试指令 与PLC通信指令的16进制指令
        /// </summary>
        public string StartTestHexCode { get; set; }

        /// <summary>
        /// 请求与响应结尾固定指令 与PLC通信指令的16进制指令
        /// </summary>
        public string SuffixHexCode { get; set; }

        /// <summary>
        /// 测试项目配置
        /// </summary>
        public List<TestItemConfig> TestItems { get; set; } = new List<TestItemConfig>();
    }
}