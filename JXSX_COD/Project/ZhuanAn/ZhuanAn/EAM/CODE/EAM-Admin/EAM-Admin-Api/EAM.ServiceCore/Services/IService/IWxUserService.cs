using EAM.ServiceCore.Model;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 企业微信发送记录表service接口
    /// </summary>
    public interface IWxUserService : IBaseService<WxUser>
    {
        WxUser GetInfo(string code);

        void SendTextMsg(string touser, string content);
    }
}