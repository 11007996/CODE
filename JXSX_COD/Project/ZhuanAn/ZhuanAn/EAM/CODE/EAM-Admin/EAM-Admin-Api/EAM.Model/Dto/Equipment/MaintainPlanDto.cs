namespace EAM.Model.Dto
{
    /// <summary>
    /// 保养计划查询对象
    /// </summary>
    public class MaintainPlanQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public int? PlanYear { get; set; }
        public int? PlanClassId { get; set; }
        public long? ExecuteDeptId { get; set; }
        public DateTime? BeginCreateTime { get; set; }
        public DateTime? EndCreateTime { get; set; }
    }

    /// <summary>
    /// 保养计划输入输出对象
    /// </summary>
    public class MaintainPlanDto
    {
        [Required(ErrorMessage = "计划Id不能为空")]
        public int PlanId { get; set; }

        [Required(ErrorMessage = "设备ID不能为空")]
        public int EquipmentId { get; set; }

        [Required(ErrorMessage = "计划班次不能为空")]
        public int PlanClassId { get; set; }

        [Required(ErrorMessage = "计划年份不能为空")]
        public int PlanYear { get; set; }

        public string PlanClass { get; set; }

        [Required(ErrorMessage = "执行部门ID不能为空")]
        public long ExecuteDeptId { get; set; }

        public string ExecuteDeptName { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string AssetNo { get; set; }

        public string AssetName { get; set; }

        public string EquipmentName { get; set; }

        public List<MaintainPlanClassItemDto> MaintainPlanClassItemNav { get; set; }
    }

    /// <summary>
    /// 获取未保养的设备查询对象
    /// </summary>
    public class ExcludeEquipmentQueryDto : PagerInfo
    {
        [Required(ErrorMessage = "计划年份不能为空")]
        public int? PlanYear { get; set; }

        public string Keyword { get; set; }
    }

    /// <summary>
    /// 保养计划输入输出对象
    /// </summary>
    public class MaintainPlanBatchAddDto
    {
        [Required(ErrorMessage = "设备不能为空")]
        public List<int> EquipmentIds { get; set; }

        [Required(ErrorMessage = "计划班次不能为空")]
        public int PlanClassId { get; set; }

        [Required(ErrorMessage = "执行部门ID不能为空")]
        public long ExecuteDeptId { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}