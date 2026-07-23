namespace EAM.Model.Workflow
{
    /// <summary>
    /// 流程实例表单数据
    /// </summary>
    [SugarTable("WF_Instance_Form_Data")]
    public class InstanceFormData
    {
        /// <summary>
        /// 流程编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 流程节点(更新时所在节点)
        /// </summary>
        [SugarColumn(ColumnName = "node_ID")]
        public int? NodeId { get; set; }

        /// <summary>
        /// 表单项名称
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "field_Name")]
        public string FieldName { get; set; }

        /// <summary>
        /// 项目值
        /// </summary>
        [SugarColumn(ColumnName = "field_Value")]
        public string FieldValue { get; set; }

        /// <summary>
        /// 更新人员
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }
    }
}