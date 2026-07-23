namespace EAM.Model.Fixture
{
    /// <summary>
    /// 治具料号关联表
    /// </summary>
    [SugarTable("FIX_Fixture_Part")]
    public class FixturePart
    {
        /// <summary>
        /// 料号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "part_ID")]
        public int? PartId { get; set; }

        /// <summary>
        /// 治具ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "fixture_ID")]
        public int? FixtureId { get; set; }

        /// <summary>
        /// 默认数量
        /// </summary>
        [SugarColumn(ColumnName = "default_Qty")]
        public int? DefaultQty { get; set; }
    }
}