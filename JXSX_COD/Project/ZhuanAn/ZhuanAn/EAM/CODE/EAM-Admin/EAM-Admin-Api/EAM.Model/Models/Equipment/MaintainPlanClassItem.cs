namespace EAM.Model.Equipment
{
    /// <summary>
    /// 保养计划班次项目
    /// </summary>
    [SugarTable("EQU_Maintain_Plan_Class_Item")]
    public class MaintainPlanClassItem
    {
        /// <summary>
        /// 计划班次ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "Plan_Class_Id")]
        public int PlanClassId { get; set; }

        /// <summary>
        /// 日期标记
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "date_Mark")]
        public string DateMark { get; set; }

        /// <summary>
        /// 日期标记值
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "date_Mark_Stamp")]
        public int DateMarkStamp { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        [SugarColumn(ColumnName = "start_Date")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [SugarColumn(ColumnName = "end_Date")]
        public DateTime? EndDate { get; set; }
    }
}