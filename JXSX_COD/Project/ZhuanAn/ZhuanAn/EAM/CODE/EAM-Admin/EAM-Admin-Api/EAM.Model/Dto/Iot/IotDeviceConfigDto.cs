namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备配置查询对象
    /// </summary>
    public class IotDeviceConfigQueryDto : PagerInfo
    {
        public int? DeviceId { get; set; }
    }

    /// <summary>
    /// 设备配置输入输出对象
    /// </summary>
    public class IotDeviceConfigDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        public int DeviceId { get; set; }

        public int? ChannelId { get; set; }
        public string ChannelName { get; set; }

        public int? SlaveAddress { get; set; }

        public int? CollectInterval { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "设备ID")]
        public string DeviceIdLabel { get; set; }
    }
}