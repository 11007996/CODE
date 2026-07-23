using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Call;

namespace EAM.Service.Call.ICallService
{
    /// <summary>
    /// 呼叫盒信息service接口
    /// </summary>
    public interface ICallBoxBaseService : IBaseService<CallBoxBase>
    {
        PagedInfo<CallBoxBaseDto> GetList(CallBoxBaseQueryDto parm);

        CallBoxBase GetInfo(int BoxId);

        CallBoxBase AddCallBoxBase(CallBoxBase parm);

        int UpdateCallBoxBase(CallBoxBase parm);

        PagedInfo<DictDataDto> GetDict(CallBoxBaseQueryDto parm);
    }
}