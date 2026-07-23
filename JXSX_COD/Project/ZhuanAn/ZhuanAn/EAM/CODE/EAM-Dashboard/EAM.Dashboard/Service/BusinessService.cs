using EAM.Dashboard.Service.IService;
using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.ServiceCore;

namespace EAM.Dashboard.Service
{
    /// <summary>
    /// 故障记录Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IBusinessService), ServiceLifetime = LifeTime.Transient)]
    public class BusinessService : BaseService<SimpleOnlineNoticeTicket>, IBusinessService
    {
        public BusinessService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 获取未处理上线通知单(简)
        /// </summary>
        /// <returns></returns>
        public List<SimpleOnlineNoticeTicketDto> GetSimpleOnlineNoticeTicketList()
        {
            return Context.Queryable<SimpleOnlineNoticeTicket>()
                .Where(it => it.Status == BusinessStatusConstant.待处理 && it.DelFlag == (int)DeleteFlagEnum.存在)
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .OrderBy((it, l) => it.NeedTime, SqlSugar.OrderByType.Asc)
                .Select((it, l) => new SimpleOnlineNoticeTicketDto()
                {
                    LineName = l.LineName,
                }, true)
                .ToList();
        }

        /// <summary>
        /// 获取上线通知单(简)的详细信息
        /// </summary>
        /// <returns></returns>
        public SimpleOnlineNoticeTicketDto GetSimpleOnlineNoticeTicketInfo(string ticketNo)
        {
            var response = Context.Queryable<SimpleOnlineNoticeTicket>()
                .Where(it => it.Status == BusinessStatusConstant.待处理 && it.DelFlag == (int)DeleteFlagEnum.存在 && it.TicketNo == ticketNo)
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .Select((it, l) => new SimpleOnlineNoticeTicketDto()
                {
                    LineName = l.LineName,
                }, true)
                .First();

            if (response != null)
            {
                response.ItemNav = Context.Queryable<SimpleOnlineNoticeTicketItem>()
                    .Where(it => it.TicketNo == ticketNo)
                    .Select(it => new SimpleOnlineNoticeTicketItemDto() { }, true)
                    .ToList();
            }

            return response;
        }
    }
}