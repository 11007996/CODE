namespace EAM.Model.Iot
{
    /// <summary>
    /// 传输通道
    /// </summary>
    [SugarTable("IOT_Common_Channel")]
    public class IotCommonChannel
    {
        /// <summary>
        /// 通道Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "channel_Id")]
        public int ChannelId { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        [SugarColumn(ColumnName = "channel_Name")]
        public string ChannelName { get; set; }

        /// <summary>
        /// 传输模式
        /// </summary>
        [SugarColumn(ColumnName = "transfer_Mode")]
        public string TransferMode { get; set; }

        /// <summary>
        /// 串口
        /// </summary>
        [SugarColumn(ColumnName = "serial_Port")]
        public string SerialPort { get; set; }

        /// <summary>
        /// 比特率
        /// </summary>
        [SugarColumn(ColumnName = "baud_Rate")]
        public int? BaudRate { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        [SugarColumn(ColumnName = "data_Bits")]
        public int? DataBits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        [SugarColumn(ColumnName = "stop_Bits")]
        public int? StopBits { get; set; }

        /// <summary>
        /// 奇偶校验
        /// </summary>
        public int? Parity { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int? Port { get; set; }

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
    }
}