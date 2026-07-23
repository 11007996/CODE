namespace EAM.Model.Dto
{
    /// <summary>
    /// 保养计划班次项目输入输出对象
    /// </summary>
    public class MaintainPlanClassItemDto
    {
        [Required(ErrorMessage = "计划班次不能为空")]
        public int PlanClassId { get; set; }

        [Required(ErrorMessage = "日期标记不能为空")]
        public string DateMark { get; set; }

        [Required(ErrorMessage = "日期标记值不能为空")]
        public int DateMarkStamp { get; set; }

        [Required(ErrorMessage = "开始日期不能为空")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "结束日期不能为空")]
        public DateTime? EndDate { get; set; }
    }
}