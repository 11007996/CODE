using EAM.Model.Dto;

namespace EAM.Model.Equipment
{
    /// <summary>
    /// 保养通知配置
    /// </summary>
    [SugarTable("EQU_Maintain_Notice_Config")]
    public class MaintainNoticeConfig
    {
        /// <summary>
        /// 通知配置ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "notice_Config_Id")]
        public int NoticeConfigId { get; set; }

        /// <summary>
        /// 日期标记
        /// </summary>
        [SugarColumn(ColumnName = "date_Mark")]
        public string DateMark { get; set; }

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

        /// <summary>
        /// 是否有效(Y：有效，N:无效)
        /// </summary>
        [SugarColumn(ColumnName = "enable_flag")]
        public string EnableFlag { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string WxChatName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<EmpSimpleDto> EmpNav { get; set; }
    }
}