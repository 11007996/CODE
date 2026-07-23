namespace EAM.Model.System
{
    /// <summary>
    /// 企业微信发送记录表
    /// </summary>
    [SugarTable("wx_message")]
    [Tenant("0")]
    public class WxMessage
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 聊天群ID
        /// </summary>
        [SugarColumn(ColumnName = "chat_id")]
        public string ChatId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [SugarColumn(ColumnName = "emp_Codes")]
        public string EmpCodes { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [SugarColumn(ColumnName = "msg_Type")]
        public string MsgType { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [SugarColumn(ColumnName = "link_Url")]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 图文集合
        /// </summary>
        public string Articles { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [SugarColumn(ColumnName = "send_Time")]
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 发送结果
        /// </summary>
        [SugarColumn(ColumnName = "result_Msg")]
        public string ResultMsg { get; set; }
    }
}