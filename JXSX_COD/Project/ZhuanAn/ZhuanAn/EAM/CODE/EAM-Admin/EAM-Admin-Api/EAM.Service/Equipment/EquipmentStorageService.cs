using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 设备保管Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IEquipmentStorageService), ServiceLifetime = LifeTime.Transient)]
    public class EquipmentStorageService : BaseService<EquipmentStorageUsing>, IEquipmentStorageService
    {
        public EquipmentStorageService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询设备保管列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentStorageUsingDto> GetList(EquipmentStorageUsingQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .LeftJoin<Employee>((it, e, emp) => it.ReceiverId == emp.EmpCode)
                .LeftJoin<Line>((it, e, emp, l) => it.LineId == l.LineId)
                .Select((it, e, emp, l) => new EquipmentStorageUsingDto
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                    ReceiverName = emp.EmpName,
                    LineName = l.LineName
                }, true)
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public EquipmentStorageUsing GetInfo(int equipmentId)
        {
            var response = Queryable()
                .Where(x => x.EquipmentId == equipmentId)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                   .LeftJoin<Employee>((it, e, emp) => it.ReceiverId == emp.EmpCode)
                .LeftJoin<Line>((it, e, emp, l) => it.LineId == l.LineId)
                .Select((it, e, emp, l) => new EquipmentStorageUsing
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                    ReceiverName = emp.EmpName,
                    LineName = l.LineName
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加设备保管
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EquipmentStorageUsing AddEquipmentStorage(EquipmentStorageUsing model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改设备保管
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateEquipmentStorage(EquipmentStorageUsing model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 领用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ReceiveEquipmentStorage(OperateEquipmentStorageDto model)
        {
            //参数检查
            if (model.EquipmentId == null || model.EquipmentId <= 0)
                throw new CustomException("资产编号不能为空");

            //通过单据领用时，检查单据信息
            if (!string.IsNullOrEmpty(model.TicketNo) && !string.IsNullOrEmpty(model.TicketType))
            {
                switch (model.TicketType)
                {
                    case TicketTypeConstant.上线通知单:
                        OnlineNoticeTicket ont = Context.Queryable<OnlineNoticeTicket>().Where(it => it.TicketNo == model.TicketNo).First();
                        model.LineId = ont.LineId;
                        model.ReceiverId = ont.InitiatorId;
                        break;

                    default:
                        throw new CustomException($"未找到类型[{model.TicketType}]单据[{model.TicketNo}]");
                }
            }
            if (model.LineId == null || model.LineId <= 0)
                throw new CustomException("产线不能为空");
            if (string.IsNullOrEmpty(model.ReceiverId))
                throw new CustomException("领用人不能为空");

            EquipmentBase equipment = Context.Queryable<EquipmentBase>().Where(it => it.EquipmentId == model.EquipmentId).First();
            EquipmentStorageUsing cs = Context.Queryable<EquipmentStorageUsing>().Where(it => it.EquipmentId == model.EquipmentId).First();

            if (equipment == null)
                throw new CustomException($"未找到设备【{model.EquipmentId}】的信息");
            if (equipment.Status != null && equipment.Status == EquipmentStatusConstant.报废)
                throw new CustomException($"当前设备状态【{equipment.Status}】,无法领用");
            if (equipment.Status != null && equipment.Status == EquipmentStatusConstant.占用)
                throw new CustomException($"当前设备状态【{equipment.Status}】,无法领用");
            if (cs != null)
                throw new CustomException($"当前设备在产线【{cs.LineId}】,无法领用");

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新领用记录
                EquipmentStorageUsing es = new()
                {
                    EquipmentId = model.EquipmentId,
                    LineId = model.LineId,
                    ReceiverId = model.ReceiverId,
                    StorageChangeType = StorageChangeTypeConstant.领用,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                Context.Insertable<EquipmentStorageUsing>(es).ExecuteCommand();
                //更新在用设备
                EquipmentStorageRecord esr = new()
                {
                    EquipmentId = model.EquipmentId,
                    LineId = model.LineId,
                    ReceiverId = model.ReceiverId,
                    StorageChangeType = StorageChangeTypeConstant.领用,
                    TicketNo = model.TicketNo,
                    TicketType = model.TicketType,
                    Remark = model.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime
                };
                Context.Insertable<EquipmentStorageRecord>(esr).ExecuteCommand();
                //更新设备状态
                equipment.Status = EquipmentStatusConstant.占用;
                Context.Updateable<EquipmentBase>(equipment).UpdateColumns(it => it.Status).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 批量设备领用
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public int BatchReceiveEquipment(List<OperateEquipmentStorageDto> models)
        {
            if (models.Count <= 0)
                throw new CustomException("未选择设备");

            //开启事务更新
            DbResult<bool> r = UseTran(() =>
            {
                foreach (var item in models)
                {
                    ReceiveEquipmentStorage(item);
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess ? models.Count : 0;
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool BackEquipmentStorage(OperateEquipmentStorageDto model)
        {
            //参数检查
            if (model.EquipmentId == null || model.EquipmentId <= 0)
                throw new CustomException("设备ID不能为空");

            EquipmentBase equipment = Context.Queryable<EquipmentBase>().Where(it => it.EquipmentId == model.EquipmentId).First();
            EquipmentStorageUsing cs = Context.Queryable<EquipmentStorageUsing>().Where(it => it.EquipmentId == model.EquipmentId).First();

            if (equipment == null)
                throw new CustomException($"未找到设备【{model.EquipmentId}】的信息");
            if (equipment.Status != EquipmentStatusConstant.占用)
                throw new CustomException($"当前设备状态【{equipment.Status}】,无法归还");
            if (cs == null)
                throw new CustomException($"未找到领用信息,无法归还");

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新领用记录
                EquipmentStorageRecord esr = new()
                {
                    EquipmentId = model.EquipmentId,
                    StorageChangeType = StorageChangeTypeConstant.归还,
                    TicketNo = cs.TicketNo,
                    TicketType = cs.TicketType,
                    Remark = cs.Remark,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                };
                Context.Insertable<EquipmentStorageRecord>(esr).ExecuteCommand();
                //删除在用设备
                Context.Deleteable<EquipmentStorageUsing>(cs).ExecuteCommand();
                //更新设备状态
                equipment.Status = EquipmentStatusConstant.正常;
                Context.Updateable<EquipmentBase>(equipment).UpdateColumns(it => it.Status).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 查询设备操作记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentStorageRecordDto> GetRecordList(EquipmentStorageRecordQueryDto parm)
        {
            var response = Context.Queryable<EquipmentStorageRecord>()
                .WhereIF(parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId)
                .WhereIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo)
                .WhereIF(parm.BeginCreateTime != null, it => it.CreateTime >= parm.BeginCreateTime)
                .WhereIF(parm.EndCreateTime != null, it => it.CreateTime <= parm.EndCreateTime)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .LeftJoin<Employee>((it, e, emp) => it.ReceiverId == emp.EmpCode)
                .LeftJoin<Line>((it, e, emp, l) => it.LineId == l.LineId)
                .OrderByIF(parm.Sort?.ToLower() == "createtime", it => it.CreateTime, parm.SortType.ToLower().StartsWith("desc") ? OrderByType.Desc : OrderByType.Asc)
                .Select((it, e, emp, l) => new EquipmentStorageRecordDto
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                    LineName = l.LineName,
                    ReceiverName = emp.EmpName
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<EquipmentStorageUsing> QueryExp(EquipmentStorageUsingQueryDto parm)
        {
            var predicate = Expressionable.Create<EquipmentStorageUsing>();

            predicate = predicate.AndIF(parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId);
            predicate = predicate.AndIF(parm.LineId != null && parm.LineId > 0, it => it.LineId == parm.LineId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo);
            return predicate;
        }
    }
}