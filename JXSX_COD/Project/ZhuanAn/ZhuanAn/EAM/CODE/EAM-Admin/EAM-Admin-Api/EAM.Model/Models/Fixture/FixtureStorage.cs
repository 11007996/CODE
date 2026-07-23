namespace EAM.Model.Fixture
{
    /// <summary>
    /// 治具存储
    /// </summary>
    [SugarTable("FIX_Fixture_Storage")]
    public class FixtureStorage
    {
        /// <summary>
        /// 治具ID
        /// </summary>
        [SugarColumn(ColumnName = "fixture_ID")]
        public int? FixtureId { get; set; }

        /// <summary>
        /// 储位ID
        /// </summary>
        [SugarColumn(ColumnName = "storage_ID")]
        public int? StorageId { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        [SugarColumn(ColumnName = "Qty")]
        public int Qty { get; set; }
    }
}