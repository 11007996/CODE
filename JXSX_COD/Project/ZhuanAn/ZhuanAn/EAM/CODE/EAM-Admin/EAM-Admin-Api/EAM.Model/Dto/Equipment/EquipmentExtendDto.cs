namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备扩展信息查询对象
    /// </summary>
    public class EquipmentExtendQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public int? EquipmentCode { get; set; }
        public string EquipmentName { get; set; }
    }

    /// <summary>
    /// 设备扩展信息输入输出对象
    /// </summary>
    public class EquipmentExtendDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        public int? EquipmentId { get; set; }

        public string AssetNo { get; set; }

        public string EquipmentName { get; set; }

        [Required(ErrorMessage = "设备代码不能为空")]
        public int? EquipmentCode { get; set; }

        public int? EquipmentNo { get; set; }

        public decimal TheoryCT { get; set; }

        public decimal Power { get; set; }

        public string IsLink { get; set; }

        public int? LineId { get; set; }

        public string IP { get; set; }

        public string LineName { get; set; }

        [ExcelColumn(Name = "是否连接")]
        public string IsLinkLabel { get; set; }
    }
}