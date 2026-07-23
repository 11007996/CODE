namespace EAM.Model.Consumable
{
    /// <summary>
    /// 耗品库存记录
    /// </summary>
    [SugarTable("CON_Consumable_Storage_Record")]
    public class ConsumableStorageRecord
    {
        /// <summary>
        /// 耗品ID
        /// </summary>
        [SugarColumn(ColumnName = "consumable_ID")]
        public int? ConsumableId { get; set; }

        /// <summary>
        /// 储位ID
        /// </summary>
        [SugarColumn(ColumnName = "storage_ID")]
        public int? StorageId { get; set; }

        /// <summary>
        /// 库存数量（变更前）
        /// </summary>
        [SugarColumn(ColumnName = "before_Qty")]
        public int BeforeQty { get; set; }

        /// <summary>
        /// 库存变动数量
        /// </summary>
        [SugarColumn(ColumnName = "change_Qty")]
        public int ChangeQty { get; set; }

        /// <summary>
        /// 库存数量（变更后）
        /// </summary>
        [SugarColumn(ColumnName = "after_Qty")]
        public int AfterQty { get; set; }

        /// <summary>
        /// 变动类型
        /// </summary>
        [SugarColumn(ColumnName = "storage_Change_Type")]
        public string StorageChangeType { get; set; }

        /// <summary>
        /// 相关人员(领用人或归还人)
        /// </summary>
        [SugarColumn(ColumnName = "related_User")]
        public string RelatedUser { get; set; }

        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        [SugarColumn(ColumnName = "ticket_Type")]
        public string TicketType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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
    }
}