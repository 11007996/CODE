using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Call;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Repository;
using EAM.Service.Call.ICallService;
using EAM.ServiceCore.Model.Enums;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Call
{
    /// <summary>
    /// 盒子操作记录Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ICallBoxOperateService), ServiceLifetime = LifeTime.Transient)]
    public class CallBoxOperateService : BaseService<CallBoxOperate>, ICallBoxOperateService
    {
        private readonly ICallFaultBaseService _callFaultBaseService;

        public CallBoxOperateService(IHttpContextAccessor contextAccessor, ICallFaultBaseService callFaultBaseService) : base(contextAccessor)
        {
            _callFaultBaseService = callFaultBaseService;
        }

        /// <summary>
        /// 查询盒子操作记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<CallBoxOperateDto> GetList(CallBoxOperateQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .LeftJoin<CallBoxBase>((it, b) => it.BoxId == b.BoxId)
                .OrderBy((it, b) => it.CreateTime, OrderByType.Desc)
                .Select((it, b) => new CallBoxOperateDto()
                {
                    BoxName = b.BoxName
                }, true)
                .ToPage<CallBoxOperateDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="RecordId"></param>
        /// <returns></returns>
        public CallBoxOperate GetInfo(long RecordId)
        {
            var response = Queryable()
                .Where(x => x.OperateId == RecordId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加盒子操作记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CallBoxOperate AddCallBoxOperate(CallBoxOperate model)
        {
            CallBoxBase box = Context.Queryable<CallBoxBase>().Where(it => it.BoxId == model.BoxId).First();
            if (box == null)
                throw new CustomException("未找到盒子信息");
            if (box.Enabled == false)
                throw new CustomException("当前呼叫盒已禁用");
            CallBoxOperate r = Insertable(model).ExecuteReturnEntity();

            //厂区配置开关：呼叫盒开启广播呼叫
            FactoryConfig fc = Context.Queryable<FactoryConfig>().Where(it => it.ConfigKey == FactoryConfigKeyConstant.呼叫盒广播呼叫开关).First();

            if (fc != null && fc.ConfigValue == SysYesNoConstant.是)
            {//关联到广播呼叫功能
                if (box.LineId == null || box.LineId <= 0)
                    throw new CustomException("盒子未关联产线");
                if (box.StationId == null || box.StationId <= 0)
                    throw new CustomException("盒子未关联工站");

                //根据不同的操作执行不同的处理
                if (model.OperateType != (int)CallBoxOperateTypeEnum.取消呼叫)
                {//操作：呼叫
                    string callTargetType = string.Empty;
                    switch (model.OperateType)
                    {
                        case (int)CallBoxOperateTypeEnum.呼叫:
                            callTargetType = CallTargetTypeConstant.生技;
                            break;

                        case (int)CallBoxOperateTypeEnum.呼叫生技:
                            callTargetType = CallTargetTypeConstant.生技;
                            break;

                        case (int)CallBoxOperateTypeEnum.呼叫品质:
                            callTargetType = CallTargetTypeConstant.品质;
                            break;

                        case (int)CallBoxOperateTypeEnum.呼叫生产:
                            callTargetType = CallTargetTypeConstant.生产;
                            break;

                        case (int)CallBoxOperateTypeEnum.呼叫物料:
                            callTargetType = CallTargetTypeConstant.物料;
                            break;

                        default:
                            throw new CustomException("未知操作类型");
                    }

                    CallFaultBase call = new CallFaultBase()
                    {
                        LineId = box.LineId,
                        StationId = box.StationId,
                        CallReason = "产品异常",
                        CallTargetType = callTargetType,
                        CallPointType = CallPointTypeConstant.工站,
                        BoxId = box.BoxId,
                        CreateBy = model.CreateBy ?? GlobalConstant.System,
                        CreateTime = model.CreateTime ?? DateTime.Now,
                    };
                    _callFaultBaseService.AddCallFaultBase(call);
                }
                else
                { //操作：取消呼叫
                    CallFaultBase call = Context.Queryable<CallFaultBase>()
                        .Where(it => it.BoxId == box.BoxId && it.FaultStatus != CallFaultStatusConstant.已中止 && it.FaultStatus != CallFaultStatusConstant.已完成)
                        .OrderBy(it => it.CreateTime, OrderByType.Desc)
                        .First();

                    if (call != null)
                    {
                        _callFaultBaseService.StopCallFaultBase(new int[] { call.CallId }, model.CreateBy ?? GlobalConstant.System);
                    }
                }
            }

            return r;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<CallBoxOperate> QueryExp(CallBoxOperateQueryDto parm)
        {
            var predicate = Expressionable.Create<CallBoxOperate>();

            predicate.AndIF(parm.BoxId > 0, it => it.BoxId == parm.BoxId);
            predicate.AndIF(parm.BeginCreateTime == null, it => it.CreateTime >= DateTime.Now.ToShortDateString().ParseToDateTime());
            predicate.AndIF(parm.BeginCreateTime != null, it => it.CreateTime >= parm.BeginCreateTime);
            predicate.AndIF(parm.EndCreateTime != null, it => it.CreateTime <= parm.EndCreateTime);

            return predicate;
        }
    }
}