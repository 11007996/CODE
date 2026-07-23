using EAM.Model.Call;

namespace EAM.Model.Call
{
    /// <summary>
    /// 故障配置
    /// </summary>
    [SugarTable("CALL_Config_Fault")]
    public class CallConfigFault
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "fault_Config_Id")]
        public int FaultConfigId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Type")]
        public string EquipmentType { get; set; }

        /// <summary>
        /// 最大处理时间
        /// </summary>
        [SugarColumn(ColumnName = "max_Handle_Times")]
        public int? MaxHandleTimes { get; set; }

        /// <summary>
        /// 最大支援时间
        /// </summary>
        [SugarColumn(ColumnName = "max_Help_Times")]
        public int? MaxHelpTimes { get; set; }

        /// <summary>
        /// 自动支援
        /// </summary>
        [SugarColumn(ColumnName = "auto_Help_Flag")]
        public string AutoHelpFlag { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(CallConfigFaultSolution.FaultConfigId), nameof(FaultConfigId))] //自定义关系映射
        public List<CallConfigFaultSolution> CallConfigFaultSolutionNav { get; set; }
    }
}