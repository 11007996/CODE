namespace EAM.Model.Dto
{
    /// <summary>
    /// 产线设备查询对象
    /// </summary>
    public class CallLineEquipmentQueryDto : PagerInfo
    {
        public int LineId { get; set; }
        public string EquipmentType { get; set; }
    }

    /// <summary>
    /// 产线设备输入输出对象
    /// </summary>
    public class CallLineEquipmentDto
    {
        [Required(ErrorMessage = "产线设备关联ID不能为空")]
        public int LineEquipmentId { get; set; }

        [Required(ErrorMessage = "产线ID不能为空")]
        public int LineId { get; set; }

        [Required(ErrorMessage = "设备类型不能为空")]
        public string EquipmentType { get; set; }

        public string EquipmentNo { get; set; }

        public string LineName { get; set; }
    }
}