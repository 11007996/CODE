namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品设备表查询对象
    /// </summary>
    public class IotDeviceQueryDto : PagerInfo
    {
        public int? ProductId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceKey { get; set; }
        public string RegisterPacket { get; set; }
        public string Keyword {  get; set; }
    }

    /// <summary>
    /// 产品设备表输入输出对象
    /// </summary>
    public class IotDeviceDto
    {
        [Required(ErrorMessage = "设备Id不能为空")]
        public int DeviceId { get; set; }

        [Required(ErrorMessage = "产品ID不能为空")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "设备名称不能为空")]
        public string DeviceName { get; set; }

        [Required(ErrorMessage = "唯一键名不能为空")]
        public string DeviceKey { get; set; }

        public string RegisterPacket { get; set; }

        public string Status { get; set; }

        public string IpAddress { get; set; }

        public DateTime? ActivateTime { get; set; }

        public DateTime? LastOnlineTime { get; set; }

        [Required(ErrorMessage = "是否可用不能为空")]
        public bool Enabled { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "产品名称")]
        public string ProductName { get; set; }

        public int? EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public int? BoxId {  get; set; }

        public string BoxName { get; set; }

        public IotDeviceBindDto BindInfo { get; set; }
    }

}