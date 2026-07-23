namespace EAM.Model.Equipment
{
    /// <summary>
    /// 设备保养项目
    /// </summary>
    [SugarTable("EQU_Maintain_Item")]
    public class MaintainItem
    {
        public static readonly string Executor_Name = "保养人签名";

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "Item_ID")]
        public int ItemId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 日期标记  【D日,W周,M:月,Q:季，Y年】
        /// </summary>
        [SugarColumn(ColumnName = "date_Mark")]
        public string DateMark { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [SugarColumn(ColumnName = "item_Name")]
        public string ItemName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "sort_No")]
        public int? SortNo { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AssetNo { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AssetName { get; set; }
    }
}