namespace EAM.Model.Workflow
{
    /// <summary>
    /// 流程实例任务
    /// </summary>
    [SugarTable("WF_Instance_Task")]
    public class InstanceTask
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "task_ID")]
        public int TaskId { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        [SugarColumn(ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(ColumnName = "node_ID")]
        public int? NodeId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        [SugarColumn(ColumnName = "task_Type")]
        public string TaskType { get; set; }

        /// <summary>
        /// 受理人ID
        /// </summary>
        [SugarColumn(ColumnName = "assignee_ID")]
        public string AssigneeId { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_Time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [SugarColumn(ColumnName = "finish_Time")]
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 受理人名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string AssigneeName { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string NodeName { get; set; }
    }
}