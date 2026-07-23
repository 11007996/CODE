using EAM.Model.Statistics;

namespace EAM.Model.Dto
{
    /// <summary>
    /// 统计设备运行记录查询对象
    /// </summary>
    public class StatEquipmentRuningRecordQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public DateTime StatData { get; set; }

        public int ClassSeq { get; set; }
    }

    /// <summary>
    /// 统计设备运行记录输入输出对象
    /// </summary>
    public class StatEquipmentRuningRecordDto
    {
        [Required(ErrorMessage = "主键不能为空")]
        public int StatId { get; set; }

        [Required(ErrorMessage = "统计日期不能为空")]
        public DateTime? StatDate { get; set; }

        public int ClassSeq { get; set; }

        [Required(ErrorMessage = "设备ID不能为空")]
        public int EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public int? EquipmentNo { get; set; }

        public decimal TheoryCT { get; set; }

        [Required(ErrorMessage = "拆分时间(日期的判断)不能为空")]
        public string SplitTime { get; set; }

        [Required(ErrorMessage = "统计开始时间不能为空")]
        public DateTime? StatStartTime { get; set; }

        [Required(ErrorMessage = "统计结束时间不能为空")]
        public DateTime? StatEndTime { get; set; }

        [Required(ErrorMessage = "数据开始时间不能为空")]
        public DateTime? DataStartTime { get; set; }

        [Required(ErrorMessage = "数据结束时间不能为空")]
        public DateTime? DataEndTime { get; set; }

        public int? ProductCount { get; set; }

        public int? DefectCount { get; set; }

        public int? RunSeconds { get; set; }

        public int? PlanEffectSeconds { get; set; }

        public int? StopSeconds { get; set; }

        public int? FaultSeconds { get; set; }

        public int? FaultCount { get; set; }

        public int? LastProductCount { get; set; }

        public decimal AvailabilityRate { get; set; }

        public decimal PerformanceRate { get; set; }

        public decimal QualityRate { get; set; }

        public decimal OEE { get; set; }

        [Required(ErrorMessage = "更新时间不能为空")]
        public DateTime? UpdateTime { get; set; }

        public List<StatEquipmentRuningWarnDto> StatEquipmentRuningWarnNav { get; set; }
    }

    /// <summary>
    /// 设备运行数据分析记录
    /// </summary>
    public class EquipmentRuningRecordAnalyseDto
    {
        public int EquipmentId { get; set; }
        public int RunState { get; set; }
        public int ProductCount { get; set; }
        public int DefectCount { get; set; }
        public int WarnState { get; set; }
        public int WarnCode { get; set; }
        public DateTime CreateTime { get; set; }

        public int PreRunState { get; set; }
        public int PreWarnState { get; set; }
        public int NextRunState { get; set; }
        public int NextProductCount { get; set; }
        public int NextDefectCount { get; set; }
        public DateTime? EndTime { get; set; }
    }
}