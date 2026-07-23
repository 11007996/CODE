namespace EAM.Model.Basic
{
    /// <summary>
    /// 产线信息
    /// </summary>
    [SugarTable("BASE_Line")]
    public class Line
    {
        /// <summary>
        /// 产线Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "line_Id")]
        public int LineId { get; set; }

        /// <summary>
        /// 产线名称
        /// </summary>
        [SugarColumn(ColumnName = "line_Name")]
        public string LineName { get; set; }

        /// <summary>
        /// 产线编码
        /// </summary>
        [SugarColumn(ColumnName = "line_Code")]
        public int? LineCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark")]
        public string Remark { get; set; }
    }
}