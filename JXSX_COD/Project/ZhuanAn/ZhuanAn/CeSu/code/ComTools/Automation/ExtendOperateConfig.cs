namespace ComTools.Automation
{
    /// <summary>
    /// 扩展操作配置
    /// </summary>
    public class ExtendOperateConfig
    {
        /// <summary>
        /// 应用路径
        /// </summary>
        public string AppPath { get; set; }

        /// <summary>
        /// 自动化ID
        /// </summary>
        public string AutomationId { get; set; }

        /// <summary>
        /// 控件元素的类型
        /// </summary>
        public string ControlType { get; set; }

        /// <summary>
        /// 扩展操作
        /// </summary>
        public OperateTypeEnum OperateType { get; set; }

        /// <summary>
        /// 窗口名称
        /// </summary>
        public string WindowName { get; set; }
    }
}