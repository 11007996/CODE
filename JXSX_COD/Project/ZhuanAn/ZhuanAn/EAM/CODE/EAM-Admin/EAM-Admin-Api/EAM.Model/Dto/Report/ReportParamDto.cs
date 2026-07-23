namespace EAM.Model.Dto
{
    /// <summary>
    /// 报表参数查询对象
    /// </summary>
    public class ReportParamQueryDto : PagerInfo
    {
        public int? ReportId { get; set; }
    }

    /// <summary>
    /// 报表参数输入输出对象
    /// </summary>
    public class ReportParamDto
    {
        [Required(ErrorMessage = "参数ID不能为空")]
        public int ParamId { get; set; }

        [Required(ErrorMessage = "报表ID不能为空")]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "参数键不能为空")]
        public string ParamKey { get; set; }

        [Required(ErrorMessage = "参数标签不能为空")]
        public string ParamLabel { get; set; }

        [Required(ErrorMessage = "标签类型不能为空")]
        public string ElementType { get; set; }

        [Required(ErrorMessage = "输入类型不能为空")]
        public string InputType { get; set; }

        public string DefaultValue { get; set; }

        public bool Required { get; set; }

        public string OptionsSource { get; set; }

        public string HeadValue { get; set; }

        public string TailValue { get; set; }

        [Required(ErrorMessage = "排序不能为空")]
        public int SortOrder { get; set; }

        [ExcelColumn(Name = "输入类型")]
        public string InputTypeLabel { get; set; }
    }
}