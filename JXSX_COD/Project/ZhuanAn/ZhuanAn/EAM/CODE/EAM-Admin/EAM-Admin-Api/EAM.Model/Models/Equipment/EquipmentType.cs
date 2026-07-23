namespace EAM.Model.Equipment
{
    /// <summary>
    /// 机台类型
    /// </summary>
    [SugarTable("EQU_Equipment_Type")]
    public class EquipmentType
    {
        /// <summary>
        /// 设备类型名称
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "equipment_Type_Name")]
        public string EquipmentTypeName { get; set; }
    }
}