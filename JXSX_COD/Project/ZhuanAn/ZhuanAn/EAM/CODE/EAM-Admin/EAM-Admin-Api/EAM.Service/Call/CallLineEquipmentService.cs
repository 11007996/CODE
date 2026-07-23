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
    /// 产线设备Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ICallLineEquipmentService), ServiceLifetime = LifeTime.Transient)]
    public class CallLineEquipmentService : BaseService<CallLineEquipment>, ICallLineEquipmentService
    {
        public CallLineEquipmentService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询产线设备列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<CallLineEquipmentDto> GetList(CallLineEquipmentQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .Where(predicate.ToExpression())
                .Select((it, l) => new CallLineEquipment()
                {
                    LineName = l.LineName,
                }, true)
                .ToPage<CallLineEquipment, CallLineEquipmentDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CallLineEquipment GetInfo(int Id)
        {
            var response = Queryable()
                .LeftJoin<Line>((x, l) => x.LineId == l.LineId)
                .Where(x => x.LineEquipmentId == Id)
                .Select((x, l) => new CallLineEquipment()
                {
                    LineName = l.LineName
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产线设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CallLineEquipment AddCallLineEquipment(CallLineEquipment model)
        {
            //判断产线是否已有此设备类型
            CallLineEquipment lineEquipment = Context.Queryable<CallLineEquipment>()
                .Where(it => it.LineId == model.LineId && it.EquipmentType == model.EquipmentType)
                .WhereIF(!string.IsNullOrEmpty(model.EquipmentNo), it => it.EquipmentNo == model.EquipmentNo)
                .First();
            if (lineEquipment != null)
                throw new CustomException("当前产线已关联了相同的设备,如有复数相同类型设备，请使用设备编号区分");

            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产线设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCallLineEquipment(CallLineEquipment model)
        {
            //判断产线是否已有此设备类型
            CallLineEquipment lineEquipment = Context.Queryable<CallLineEquipment>()
                .Where(it => it.LineId == model.LineId && it.EquipmentType == model.EquipmentType && it.LineEquipmentId != model.LineEquipmentId)
                .WhereIF(!string.IsNullOrEmpty(model.EquipmentNo), it => it.EquipmentNo == model.EquipmentNo)
                .First();
            if (lineEquipment != null)
                throw new CustomException("当前产线已关联了相同的设备,如有复数相同类型设备，请使用设备编号区分");

            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<CallLineEquipment> QueryExp(CallLineEquipmentQueryDto parm)
        {
            var predicate = Expressionable.Create<CallLineEquipment>();

            predicate = predicate.AndIF(parm.LineId > 0, it => it.LineId == parm.LineId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.EquipmentType), it => it.EquipmentType == parm.EquipmentType);

            return predicate;
        }
    }
}