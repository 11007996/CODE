using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 动作调用 service接口
    /// </summary>
    public interface IIotActionInvokeService : IBaseService<IotProductEventAction>
    {
        Dictionary<string, object> ExeIotActionInvoke(IotActionInvokeDto parm);
    }
}