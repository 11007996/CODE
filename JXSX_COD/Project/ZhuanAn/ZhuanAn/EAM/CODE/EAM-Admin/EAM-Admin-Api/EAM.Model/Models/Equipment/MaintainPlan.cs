namespace EAM.Model.Equipment
{
    /// <summary>
    /// 保养计划
    /// </summary>
    [SugarTable("EQU_Maintain_Plan")]
    public class MaintainPlan
    {
        /// <summary>
        /// 计划Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "plan_Id")]
        public int PlanId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Id")]
        public int EquipmentId { get; set; }

        /// <summary>
        /// 计划年份
        /// </summary>
        [SugarColumn(ColumnName = "Plan_Year")]
        public int PlanYear { get; set; }

        /// <summary>
        /// 计划班次
        /// </summary>
        [SugarColumn(ColumnName = "Plan_Class_Id")]
        public int PlanClassId { get; set; }

        /// <summary>
        /// 执行部门ID
        /// </summary>
        [SugarColumn(ColumnName = "execute_Dept_Id")]
        public long ExecuteDeptId { get; set; }

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
        /// 计划班次
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string PlanClass { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(MaintainPlanClassItem.PlanClassId), nameof(PlanClassId))] //自定义关系映射
        public List<MaintainPlanClassItem> MaintainPlanClassItemNav { get; set; }
    }
}