using ComTools.Util;
using System;
using System.Collections.Generic;

namespace ComTools.AgingTest
{
    /// <summary>
    /// 测试项目的结果
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// 单次测试的不同测试项目结果,key:测试项目名称，value:测试结果（PASS,FAIL,失败说明）
        /// </summary>
        public Dictionary<string, string> ItemResult = new Dictionary<string, string>();

        /// <summary>
        /// 当前测试项目名
        /// </summary>
        public String CurrTestItemName { get; set; }

        /// <summary>
        /// 失败次数
        /// </summary>
        public int FailCount { get; set; }

        /// <summary>
        /// 附加消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 成功次数
        /// </summary>
        public int PassCount { get; set; }

        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 串口状态（true:可用，false:不可用）
        /// </summary>
        public bool? PortState { get; set; }

        /// <summary>
        /// 测试进度
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// 测试状态
        /// </summary>
        public TestStateEnum State { get; set; }
    }
}