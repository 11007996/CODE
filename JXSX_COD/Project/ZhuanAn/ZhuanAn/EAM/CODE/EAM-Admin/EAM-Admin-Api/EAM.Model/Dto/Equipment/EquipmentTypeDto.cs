namespace EAM.Model.Dto
{
    /// <summary>
    /// 机台类型查询对象
    /// </summary>
    public class EquipmentTypeQueryDto : PagerInfo
    {
        public string EquipmentTypeName { get; set; }
    }

    /// <summary>
    /// 机台类型输入输出对象
    /// </summary>
    public class EquipmentTypeDto
    {
        [Required(ErrorMessage = "设备类型名称不能为空")]
        public string EquipmentTypeName { get; set; }
    }
}