namespace EAM.Model.Call
{
    /// <summary>
    /// 产线设备
    /// </summary>
    [SugarTable("CALL_Line_Equipment")]
    public class CallLineEquipment
    {
        /// <summary>
        /// 产线ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "Line_Equipment_Id")]
        public int LineEquipmentId { get; set; }

        /// <summary>
        /// 产线ID
        /// </summary>
        [SugarColumn(ColumnName = "line_Id")]
        public int LineId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Type")]
        public string EquipmentType { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [SugarColumn(ColumnName = "equipment_No")]
        public string EquipmentNo { get; set; }

        /// <summary>
        /// 产线名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string LineName { get; set; }
    }
}