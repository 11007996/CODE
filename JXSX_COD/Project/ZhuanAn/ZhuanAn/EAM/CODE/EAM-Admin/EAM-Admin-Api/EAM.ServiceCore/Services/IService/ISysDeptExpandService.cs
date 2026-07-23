using EAM.Model.System;
using EAM.Model.System.Dto;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 系统部门扩展service接口
    /// </summary>
    public interface ISysDeptExpandService : IBaseService<SysDeptExpand>
    {
        List<SysDeptExpandDto> GetList(SysDeptExpandQueryDto parm);

        SysDeptExpand GetInfo(long SysDeptId);

        SysDeptExpand AddSysDeptExpand(SysDeptExpand parm);

        int UpdateSysDeptExpand(SysDeptExpand parm);

        bool SyncSysDeptExpand();
    }
}