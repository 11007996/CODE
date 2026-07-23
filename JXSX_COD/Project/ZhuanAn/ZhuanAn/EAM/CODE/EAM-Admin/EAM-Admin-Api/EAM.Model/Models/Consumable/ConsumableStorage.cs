namespace EAM.Model.Consumable
{
    /// <summary>
    /// 耗品存储表
    /// </summary>
    [SugarTable("CON_Consumable_Storage")]
    public class ConsumableStorage
    {
        /// <summary>
        /// 耗品ID
        /// </summary>
        [SugarColumn(ColumnName = "consumable_ID")]
        public int ConsumableId { get; set; }

        /// <summary>
        /// 储位ID
        /// </summary>
        [SugarColumn(ColumnName = "storage_ID")]
        public int StorageId { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        [SugarColumn(ColumnName = "Qty")]
        public int Qty { get; set; }
    }
}