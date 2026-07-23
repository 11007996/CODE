namespace EAM.Model.Statistics
{
    /// <summary>
    /// 统计设备运行记录
    /// </summary>
    [SugarTable("STAT_Equipment_Runing_Warn")]
    public class StatEquipmentRuningWarn
    {
        /// <summary>
        /// 统计设备运行数据表的主键
        /// </summary>
        [SugarColumn(ColumnName = "stat_Id")]
        public int StatId { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        [SugarColumn(ColumnName = "stat_Date")]
        public DateTime? StatDate { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Id")]
        public int EquipmentId { get; set; }

        /// <summary>
        /// 报警代码
        /// </summary>
        [SugarColumn(ColumnName = "warn_Code")]
        public int WarnCode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "data_Start_Time")]
        public DateTime? DataStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "data_End_Time")]
        public DateTime? DataEndTime { get; set; }

        /// <summary>
        /// 故障时间
        /// </summary>
        [SugarColumn(ColumnName = "fault_Seconds")]
        public int FaultSeconds { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }
    }
}