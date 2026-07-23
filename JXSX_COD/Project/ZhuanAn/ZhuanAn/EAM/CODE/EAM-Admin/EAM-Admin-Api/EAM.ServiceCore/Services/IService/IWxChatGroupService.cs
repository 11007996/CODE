using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.System;
using EAM.Model.System.Dto;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 微信聊天群service接口
    /// </summary>
    public interface IWxChatGroupService : IBaseService<WxChatGroup>
    {
        PagedInfo<WxChatGroupDto> GetList(WxChatGroupQueryDto parm);

        WxChatGroup GetInfo(string ChatId);

        WxChatGroup AddWxChatGroup(WxChatGroup parm);

        int UpdateWxChatGroup(WxChatGroup parm);

        PagedInfo<DictDataDto> GetDict(WxChatGroupQueryDto parm);
    }
}