using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Model.Workflow
{
    /// <summary>
    /// 流程实例
    /// </summary>
    [SugarTable("WF_Process_Instance")]
    public class ProcessInstance
    {
        /// <summary>
        /// 流程编号(6位流程编号-8位日期-4位流水)
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 流程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        [SugarColumn(ColumnName = "process_ID")]
        public string ProcessId { get; set; }

        /// <summary>
        /// 流程模板名称
        /// </summary>
        [SugarColumn(ColumnName = "process_Template")]
        public string ProcessTemplate { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        [SugarColumn(ColumnName = "current_Node_ID")]
        public int? CurrentNodeId { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        [SugarColumn(ColumnName = "initiator_ID")]
        public string InitiatorId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>

        [SugarColumn(ColumnName = "Dept_Id")]
        public long? DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [SugarColumn(ColumnName = "Dept_Name")]
        public string DeptName { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 删除标记(0正常，1:删除)
        /// </summary>
        [SugarColumn(ColumnName = "delFlag")]
        public string DelFlag { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string ProcessName { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string CurrentNodeName { get; set; }

        /// <summary>
        /// 发起人姓名
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string InitiatorName { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(InstanceApproval.ProcessInstanceId), nameof(ProcessInstanceId))] //自定义关系映射
        public List<InstanceApproval> InstanceApprovalNav { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(InstanceAttachment.ProcessInstanceId), nameof(ProcessInstanceId))] //自定义关系映射
        public List<InstanceAttachment> InstanceAttachmentNav { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(InstanceFormData.ProcessInstanceId), nameof(ProcessInstanceId))] //自定义关系映射
        public List<InstanceFormData> InstanceFormDataNav { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(InstanceTask.ProcessInstanceId), nameof(ProcessInstanceId))] //自定义关系映射
        public List<InstanceTask> InstanceTaskNav { get; set; }
    }
}