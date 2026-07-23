namespace EAM.Model.Call
{
    /// <summary>
    /// 故障记录
    /// </summary>
    [SugarTable("CALL_Fault_Base")]
    public class CallFaultBase
    {
        /// <summary>
        /// 呼叫记录ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "call_Id")]
        public int CallId { get; set; }

        /// <summary>
        /// 线别ID
        /// </summary>
        [SugarColumn(ColumnName = "line_Id")]
        public int? LineId { get; set; }

        /// <summary>
        /// 呼叫原因
        /// </summary>
        [SugarColumn(ColumnName = "call_Reason")]
        public string CallReason { get; set; }

        /// <summary>
        /// 呼叫目标
        /// </summary>
        [SugarColumn(ColumnName = "Call_Target_Type")]
        public string CallTargetType { get; set; }

        #region 位置信息

        [SugarColumn(ColumnName = "Call_Point_Type")]
        public string CallPointType { get; set; }

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
        /// 工站Id
        /// </summary>
        [SugarColumn(ColumnName = "Station_Id")]
        public int? StationId { get; set; }

        #endregion 位置信息

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 故障状态
        /// </summary>
        [SugarColumn(ColumnName = "fault_Status")]
        public string FaultStatus { get; set; }

        /// <summary>
        /// 最大处理时间（分钟）
        /// </summary>
        [SugarColumn(ColumnName = "max_Handle_Times")]
        public int? MaxHandleTimes { get; set; }

        /// <summary>
        /// 最大支援时间（分钟）
        /// </summary>
        [SugarColumn(ColumnName = "max_Help_Times")]
        public int? MaxHelpTimes { get; set; }

        /// <summary>
        /// 开始处理时间
        /// </summary>
        [SugarColumn(ColumnName = "handle_Time")]
        public DateTime? HandleTime { get; set; }

        /// <summary>
        /// 呼叫支援时间
        /// </summary>
        [SugarColumn(ColumnName = "call_Help_Time")]
        public DateTime? CallHelpTime { get; set; }

        /// <summary>
        /// 开始支援时间
        /// </summary>
        [SugarColumn(ColumnName = "help_Time")]
        public DateTime? HelpTime { get; set; }

        /// <summary>
        /// 结束处理时间
        /// </summary>
        [SugarColumn(ColumnName = "end_Time")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 处理人编号
        /// </summary>
        [SugarColumn(ColumnName = "handler_No")]
        public string HandlerNo { get; set; }

        /// <summary>
        /// 支援人编号
        /// </summary>
        [SugarColumn(ColumnName = "helper_No")]
        public string HelperNo { get; set; }

        /// <summary>
        /// 解决人编号
        /// </summary>
        [SugarColumn(ColumnName = "solver_No")]
        public string SolverNo { get; set; }

        /// <summary>
        /// 品质确认人工号
        /// </summary>
        [SugarColumn(ColumnName = "qC_No")]
        public string QcNo { get; set; }

        /// <summary>
        /// 故障类型
        /// </summary>
        [SugarColumn(ColumnName = "fault_Type")]
        public string FaultType { get; set; }

        /// <summary>
        /// 故障内容
        /// </summary>
        [SugarColumn(ColumnName = "fault_Content")]
        public string FaultContent { get; set; }

        /// <summary>
        /// 解决方案内容
        /// </summary>
        [SugarColumn(ColumnName = "solution_Content")]
        public string SolutionContent { get; set; }

        /// <summary>
        /// 制品跟踪数
        /// </summary>
        [SugarColumn(ColumnName = "prod_Count")]
        public int? ProdCount { get; set; }

        /// <summary>
        /// 良品数量
        /// </summary>
        [SugarColumn(ColumnName = "pass_Count")]
        public int? PassCount { get; set; }

        /// <summary>
        /// 不良品数量
        /// </summary>
        [SugarColumn(ColumnName = "nG_Count")]
        public int? NgCount { get; set; }

        /// <summary>
        /// 呼叫支援方式（0超时触发，1主动呼叫）
        /// </summary>
        [SugarColumn(ColumnName = "call_Help_Way")]
        public string CallHelpWay { get; set; }

        /// <summary>
        /// 呼叫电脑IP
        /// </summary>
        [SugarColumn(ColumnName = "pC_IP")]
        public string PcIp { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 最后更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

        [SugarColumn(ColumnName = "del_Flag")]
        public int DelFlag { get; set; }

        /// <summary>
        /// 呼叫盒Id
        /// </summary>
        [SugarColumn(ColumnName = "box_Id")]
        public int? BoxId { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(CallFaultOperate.CallId), nameof(CallId))] //自定义关系映射
        public List<CallFaultOperate> CallFaultOperateNav { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string LineName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string HandlerName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string HelperName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string SolverName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string QcName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string StationName { get; set; }
    }
}