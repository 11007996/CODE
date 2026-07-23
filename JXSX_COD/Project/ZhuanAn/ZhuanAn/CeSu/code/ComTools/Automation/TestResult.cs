using ComTools.Util;

namespace ComTools.Automation
{
    /// <summary>
    /// 测试项目的结果
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// 结果说明,Pass时传递得到的控件值，fail时传递失败原因
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 最新值
        /// </summary>
        public string newValue { get; set; }

        /// <summary>
        /// 初始值
        /// </summary>
        public string oldValue { get; set; }

        /// <summary>
        /// 测试进度
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// 测试状态
        /// </summary>
        public TestStateEnum State { get; set; }

        /// <summary>
        /// 测试项目
        /// </summary>
        public string TestItem { get; set; }
    }
}