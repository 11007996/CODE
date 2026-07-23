namespace EAM.Model.Basic
{
    /// <summary>
    /// 料号
    /// </summary>
    [SugarTable("Base_Part")]
    public class Part
    {
        /// <summary>
        /// 料号ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "part_Id")]
        public int? PartId { get; set; }

        /// <summary>
        /// 料号名称
        /// </summary>
        [SugarColumn(ColumnName = "part_No")]
        public string PartNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark")]
        public string Remark { get; set; }
    }
}