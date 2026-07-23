using EAM.Model.System;

namespace EAM.ServiceCore.Services
{
    public interface ISysPermissionService
    {
        public List<string> GetRolePermission(SysUser user);

        public List<string> GetMenuPermission(SysUser user);
    }
}