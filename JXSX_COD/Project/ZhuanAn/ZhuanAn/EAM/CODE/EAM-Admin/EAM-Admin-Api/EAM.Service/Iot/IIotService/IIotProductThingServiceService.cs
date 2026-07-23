using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 产品物模型服务service接口
    /// </summary>
    public interface IIotProductThingServiceService : IBaseService<IotProductThingService>
    {
        PagedInfo<IotProductThingServiceDto> GetList(IotProductThingServiceQueryDto parm);

        IotProductThingService GetInfo(int ServiceId);

        IotProductThingService AddIotProductThingService(IotProductThingService parm);

        int UpdateIotProductThingService(IotProductThingService parm);
    }
}