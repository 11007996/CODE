namespace EAM.Model.Dto
{
    /// <summary>
    /// 保养计划班次查询对象
    /// </summary>
    public class MaintainPlanClassQueryDto : PagerInfo
    {
        public int? PlanYear { get; set; }
        public string PlanClass { get; set; }
    }

    /// <summary>
    /// 保养计划班次输入输出对象
    /// </summary>
    public class MaintainPlanClassDto
    {
        [Required(ErrorMessage = "计划班次Id不能为空")]
        public int PlanClassId { get; set; }

        [Required(ErrorMessage = "计划年份不能为空")]
        public int PlanYear { get; set; }

        [Required(ErrorMessage = "计划班次不能为空")]
        public string PlanClass { get; set; }

        public List<MaintainPlanClassItemDto> MaintainPlanClassItemNav { get; set; }
    }
}