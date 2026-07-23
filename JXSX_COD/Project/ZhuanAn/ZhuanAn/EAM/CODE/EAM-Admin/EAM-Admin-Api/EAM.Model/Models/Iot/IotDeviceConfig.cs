namespace EAM.Model.Iot
{
    /// <summary>
    /// 设备配置
    /// </summary>
    [SugarTable("IOT_Device_Config")]
    public class IotDeviceConfig
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "device_Id")]
        public int DeviceId { get; set; }

        /// <summary>
        /// 关联通道
        /// </summary>
        [SugarColumn(ColumnName = "channel_Id")]
        public int? ChannelId { get; set; }

        /// <summary>
        /// 从站地址
        /// </summary>
        [SugarColumn(ColumnName = "slave_Address")]
        public int? SlaveAddress { get; set; }

        /// <summary>
        /// 采集间隔(ms)
        /// </summary>
        [SugarColumn(ColumnName = "collect_Interval")]
        public int? CollectInterval { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string ChannelName { get; set; }
    }
}