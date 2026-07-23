using Infrastructure;
using Infrastructure.Model;
using LxMail.Models;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LxMail.Common
{
    public class MailHelper
    {
        public static bool SendEmail(string mailTitle, string mailContent, string mailTo, string contentType = "text/plain")
        {
            return SendEmail(mailTitle, mailContent, mailTo, null, null, null, null, contentType);
        }

        public static bool SendEmail(string mailTitle, string mailContent, string mailTo, List<SysFile> files, string contentType = "text/plain")
        {
            return SendEmail(mailTitle, mailContent, mailTo, null, null, files, null, contentType);
        }

        public static bool SendEmail(string mailTitle, string mailContent, string mailTo, List<SysFile> files, string imgPath, string contentType = "text/plain")
        {
            return SendEmail(mailTitle, mailContent, mailTo, null, null, files, imgPath, contentType);
        }

        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="mailTitle">发送邮件标题</param>
        /// <param name="mailContent">发送邮件内容</param>
        /// <param name="mailRecipient">接收人邮件,多个用逗号隔开</param>
        /// <param name="mailRecipientCC">抄送人邮箱,多个用逗号隔开</param>
        /// <param name="mailRecipientBCC">密送人邮箱,多个用逗号隔开</param>
        /// <param name="files">邮件附件</param>
        /// <param name="imgPath">备用图片</param>
        /// <param name="contentType">邮件内容类型</param>
        /// <param name="priority">优先级</param>
        /// <returns></returns>
        public static bool SendEmail(string mailTitle, string mailContent, string mailRecipient, string mailRecipientCC, string mailRecipientBCC, List<SysFile> files = null, string imgPath = null, string contentType = "text/plain", MailPriority priority = MailPriority.Normal)
        {
            //设置发送方邮件信息，例如：qq邮箱
            MailOptions options = AppSettings.Get<MailOptions>("MailOptions");
            string stmpServer = options.Smtp;//smtp服务器地址
            string fromName = options.FromName;
            string fromEmail = options.FromEmail;
            string pwd = options.Password;
            int port = options.Port;
            string signature = options.Signature;

            //邮件服务客户端
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = stmpServer;//指定发送方SMTP服务器
            smtpClient.EnableSsl = options.UseSsl;//使用安全加密连接
            smtpClient.Credentials = new NetworkCredential(fromName, pwd);//设置发送账号密码
            smtpClient.Port = port;

            //邮件信息
            MailMessage mailMessage = new MailMessage(fromEmail, mailRecipient);//实例化邮件信息实体并设置发送方和接收方
            mailMessage.Subject = mailTitle;//设置发送邮件得标题
            mailMessage.BodyEncoding = Encoding.UTF8;//设置发送邮件得编码
            mailMessage.IsBodyHtml = true;//设置内容是否为HTML格式
            mailMessage.Priority = priority;//设置邮件发送优先级
            if (!string.IsNullOrEmpty(mailRecipientCC))//抄送
                mailMessage.CC.Add(mailRecipientCC);
            if (!string.IsNullOrEmpty(mailRecipientBCC))//密送
                mailMessage.Bcc.Add(mailRecipientBCC);

            //签名
            signature = $"<div style='color: red;'>{signature}</div><br>";
            //内容
            if (contentType != "text/html")
            {
                mailContent = "<pre>" + WebUtility.HtmlEncode(mailContent) + "</pre>";//标签内容转为编译内容,<pre>用来保留内容里的空白、换行特殊字符
            }
            mailMessage.Body = signature + mailContent;//设置发送邮件内容

            //邮件嵌入外部资源
            if (File.Exists(imgPath))
            {
                //获取图片的MIME类型
                var provider = new FileExtensionContentTypeProvider();
                provider.TryGetContentType(imgPath, out string imgContentType);
                //图片资源
                AlternateView imgView = AlternateView.CreateAlternateViewFromString(mailMessage.Body + "<br><img src=\"cid:imgLogo\"  />", null, "text/html");
                LinkedResource lr = new LinkedResource(imgPath, imgContentType);
                lr.ContentId = "imgLogo";
                imgView.LinkedResources.Add(lr);
                mailMessage.AlternateViews.Add(imgView);
            }

            // 添加附件
            if (files != null && files.Count > 0)
            {
                foreach (SysFile file in files)
                {
                    Attachment attachment = new Attachment(file.FileUrl);
                    attachment.Name = file.RealName;
                    mailMessage.Attachments.Add(attachment);
                }
            }

            smtpClient.Send(mailMessage);//发送邮件
            return true;
        }
    }
}