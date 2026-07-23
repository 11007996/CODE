using EAM.Model;
using EAM.Model.Dto;
using EAM.ServiceCore.Model;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 邮件发送记录service接口
    /// </summary>
    public interface IEmailLogService : IBaseService<EmailLog>
    {
        PagedInfo<EmailLogDto> GetList(EmailLogQueryDto parm);

        EmailLog GetInfo(long Id);

        EmailLog AddEmailLog(EmailLog parm);

        int UpdateEmailLog(EmailLog parm);
    }
}