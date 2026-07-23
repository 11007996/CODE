using EAM.Model;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.Model.System.Dto;
using EAM.Repository;
using Infrastructure;
using Infrastructure.Attribute;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 用户关联厂区Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ISysUserFactoryService), ServiceLifetime = LifeTime.Transient)]
    public class SysUserFactoryService : BaseService<SysUserFactory>, ISysUserFactoryService
    {
        /// <summary>
        /// 获取用户数据根据厂区id
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<SysUser> GetSysUserByFactroy(SysUserFactoryQueryDto parm)
        {
            var res = Context.Queryable<SysUser>()
                .LeftJoin<SysUserFactory>((u, uf) => u.UserId == uf.UserId)
                .Where((u, uf) => uf.FactoryId == parm.FactoryId && u.DelFlag == (int)DeleteFlagEnum.存在)
                .WhereIF(!string.IsNullOrEmpty(parm.UserName), (u, uf) => u.UserName.Contains(parm.UserName))
                .Select((u, uf) => u)
                .ToPage(parm);

            return res;
        }

        /// <summary>
        /// 分配置工厂用户
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int AddFactoryUser(FactoryUsersOperateDto parm)
        {
            List<SysUserFactory> userFactorys = new();
            foreach (var item in parm.UserIds)
            {
                userFactorys.Add(new SysUserFactory() { FactoryId = parm.FactoryId, UserId = item });
            }

            return userFactorys.Count > 0 ? Insert(userFactorys) : 0;
        }

        /// <summary>
        /// 删除工厂用户
        /// </summary>
        /// <param name="factoryId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public int DeleteFactoryUserByUserIds(string factoryId, List<long> userIds)
        {
            return Delete(it => it.FactoryId == factoryId && userIds.Contains(it.UserId)) ? 1 : 0;
        }

        /// <summary>
        /// 获取工厂未分配用户
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<SysUser> GetExcludedSysUsersByFactory(SysUserFactoryQueryDto parm)
        {
            var query = Context.Queryable<SysUser>()
              .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在)
              .Where(it => SqlFunc.Subqueryable<SysUserFactory>().Where(s => s.UserId == it.UserId && s.FactoryId == parm.FactoryId).NotAny())
              .WhereIF(parm.UserName.IsNotEmpty(), it => it.UserName.Contains(parm.UserName));

            return query.ToPage(parm);
        }

        /// <summary>
        /// 新增用户厂区信息
        /// </summary>
        /// <param name="user"></param>
        public void InsertUserFactory(SysUser user)
        {
            // 新增用户与厂区管理
            List<SysUserFactory> list = new();
            foreach (var item in user.FactoryIds)
            {
                list.Add(new SysUserFactory() { FactoryId = item, UserId = user.UserId });
            }
            InsertRange(list);
        }

        /// <summary>
        /// 检查用户是否有厂区的权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="factoryId"></param>
        /// <returns></returns>
        public bool CheckUserFactoryPermission(string userName, string factoryId)
        {
            if (userName == GlobalConstant.AdminRole)
            {
                return true;
            }
            else
            {
                var user = Context.Queryable<SysUser>().Where(it => it.UserName == userName).First();
                if (user != null)
                {
                    return Queryable().Where(it => it.UserId == user.UserId && it.FactoryId == factoryId).Count() >= 1;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取用户可登入厂区
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<SysFactory> GetUserFactorysByUserName(string username)
        {
            SysUser sysUser = Context.Queryable<SysUser>().ClearFilter().Where(x => x.UserName == username).First();
            if (sysUser == null)
                throw new CustomException("未找到用户信息");
            if (sysUser.IsAdmin)
            {
                return Context.Queryable<SysFactory>().Select(it => new SysFactory()
                {
                    FactoryId = it.FactoryId,
                    FactoryName = it.FactoryName
                })
                .ToList();
            }
            else
            {
                return Queryable().LeftJoin<SysUser>((it, u) => it.UserId == u.UserId)
                  .LeftJoin<SysFactory>((it, u, f) => it.FactoryId == f.FactoryId)
                  .Where((it, u, f) => u.UserName == username)
                  .Select((it, u, f) => new SysFactory()
                  {
                      FactoryId = f.FactoryId,
                      FactoryName = f.FactoryName,
                  }).ToList();
            }
        }
    }
}