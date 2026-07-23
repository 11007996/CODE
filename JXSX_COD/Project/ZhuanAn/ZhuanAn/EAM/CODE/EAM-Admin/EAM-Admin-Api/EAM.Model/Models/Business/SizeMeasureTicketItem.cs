namespace EAM.Model.Business
{
    /// <summary>
    /// 治具尺寸量测验收单_治具测量值
    /// </summary>
    [SugarTable("BU_Size_Measure_Ticket_Item")]
    public class SizeMeasureTicketItem
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 治具编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "fixture_No")]
        public string FixtureNo { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "order_No")]
        public int OrderNo { get; set; }

        /// <summary>
        /// 实测值
        /// </summary>
        [SugarColumn(ColumnName = "actual_Value")]
        public decimal? ActualValue { get; set; }
    }
}