namespace EAM.Model.Dto
{
    /// <summary>
    /// 故障配置查询对象
    /// </summary>
    public class CallConfigFaultQueryDto : PagerInfo
    {
        public string EquipmentType { get; set; }
    }

    /// <summary>
    /// 故障配置输入输出对象
    /// </summary>
    public class CallConfigFaultDto
    {
        [Required(ErrorMessage = "配置ID不能为空")]
        public int FaultConfigId { get; set; }

        [Required(ErrorMessage = "设备类型不能为空")]
        public string EquipmentType { get; set; }

        public int? MaxHandleTimes { get; set; }

        public int? MaxHelpTimes { get; set; }

        public string AutoHelpFlag { get; set; }

        public List<CallConfigFaultSolutionDto> CallConfigFaultSolutionNav { get; set; }

        [ExcelColumn(Name = "机台类型")]
        public string EquipmentTypeLabel { get; set; }
    }
}