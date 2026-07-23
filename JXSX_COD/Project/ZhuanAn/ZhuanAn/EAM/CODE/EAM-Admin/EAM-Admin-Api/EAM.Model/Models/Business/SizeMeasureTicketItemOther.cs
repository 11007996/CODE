namespace EAM.Model.Business
{
    /// <summary>
    /// 治具尺寸量测验收单_外观、结构、使用效果评估
    /// </summary>
    [SugarTable("BU_Size_Measure_Ticket_Item_Other")]
    public class SizeMeasureTicketItemOther
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "order_No")]
        public int OrderNo { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        [SugarColumn(ColumnName = "item_Desc")]
        public string ItemDesc { get; set; }

        /// <summary>
        /// 自动化评估结果
        /// </summary>
        [SugarColumn(ColumnName = "automation_Result")]
        public string AutomationResult { get; set; }

        /// <summary>
        /// 品管确认评估结果
        /// </summary>
        [SugarColumn(ColumnName = "qC_Result")]
        public string QcResult { get; set; }
    }
}