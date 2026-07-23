namespace EAM.Model.Dto
{
    /// <summary>
    /// 表单模板查询对象
    /// </summary>
    public class FormTemplateQueryDto : PagerInfo
    {
        public string FormName { get; set; }
    }

    /// <summary>
    /// 表单模板输入输出对象
    /// </summary>
    public class FormTemplateDto
    {
        [Required(ErrorMessage = "表单ID不能为空")]
        public int FormId { get; set; }

        [Required(ErrorMessage = "表单名称不能为空")]
        public string FormName { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        public List<FormFieldDto> FormFieldNav { get; set; }

        [ExcelColumn(Name = "字段类型")]
        public string FieldTypeLabel { get; set; }
    }
}