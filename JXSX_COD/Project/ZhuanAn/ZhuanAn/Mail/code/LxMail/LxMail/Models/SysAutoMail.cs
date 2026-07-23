using SqlSugar;

namespace LxMail.Models
{
    [Tenant("0")]
    [SugarTable("Sys_Auto_Mail")]
    public class SysAutoMail
    {
        /// <summary>
        /// 邮件ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "Mail_ID")]
        public long MailID { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        [SugarColumn(ColumnName = "Mail_Subject")]
        public string MailSubject { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        [SugarColumn(ColumnName = "Mail_Body")]
        public string MailBody { get; set; }

        /// <summary>
        /// 收件人，逗号分隔
        /// </summary>
        [SugarColumn(ColumnName = "Mail_Recipient")]
        public string MailRecipient { get; set; }

        /// <summary>
        /// 抄送，逗号分隔
        /// </summary>
        [SugarColumn(ColumnName = "Mail_RecipientCC")]
        public string MailRecipientCC { get; set; }

        /// <summary>
        /// 密送，逗号分隔
        /// </summary>
        [SugarColumn(ColumnName = "Mail_RecipientBCC")]
        public string MailRecipientBCC { get; set; }

        /// <summary>
        /// 邮件内容类型（MIME类型）
        /// </summary>
        [SugarColumn(ColumnName = "Mail_BodyType")]
        public string MailBodyType { get; set; }

        /// <summary>
        /// 附件列表, 逗号分隔（上传文件的ID）
        /// </summary>
        [SugarColumn(ColumnName = "Attachments")]
        public string Attachments { get; set; }

        /// <summary>
        /// 备用图片(单个,上传的图片文件的ID)
        /// </summary>
        [SugarColumn(ColumnName = "Alternate_Image")]
        public string AlternateImage { get; set; }

        /// <summary>
        /// 邮件优先级(0:一般，1:低，2:高)
        /// </summary>
        [SugarColumn(ColumnName = "Mail_Priority")]
        public int MailPriority { get; set; }

        /// <summary>
        /// 发件人来源名称(例如:xx程序，xxIP服务器应用)
        /// </summary>
        [SugarColumn(ColumnName = "Mail_FromProfile")]
        public string MailFromProfile { get; set; }

        /// <summary>
        /// 发件人身份ID
        /// </summary>
        [SugarColumn(ColumnName = "Mail_ItemId")]
        public string MailItemId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "Create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "Create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 是否发送(N:未发送，Y:已发送)
        /// </summary>
        [SugarColumn(ColumnName = "Send_Flag")]
        public string SendFlag { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [SugarColumn(ColumnName = "Send_Time")]
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 发件失败次数
        /// </summary>
        [SugarColumn(ColumnName = "Send_Failed_Count")]
        public int SendFailedCount { get; set; }

        /// <summary>
        /// 发件报错信息
        /// </summary>
        [SugarColumn(ColumnName = "Err_Msg")]
        public string ErrMsg { get; set; }
    }
}