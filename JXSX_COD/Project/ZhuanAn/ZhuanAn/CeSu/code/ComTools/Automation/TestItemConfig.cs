namespace ComTools.Automation
{
    /// <summary>
    /// DP线测试的控件配置
    /// </summary>
    public class TestItemConfig
    {
        /// <summary>
        /// 自动化ID
        /// </summary>
        public string AutomationID { get; set; }

        /// <summary>
        /// 控件元素的类型
        /// </summary>
        public string ControlType { get; set; }

        /// <summary>
        /// 过滤取值，使用@value表示要提取的值
        /// </summary>
        public string FilterFormat { get; set; }

        /// <summary>
        /// 运算符
        /// </summary>
        public OperatorEnum Operator { get; set; }

        /// <summary>
        /// 超时时长（秒）,小于等于0表示不限制测试时长
        /// </summary>
        public decimal Overtime { get; set; }

        /// <summary>
        /// 前置操作
        /// </summary>
        public ExtendOperateConfig PrefixOperate { get; set; } = new ExtendOperateConfig();

        /// <summary>
        /// 请求指令（PLC主动请求的16进制指令）
        /// </summary>
        public string RequestHexCode { get; set; }

        /// <summary>
        /// Fail响应指令（测试失败返回给PLC的指令）
        /// </summary>
        public string ResponseFailHexCode { get; set; }

        /// <summary>
        /// Pass响应指令（测试通过返回给PLC的指令）
        /// </summary>
        public string ResponsePassHexCode { get; set; }

        /// <summary>
        /// 后置操作
        /// </summary>
        public ExtendOperateConfig SuffixOperate { get; set; } = new ExtendOperateConfig();

        /// <summary>
        /// 测试项目名
        /// </summary>
        public string TestItem { get; set; }

        /// <summary>
        /// 条件值
        /// </summary>
        public string WhereValue { get; set; }

        /// <summary>
        /// 窗口名称
        /// </summary>
        public string WindowName { get; set; }
    }
}