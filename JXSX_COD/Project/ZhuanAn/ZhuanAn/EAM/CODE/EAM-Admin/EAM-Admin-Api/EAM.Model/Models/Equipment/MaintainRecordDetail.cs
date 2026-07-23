namespace EAM.Model.Equipment
{
    /// <summary>
    /// 设备保养记录详情
    /// </summary>
    [SugarTable("EQU_Maintain_Record_Detail")]
    public class MaintainRecordDetail
    {
        /// <summary>
        /// 保养记录ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "record_Id")]
        public int? RecordId { get; set; }

        /// <summary>
        /// 保养项目ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "item_Id")]
        public int? ItemId { get; set; }

        /// <summary>
        /// 项目值
        /// </summary>
        [SugarColumn(ColumnName = "item_Value")]
        public string ItemValue { get; set; }

        /// <summary>
        /// 保养项目
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string ItemName { get; set; }
    }
}