namespace EAM.Model.Equipment
{
    /// <summary>
    /// 设备报警代码
    /// </summary>
    [SugarTable("EQU_Equipment_Warn_Code")]
    public class EquipmentWarnCode
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "Equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 报警代码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "warn_Code")]
        public int? WarnCode { get; set; }

        /// <summary>
        /// 报警代码描述
        /// </summary>
        [SugarColumn(ColumnName = "warn_Desc")]
        public string WarnDesc { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AssetNo { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AssetName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string EquipmentName { get; set; }
    }
}