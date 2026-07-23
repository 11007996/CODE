namespace EAM.Model.Dto
{
    /// <summary>
    /// 流程定义查询对象
    /// </summary>
    public class ProcessDefineQueryDto : PagerInfo
    {
        public string ProcessName { get; set; }
        public string Status { get; set; }
    }

    /// <summary>
    /// 流程定义输入输出对象
    /// </summary>
    public class ProcessDefineDto
    {
        [Required(ErrorMessage = "流程编号不能为空")]
        public string ProcessId { get; set; }

        [Required(ErrorMessage = "流程名称不能为空")]
        public string ProcessName { get; set; }

        public string Description { get; set; }

        public int? FormId { get; set; }

        [Required(ErrorMessage = "流程模板不能为空")]
        public string ProcessTemplate { get; set; }

        [Required(ErrorMessage = "状态不能为空")]
        public string Status { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "状态")]
        public string StatusLabel { get; set; }
    }
}