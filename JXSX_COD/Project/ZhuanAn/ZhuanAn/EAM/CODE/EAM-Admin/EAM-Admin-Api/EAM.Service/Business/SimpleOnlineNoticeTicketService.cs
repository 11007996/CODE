using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Repository;
using EAM.Service.Business.IBusinessService;
using EAM.ServiceCore.Model.Enums;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EAM.Service.Business
{
    /// <summary>
    /// 上线通知单Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ISimpleOnlineNoticeTicketService), ServiceLifetime = LifeTime.Transient)]
    public class SimpleOnlineNoticeTicketService : BaseService<SimpleOnlineNoticeTicket>, ISimpleOnlineNoticeTicketService
    {
        public SimpleOnlineNoticeTicketService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询上线通知单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<SimpleOnlineNoticeTicketDto> GetList(SimpleOnlineNoticeTicketQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.OnlineNoticeTicketItemNav) //填充子对象
                .Where(predicate.ToExpression())
                .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在)
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .OrderByIF(parm.Sort.ToLower() == "createtime", (it, l) => it.CreateTime, parm.SortType.ToLower().StartsWith("desc") ? OrderByType.Desc : OrderByType.Asc)
                .Select((it, l) => new SimpleOnlineNoticeTicketDto
                {
                    LineName = l.LineName,
                }, true)
                .ToPageNoSort<SimpleOnlineNoticeTicketDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public SimpleOnlineNoticeTicket GetInfo(string TicketNo)
        {
            var response = Queryable()
                .Includes(x => x.ItemNav) //填充子对象
                .Where(x => x.TicketNo == TicketNo)
                .Select(x => new SimpleOnlineNoticeTicket()
                {
                    ItemNav = x.ItemNav,
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加上线通知单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SimpleOnlineNoticeTicket AddSimpleOnlineNoticeTicket(SimpleOnlineNoticeTicket model)
        {
            model.TicketNo = GetNewId();
            model.DelFlag = (int)DeleteFlagEnum.存在;
            model.Status = BusinessStatusConstant.待处理;
            SimpleOnlineNoticeTicket ont = null;

            DbResult<bool> r = UseTran(() =>
            {
                ont = Context.InsertNav(model)
                        .Include(s1 => s1.ItemNav)
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
        public int UpdateSimpleOnlineNoticeTicket(SimpleOnlineNoticeTicket model)
        {
            //判断是否可用
            SimpleOnlineNoticeTicket old = GetInfo(model.TicketNo);
            if (old == null || old.Status == BusinessStatusConstant.已结案 || old.DelFlag == (int)DeleteFlagEnum.删除)
            {
                throw new CustomException("业务工单已结案或删除");
            }

            DbResult<bool> r = UseTran(() =>
            {
                Context.UpdateNav(model, new UpdateNavRootOptions()
                {
                    IgnoreColumns = new string[] { "DelFlag", "Status", "Process_Instance_ID" }
                })
                .Include(z1 => z1.ItemNav)
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
        public int DeleteSimpleOnlineNoticeTicket(string ticketNo)
        {
            if (string.IsNullOrEmpty(ticketNo))
                throw new CustomException("业务编号不能为空");
            SimpleOnlineNoticeTicket entity = Queryable().Where(it => it.TicketNo == ticketNo).First();
            entity.DelFlag = (int)DeleteFlagEnum.删除;
            return Update(entity, true);
        }

        /// <summary>
        /// 业务工单结案
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public int CloseSimpleOnlineNoticeTicket(string TicketNo)
        {
            SimpleOnlineNoticeTicket old = GetInfo(TicketNo);
            if (old == null || old.Status == BusinessStatusConstant.已结案 || old.DelFlag == (int)DeleteFlagEnum.删除)
            {
                throw new CustomException("业务工单已结案或删除");
            }

            return Context.Updateable<SimpleOnlineNoticeTicket>().SetColumns(it => it.Status == BusinessStatusConstant.已结案)
                 .Where(it => it.TicketNo == TicketNo)
                 .ExecuteCommand();
        }

        /// <summary>
        /// 项目字典查询
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> QueryOnlineNoticeTicketItemDict(SimpleOnlineNoticeTicketDictQueryDto parm)
        {
            return Context.Queryable<SimpleOnlineNoticeTicketItem>()
                .WhereIF(!string.IsNullOrEmpty(parm.Keyword), it => it.ItemName.Contains(parm.Keyword))
                .GroupBy(it => new { it.ItemName })
                .Select(it => new DictDataDto()
                {
                    DictValue = it.ItemName,
                    DictLabel = it.ItemName
                })
                .ToPage(parm);
        }

        /// <summary>
        /// 料号字典查询
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> QueryOnlineNoticeTicketPartNameDict(SimpleOnlineNoticeTicketDictQueryDto parm)
        {
            return Context.Queryable<SimpleOnlineNoticeTicket>()
                .WhereIF(!string.IsNullOrEmpty(parm.Keyword), it => it.NewPartName.Contains(parm.Keyword))
                .GroupBy(it => new { it.NewPartName })
                .Select(it => new DictDataDto()
                {
                    DictValue = it.NewPartName,
                    DictLabel = it.NewPartName
                })
                .ToPage(parm);
        }

        /// <summary>
        /// 查询历史料号关联的设备治具信息
        /// </summary>
        /// <param name="partName"></param>
        /// <returns></returns>
        public List<SimpleOnlineNoticeTicketItemDto> QueryOnlineNoticeTicketItemsByPart(string partName)
        {
            SimpleOnlineNoticeTicket ticket = Queryable().Where(it => it.NewPartName == partName).OrderBy(it => it.CreateTime, OrderByType.Desc).First();
            List<SimpleOnlineNoticeTicketItemDto> response = new List<SimpleOnlineNoticeTicketItemDto>();

            if (ticket != null)
                response = Context.Queryable<SimpleOnlineNoticeTicketItem>()
                .Where(it => it.TicketNo == ticket.TicketNo)
                .Select(it => new SimpleOnlineNoticeTicketItemDto()
                {
                    ItemName = it.ItemName,
                    NeedQty = it.NeedQty
                })
                .ToList();

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<SimpleOnlineNoticeTicket> QueryExp(SimpleOnlineNoticeTicketQueryDto parm)
        {
            var predicate = Expressionable.Create<SimpleOnlineNoticeTicket>();

            predicate = predicate.AndIF(parm.LineId > 0, it => it.LineId == parm.LineId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.NewPartName), it => it.NewPartName.Contains(parm.NewPartName));
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