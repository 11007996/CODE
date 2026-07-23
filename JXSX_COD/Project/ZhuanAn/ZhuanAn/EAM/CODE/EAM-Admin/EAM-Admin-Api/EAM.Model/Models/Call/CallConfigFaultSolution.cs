namespace EAM.Model.Call
{
    /// <summary>
    /// 解决方案配置
    /// </summary>
    [SugarTable("CALL_Config_Fault_Solution")]
    public class CallConfigFaultSolution
    {
        /// <summary>
        /// 故障配置ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "fault_Config_Id")]
        public int? FaultConfigId { get; set; }

        /// <summary>
        /// 故障内容
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "fault_Content")]
        public string FaultContent { get; set; }

        /// <summary>
        /// 解决方案
        /// </summary>
        [SugarColumn(ColumnName = "solution_Content")]
        public string SolutionContent { get; set; }
    }
}