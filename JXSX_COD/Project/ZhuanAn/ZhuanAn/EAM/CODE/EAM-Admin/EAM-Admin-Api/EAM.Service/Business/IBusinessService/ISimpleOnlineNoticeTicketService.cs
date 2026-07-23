using EAM.Model;
using EAM.Model.Business;

using EAM.Model.Dto;

namespace EAM.Service.Business.IBusinessService
{
    /// <summary>
    /// 上线通知单service接口
    /// </summary>
    public interface ISimpleOnlineNoticeTicketService : IBaseService<SimpleOnlineNoticeTicket>
    {
        PagedInfo<SimpleOnlineNoticeTicketDto> GetList(SimpleOnlineNoticeTicketQueryDto parm);

        SimpleOnlineNoticeTicket GetInfo(string TicketNo);

        SimpleOnlineNoticeTicket AddSimpleOnlineNoticeTicket(SimpleOnlineNoticeTicket parm);

        int UpdateSimpleOnlineNoticeTicket(SimpleOnlineNoticeTicket parm);

        int CloseSimpleOnlineNoticeTicket(string TicketNo);

        int DeleteSimpleOnlineNoticeTicket(string TicketNo);

        PagedInfo<DictDataDto> QueryOnlineNoticeTicketItemDict(SimpleOnlineNoticeTicketDictQueryDto parm);

        PagedInfo<DictDataDto> QueryOnlineNoticeTicketPartNameDict(SimpleOnlineNoticeTicketDictQueryDto parm);

        List<SimpleOnlineNoticeTicketItemDto> QueryOnlineNoticeTicketItemsByPart(string partName);
    }
}