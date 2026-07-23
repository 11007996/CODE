namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备报警代码查询对象
    /// </summary>
    public class EquipmentWarnCodeQueryDto : PagerInfo
    {
        public int EquipmentId { get; set; }

        public int WarnCode { get; set; }
    }

    /// <summary>
    /// 设备报警代码输入输出对象
    /// </summary>
    public class EquipmentWarnCodeDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        [ExcelColumn(Name = "设备ID")]
        public int? EquipmentId { get; set; }

        public string AssetNo { get; set; }
        public string AssetName { get; set; }
        public string EquipmentName { get; set; }

        [Required(ErrorMessage = "报警代码不能为空")]
        [ExcelColumn(Name = "报警代码")]
        public int WarnCode { get; set; }

        [Required(ErrorMessage = "报警代码描述不能为空")]
        [ExcelColumn(Name = "报警代码描述")]
        public string WarnDesc { get; set; }
    }
}