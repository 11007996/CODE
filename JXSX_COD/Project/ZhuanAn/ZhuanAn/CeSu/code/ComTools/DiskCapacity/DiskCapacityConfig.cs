namespace ComTools.DiskCapacity
{
    /// <summary>
    /// 磁盘测速设置
    /// </summary>
    public class DiskCapacityConfig
    {
        public bool AutoFlag { get; set; }

        /// <summary>
        /// 目标盘符
        /// </summary>
        public string TargetDisk { get; set; }
    }
}