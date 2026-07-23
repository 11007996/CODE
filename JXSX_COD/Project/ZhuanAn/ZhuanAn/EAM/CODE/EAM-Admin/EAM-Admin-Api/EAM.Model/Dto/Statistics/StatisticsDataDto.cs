namespace EAM.Model.Dto
{
    /// <summary>
    /// 统计数据查询对象
    /// </summary>
    public class StatisticsDataQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 统计数据输入输出对象
    /// </summary>
    public class StatisticsDataDto
    {
        [Required(ErrorMessage = "主键不能为空")]
        public int Id { get; set; }

        [Required(ErrorMessage = "统计日期不能为空")]
        public DateTime? StatDate { get; set; }

        [Required(ErrorMessage = "统计指标的名称不能为空")]
        public string MetricName { get; set; }

        public string MetricKey { get; set; }

        [Required(ErrorMessage = "统计指标的值不能为空")]
        public decimal MetricValue { get; set; }

        public string Meta { get; set; }

        [Required(ErrorMessage = "更新时间不能为空")]
        public DateTime? UpdateTime { get; set; }
    }

    public class StatisticsDataNewestQueryDto
    {
        [Required(ErrorMessage = "统计指标的名称不能为空")]
        public string Names { get; set; }
        public string Key { get; set; }

        public int? Days { get; set; }
    }
}