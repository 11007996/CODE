using EAM.Model;
using EAM.Model.Business;

using EAM.Model.Dto;

namespace EAM.Service.Business.IBusinessService
{
    /// <summary>
    /// 治具尺寸量测验收单service接口
    /// </summary>
    public interface ISizeMeasureTicketService : IBaseService<SizeMeasureTicket>
    {
        PagedInfo<SizeMeasureTicketDto> GetList(SizeMeasureTicketQueryDto parm);

        SizeMeasureTicket GetInfo(string TicketNo);

        SizeMeasureTicket AddSizeMeasureTicket(SizeMeasureTicket parm);

        int UpdateSizeMeasureTicket(SizeMeasureTicket parm);

        int DeleteSizeMeasureTicket(string ticketNo);

        int SizeMeasureTicketInStorage(SizeMeasureTicketInStorageDto modal);
    }
}