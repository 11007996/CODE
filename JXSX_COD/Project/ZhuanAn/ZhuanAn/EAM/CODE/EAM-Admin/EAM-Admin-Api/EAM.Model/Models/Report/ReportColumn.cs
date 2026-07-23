namespace EAM.Model.Report
{
    /// <summary>
    /// 报表数据列
    /// </summary>
    [SugarTable("rep_report_column")]
    [Tenant("0")]
    public class ReportColumn
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "column_Id")]
        public int ColumnId { get; set; }

        /// <summary>
        /// 报表ID
        /// </summary>
        [SugarColumn(ColumnName = "report_Id")]
        public int ReportId { get; set; }

        /// <summary>
        /// 列字段名
        /// </summary>
        [SugarColumn(ColumnName = "column_Key")]
        public string ColumnKey { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [SugarColumn(ColumnName = "column_Label")]
        public string ColumnLabel { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// 默认是否显示
        /// </summary>
        [SugarColumn(ColumnName = "is_Visible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// 是否可以排序
        /// </summary>
        [SugarColumn(ColumnName = "is_Sort")]
        public bool IsSort { get; set; }

        /// <summary>
        /// 列名显示顺序
        /// </summary>
        [SugarColumn(ColumnName = "sort_Order")]
        public int SortOrder { get; set; }
    }
}