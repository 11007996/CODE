using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Call.ICallService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Call
{
    /// <summary>
    /// 广播区域Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ICallAreaService), ServiceLifetime = LifeTime.Transient)]
    public class CallAreaService : BaseService<CallArea>, ICallAreaService
    {
        public CallAreaService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询广播区域列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<CallAreaDto> GetList(CallAreaQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.CallAreaLineNav) //填充子对象
                .Where(predicate.ToExpression())
                .ToPage<CallArea, CallAreaDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public CallArea GetInfo(int AreaId)
        {
            var response = Queryable()
                //.Includes(x => x.CallAreaLineNav) //填充子对象
                .Where(x => x.AreaId == AreaId)
                .First();

            List<CallAreaLine> areaLines = Context.Queryable<CallAreaLine>()
                .LeftJoin<Line>((a, l) => a.LineId == l.LineId)
                .Where((a, l) => a.AreaId == AreaId)
                .Select((a, l) => new CallAreaLine()
                {
                    LineName = l.LineName
                }, true)
                .ToList();

            response.CallAreaLineNav = areaLines;

            return response;
        }

        /// <summary>
        /// 添加广播区域
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CallArea AddCallArea(CallArea model)
        {
            //检查区域名称
            var area = Context.Queryable<CallArea>().Where(it => it.AreaName == model.AreaName).First();
            if (area != null)
                throw new CustomException("区域名称已存在");

            //检查是否有重复的产线
            if (model.CallAreaLineNav != null && model.CallAreaLineNav.Count > 0)
            {
                if (model.CallAreaLineNav.GroupBy(it => it.LineId).Any(g => g.Count() > 1))
                    throw new CustomException("产线存在相同数据");
            }

            return Context.InsertNav(model).Include(s1 => s1.CallAreaLineNav).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改广播区域
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCallArea(CallArea model)
        {
            //检查区域名称
            var area = Context.Queryable<CallArea>().Where(it => it.AreaName == model.AreaName && it.AreaId != model.AreaId).First();
            if (area != null)
                throw new CustomException("区域名称已存在");

            //检查是否有重复的产线
            if (model.CallAreaLineNav != null && model.CallAreaLineNav.Count > 0)
            {
                if (model.CallAreaLineNav.GroupBy(it => it.LineId).Any(g => g.Count() > 1))
                    throw new CustomException("产线存在相同数据");
            }

            return Context.UpdateNav(model).Include(z1 => z1.CallAreaLineNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 删除广播区域
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool DeleteCallArea(int AreaId)
        {
            DbResult<bool> r = UseTran(() =>
            {
                Context.Deleteable<CallAreaLine>().Where(it => it.AreaId == AreaId).ExecuteCommand();
                Context.Deleteable<CallArea>().Where(it => it.AreaId == AreaId).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 获取区域字典类型数据
        /// </summary>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(CallAreaQueryDto parm)
        {
            var response = Queryable()
                .WhereIF(!string.IsNullOrEmpty(parm.AreaName),it=>it.AreaName.Contains(parm.AreaName))
                .Select(it => new DictDataDto
                {
                    DictValue = it.AreaId.ToString(),
                    DictLabel = it.AreaName
                })
                .ToPage(parm);
            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<CallArea> QueryExp(CallAreaQueryDto parm)
        {
            var predicate = Expressionable.Create<CallArea>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.AreaName), it => it.AreaName.Contains(parm.AreaName));

            return predicate;
        }
    }
}