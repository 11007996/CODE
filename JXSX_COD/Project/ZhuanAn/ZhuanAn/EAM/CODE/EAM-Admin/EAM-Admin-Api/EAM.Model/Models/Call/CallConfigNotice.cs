using EAM.Model.Dto;

namespace EAM.Model.Call
{
    /// <summary>
    /// 呼叫通知配置
    /// </summary>
    [SugarTable("CALL_Config_Notice")]
    public class CallConfigNotice
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "notice_Config_Id")]
        public int NoticeConfigId { get; set; }

        /// <summary>
        /// 呼叫阶段
        /// </summary>
        [SugarColumn(ColumnName = "call_Stage_Type")]
        public string CallStageType { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [SugarColumn(ColumnName = "area_Id")]
        public int? AreaId { get; set; }

        /// <summary>
        /// 呼叫目标
        /// </summary>
        [SugarColumn(ColumnName = "Call_Target_Type")]
        public string CallTargetType { get; set; }

        /// <summary>
        /// 微信群id
        /// </summary>
        [SugarColumn(ColumnName = "Wx_Chat_Id")]
        public string WxChatId { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        [SugarColumn(ColumnName = "Emp_Codes")]
        public string EmpCodes { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string WxChatName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AreaName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<EmpSimpleDto> EmpNav { get; set; }
    }
}