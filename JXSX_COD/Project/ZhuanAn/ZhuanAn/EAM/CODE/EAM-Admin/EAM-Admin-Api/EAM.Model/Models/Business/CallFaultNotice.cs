namespace EAM.Model.Business
{
    /// <summary>
    /// 呼叫通知记录
    /// </summary>
    [SugarTable("CALL_Fault_Notice")]
    public class CallFaultNotice
    {
        /// <summary>
        /// 呼叫ID
        /// </summary>
        [SugarColumn(ColumnName = "call_Id")]
        public int CallId { get; set; }

        /// <summary>
        /// 呼叫阶段
        /// </summary>
        [SugarColumn(ColumnName = "call_Stage_Type")]
        public string CallStageType { get; set; }

        /// <summary>
        /// 通知ID
        /// </summary>
        [SugarColumn(ColumnName = "wx_Notice_Id")]
        public long? WxNoticeId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }
    }
}