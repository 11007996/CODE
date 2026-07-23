using EAM.Model;
using EAM.Model.Business;

using EAM.Model.Dto;

namespace EAM.Service.Business.IBusinessService
{
    /// <summary>
    /// 产品测量报告service接口
    /// </summary>
    public interface IProdMeasureTicketService : IBaseService<ProdMeasureTicket>
    {
        PagedInfo<ProdMeasureTicketDto> GetList(ProdMeasureTicketQueryDto parm);

        ProdMeasureTicket GetInfo(string TicketNo);

        ProdMeasureTicket AddProdMeasureTicket(ProdMeasureTicket parm);

        int UpdateProdMeasureTicket(ProdMeasureTicket parm);

        int DeleteProdMeasureTicket(string ticketNo);
    }
}