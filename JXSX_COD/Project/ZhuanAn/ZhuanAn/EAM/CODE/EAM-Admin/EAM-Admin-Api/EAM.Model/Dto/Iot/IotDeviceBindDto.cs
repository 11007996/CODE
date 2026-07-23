namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品设备表查询对象
    /// </summary>
    public class IotDeviceBindQueryDto : PagerInfo
    {
        public int? DeviceId { get; set; }
        public int? EquipmentId { get; set; }
    }

    /// <summary>
    /// 产品设备表输入输出对象
    /// </summary>
    public class IotDeviceBindDto
    {
        [Required(ErrorMessage = "Iot设备Id不能为空")]
        public int DeviceId { get; set; }

        public int? EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public int? BoxId { get; set; }

        public string BoxName { get; set; }
    }
}