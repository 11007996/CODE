using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.System;
using EAM.Model.System.Dto;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 厂区管理service接口
    /// </summary>
    public interface ISysFactoryService : IBaseService<SysFactory>
    {
        PagedInfo<SysFactoryDto> GetList(SysFactoryQueryDto parm);

        PagedInfo<SysFactoryDto> GetDetailList(SysFactoryQueryDto parm);

        PagedInfo<DictDataDto> GetDict(SysFactoryQueryDto parm);

        SysFactory GetInfo(string FactoryId);

        SysFactory AddFactory(SysFactory parm);

        int UpdateFactory(SysFactory parm);
    }
}