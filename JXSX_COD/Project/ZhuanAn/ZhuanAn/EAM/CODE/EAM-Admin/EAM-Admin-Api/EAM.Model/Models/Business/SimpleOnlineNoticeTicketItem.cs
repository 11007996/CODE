namespace EAM.Model.Business
{
    /// <summary>
    /// 上线通知单_需求清单
    /// </summary>
    [SugarTable("BU_Simple_Online_Notice_Ticket_Item")]
    public class SimpleOnlineNoticeTicketItem
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 设备*治具名称
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "item_Name")]
        public string ItemName { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        [SugarColumn(ColumnName = "need_Qty")]
        public int NeedQty { get; set; }


        /// <summary>
        /// 是否准备就续
        /// </summary>
        [SugarColumn(ColumnName = "Is_Ready")]
        public bool IsReady { get; set; }
    }
}