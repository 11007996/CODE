namespace EAM.Model.Fixture
{
    /// <summary>
    /// 产线领用中的治具
    /// </summary>
    [SugarTable("FIX_Fixture_Storage_Using")]
    public class FixtureStorageUsing
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "Fixture_Using_Id")]
        public int FixtureUsingId { get; set; }

        /// <summary>
        /// 治具ID
        /// </summary>
        [SugarColumn(ColumnName = "fixture_ID")]
        public int FixtureId { get; set; }

        /// <summary>
        /// 领用数量
        /// </summary>
        [SugarColumn(ColumnName = "Receive_Qty")]
        public int ReceiveQty { get; set; }

        /// <summary>
        /// 占用数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 原储位ID
        /// </summary>
        [SugarColumn(ColumnName = "storage_ID")]
        public int StorageId { get; set; }

        /// <summary>
        /// 领用人
        /// </summary>
        [SugarColumn(ColumnName = "related_User")]
        public string RelatedUser { get; set; }

        /// <summary>
        /// 领用产线
        /// </summary>
        [SugarColumn(ColumnName = "Line_Id")]
        public int? LineId { get; set; }

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