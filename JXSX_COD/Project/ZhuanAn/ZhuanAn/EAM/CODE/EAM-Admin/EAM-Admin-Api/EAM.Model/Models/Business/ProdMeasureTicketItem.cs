namespace EAM.Model.Business
{
    /// <summary>
    /// 产品测量报告_测量值
    /// </summary>
    [SugarTable("BU_Prod_Measure_Ticket_Item")]
    public class ProdMeasureTicketItem
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 治具编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "product_No")]
        public string ProductNo { get; set; }

        /// <summary>
        /// 测试项
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