using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.System;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 企业微信发送记录表service接口
    /// </summary>
    public interface IWxMessageService : IBaseService<WxMessage>
    {
        PagedInfo<WxMessageDto> GetList(WxMessageQueryDto parm);

        WxMessage GetInfo(int Id);

        WxMessage AddWxMessage(WxMessage parm);

        int UpdateWxMessage(WxMessage parm);

        WxMessage SendWxChatMessage(string chatId, string content);

        WxMessage SendWxEmpMessage(string empCodes, string content);

        WxMessage SendTextCardMessage(string empCodes, string content, string title, string linkUrl);
    }
}