namespace EAM.Model.Dto
{
    /// <summary>
    /// 流程实例查询对象
    /// </summary>
    public class ProcessInstanceQueryDto : PagerInfo
    {
        public string ProcessInstanceId { get; set; }
        public string UserName { get; set; }

        /// <summary>
        /// 状态：   Pending 待处理，Processed 已处理， Completed 已办结 ，Created 我的请求
        /// </summary>
        public string Status { get; set; }
    }

    public class ProcessInstanceInitDto
    {
        [Required(ErrorMessage = "流程编号不能为空")]
        public string ProcessId { get; set; }

        public string InitiatorId { get; set; }
    }

    /// <summary>
    /// 流程实例输入输出对象
    /// </summary>
    public class ProcessInstanceDto
    {
        [Required(ErrorMessage = "流程编号不能为空")]
        public string ProcessInstanceId { get; set; }

        [Required(ErrorMessage = "流程标题不能为空")]
        public string Title { get; set; }

        [Required(ErrorMessage = "流程ID不能为空")]
        public string ProcessId { get; set; }

        public string ProcessTemplate { get; set; }

        public string ProcessName { get; set; }

        [Required(ErrorMessage = "当前节点不能为空")]
        public int CurrentNodeId { get; set; }

        public string CurrentNodeName { get; set; }

        [Required(ErrorMessage = "发起人不能为空")]
        public string InitiatorId { get; set; }

        public string InitiatorName { get; set; }

        public long? DeptId { get; set; }

        public string DeptName { get; set; }

        [Required(ErrorMessage = "流程状态不能为空")]
        public string Status { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<InstanceApprovalDto> InstanceApprovalNav { get; set; }

        public List<InstanceAttachmentDto> InstanceAttachmentNav { get; set; }

        public List<InstanceFormDataDto> InstanceFormDataNav { get; set; }

        public List<InstanceTaskDto> InstanceTaskNav { get; set; }

        [ExcelColumn(Name = "流程状态")]
        public string StatusLabel { get; set; }
    }

    /// <summary>
    /// 用户可操作的流程节点
    /// </summary>
    public class QueryUserActionInProcessDto
    {
        public string ProcessId { get; set; }
        public string ProcessInstanceId { get; set; }
        public string UserId { get; set; }
    }

    /// <summary>
    /// 流程实例用户操作
    /// </summary>
    public class ProcessInstanceActionDto
    {
        public string ProcessInstanceId { get; set; }
        public string Title { get; set; }
        public string ProcessId { get; set; }
        public string InitiatorId { get; set; }
        public long? DeptId { get; set; }
        public string DeptName { get; set; }
        public int ActionNodeId { get; set; }
        public string ActionType { get; set; }
        public string Opinion { get; set; }

        //临时字段：【转发】等操作时，受理人工号
        public string AcceptorId { get; set; }

        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public List<FormItemData> FormData { get; set; }
    }

    public class FormItemData
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}