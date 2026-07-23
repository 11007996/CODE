namespace EAM.Model.Equipment
{
    /// <summary>
    /// 设备运行数据
    /// </summary>
    [SugarTable("EQU_Equipment_Runing_Record")]
    public class EquipmentRuningRecord
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Id")]
        public int EquipmentId { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        [SugarColumn(ColumnName = "run_State")]
        public int? RunState { get; set; }

        /// <summary>
        /// 产能数量
        /// </summary>
        [SugarColumn(ColumnName = "product_Count")]
        public int? ProductCount { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        [SugarColumn(ColumnName = "defect_Count")]
        public int? DefectCount { get; set; }

        /// <summary>
        /// 报警状态
        /// </summary>
        [SugarColumn(ColumnName = "warn_State")]
        public int? WarnState { get; set; }

        /// <summary>
        /// 报警代码
        /// </summary>
        [SugarColumn(ColumnName = "warn_Code")]
        public int? WarnCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }
    }
}