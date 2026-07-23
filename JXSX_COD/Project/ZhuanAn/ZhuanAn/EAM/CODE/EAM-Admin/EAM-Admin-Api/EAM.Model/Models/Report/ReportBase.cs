
namespace EAM.Model.Report
{
    /// <summary>
    /// 报表基本信息
    /// </summary>
    [SugarTable("rep_report_base")]
    [Tenant("0")]
    public class ReportBase
    {
        /// <summary>
        /// 报表ID 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "report_Id")]
        public int ReportId { get; set; }

        /// <summary>
        /// 报表名称 
        /// </summary>
        [SugarColumn(ColumnName = "report_Name")]
        public string ReportName { get; set; }

        /// <summary>
        /// 分组ID 
        /// </summary>
        [SugarColumn(ColumnName = "group_Id")]
        public int GroupId { get; set; }

        /// <summary>
        /// 数据源ID 
        /// </summary>
        [SugarColumn(ColumnName = "dataSource_Id")]
        public string DatasourceId { get; set; }

        /// <summary>
        /// SQL模板 
        /// </summary>
        [SugarColumn(ColumnName = "sql_Template")]
        public string SqlTemplate { get; set; }

        /// <summary>
        /// 是否可用 
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 备注 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 排序 
        /// </summary>
        [SugarColumn(ColumnName = "sort_Order")]
        public int SortOrder { get; set; }

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
        /// 最后更新时间 
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

    }
}