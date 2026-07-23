namespace EAM.Model.Dto
{
    /// <summary>
    /// 表单字段配置查询对象
    /// </summary>
    public class FormFieldQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 表单字段配置输入输出对象
    /// </summary>
    public class FormFieldDto
    {
        //[Required(ErrorMessage = "所属表单ID不能为空")]
        public int? FormId { get; set; }

        [Required(ErrorMessage = "字段名不能为空")]
        public string FieldName { get; set; }

        [Required(ErrorMessage = "字段描述不能为空")]
        public string FieldDesc { get; set; }

        [Required(ErrorMessage = "字段类型不能为空")]
        public string FieldType { get; set; }

        public string DefaultValue { get; set; }

        public int? Size { get; set; }

        [ExcelColumn(Name = "字段类型")]
        public string FieldTypeLabel { get; set; }
    }
}