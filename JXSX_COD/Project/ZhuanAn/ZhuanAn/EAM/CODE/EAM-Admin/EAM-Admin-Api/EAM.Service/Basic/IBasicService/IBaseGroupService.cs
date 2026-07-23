using EAM.Model;
using EAM.Model.Basic;

using EAM.Model.Dto;

namespace EAM.Service.Basic.IBasicService
{
    /// <summary>
    /// 基础分组service接口
    /// </summary>
    public interface IBaseGroupService : IBaseService<BaseGroup>
    {
        PagedInfo<BaseGroupDto> GetList(BaseGroupQueryDto parm);

        BaseGroup GetInfo(int GroupId);

        BaseGroup AddBaseGroup(BaseGroup parm);

        int UpdateBaseGroup(BaseGroup parm);

        int DeleteBaseGroup(int GroupId);
    }
}