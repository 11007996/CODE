namespace EAM.Model.System
{
    /// <summary>
    /// 微信聊天群
    /// </summary>
    [SugarTable("wx_chat_group")]
    [Tenant("0")]
    public class WxChatGroup
    {
        /// <summary>
        /// 群ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "chat_ID")]
        public string ChatId { get; set; }

        /// <summary>
        /// 群名称
        /// </summary>
        [SugarColumn(ColumnName = "chat_Name")]
        public string ChatName { get; set; }

        /// <summary>
        /// 关联厂区
        /// </summary>
        [SugarColumn(ColumnName = "factory_Id")]
        public string FactoryId { get; set; }
    }
}