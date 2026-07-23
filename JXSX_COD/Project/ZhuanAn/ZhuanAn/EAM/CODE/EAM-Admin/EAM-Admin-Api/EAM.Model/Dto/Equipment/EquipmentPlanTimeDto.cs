namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备计划停机时间查询对象
    /// </summary>
    public class EquipmentPlanTimeQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 设备计划停机时间输入输出对象
    /// </summary>
    public class EquipmentPlanTimeDto
    {
        [Required(ErrorMessage = "计划ID不能为空")]
        public int PlanId { get; set; }

        [Required(ErrorMessage = "计划名称不能为空")]
        public string PlanName { get; set; }

        [Required(ErrorMessage = "开始时间不能为空")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "结束时间不能为空")]
        public string EndTime { get; set; }

        [Required(ErrorMessage = "最大生效时间(秒)不能为空")]
        public int MaxSeconds { get; set; }
    }
}