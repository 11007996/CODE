namespace EAM.Model.Report
{
    /// <summary>
    /// 报表分组
    /// </summary>
    [SugarTable("rep_report_group")]
    [Tenant("0")]
    public class ReportGroup
    {
        /// <summary>
        /// 分组Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "group_Id")]
        public int GroupId { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [SugarColumn(ColumnName = "group_Name")]
        public string GroupName { get; set; }
    }
}