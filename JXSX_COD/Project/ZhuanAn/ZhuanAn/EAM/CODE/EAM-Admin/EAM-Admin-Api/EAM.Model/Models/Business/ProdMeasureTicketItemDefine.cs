namespace EAM.Model.Business
{
    /// <summary>
    /// 产品测量报告_测试项目定义
    /// </summary>
    [SugarTable("BU_Prod_Measure_Ticket_Item_Define")]
    public class ProdMeasureTicketItemDefine
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public int OrderNo { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [SugarColumn(ColumnName = "item_Name")]
        public string ItemName { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        public decimal Standard { get; set; }

        /// <summary>
        /// 正公差
        /// </summary>
        public decimal Positive { get; set; }

        /// <summary>
        /// 负公差
        /// </summary>
        public decimal Caption { get; set; }
    }
}