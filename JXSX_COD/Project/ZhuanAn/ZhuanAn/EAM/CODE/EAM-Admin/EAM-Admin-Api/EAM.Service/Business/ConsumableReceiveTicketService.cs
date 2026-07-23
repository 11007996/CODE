using EAM.Model;
using EAM.Model.Business;
using EAM.Model.Constant;
using EAM.Model.Consumable;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Repository;
using EAM.Service.Business.IBusinessService;
using EAM.Service.Consumable.IConsumableService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Business
{
    /// <summary>
    /// 耗品领用单Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IConsumableReceiveTicketService), ServiceLifetime = LifeTime.Transient)]
    public class ConsumableReceiveTicketService : BaseService<ConsumableReceiveTicket>, IConsumableReceiveTicketService
    {
        private readonly IConsumableStorageService _consumableStorageService;

        public ConsumableReceiveTicketService(IHttpContextAccessor httpContextAccessor, IConsumableStorageService consumableStorageService) : base(httpContextAccessor)
        {
            _consumableStorageService = consumableStorageService;
        }

        /// <summary>
        /// 查询耗品领用单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ConsumableReceiveTicketDto> GetList(ConsumableReceiveTicketQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.ConsumableReceiveTicketItemNav) //填充子对象
                .Where(predicate.ToExpression())
                .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在)
                .ToPage<ConsumableReceiveTicket, ConsumableReceiveTicketDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public ConsumableReceiveTicket GetInfo(string TicketNo)
        {
            var response = Queryable()
                //.Includes(x => x.ConsumableReceiveTicketItemNav) //填充子对象
                .Where(x => x.TicketNo == TicketNo)
                .First();

            List<ConsumableReceiveTicketItem> consumables = Context.Queryable<ConsumableReceiveTicketItem>()
                                                        .Where(it => it.TicketNo == TicketNo)
                                                        .LeftJoin<ConsumableBase>((it, c) => it.ConsumableId == c.ConsumableId)
                                                        .Select((it, c) => new ConsumableReceiveTicketItem()
                                                        {
                                                            ConsumablePart = c.ConsumablePart,
                                                            ConsumableName = c.ConsumableName,
                                                            Spec = c.Spec,
                                                            Price = c.Price,
                                                        }, true)
                                                        .ToList();

            response.ConsumableNav = consumables;

            return response;
        }

        /// <summary>
        /// 添加耗品领用单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ConsumableReceiveTicket AddConsumableReceiveTicket(ConsumableReceiveTicket model)
        {
            model.TicketNo = GetNewId();
            model.DelFlag = (int)DeleteFlagEnum.存在;
            return Context.InsertNav(model).Include(s1 => s1.ConsumableNav).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改耗品领用单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateConsumableReceiveTicket(ConsumableReceiveTicket model)
        {
            return Context.UpdateNav(model, new UpdateNavRootOptions()
            {
                IgnoreColumns = new string[] { "DelFlag", "Status", "Process_Instance_ID" }
            }).Include(z1 => z1.ConsumableNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 删除耗品领用单
        /// </summary>
        /// <param name="ticketNo"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int DeletConsumableReceiveTicket(string ticketNo)
        {
            if (string.IsNullOrEmpty(ticketNo))
                throw new CustomException("业务编号不能为空");
            ConsumableReceiveTicket entity = Queryable().Where(it => it.TicketNo == ticketNo).First();
            entity.DelFlag = (int)DeleteFlagEnum.删除;
            return Update(entity, true);
        }

        /// <summary>
        /// 查询耗品领用单_清单概要
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public List<ConsumableReceiveTicketItemSummaryDto> GetItemSummary(string TicketNo)
        {
            var response = Context.Queryable<ConsumableReceiveTicketItem>()
                .Where(it => it.TicketNo == TicketNo)
                .LeftJoin<ConsumableBase>((it, c) => it.ConsumableId == c.ConsumableId)
                .Select((it, c) => new ConsumableReceiveTicketItemSummaryDto()
                {
                    ConsumableName = c.ConsumableName,
                    ConsumablePart = c.ConsumablePart,
                    Spec = c.Spec,
                    //已领用数量
                    ReceiveQty = -SqlFunc.Subqueryable<ConsumableStorageRecord>()
                                 .Where(o => o.TicketNo == TicketNo && o.ConsumableId == it.ConsumableId && o.StorageChangeType == StorageChangeTypeConstant.领用)
                                 .Sum(o => o.ChangeQty),
                    //已归还数量
                    BackQty = SqlFunc.Subqueryable<ConsumableStorageRecord>()
                                .Where(i => i.TicketNo == TicketNo && i.ConsumableId == it.ConsumableId && i.StorageChangeType == StorageChangeTypeConstant.归还)
                                .Sum(o => o.ChangeQty)
                }, true)
                .ToList();

            return response;
        }

        /// <summary>
        /// 查询耗品领用单_清单
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public List<ConsumableReceiveTicketItemReceiveDto> GetItemReceive(string TicketNo)
        {
            //查询耗品出入明细
            var response = Context.Queryable<ConsumableStorageRecord>()
                .Where(it => it.TicketNo == TicketNo)
                .GroupBy(it => new { it.TicketNo, it.ConsumableId })
                .Select(it => new
                {
                    it.TicketNo,
                    ConsumableId = (int)it.ConsumableId,
                    ReceiveQty = -SqlFunc.AggregateSum(SqlFunc.IF(it.StorageChangeType == StorageChangeTypeConstant.领用).Return(it.ChangeQty).End(0)),
                    BackQty = SqlFunc.AggregateSum(SqlFunc.IF(it.StorageChangeType == StorageChangeTypeConstant.归还).Return(it.ChangeQty).End(0)),
                })
                .LeftJoin<ConsumableBase>((it, c) => it.ConsumableId == c.ConsumableId)
                .Where(it => it.ReceiveQty != it.BackQty)
                .Select((it, c) => new ConsumableReceiveTicketItemReceiveDto()
                {
                    ConsumablePart = c.ConsumablePart,
                    ConsumableName = c.ConsumableName,
                    Spec = c.Spec,
                }, true)
                .ToList();
            return response;
        }

        /// <summary>
        /// 批量领用耗品
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public int BatchReceiveConsumable(BatchReceiveConsumableDto parm)
        {
            //检查参数
            if (string.IsNullOrEmpty(parm.TicketNo))
                throw new CustomException("耗品领用单编号不能为空");
            if (parm.Consumables == null || parm.Consumables.Count <= 0)
                throw new CustomException("未选择耗品");

            //检查领用数量
            List<ConsumableReceiveTicketItemSummaryDto> summary = GetItemSummary(parm.TicketNo);
            List<OperateConsumableStorageDto> receiving = parm.Consumables.GroupBy(it => new { it.ConsumableId }).Select(it => new OperateConsumableStorageDto { ConsumableId = it.Key.ConsumableId, ChangeQty = it.Sum(c => c.ChangeQty) }).ToList();
            ConsumableReceiveTicketItemSummaryDto consumableInfo;
            foreach (var item in receiving)
            {
                //需求信息
                consumableInfo = summary.Where(it => it.ConsumableId == item.ConsumableId).First();
                //检查领用的数量是否超出（当前领用数量>需求数量-已领用数量）
                if (item.ChangeQty > (consumableInfo.NeedQty - consumableInfo.ReceiveQty))
                    throw new CustomException($"领用数量超出。名称【{consumableInfo.ConsumableName}】:需求数量{consumableInfo.NeedQty},已领数量{consumableInfo.ReceiveQty},当前领用数量{item.ChangeQty}");
            }

            //开启事务更新
            DbResult<bool> r = UseTran(() =>
            {
                ConsumableReceiveTicket ont = GetInfo(parm.TicketNo);
                //数据
                foreach (var item in parm.Consumables)
                {
                    item.RelatedUser = ont.InitiatorId;
                    item.LineId = ont.LineId;
                    item.TicketNo = parm.TicketNo;
                    item.TicketType = TicketTypeConstant.耗品领用单;
                    item.CreateBy = parm.CreateBy;
                    item.CreateTime = parm.CreateTime;
                    _consumableStorageService.ReceiveConsumableStorage(item);
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess ? parm.Consumables.Count : 0;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ConsumableReceiveTicket> QueryExp(ConsumableReceiveTicketQueryDto parm)
        {
            var predicate = Expressionable.Create<ConsumableReceiveTicket>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProcessInstanceId), it => it.ProcessInstanceId == parm.ProcessInstanceId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.InitiatorId), it => it.InitiatorId == parm.InitiatorId);
            predicate = predicate.AndIF(parm.LineId != null && parm.LineId > 0, it => it.LineId == parm.LineId);
            return predicate;
        }

        private string GetNewId()
        {
            string code = "CR";
            string newId = $"{code}{DateTime.Now:yyyyMMdd}";
            var max = Queryable().Where(it => it.TicketNo.StartsWith(newId)).Max(it => it.TicketNo);
            if (max == null)
            {
                newId += "0001";
            }
            else
            {
                int num = Convert.ToInt32(max.Replace(newId, ""));
                newId += (num + 1).ToString().PadLeft(4, '0');
            }
            return newId;
        }
    }
}