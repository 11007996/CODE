namespace EAM.Model.Dto
{
    /// <summary>
    /// 传输通道查询对象
    /// </summary>
    public class IotCommonChannelQueryDto : PagerInfo
    {
        public string ChannelName { get; set; }
    }

    /// <summary>
    /// 传输通道输入输出对象
    /// </summary>
    public class IotCommonChannelDto
    {
        [Required(ErrorMessage = "通道Id不能为空")]
        public int ChannelId { get; set; }

        [Required(ErrorMessage = "通道名称不能为空")]
        public string ChannelName { get; set; }

        [Required(ErrorMessage = "传输模式不能为空")]
        public string TransferMode { get; set; }

        public string SerialPort { get; set; }

        public int? BaudRate { get; set; }

        public int? DataBits { get; set; }

        public int? StopBits { get; set; }

        public int? Parity { get; set; }

        public string Ip { get; set; }

        public int? Port { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "比特率")]
        public string BaudRateLabel { get; set; }
    }
}