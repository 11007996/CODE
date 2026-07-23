using EAM.Model.Business;
using EAM.Model.Dto;
using EAM.ServiceCore;

namespace EAM.Dashboard.Service.IService
{
    /// <summary>
    /// 业务工单service接口
    /// </summary>
    public interface IBusinessService : IBaseService<SimpleOnlineNoticeTicket>
    {
        List<SimpleOnlineNoticeTicketDto> GetSimpleOnlineNoticeTicketList();

        SimpleOnlineNoticeTicketDto GetSimpleOnlineNoticeTicketInfo(string ticketNo);
    }
}