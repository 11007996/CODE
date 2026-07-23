namespace EAM.Model.Business
{
    /// <summary>
    /// 产品测量报告_判定结果
    /// </summary>
    [SugarTable("BU_Prod_Measure_Ticket_Item_Result")]
    public class ProdMeasureTicketItemResult
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
        /// 尺寸判定结果
        /// </summary>
        [SugarColumn(ColumnName = "size_Result")]
        public string SizeResult { get; set; }

        /// <summary>
        /// 外观判定结果
        /// </summary>
        [SugarColumn(ColumnName = "facade_Result")]
        public string FacadeResult { get; set; }
    }
}