using System.Drawing;

namespace ComTools.Automation
{
    /// <summary>
    /// 自动化元素信息
    /// </summary>
    public class ElementInfo
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
        /// 控件的值(text或value)
        /// </summary>
        public string ControlValue { get; set; }

        /// <summary>
        /// 屏幕坐标区域
        /// </summary>
        public Rectangle Rect { get; set; }

        /// <summary>
        /// 窗口名称
        /// </summary>
        public string WindowName { get; set; }
    }
}