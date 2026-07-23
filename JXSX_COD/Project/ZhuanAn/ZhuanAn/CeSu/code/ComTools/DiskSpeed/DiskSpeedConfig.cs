namespace ComTools.DiskSpeed
{
    /// <summary>
    /// 磁盘测速设置
    /// </summary>
    public class DiskSpeedConfig
    {
        public bool AutoFlag { get; set; }

        /// <summary>
        /// 传输块大小(单位byte)
        /// </summary>

        public int BlockSize { get; set; }

        /// <summary>
        /// 测试文件大小（单位byte）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        ///读取速度(MB)
        /// </summary>
        public int ReadSpeedRange { get; set; }

        /// <summary>
        /// 运行次数（1次或循环）
        /// </summary>
        public int RunCount { get; set; }

        /// <summary>
        /// 目标盘符
        /// </summary>
        public string TargetDisk { get; set; }

        /// <summary>
        /// 写入速度(MB)
        /// </summary>
        public int WriteSpeedRange { get; set; }
    }
}