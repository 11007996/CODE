namespace EAM.Model.Dto
{
    /// <summary>
    /// 报表执行查询对象
    /// </summary>
    public class ReportExecuteQueryDto : PagerInfo
    {
        public int? ReportId { get; set; }

        public string FactoryId { get; set; }

        /// <summary>
        /// json对象序列化参数
        /// </summary>
        public string JsonParams { get; set; }
    }

    /// <summary>
    /// 查询报表的参数数据源
    /// </summary>
    public class ReportParamOptionsQueryDto : PagerInfo
    {
        public int? ReportId { get; set; }

        public string FactoryId { get; set; }

        public string ParamKey { get; set; }

        public string Keyword { get; set; }
    }

    /// <summary>
    /// 报表信息输出对象
    /// </summary>
    public class ReportInfoDto
    {
        [Required(ErrorMessage = "报表ID不能为空")]
        public int ReportId { get; set; }

        public List<ReportParamDto> Params { get; set; }

        public List<ReportColumnDto> Columns { get; set; }
    }
}