namespace EAM.Model.Business
{
    /// <summary>
    /// 治具尺寸量测验收单_结果判定
    /// </summary>
    [SugarTable("BU_Size_Measure_Ticket_Item_Result")]
    public class SizeMeasureTicketItemResult
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
        /// 判定结果
        /// </summary>
        [SugarColumn(ColumnName = "size_Result")]
        public string SizeResult { get; set; }

        /// <summary>
        /// 是否入库
        /// </summary>
        [SugarColumn(ColumnName = "in_Storage")]
        public string InStorage { get; set; }
    }
}