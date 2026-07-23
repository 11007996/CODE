using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 产品事件处理动作service接口
    /// </summary>
    public interface IIotProductEventActionService : IBaseService<IotProductEventAction>
    {
        PagedInfo<IotProductEventActionDto> GetList(IotProductEventActionQueryDto parm);

        IotProductEventAction GetInfo(int ActionId);

        IotProductEventAction AddIotProductEventAction(IotProductEventAction parm);

        int UpdateIotProductEventAction(IotProductEventAction parm);
    }
}