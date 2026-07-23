using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Basic;
using EAM.Model.System;

namespace EAM.Service.Basic.IBasicService
{
    /// <summary>
    /// 分组用户service接口
    /// </summary>
    public interface IBaseGroupUserService : IBaseService<BaseGroupUser>
    {
        PagedInfo<BaseGroupUserDto> GetList(BaseGroupUserQueryDto parm);

        BaseGroupUser GetInfo(int GroupId);

        BaseGroupUser AddBaseGroupUser(BaseGroupUser parm);
        int BatchAddBaseGroupUser(BatchBaseGroupUserDto parm);

        int BatchDeleteBaseGroupUser(BatchBaseGroupUserDto parm);
        PagedInfo<EmployeeDto> GetExcludedUsersByGroupId(BaseGroupUserQueryDto parm);
    }
}
