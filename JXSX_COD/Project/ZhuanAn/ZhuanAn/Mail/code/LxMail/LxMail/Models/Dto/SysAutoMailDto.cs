using System.ComponentModel.DataAnnotations;

namespace LxMail.Models.Dto
{
    public class SysAutoMailDto
    {
        /// <summary>
        /// 邮件ID
        /// </summary>
        public long MailID { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        [Required(ErrorMessage = "邮件主题不能为空")]
        public string MailSubject { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        [Required(ErrorMessage = "邮件内容不能为空")]
        public string MailBody { get; set; }

        /// <summary>
        /// 收件人，逗号分隔
        /// </summary>
        [Required(ErrorMessage = "收件人不能为空")]
        public string MailRecipient { get; set; }

        /// <summary>
        /// 抄送，逗号分隔
        /// </summary>
        public string MailRecipientCC { get; set; }

        /// <summary>
        /// 密送，逗号分隔
        /// </summary>
        public string MailRecipientBCC { get; set; }

        /// <summary>
        /// 邮件内容类型（MIME类型,默认text/plain）
        /// </summary>
        public string MailBodyType { get; set; }

        /// <summary>
        /// 附件列表, 逗号分隔（上传文件的ID）
        /// </summary>
        public string Attachments { get; set; }

        /// <summary>
        /// 备用图片(单个,上传的图片文件的ID)
        /// </summary>
        public string AlternateImage { get; set; }

        /// <summary>
        /// 邮件优先级(0:一般，1:低，2:高)
        /// </summary>
        public int MailPriority { get; set; }

        /// <summary>
        /// 发件人来源名称(例如:xx程序，xxIP服务器应用)
        /// </summary>
        [Required(ErrorMessage = "邮件来源[MailFromProfile]不能为空")]
        public string MailFromProfile { get; set; }

        /// <summary>
        /// 发件人身份ID
        /// </summary>
        public string MailItemId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 是否发送(N:未发送，Y:已发送)
        /// </summary>
        public string SendFlag { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 发送失败报错信息
        /// </summary>
        public string ErrMsg { get; set; }
    }
}