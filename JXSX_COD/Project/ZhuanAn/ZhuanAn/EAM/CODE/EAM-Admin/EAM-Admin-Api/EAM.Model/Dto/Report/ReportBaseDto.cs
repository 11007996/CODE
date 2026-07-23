namespace EAM.Model.Dto
{
    /// <summary>
    /// 报表基本信息查询对象
    /// </summary>
    public class ReportBaseQueryDto : PagerInfo
    {
        public string ReportName { get; set; }

        public int? GroupId { get; set; }
    }

    /// <summary>
    /// 报表基本信息输入输出对象
    /// </summary>
    public class ReportBaseDto
    {
        [Required(ErrorMessage = "报表ID不能为空")]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "报表名称不能为空")]
        public string ReportName { get; set; }

        [Required(ErrorMessage = "分组ID不能为空")]
        public int GroupId { get; set; }

        public string DatasourceId { get; set; }

        public string SqlTemplate { get; set; }

        [Required(ErrorMessage = "是否可用不能为空")]
        public bool Enabled { get; set; }

        public string Remark { get; set; }

        [Required(ErrorMessage = "排序不能为空")]
        public int SortOrder { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}