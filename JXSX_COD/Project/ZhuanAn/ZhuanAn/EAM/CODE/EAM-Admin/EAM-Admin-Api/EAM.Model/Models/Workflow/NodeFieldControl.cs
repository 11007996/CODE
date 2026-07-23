namespace EAM.Model.Workflow
{
    /// <summary>
    /// 节点字段控件配置
    /// </summary>
    [SugarTable("WF_Node_Field_Control")]
    public class NodeFieldControl
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "node_ID")]
        public int NodeId { get; set; }

        /// <summary>
        /// 表单ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "form_ID")]
        public int FormId { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "field_Name")]
        public string FieldName { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool? Hidden { get; set; }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool? Editable { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool? Required { get; set; }
    }
}