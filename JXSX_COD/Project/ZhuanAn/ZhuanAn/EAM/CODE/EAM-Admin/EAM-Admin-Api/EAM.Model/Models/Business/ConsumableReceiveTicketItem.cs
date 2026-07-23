namespace EAM.Model.Business
{
    /// <summary>
    /// 耗品领用单_需求清单
    /// </summary>
    [SugarTable("BU_Consumable_Receive_Ticket_Item")]
    public class ConsumableReceiveTicketItem
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 耗品ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "consumable_ID")]
        public int ConsumableId { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        [SugarColumn(ColumnName = "need_Qty")]
        public int NeedQty { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string ConsumablePart { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string ConsumableName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string Spec { get; set; }

        [SugarColumn(IsIgnore = true)]
        public decimal Price { get; set; }
    }
}