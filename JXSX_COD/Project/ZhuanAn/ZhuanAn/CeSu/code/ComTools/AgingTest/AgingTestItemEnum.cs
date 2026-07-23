using System.ComponentModel;

namespace ComTools.AgingTest
{
    /// <summary>
    /// 老化测试项目枚举
    /// </summary>
    public enum AgingTestItemEnum
    {
        [Description("启动刷屏")]
        Start_Screen_Refresh,

        [Description("获取刷屏结果")]
        Screen_Refresh_Result,
    }
}