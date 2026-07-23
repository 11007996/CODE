using EAM.Model;
using EAM.Model.Business;

using EAM.Model.Dto;

namespace EAM.Service.Business.IBusinessService
{
    /// <summary>
    /// 耗品领用单service接口
    /// </summary>
    public interface IConsumableReceiveTicketService : IBaseService<ConsumableReceiveTicket>
    {
        PagedInfo<ConsumableReceiveTicketDto> GetList(ConsumableReceiveTicketQueryDto parm);

        ConsumableReceiveTicket GetInfo(string TicketNo);

        ConsumableReceiveTicket AddConsumableReceiveTicket(ConsumableReceiveTicket parm);

        int UpdateConsumableReceiveTicket(ConsumableReceiveTicket parm);

        int DeletConsumableReceiveTicket(string ticketNo);

        List<ConsumableReceiveTicketItemSummaryDto> GetItemSummary(string TicketNo);

        List<ConsumableReceiveTicketItemReceiveDto> GetItemReceive(string TicketNo);

        int BatchReceiveConsumable(BatchReceiveConsumableDto parm);
    }
}