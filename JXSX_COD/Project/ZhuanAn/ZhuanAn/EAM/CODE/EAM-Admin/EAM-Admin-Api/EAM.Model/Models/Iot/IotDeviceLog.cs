namespace EAM.Model.Iot
{
    /// <summary>
    /// 设备日志
    /// </summary>
    [SugarTable("IOT_Device_Log")]
    public class IotDeviceLog
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "log_Id")]
        public long LogId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "device_Id")]
        public int DeviceId { get; set; }

        /// <summary>
        /// 追踪ID
        /// </summary>
        [SugarColumn(ColumnName = "trace_Id")]
        public string TraceId { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        [SugarColumn(ColumnName = "business_Type")]
        public string BusinessType { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 日志状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }
    }
}