namespace EAM.Model.Equipment
{
    /// <summary>
    /// 设备保管
    /// </summary>
    [SugarTable("EQU_Equipment_Storage_Using")]
    public class EquipmentStorageUsing
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "Equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 产线
        /// </summary>
        [SugarColumn(ColumnName = "line_Id")]
        public int? LineId { get; set; }

        /// <summary>
        /// 领用人
        /// </summary>
        [SugarColumn(ColumnName = "receiver_ID")]
        public string ReceiverId { get; set; }

        /// <summary>
        /// 变动类型
        /// </summary>
        [SugarColumn(ColumnName = "storage_Change_Type")]
        public string StorageChangeType { get; set; }

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
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AssetNo { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AssetName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string LineName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string ReceiverName { get; set; }
    }
}