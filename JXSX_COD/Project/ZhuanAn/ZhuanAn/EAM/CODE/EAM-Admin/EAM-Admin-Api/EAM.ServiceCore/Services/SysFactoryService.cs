using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.Model.System.Dto;
using EAM.Repository;
using Infrastructure.Attribute;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 厂区管理Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ISysFactoryService), ServiceLifetime = LifeTime.Transient)]
    public class SysFactoryService : BaseService<SysFactory>, ISysFactoryService
    {
        /// <summary>
        /// 查询厂区管理列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<SysFactoryDto> GetList(SysFactoryQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("FactoryId asc")
                .Where(predicate.ToExpression())
                .ToPage<SysFactory, SysFactoryDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取厂区详情列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<SysFactoryDto> GetDetailList(SysFactoryQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("FactoryId asc")
                .Where(predicate.ToExpression())
                .LeftJoin<SysDept>((it, d) => it.RootDeptId == d.DeptId)
                .LeftJoin<SysRole>((it, d, r) => it.DefaultRoleId == r.RoleId)
                .Select((it, d, r) => new SysFactoryDto
                {
                    RootDeptName = d.DeptName,
                    DefaultRoleName = r.RoleName,
                    UserCount = SqlFunc.Subqueryable<SysUserFactory>().Where(f => f.FactoryId == it.FactoryId).Count()
                }, true)
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取厂区字典列表
        /// </summary>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(SysFactoryQueryDto parm)
        {
            var predicate = QueryExp(parm);
            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto
                {
                    DictValue = it.FactoryId,
                    DictLabel = it.FactoryName
                })
                .ToPage(parm);
            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="FactoryId"></param>
        /// <returns></returns>
        public SysFactory GetInfo(string FactoryId)
        {
            var response = Queryable()
                .Where(x => x.FactoryId == FactoryId)
                .LeftJoin<SysRole>((x, r) => x.DefaultRoleId == r.RoleId)
                .Select((x, r) => new SysFactory()
                {
                    DefaultRoleName = r.RoleName,
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加厂区管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SysFactory AddFactory(SysFactory model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改厂区管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateFactory(SysFactory model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<SysFactory> QueryExp(SysFactoryQueryDto parm)
        {
            var predicate = Expressionable.Create<SysFactory>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.FactoryId), it => it.FactoryId == parm.FactoryId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.FactoryName), it => it.FactoryName.Contains(parm.FactoryName));
            return predicate;
        }
    }
}