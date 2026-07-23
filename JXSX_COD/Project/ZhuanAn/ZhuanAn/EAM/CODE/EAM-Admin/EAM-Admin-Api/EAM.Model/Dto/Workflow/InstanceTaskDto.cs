namespace EAM.Model.Dto
{
    /// <summary>
    /// 流程实例任务查询对象
    /// </summary>
    public class InstanceTaskQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 流程实例任务输入输出对象
    /// </summary>
    public class InstanceTaskDto
    {
        [Required(ErrorMessage = "TaskId不能为空")]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "ProcessInstanceId不能为空")]
        public string ProcessInstanceId { get; set; }

        [Required(ErrorMessage = "NodeId不能为空")]
        public int NodeId { get; set; }

        public string NodeName { get; set; }

        [Required(ErrorMessage = "TaskType不能为空")]
        public string TaskType { get; set; }

        [Required(ErrorMessage = "AssigneeId不能为空")]
        public string AssigneeId { get; set; }

        public string AssigneeName { get; set; }

        [Required(ErrorMessage = "Status不能为空")]
        public string Status { get; set; }

        [Required(ErrorMessage = "CreateTime不能为空")]
        public DateTime? CreateTime { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? FinishTime { get; set; }

        [ExcelColumn(Name = "Status")]
        public string StatusLabel { get; set; }
    }
}