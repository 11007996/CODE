namespace EAM.Model.Dto
{
    /// <summary>
    /// 流程审批记录查询对象
    /// </summary>
    public class InstanceApprovalQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 流程审批记录输入输出对象
    /// </summary>
    public class InstanceApprovalDto
    {
        [Required(ErrorMessage = "ApprovalId不能为空")]
        public int ApprovalId { get; set; }

        [Required(ErrorMessage = "流程实例不能为空")]
        public string ProcessInstanceId { get; set; }

        [Required(ErrorMessage = "流程节点不能为空")]
        public int NodeId { get; set; }

        public string NodeName { get; set; }

        [Required(ErrorMessage = "审批人不能为空")]
        public string ApproverId { get; set; }

        public string ApproverName { get; set; }

        [Required(ErrorMessage = "操作时间不能为空")]
        public DateTime? ActionTime { get; set; }

        [Required(ErrorMessage = "操作类型不能为空")]
        public string ActionType { get; set; }

        public string Opinion { get; set; }

        public string Receiver { get; set; }

        public string DeptName { get; set; }

        [ExcelColumn(Name = "操作类型")]
        public string ActionTypeLabel { get; set; }
    }
}