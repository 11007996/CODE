using EAM.Model;
using EAM.Model.Call;
using EAM.Model.Dto;

namespace EAM.Service.Call.ICallService
{
    /// <summary>
    /// 产线设备service接口
    /// </summary>
    public interface ICallLineEquipmentService : IBaseService<CallLineEquipment>
    {
        PagedInfo<CallLineEquipmentDto> GetList(CallLineEquipmentQueryDto parm);

        CallLineEquipment GetInfo(int Id);

        CallLineEquipment AddCallLineEquipment(CallLineEquipment parm);

        int UpdateCallLineEquipment(CallLineEquipment parm);
    }
}