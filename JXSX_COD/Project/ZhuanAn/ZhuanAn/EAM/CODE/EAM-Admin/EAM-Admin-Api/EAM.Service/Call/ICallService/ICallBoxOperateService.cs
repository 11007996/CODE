using EAM.Model;
using EAM.Model.Call;
using EAM.Model.Dto;

namespace EAM.Service.Call.ICallService
{
    /// <summary>
    /// 盒子操作记录service接口
    /// </summary>
    public interface ICallBoxOperateService : IBaseService<CallBoxOperate>
    {
        PagedInfo<CallBoxOperateDto> GetList(CallBoxOperateQueryDto parm);

        CallBoxOperate GetInfo(long RecordId);

        CallBoxOperate AddCallBoxOperate(CallBoxOperate parm);
    }
}