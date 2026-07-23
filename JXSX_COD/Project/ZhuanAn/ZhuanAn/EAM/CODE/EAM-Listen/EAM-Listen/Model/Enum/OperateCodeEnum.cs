namespace EAM.Listen.Model
{
    /// <summary>
    /// 操作指令
    /// </summary>
    public enum OperateCodeEnum : byte
    {
        /// <summary>
        /// 不执行任何检查操作
        /// </summary>
        NONE = 0,

        /// <summary>
        /// 检查保养状态
        /// </summary>
        Maintenance = 1
    }
}