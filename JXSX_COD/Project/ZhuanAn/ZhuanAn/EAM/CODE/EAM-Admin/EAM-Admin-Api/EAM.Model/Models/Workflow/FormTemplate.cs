using EAM.Model.Workflow;

namespace EAM.Model.Workflow
{
    /// <summary>
    /// 表单模板
    /// </summary>
    [SugarTable("WF_Form_Template")]
    public class FormTemplate
    {
        /// <summary>
        /// 表单ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "form_ID")]
        public int FormId { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        [SugarColumn(ColumnName = "form_Name")]
        public string FormName { get; set; }

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

        [Navigate(NavigateType.OneToMany, nameof(FormField.FormId), nameof(FormId))] //自定义关系映射
        public List<FormField> FormFieldNav { get; set; }
    }
}