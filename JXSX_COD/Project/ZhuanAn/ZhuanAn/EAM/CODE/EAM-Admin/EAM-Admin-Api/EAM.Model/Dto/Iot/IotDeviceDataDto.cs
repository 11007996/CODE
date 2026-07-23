namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备采集数据查询对象
    /// </summary>
    public class IotDeviceDataQueryDto : PagerInfo
    {
        public int? DeviceId { get; set; }
        public string Identifier { get; set; }
        public DateTime? BeginCollectTime { get; set; }
        public DateTime? EndCollectTime { get; set; }
    }

    /// <summary>
    /// 设备采集数据输入输出对象
    /// </summary>
    public class IotDeviceDataDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        public int DeviceId { get; set; }

        [Required(ErrorMessage = "属性标识不能为空")]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "上报时间不能为空")]
        public DateTime? CollectTime { get; set; }

        public decimal Value { get; set; }

        [Required(ErrorMessage = "原始值不能为空")]
        public string RawValue { get; set; }

        [Required(ErrorMessage = "数据质量不能为空")]
        public byte Quality { get; set; }

        public string Unit { get; set; }

        [ExcelColumn(Name = "设备名称")]
        public string DeviceName { get; set; }

        [ExcelColumn(Name = "设备Key")]
        public string DeviceKey { get; set; }
    }
}