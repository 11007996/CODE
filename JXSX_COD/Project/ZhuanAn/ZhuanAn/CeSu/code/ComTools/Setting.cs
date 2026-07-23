using ComTools.AgingTest;
using ComTools.Automation;
using ComTools.DiskCapacity;
using ComTools.DiskSpeed;
using ComTools.DPLineTest;

namespace ComTools
{
    /// <summary>
    /// 全局的配置入口
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 测速度相关设置
        /// </summary>
        public static DiskSpeedConfig DiskSpeed = new DiskSpeedConfig();

        public static DiskCapacityConfig DiskCapacity = new DiskCapacityConfig();
        public static AutomationTestConfig AutomationTest = new AutomationTestConfig();
        public static DPLineTestConfig DPLine = new DPLineTestConfig();
        public static AgingTestConfig AgingTest = new AgingTestConfig();
    }

    /// <summary>
    /// 用于序列化保存测试的设置
    /// </summary>
    public class SettingJson
    {
        public DiskSpeedConfig DiskSpeed = new DiskSpeedConfig();
        public DiskCapacityConfig DiskCapacity = new DiskCapacityConfig();
        public AutomationTestConfig AutomationTest = new AutomationTestConfig();
        public DPLineTestConfig DPLine = new DPLineTestConfig();
        public AgingTestConfig AgingTest = new AgingTestConfig();
    }
}