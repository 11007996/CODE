using Infrastructure.Attribute;
using Infrastructure.Extensions;
using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Basic;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;
using System.Linq;
using Microsoft.AspNetCore.Http;
using EAM.Model.System;

namespace EAM.Service.Basic
{
    /// <summary>
    /// 基础分组Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IBaseGroupService), ServiceLifetime = LifeTime.Transient)]
    public class BaseGroupService : BaseService<BaseGroup>, IBaseGroupService
    {

        public BaseGroupService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询基础分组列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<BaseGroupDto> GetList(BaseGroupQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new BaseGroupDto()
                {
                    UserNum = SqlFunc.Subqueryable<BaseGroupUser>().Where(f => f.GroupId == it.GroupId).Count()
                }, true)
                .ToPage<BaseGroupDto>(parm);

            return response;
        }


        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public BaseGroup GetInfo(int GroupId)
        {
            var response = Queryable()
                .Where(x => x.GroupId == GroupId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加基础分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseGroup AddBaseGroup(BaseGroup model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改基础分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateBaseGroup(BaseGroup model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 删除基础分组
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public int DeleteBaseGroup(int groupId)
        {
            Context.Deleteable<BaseGroupUser>().Where(it => it.GroupId == groupId).ExecuteCommand();
            return Delete(groupId);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<BaseGroup> QueryExp(BaseGroupQueryDto parm)
        {
            var predicate = Expressionable.Create<BaseGroup>();

            return predicate;
        }
    }
}