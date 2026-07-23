using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.Equipment;
using EAM.Model.Fixture;
using EAM.Repository;
using EAM.Service.Business.IBusinessService;
using EAM.Service.Equipment.IEquipmentService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Business
{
    /// <summary>
    /// 上线通知单Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IOnlineNoticeTicketService), ServiceLifetime = LifeTime.Transient)]
    public class OnlineNoticeTicketService : BaseService<OnlineNoticeTicket>, IOnlineNoticeTicketService
    {
        private readonly IEquipmentStorageService _equipmentStorageService;

        public OnlineNoticeTicketService(IHttpContextAccessor httpContextAccessor, IEquipmentStorageService equipmentStorageService) : base(httpContextAccessor)
        {
            _equipmentStorageService = equipmentStorageService;
        }

        /// <summary>
        /// 查询上线通知单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<OnlineNoticeTicketDto> GetList(OnlineNoticeTicketQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.OnlineNoticeTicketItemNav) //填充子对象
                .Where(predicate.ToExpression())
                .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在)
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .OrderByIF(parm.Sort.ToLower() == "createtime", (it, l) => it.CreateTime, parm.SortType.ToLower().StartsWith("desc") ? OrderByType.Desc : OrderByType.Asc)
                .Select((it, l) => new OnlineNoticeTicketDto
                {
                    LineName = l.LineName,
                }, true)
                .ToPageNoSort<OnlineNoticeTicketDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public OnlineNoticeTicket GetInfo(string TicketNo)
        {
            var response = Queryable()
                .Includes(x => x.EquipmentNav) //填充子对象
                                               // .Includes(x=>x.FixtureNav)
                .Where(x => x.TicketNo == TicketNo)
                .Select(x => new OnlineNoticeTicket()
                {
                    EquipmentNav = x.EquipmentNav,
                }, true)
                .First();

            //查询治具信息
            var fixtrueNav = Context.Queryable<OnlineNoticeTicketFixture>()
                .Where(ontf => ontf.TicketNo == TicketNo)
                .Select(ontf => new OnlineNoticeTicketFixture
                {
                    TicketNo = ontf.TicketNo,
                    FixtureName = ontf.FixtureName,
                    NeedQty = ontf.NeedQty,
                })
                .ToList();

            response.FixtureNav = fixtrueNav;

            return response;
        }

        /// <summary>
        /// 添加上线通知单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OnlineNoticeTicket AddOnlineNoticeTicket(OnlineNoticeTicket model)
        {
            model.TicketNo = GetNewId();
            model.DelFlag = (int)DeleteFlagEnum.存在;
            OnlineNoticeTicket ont = null;

            DbResult<bool> r = UseTran(() =>
            {
                ont = Context.InsertNav(model)
                        .Include(s1 => s1.EquipmentNav)
                        .Include(s1 => s1.FixtureNav)
                        .ExecuteReturnEntity();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return ont;
        }

        /// <summary>
        /// 修改上线通知单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateOnlineNoticeTicket(OnlineNoticeTicket model)
        {
            DbResult<bool> r = UseTran(() =>
            {
                Context.UpdateNav(model, new UpdateNavRootOptions()
                {
                    IgnoreColumns = new string[] { "DelFlag", "Status", "Process_Instance_ID" }
                })
                .Include(z1 => z1.EquipmentNav)
                .Include(z1 => z1.FixtureNav)
                .ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess ? 1 : 0;
        }

        /// <summary>
        /// 删除上线通知单
        /// </summary>
        /// <param name="ticketNo"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int DeleteOnlineNoticeTicket(string ticketNo)
        {
            if (string.IsNullOrEmpty(ticketNo))
                throw new CustomException("业务编号不能为空");
            OnlineNoticeTicket entity = Queryable().Where(it => it.TicketNo == ticketNo).First();
            entity.DelFlag = (int)DeleteFlagEnum.删除;
            return Update(entity, true);
        }

        /// <summary>
        /// 上线通知单_设备清单概要
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public OnlineNoticeTicketEquipmentSummaryDto GetEquipmentSummary(string TicketNo)
        {
            OnlineNoticeTicketEquipmentSummaryDto res = new OnlineNoticeTicketEquipmentSummaryDto();
            res.TicketNo = TicketNo;
            res.TicketType = TicketTypeConstant.上线通知单;

            var demandList = Context.Queryable<OnlineNoticeTicketEquipment>()
                .Where(it => it.TicketNo == TicketNo)
                .Select(it => new EquipmentDemandDto(), true)
                .ToList();
            res.DemandList = demandList;

            //领用归还
            var receiveList = Context.Queryable<EquipmentStorageRecord>()
                                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                                .Where((it, e) => it.TicketNo == TicketNo && it.StorageChangeType == StorageChangeTypeConstant.领用)
                                .GroupBy((it, e) => new { it.EquipmentId, e.AssetName })
                                .Select((it, e) => new EquipmentReceiveDto
                                {
                                    EquipmentId = (int)it.EquipmentId,
                                    EquipmentName = e.AssetName,
                                    ReceiveQty = SqlFunc.AggregateCount(it.EquipmentId),
                                    BackQty = SqlFunc.Subqueryable<EquipmentStorageRecord>()
                                                             .Where(o => o.TicketNo == TicketNo && o.EquipmentId == it.EquipmentId && o.StorageChangeType == StorageChangeTypeConstant.归还)
                                                             .Count()
                                })
                                .ToList();
            res.ReceiveList = receiveList;

            //操作记录
            var stroageList = Context.Queryable<EquipmentStorageRecord>().Where(it => it.TicketNo == TicketNo)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .OrderBy(it => it.CreateTime, OrderByType.Desc)
                .Select((it, e) => new EquipmentStorageRecordDto() { }, true)
                .ToList();
            res.StorageRecordList = stroageList;

            return res;
        }

        /// <summary>
        /// 查询上线通知单_治具清单概要
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public OnlineNoticeTicketFixtureSummaryDto GetFixtureSummary(string TicketNo)
        {
            OnlineNoticeTicketFixtureSummaryDto res = new OnlineNoticeTicketFixtureSummaryDto();
            res.TicketNo = TicketNo;
            res.TicketType = TicketTypeConstant.上线通知单;

            //需求数量
            var demandList = Context.Queryable<OnlineNoticeTicketFixture>()
                .Where(it => it.TicketNo == TicketNo)
                .Select(it => new FixtureDemandDto(), true)
                .ToList();
            res.DemandList = demandList;

            //领用占用
            var receiveList = Context.Queryable<FixtureStorageRecord>()
                                .LeftJoin<FixtureBase>((it, f) => it.FixtureId == f.FixtureId)
                                .Where((it, f) => it.TicketNo == TicketNo && it.StorageChangeType == StorageChangeTypeConstant.领用)
                                .GroupBy((it, f) => new { it.FixtureId, f.FixtureName, f.Series })
                                .Select((it, f) => new FixtureReceiveDto
                                {
                                    FixtureId = (int)it.FixtureId,
                                    Series = f.Series,
                                    FixtureName = f.FixtureName,
                                    ReceiveQty = SqlFunc.Abs<int>(SqlFunc.AggregateSum(it.ChangeQty)),
                                    BackQty = SqlFunc.Subqueryable<FixtureStorageRecord>()
                                                             .Where(o => o.TicketNo == TicketNo && o.FixtureId == it.FixtureId && o.StorageChangeType == StorageChangeTypeConstant.归还)
                                                             .Sum(o => o.ChangeQty),
                                    UsingQty = SqlFunc.Subqueryable<FixtureStorageUsing>()
                                                             .Where(o => o.TicketNo == TicketNo && o.FixtureId == it.FixtureId)
                                                             .Sum(o => o.Qty),
                                })
                                .ToList();
            res.ReceiveList = receiveList;

            //操作记录
            var stroageList = Context.Queryable<FixtureStorageRecord>().Where(it => it.TicketNo == TicketNo)
                .LeftJoin<FixtureBase>((it, e) => it.FixtureId == e.FixtureId)
                .OrderBy(it => it.CreateTime, OrderByType.Desc)
                .Select((it, e) => new FixtureStorageRecordDto() { }, true)
                .ToList();
            res.StorageRecordList = stroageList;

            return res;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<OnlineNoticeTicket> QueryExp(OnlineNoticeTicketQueryDto parm)
        {
            var predicate = Expressionable.Create<OnlineNoticeTicket>();

            predicate = predicate.AndIF(parm.LineId > 0, it => it.LineId == parm.LineId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProcessInstanceId), it => it.ProcessInstanceId == parm.ProcessInstanceId);
            return predicate;
        }

        private string GetNewId()
        {
            string code = "ON";
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