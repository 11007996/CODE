using EAM.Model;
using EAM.Model.Business;

using EAM.Model.Dto;

namespace EAM.Service.Business.IBusinessService
{
    /// <summary>
    /// 上线通知单service接口
    /// </summary>
    public interface IOnlineNoticeTicketService : IBaseService<OnlineNoticeTicket>
    {
        PagedInfo<OnlineNoticeTicketDto> GetList(OnlineNoticeTicketQueryDto parm);

        OnlineNoticeTicket GetInfo(string TicketNo);

        OnlineNoticeTicket AddOnlineNoticeTicket(OnlineNoticeTicket parm);

        int UpdateOnlineNoticeTicket(OnlineNoticeTicket parm);

        int DeleteOnlineNoticeTicket(string TicketNo);

        OnlineNoticeTicketEquipmentSummaryDto GetEquipmentSummary(string TicketNo);

        OnlineNoticeTicketFixtureSummaryDto GetFixtureSummary(string TicketNo);
    }
}