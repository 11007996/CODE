using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 产品物模型事件service接口
    /// </summary>
    public interface IIotProductThingEventService : IBaseService<IotProductThingEvent>
    {
        PagedInfo<IotProductThingEventDto> GetList(IotProductThingEventQueryDto parm);

        IotProductThingEvent GetInfo(int EventId);

        IotProductThingEvent AddIotProductThingEvent(IotProductThingEvent parm);

        int UpdateIotProductThingEvent(IotProductThingEvent parm);
    }
}