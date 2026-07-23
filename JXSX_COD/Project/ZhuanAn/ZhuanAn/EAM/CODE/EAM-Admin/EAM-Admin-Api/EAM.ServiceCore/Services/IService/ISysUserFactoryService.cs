using EAM.Model;
using EAM.Model.System;
using EAM.Model.System.Dto;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 用户关联厂区service接口
    /// </summary>
    public interface ISysUserFactoryService : IBaseService<SysUserFactory>
    {
        PagedInfo<SysUser> GetSysUserByFactroy(SysUserFactoryQueryDto parm);

        int AddFactoryUser(FactoryUsersOperateDto parm);

        int DeleteFactoryUserByUserIds(string factoryId, List<long> userIds);

        public PagedInfo<SysUser> GetExcludedSysUsersByFactory(SysUserFactoryQueryDto parm);

        List<SysFactory> GetUserFactorysByUserName(string username);

        public void InsertUserFactory(SysUser user);

        bool CheckUserFactoryPermission(string userName, string factoryId);
    }
}