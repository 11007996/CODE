using Infrastructure;
using Infrastructure.Attribute;
using LxMail.Common;
using LxMail.CoreService;
using LxMail.CoreService.Constants;
using LxMail.Models;
using LxMail.Models.Dto;
using System.Net.Mail;

namespace LxMail.Service
{
    /// <summary>
    /// 系统邮件
    /// </summary>
    [AppService(ServiceType = typeof(ISysAutoMailService), ServiceLifetime = LifeTime.Transient)]
    public class SysAutoMailService : BaseService<SysAutoMail>, ISysAutoMailService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail"></param>
        /// <exception cref="CustomException"></exception>
        public void SendMail(SysAutoMail mail)
        {
            if (mail == null) throw new CustomException("未找到要发送的邮件信息");
            //检查数据
            if (string.IsNullOrEmpty(mail.MailRecipient))
                throw new CustomException("收件人邮箱不能为空");
            if (string.IsNullOrEmpty(mail.MailSubject))
                throw new CustomException("邮件标题不能为空");
            if (string.IsNullOrEmpty(mail.MailBody))
                throw new CustomException("邮件内容不能为空");
            //附件清单
            List<SysFile> files = null;
            if (!string.IsNullOrEmpty(mail.Attachments))
            {
                List<long> fileIds = mail.Attachments.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(it => Convert.ToInt64(it)).ToList();
                files = Context.Queryable<SysFile>().Where(it => fileIds.Contains(it.Id)).ToList();
            }
            //备用图片
            string imagePath = mail.AlternateImage;
            if (long.TryParse(mail.AlternateImage, out long imgFileId))
            {
                SysFile imageFile = Context.Queryable<SysFile>().Where(it => it.Id == imgFileId).First();
                imagePath = imageFile?.FileUrl;
            }
            try
            {
                MailHelper.SendEmail(mail.MailSubject, mail.MailBody, mail.MailRecipient, mail.MailRecipientCC, mail.MailRecipientBCC, files, imagePath, mail.MailBodyType, (MailPriority)mail.MailPriority);
                mail.SendFlag = MailSendFlagConstant.已发送;
                mail.SendTime = DateTime.Now;
                Context.Updateable(mail).UpdateColumns(it => new { it.SendFlag, it.SendTime }).ExecuteCommand();
            }
            catch (Exception ex)
            {
                mail.ErrMsg += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + ex.Message + Environment.NewLine;
                mail.SendFailedCount++;
                Context.Updateable(mail).UpdateColumns(it => new { it.ErrMsg, it.SendFailedCount }).ExecuteCommand();
                throw new CustomException("发送失败: " + ex.Message);
            }
        }

        /// <summary>
        /// 查询邮件列表
        /// </summary>
        /// <returns></returns>
        public List<SysAutoMail> ListSysAutoMail(SysAutoMailDto parm)
        {
            return Queryable().WhereIF(!string.IsNullOrEmpty(parm.SendFlag), it => it.SendFlag == parm.SendFlag)
                .WhereIF(parm.CreateTime != null, it => it.CreateTime > parm.CreateTime)
                .ToList();
        }

        public SysAutoMail GetSysSendMail(long id)
        {
            return GetById(id);
        }

        /// <summary>
        /// 添加邮件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SysAutoMail AddSysAutoMail(SysAutoMail model)
        {
            model.SendFlag = MailSendFlagConstant.未发送;
            model.SendTime = null;
            model.ErrMsg = null;
            model.SendFailedCount = 0;
            return Insertable(model).ExecuteReturnEntity();
        }
    }
}