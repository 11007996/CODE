using EAM.Model.Dto;

namespace EAM.Model.Consumable
{
    /// <summary>
    /// 耗品通知配置
    /// </summary>
    [SugarTable("CON_Config_Notice")]
    public class ConsumableConfigNotice
    {
        /// <summary>
        /// 通知配置ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "notice_Config_Id")]
        public int NoticeConfigId { get; set; }

        /// <summary>
        /// 微信群ID
        /// </summary>
        [SugarColumn(ColumnName = "wx_chat_Id")]
        public string WxChatId { get; set; }

        /// <summary>
        /// 人员工号
        /// </summary>
        [SugarColumn(ColumnName = "emp_Codes")]
        public string EmpCodes { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string WxChatName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<EmpSimpleDto> EmpNav { get; set; }
    }
}