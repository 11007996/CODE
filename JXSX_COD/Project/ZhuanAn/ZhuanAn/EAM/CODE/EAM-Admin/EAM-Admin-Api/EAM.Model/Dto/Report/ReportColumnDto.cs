namespace EAM.Model.Dto
{
    /// <summary>
    /// 报表数据列查询对象
    /// </summary>
    public class ReportColumnQueryDto : PagerInfo
    {
        public int? ReportId { get; set; }
    }

    /// <summary>
    /// 报表数据列输入输出对象
    /// </summary>
    public class ReportColumnDto
    {
        [Required(ErrorMessage = "主键ID不能为空")]
        public int ColumnId { get; set; }

        [Required(ErrorMessage = "报表ID不能为空")]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "列字段名不能为空")]
        public string ColumnKey { get; set; }

        [Required(ErrorMessage = "显示名称不能为空")]
        public string ColumnLabel { get; set; }

        public int? Width { get; set; }

        public bool IsVisible { get; set; }
        public bool IsSort { get; set; }

        [Required(ErrorMessage = "排序不能为空")]
        public int SortOrder { get; set; }
    }
}