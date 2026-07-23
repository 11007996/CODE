using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Basic
{
    /// <summary>
    /// 分组用户Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IBaseGroupUserService), ServiceLifetime = LifeTime.Transient)]
    public class BaseGroupUserService : BaseService<BaseGroupUser>, IBaseGroupUserService
    {
        public BaseGroupUserService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询分组用户列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<BaseGroupUserDto> GetList(BaseGroupUserQueryDto parm)
        {
            var response = Queryable()
                .LeftJoin<Employee>((it, e) => it.EmpCode == e.EmpCode)
                .WhereIF(parm.GroupId > 0, (it, e) => it.GroupId == parm.GroupId)
                .WhereIF(!string.IsNullOrEmpty(parm.EmpCode), (it, e) => it.EmpCode == parm.EmpCode)
                .WhereIF(!string.IsNullOrEmpty(parm.EmpName), (it, e) => e.EmpName.Contains(parm.EmpName))
                .Select((it, e) => new BaseGroupUserDto()
                {
                    EmpName = e.EmpName
                }, true)
                .ToPage<BaseGroupUserDto>(parm);
            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public BaseGroupUser GetInfo(int GroupId)
        {
            var response = Queryable()
                .Where(x => x.GroupId == GroupId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加分组用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseGroupUser AddBaseGroupUser(BaseGroupUser model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 批量添加分组用户
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int BatchAddBaseGroupUser(BatchBaseGroupUserDto parm)
        {
            List<BaseGroupUser> list = new();
            foreach (var item in parm.EmpCodes)
            {
                list.Add(new BaseGroupUser() { GroupId = parm.GroupId, EmpCode = item });
            }
            if (list.Count > 0)
            {
                return Insert(list);
            }
            return list.Count > 0 ? Insert(list) : 0;
        }

        /// <summary>
        /// 批量删除分组对应用户
        /// </summary>
        /// <returns></returns>
        public int BatchDeleteBaseGroupUser(BatchBaseGroupUserDto parm)
        {
            return Delete(it => it.GroupId == parm.GroupId && parm.EmpCodes.Contains(it.EmpCode)) ? 1 : 0;
        }

        /// <summary>
        /// 获取未在分组中的用户
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EmployeeDto> GetExcludedUsersByGroupId(BaseGroupUserQueryDto parm)
        {
            var query = Context.Queryable<Employee>()
            .Where(it => it.DelFlag == 0)
            .Where(it => SqlFunc.Subqueryable<BaseGroupUser>().Where(s => s.EmpCode == it.EmpCode && s.GroupId == parm.GroupId).NotAny())
            .WhereIF(parm.EmpCode.IsNotEmpty(), it => it.EmpCode.Contains(parm.EmpCode))
            .WhereIF(parm.EmpName.IsNotEmpty(), it => it.EmpName.Contains(parm.EmpName));
            return query.ToPage<Employee, EmployeeDto>(parm);
        }
    }
}