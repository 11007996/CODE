using EAM.Model;
using EAM.Model.Call;
using EAM.Model.Dto;

namespace EAM.Service.Call.ICallService
{
    /// <summary>
    /// 故障配置service接口
    /// </summary>
    public interface ICallConfigFaultService : IBaseService<CallConfigFault>
    {
        PagedInfo<CallConfigFaultDto> GetList(CallConfigFaultQueryDto parm);

        CallConfigFault GetInfo(int FaultConfigId);

        CallConfigFault AddCallConfigFault(CallConfigFault parm);

        int UpdateCallConfigFault(CallConfigFault parm);

        bool DeleteCallConfigFault(int FaultConfigId);
    }
}