using EAM.Model;
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
    /// 故障配置Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ICallConfigFaultService), ServiceLifetime = LifeTime.Transient)]
    public class CallConfigFaultService : BaseService<CallConfigFault>, ICallConfigFaultService
    {
        public CallConfigFaultService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询故障配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<CallConfigFaultDto> GetList(CallConfigFaultQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.CallConfigFaultSolutionNav) //填充子对象
                .Where(predicate.ToExpression())
                .ToPage<CallConfigFault, CallConfigFaultDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="FaultConfigId"></param>
        /// <returns></returns>
        public CallConfigFault GetInfo(int FaultConfigId)
        {
            var response = Queryable()
                .Includes(x => x.CallConfigFaultSolutionNav) //填充子对象
                .Where(x => x.FaultConfigId == FaultConfigId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加故障配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CallConfigFault AddCallConfigFault(CallConfigFault model)
        {
            //检查设备类型是否重复
            var config = Context.Queryable<CallConfigFault>().Where(it => it.EquipmentType == model.EquipmentType).First();
            if (config != null)
                throw new CustomException("设备类型名称重复");

            //检查是否有重复的产线
            if (model.CallConfigFaultSolutionNav != null && model.CallConfigFaultSolutionNav.Count > 0)
            {
                if (model.CallConfigFaultSolutionNav.GroupBy(it => it.FaultContent).Any(g => g.Count() > 1))
                    throw new CustomException("故障内容存在相同数据");
            }

            return Context.InsertNav(model).Include(s1 => s1.CallConfigFaultSolutionNav).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改故障配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCallConfigFault(CallConfigFault model)
        {
            //检查设备类型是否重复
            var config = Context.Queryable<CallConfigFault>().Where(it => it.EquipmentType == model.EquipmentType && it.FaultConfigId != model.FaultConfigId).First();
            if (config != null)
                throw new CustomException("设备类型名称重复");

            //检查是否有重复的产线
            if (model.CallConfigFaultSolutionNav != null && model.CallConfigFaultSolutionNav.Count > 0)
            {
                if (model.CallConfigFaultSolutionNav.GroupBy(it => it.FaultContent).Any(g => g.Count() > 1))
                    throw new CustomException("故障内容存在相同数据");
            }

            return Context.UpdateNav(model).Include(z1 => z1.CallConfigFaultSolutionNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 删除故障配置
        /// </summary>
        /// <param name="FaultConfigId"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool DeleteCallConfigFault(int FaultConfigId)
        {
            DbResult<bool> r = UseTran(() =>
            {
                Context.Deleteable<CallConfigFaultSolution>().Where(it => it.FaultConfigId == FaultConfigId).ExecuteCommand();
                Context.Deleteable<CallConfigFault>().Where(it => it.FaultConfigId == FaultConfigId).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<CallConfigFault> QueryExp(CallConfigFaultQueryDto parm)
        {
            var predicate = Expressionable.Create<CallConfigFault>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.EquipmentType), it => it.EquipmentType == parm.EquipmentType);

            return predicate;
        }
    }
}