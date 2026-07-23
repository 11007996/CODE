namespace EAM.Model.Business
{
    /// <summary>
    /// 上线通知单_治具需求清单
    /// </summary>
    [SugarTable("BU_Online_Notice_Ticket_Fixture")]
    public class OnlineNoticeTicketFixture
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 治具名称
        /// </summary>
        [SugarColumn(ColumnName = "fixture_Name")]
        public string FixtureName { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        [SugarColumn(ColumnName = "need_Qty")]
        public int NeedQty { get; set; }

        ///// <summary>
        ///// 系列
        ///// </summary>
        //[SugarColumn(IsIgnore = true)]
        //public string Series { get; set; }

        ///// <summary>
        ///// 单价
        ///// </summary>
        //[SugarColumn(IsIgnore = true)]
        //public decimal Price { get; set; }
    }
}