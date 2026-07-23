namespace EAM.Model.Statistics
{
    /// <summary>
    /// 统计数据
    /// </summary>
    [SugarTable("STAT_Statistics_Data")]
    public class StatisticsData
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        [SugarColumn(ColumnName = "stat_Date")]
        public DateTime? StatDate { get; set; }

        /// <summary>
        /// 统计指标的名称
        /// </summary>
        [SugarColumn(ColumnName = "metric_name")]
        public string MetricName { get; set; }

        /// <summary>
        /// 统计指标的键名
        /// </summary>
        [SugarColumn(ColumnName = "metric_key")]
        public string MetricKey { get; set; }

        /// <summary>
        /// 统计指标的值
        /// </summary>
        [SugarColumn(ColumnName = "metric_value")]
        public decimal MetricValue { get; set; }

        /// <summary>
        /// 额外的元数据或分类信息
        /// </summary>
        public string Meta { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }
    }
}