namespace EAM.Model.Call
{
    /// <summary>
    /// 呼叫操作记录
    /// </summary>
    [SugarTable("CALL_Fault_Operate")]
    public class CallFaultOperate
    {
        /// <summary>
        /// 关联故障Id
        /// </summary>
        [SugarColumn(ColumnName = "call_Id")]
        public int CallId { get; set; }

        /// <summary>
        /// 处理人编号
        /// </summary>
        [SugarColumn(ColumnName = "operater_No")]
        public string OperaterNo { get; set; }

        /// <summary>
        /// 故障状态
        /// </summary>
        [SugarColumn(ColumnName = "fault_Status")]
        public string FaultStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string OperaterName { get; set; }
    }
}