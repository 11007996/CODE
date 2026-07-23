using EAM.Model;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.Model.System.Dto;
using EAM.Repository;
using Infrastructure;
using Infrastructure.Attribute;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [AppService(ServiceType = typeof(ISysUserRoleService), ServiceLifetime = LifeTime.Transient)]
    public class SysUserRoleService : BaseService<SysUserRole>, ISysUserRoleService
    {
        /// <summary>
        /// 通过角色ID查询角色使用数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int CountUserRoleByRoleId(long roleId)
        {
            return Count(it => it.RoleId == roleId);
        }

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int DeleteUserRoleByUserId(int userId)
        {
            return Delete(it => it.UserId == userId) ? 1 : 0;
        }

        /// <summary>
        /// 批量删除角色对应用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public int DeleteRoleUserByUserIds(long roleId, List<long> userIds)
        {
            return Delete(it => it.RoleId == roleId && userIds.Contains(it.UserId)) ? 1 : 0;
        }

        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="sysUserRoles"></param>
        /// <returns></returns>
        public int AddUserRole(List<SysUserRole> sysUserRoles)
        {
            return Insert(sysUserRoles);
        }

        /// <summary>
        /// 获取用户数据根据角色id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysUser> GetSysUsersByRoleId(long roleId)
        {
            return Context.Queryable<SysUserRole, SysUser>((t1, u) => new JoinQueryInfos(
                   JoinType.Left, t1.UserId == u.UserId))
                .WithCache(60 * 10)
                .Where((t1, u) => t1.RoleId == roleId && u.DelFlag == (int)DeleteFlagEnum.存在)
                .Select((t1, u) => u)
                .ToList();
        }

        /// <summary>
        /// 获取用户数据根据角色id
        /// </summary>
        /// <param name="roleUserQueryDto"></param>
        /// <returns></returns>
        public PagedInfo<SysUser> GetSysUsersByRoleId(RoleUserQueryDto roleUserQueryDto)
        {
            var query = Context.Queryable<SysUserRole, SysUser>((t1, u) => new JoinQueryInfos(
                JoinType.Left, t1.UserId == u.UserId))
                .Where((t1, u) => t1.RoleId == roleUserQueryDto.RoleId && u.DelFlag == (int)DeleteFlagEnum.存在);
            if (!string.IsNullOrEmpty(roleUserQueryDto.UserName))
            {
                query = query.Where((t1, u) => u.UserName.Contains(roleUserQueryDto.UserName));
            }
            return query.Select((t1, u) => u).ToPage(roleUserQueryDto);
        }

        /// <summary>
        /// 获取尚未指派的用户数据根据角色id
        /// </summary>
        /// <param name="roleUserQueryDto"></param>
        /// <returns></returns>
        public PagedInfo<SysUser> GetExcludedSysUsersByRoleId(RoleUserQueryDto roleUserQueryDto)
        {
            var query = Context.Queryable<SysUser>()
                .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在)
                .Where(it => SqlFunc.Subqueryable<SysUserRole>().Where(s => s.UserId == it.UserId && s.RoleId == roleUserQueryDto.RoleId).NotAny())
                .WhereIF(roleUserQueryDto.UserName.IsNotEmpty(), it => it.UserName.Contains(roleUserQueryDto.UserName));

            return query.ToPage(roleUserQueryDto);
        }

        /// <summary>
        /// 新增用户角色信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int InsertUserRole(SysUser user)
        {
            if (user.RoleIds == null) return 0;
            List<SysUserRole> userRoles = new();
            foreach (var item in user.RoleIds)
            {
                userRoles.Add(new SysUserRole() { RoleId = item, UserId = user.UserId });
            }

            return userRoles.Count > 0 ? AddUserRole(userRoles) : 0;
        }

        /// <summary>
        /// 新增加角色用户
        /// </summary>
        /// <param name="roleUsersCreateDto"></param>
        /// <returns></returns>
        public int InsertRoleUser(RoleUsersCreateDto roleUsersCreateDto)
        {
            List<SysUserRole> userRoles = new();
            foreach (var item in roleUsersCreateDto.UserIds)
            {
                userRoles.Add(new SysUserRole() { RoleId = roleUsersCreateDto.RoleId, UserId = item });
            }

            return userRoles.Count > 0 ? AddUserRole(userRoles) : 0;
        }

        /// <summary>
        /// 通过工厂的角色分配用户权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int AddFactoryRoleUser(FactoryRoleUserOpeateDto model)
        {
            //检查厂区是否有这个角色的权限
            bool hasPerms = Context.Queryable<SysRole>().Where(it => it.FactoryId == model.FactoryId && it.RoleId == model.RoleId).Any();
            if (!hasPerms)
                throw new CustomException("当前工厂没有这个角色的操作权限");

            //检查员工检查
            bool hasSysUser = Context.Queryable<SysUser>().Where(it => model.UserIds.Contains(it.UserId) && it.UserType == UserTypeConstant.系统用户).Any();
            if (hasSysUser)
                throw new CustomException("当前用户中存在非OA用户，请检查");

            //检查员工是否与厂区关联
            int hasBindCount = Context.Queryable<SysUserFactory>().Where(it => it.FactoryId == model.FactoryId && model.UserIds.Contains(it.UserId)).Count();
            if (hasBindCount != model.UserIds.Count)
                throw new CustomException($"当前用户中存在未关联【{model.FactoryId}】的用户，请检查");

            RoleUsersCreateDto dto = new RoleUsersCreateDto();
            dto.RoleId = model.RoleId;
            dto.UserIds = model.UserIds;

            return InsertRoleUser(dto);
        }

        /// <summary>
        /// 通过工厂的角色删除用户权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int DelFactoryRoleUser(FactoryRoleUserOpeateDto model)
        {
            //检查厂区是否有这个角色的权限
            bool hasPerms = Context.Queryable<SysRole>().Where(it => it.FactoryId == model.FactoryId && it.RoleId == model.RoleId).Any();
            if (!hasPerms)
                throw new CustomException("当前工厂没有这个角色的操作权限");

            //检查员工检查
            bool hasSysUser = Context.Queryable<SysUser>().Where(it => model.UserIds.Contains(it.UserId) && it.UserType == UserTypeConstant.系统用户).Any();
            if (hasSysUser)
                throw new CustomException("当前用户中存在非OA用户，请检查");

            //检查员工是否与厂区关联
            int hasBindCount = Context.Queryable<SysUserFactory>().Where(it => it.FactoryId == model.FactoryId && model.UserIds.Contains(it.UserId)).Count();
            if (hasBindCount != model.UserIds.Count)
                throw new CustomException($"当前用户中存在未关联【{model.FactoryId}】的用户，请检查");

            return DeleteRoleUserByUserIds(model.RoleId, model.UserIds);
        }

        /// <summary>
        /// 获取尚未指派的厂区用户数据根据角色id
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<SysUser> GetExcludedFactoryUsersByRoleId(RoleUserQueryDto parm)
        {
            var roleFactory = Context.Queryable<SysRole>().Where(it => it.RoleId == parm.RoleId).First();
            if (roleFactory == null)
                return null;

            var query = Context.Queryable<SysUser>()
              .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在)
              .Where(it => SqlFunc.Subqueryable<SysUserRole>().Where(s => s.UserId == it.UserId && s.RoleId == parm.RoleId).NotAny())//角色未关联用户
              .Where(it => SqlFunc.Subqueryable<SysUserFactory>().Where(f => f.UserId == it.UserId && f.FactoryId == roleFactory.FactoryId).Any())//厂区关联了用户
              .WhereIF(parm.UserName.IsNotEmpty(), it => it.UserName.Contains(parm.UserName));

            return query.ToPage(parm);
        }

        /// <summary>
        /// 批量导入员工角色绑定信息
        /// </summary>
        /// <returns></returns>
        public (string, object, object) ImportFactoryRoleUser(int roleId, string factory, List<EmpSimpleDto> list)
        {
            if (roleId <= 0)
                throw new CustomException("角色Id非法");

            List<StorageableMessage<EmpSimpleDto>> insertList = new List<StorageableMessage<EmpSimpleDto>>();
            List<StorageableMessage<EmpSimpleDto>> ignoreList = new List<StorageableMessage<EmpSimpleDto>>();
            List<StorageableMessage<EmpSimpleDto>> errorList = new List<StorageableMessage<EmpSimpleDto>>();
            FactoryRoleUserOpeateDto temp = null;
            SysUser user = null;
            SysUserRole userR = null;
            foreach (EmpSimpleDto emp in list)
            {
                try
                {
                    user = Context.Queryable<SysUser>().Where(x => x.UserName == emp.EmpCode).First();
                    if (user != null)
                        userR = Context.Queryable<SysUserRole>().Where(x => x.UserId == user.UserId && x.RoleId == roleId).First();
                    if (user != null && userR == null)
                    {
                        temp = new FactoryRoleUserOpeateDto()
                        {
                            RoleId = roleId,
                            UserIds = new List<long> { user.UserId },
                            FactoryId = factory
                        };
                        AddFactoryRoleUser(temp);
                        insertList.Add(new StorageableMessage<EmpSimpleDto>() { StorageMessage = emp.EmpCode + "成功", Item = emp });
                    }
                    else
                        ignoreList.Add(new StorageableMessage<EmpSimpleDto>() { StorageMessage = emp.EmpCode + "已存在", Item = emp });
                }
                catch (Exception ex)
                {
                    errorList.Add(new StorageableMessage<EmpSimpleDto>() { StorageMessage = emp.EmpCode + "错误:" + ex.Message, Item = emp });
                }
            }

            string msg = $"插入{insertList.Count} 忽略{ignoreList.Count} 错误{errorList.Count} 总共{list.Count}";

            return (msg, errorList, ignoreList);
        }
    }
}