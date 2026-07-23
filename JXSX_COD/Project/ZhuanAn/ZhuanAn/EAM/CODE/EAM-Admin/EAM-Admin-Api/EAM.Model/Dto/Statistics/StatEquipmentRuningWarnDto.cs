namespace EAM.Model.Dto
{
    /// <summary>
    /// 统计设备运行记录查询对象
    /// </summary>
    public class StatEquipmentRuningWarnQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 统计设备运行记录输入输出对象
    /// </summary>
    public class StatEquipmentRuningWarnDto
    {
        [Required(ErrorMessage = "统计设备运行数据表的主键不能为空")]
        public int StatId { get; set; }

        [Required(ErrorMessage = "统计日期不能为空")]
        public DateTime? StatDate { get; set; }

        [Required(ErrorMessage = "设备ID不能为空")]
        public int EquipmentId { get; set; }

        [Required(ErrorMessage = "报警代码不能为空")]
        public int WarnCode { get; set; }

        public string WarnDesc {  get; set; }

        [Required(ErrorMessage = "开始时间不能为空")]
        public DateTime? DataStartTime { get; set; }

        [Required(ErrorMessage = "结束时间不能为空")]
        public DateTime? DataEndTime { get; set; }

        [Required(ErrorMessage = "故障时间不能为空")]
        public int FaultSeconds { get; set; }

        [Required(ErrorMessage = "更新时间不能为空")]
        public DateTime? UpdateTime { get; set; }
    }
}