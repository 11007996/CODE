namespace EAM.Model.Workflow
{
    /// <summary>
    /// 表单字段配置
    /// </summary>
    [SugarTable("WF_Form_Field")]
    public class FormField
    {
        /// <summary>
        /// 所属表单ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "form_ID")]
        public int? FormId { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "field_Name")]
        public string FieldName { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        [SugarColumn(ColumnName = "field_Desc")]
        public string FieldDesc { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        [SugarColumn(ColumnName = "field_Type")]
        public string FieldType { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [SugarColumn(ColumnName = "default_Value")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 字符长度
        /// </summary>
        public int? Size { get; set; }
    }
}