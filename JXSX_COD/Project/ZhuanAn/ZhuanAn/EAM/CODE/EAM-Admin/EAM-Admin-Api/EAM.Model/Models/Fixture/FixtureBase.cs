namespace EAM.Model.Fixture
{
    /// <summary>
    /// 治具信息
    /// </summary>
    [SugarTable("FIX_Fixture_Base")]
    public class FixtureBase
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "fixture_ID")]
        public int? FixtureId { get; set; }

        /// <summary>
        /// 治具名称
        /// </summary>
        [SugarColumn(ColumnName = "fixture_Name")]
        public string FixtureName { get; set; }

        /// <summary>
        /// 系列
        /// </summary>
        public string Series { get; set; }

        /// <summary>
        /// 图纸编号
        /// </summary>
        [SugarColumn(ColumnName = "drawing_No")]
        public string DrawingNo { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 安全库存
        /// </summary>
        [SugarColumn(ColumnName = "safety_Qty")]
        public int? SafetyQty { get; set; }

        /// <summary>
        /// 状态,0正常，1停用
        /// </summary>
        public string Status { get; set; }

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

        /// <summary>
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 最后更新
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        [SugarColumn(ColumnName = "del_Flag")]
        public int DelFlag { get; set; }
    }
}