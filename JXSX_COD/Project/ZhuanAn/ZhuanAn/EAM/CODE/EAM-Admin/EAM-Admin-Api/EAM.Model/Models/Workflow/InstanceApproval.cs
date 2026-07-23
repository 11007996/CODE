namespace EAM.Model.Workflow
{
    /// <summary>
    /// 流程审批记录
    /// </summary>
    [SugarTable("WF_Instance_Approval")]
    public class InstanceApproval
    {
        /// <summary>
        /// ApprovalId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "approval_ID")]
        public int ApprovalId { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        [SugarColumn(ColumnName = "Task_ID")]
        public int TaskId { get; set; }

        /// <summary>
        /// 流程实例
        /// </summary>
        [SugarColumn(ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 流程节点
        /// </summary>
        [SugarColumn(ColumnName = "node_ID")]
        public int? NodeId { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        [SugarColumn(ColumnName = "approver_ID")]
        public string ApproverId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [SugarColumn(ColumnName = "action_Time")]
        public DateTime? ActionTime { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [SugarColumn(ColumnName = "action_Type")]
        public string ActionType { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 审批人部门
        /// </summary>
        [SugarColumn(ColumnName = "dept_Name")]
        public string DeptName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string ApproverName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string NodeName { get; set; }
    }
}