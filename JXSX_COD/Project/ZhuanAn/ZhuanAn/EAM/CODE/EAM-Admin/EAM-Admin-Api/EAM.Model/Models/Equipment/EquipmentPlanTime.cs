namespace EAM.Model.Equipment
{
    /// <summary>
    /// 设备计划停机时间
    /// </summary>
    [SugarTable("EQU_Plan_Time")]
    public class EquipmentPlanTime
    {
        /// <summary>
        /// 计划ID
        /// </summary>
        [SugarColumn(ColumnName = "plan_Id", IsPrimaryKey = true, IsIdentity = true)]
        public int PlanId { get; set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        [SugarColumn(ColumnName = "plan_Name")]
        public string PlanName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_Time")]
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "end_Time")]
        public string EndTime { get; set; }

        /// <summary>
        /// 最大生效时间(秒)
        /// </summary>
        [SugarColumn(ColumnName = "max_Seconds")]
        public int MaxSeconds { get; set; }
    }
}