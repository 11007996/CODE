using EAM.Model;
using EAM.Model.Business;

using EAM.Model.Dto;

namespace EAM.Service.Business.IBusinessService
{
    /// <summary>
    /// 产品开发需求单service接口
    /// </summary>
    public interface IProductDevDemandTicketService : IBaseService<ProductDevDemandTicket>
    {
        PagedInfo<ProductDevDemandTicketDto> GetList(ProductDevDemandTicketQueryDto parm);

        ProductDevDemandTicket GetInfo(string TicketNo);

        ProductDevDemandTicket AddProductDevDemandTicket(ProductDevDemandTicket parm);

        int UpdateProductDevDemandTicket(ProductDevDemandTicket parm);

        int DeleteProductDevDemandTicket(string ticketNo);

        int AsyncProductDevDemandTicket(string ticketNo);
    }
}