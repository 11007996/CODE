namespace EAM.Model.Equipment
{
    /// <summary>
    /// 保养计划班次
    /// </summary>
    [SugarTable("EQU_Maintain_Plan_Class")]
    public class MaintainPlanClass
    {
        /// <summary>
        /// 计划班次ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "Plan_Class_Id")]
        public int PlanClassId { get; set; }

        /// <summary>
        /// 计划年份
        /// </summary>
        [SugarColumn(ColumnName = "plan_Year")]
        public int PlanYear { get; set; }

        /// <summary>
        /// 计划班次
        /// </summary>
        [SugarColumn(ColumnName = "plan_Class")]
        public string PlanClass { get; set; }

        /// <summary>
        /// 计划班次项目
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(MaintainPlanClassItem.PlanClassId), nameof(PlanClassId))] //自定义关系映射
        public List<MaintainPlanClassItem> MaintainPlanClassItemNav { get; set; }
    }
}