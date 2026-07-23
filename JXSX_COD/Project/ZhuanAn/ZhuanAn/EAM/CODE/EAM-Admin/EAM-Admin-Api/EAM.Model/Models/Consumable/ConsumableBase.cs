namespace EAM.Model.Consumable
{
    /// <summary>
    /// 耗品表
    /// </summary>
    [SugarTable("CON_Consumable_Base")]
    public class ConsumableBase
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "consumable_ID")]
        public int? ConsumableId { get; set; }

        /// <summary>
        /// 请购料号
        /// </summary>
        [SugarColumn(ColumnName = "consumable_Part")]
        public string ConsumablePart { get; set; }

        /// <summary>
        /// 耗品名称
        /// </summary>
        [SugarColumn(ColumnName = "consumable_Name")]
        public string ConsumableName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

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
        /// 类别
        /// </summary>
        public string Category { get; set; }

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