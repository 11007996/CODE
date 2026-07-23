namespace EAM.Model.Constant
{
    /// <summary>
    /// 采集事件动作类型
    /// </summary>
    public class CollectEventActionTypeConstant
    {
        public const string 下发设备指令 = "send_device_command";
        public const string 发送通知 = "send_notification";
        public const string 调用HTTP接口 = "http_webhook";
        public const string 写入时序数据库 = "write_timeseries";
        public const string 记录审计日志 = "log_to_audit";
        public const string 检查设备保养状态 = "check_equipment_maintain";
    }
}