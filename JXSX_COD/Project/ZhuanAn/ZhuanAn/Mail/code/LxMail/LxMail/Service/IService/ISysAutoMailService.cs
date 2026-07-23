using LxMail.Models;
using LxMail.Models.Dto;

namespace LxMail.Service
{
    public interface ISysAutoMailService
    {
        public void SendMail(SysAutoMail mail);

        public SysAutoMail GetSysSendMail(long id);

        public List<SysAutoMail> ListSysAutoMail(SysAutoMailDto parm);

        public SysAutoMail AddSysAutoMail(SysAutoMail model);
    }
}