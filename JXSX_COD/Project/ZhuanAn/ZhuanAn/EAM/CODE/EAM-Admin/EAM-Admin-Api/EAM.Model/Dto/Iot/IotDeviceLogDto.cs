namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备日志查询对象
    /// </summary>
    public class IotDeviceLogQueryDto : PagerInfo
    {
        public int? DeviceId { get; set; }
        public string TraceId { get; set; }
        public DateTime? BeginCreateTime { get; set; }
        public DateTime? EndCreateTime { get; set; }
    }

    /// <summary>
    /// 设备日志输入输出对象
    /// </summary>
    public class IotDeviceLogDto
    {
        [Required(ErrorMessage = "日志ID不能为空")]
        public long LogId { get; set; }

        [Required(ErrorMessage = "设备ID不能为空")]
        public int DeviceId { get; set; }

        public string TraceId { get; set; }

        [Required(ErrorMessage = "业务类型不能为空")]
        public string BusinessType { get; set; }

        public string Operation { get; set; }

        [Required(ErrorMessage = "内容不能为空")]
        public string Content { get; set; }

        [Required(ErrorMessage = "日志状态不能为空")]
        public string Status { get; set; }

        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "设备名称")]
        public string DeviceName { get; set; }

        [ExcelColumn(Name = "设备唯一键名")]
        public string DeviceKey { get; set; }
    }
}