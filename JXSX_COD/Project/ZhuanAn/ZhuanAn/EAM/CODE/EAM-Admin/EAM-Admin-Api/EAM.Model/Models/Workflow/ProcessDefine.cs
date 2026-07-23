namespace EAM.Model.Workflow
{
    /// <summary>
    /// 流程定义
    /// </summary>
    [SugarTable("WF_Process_Define")]
    public class ProcessDefine
    {
        /// <summary>
        /// 流程ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "process_ID")]
        public string ProcessId { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [SugarColumn(ColumnName = "process_Name")]
        public string ProcessName { get; set; }

        /// <summary>
        /// 流程描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 关联表单ID
        /// </summary>
        [SugarColumn(ColumnName = "Form_ID")]
        public int? FormId { get; set; }

        /// <summary>
        /// 流程模板
        /// </summary>
        [SugarColumn(ColumnName = "Process_Template")]
        public string ProcessTemplate { get; set; }

        /// <summary>
        /// 状态
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
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }
    }
}