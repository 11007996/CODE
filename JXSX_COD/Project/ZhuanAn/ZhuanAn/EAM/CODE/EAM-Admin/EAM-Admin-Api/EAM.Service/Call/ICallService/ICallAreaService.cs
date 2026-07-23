using EAM.Model;
using EAM.Model.Call;
using EAM.Model.Dto;

namespace EAM.Service.Call.ICallService
{
    /// <summary>
    /// 广播区域service接口
    /// </summary>
    public interface ICallAreaService : IBaseService<CallArea>
    {
        PagedInfo<CallAreaDto> GetList(CallAreaQueryDto parm);

        CallArea GetInfo(int AreaId);

        CallArea AddCallArea(CallArea parm);

        int UpdateCallArea(CallArea parm);

        bool DeleteCallArea(int AreaId);

        PagedInfo<DictDataDto> GetDict(CallAreaQueryDto parm);
    }
}