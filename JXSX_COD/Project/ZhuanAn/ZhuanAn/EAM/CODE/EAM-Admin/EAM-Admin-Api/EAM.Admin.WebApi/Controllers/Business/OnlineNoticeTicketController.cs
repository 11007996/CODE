using EAM.Model.Business;
using EAM.Model.Dto;
using EAM.Service.Business.IBusinessService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-06-27
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 上线通知单
    /// </summary>
    [Verify]
    [Route("business/OnlineNoticeTicket")]
    public class OnlineNoticeTicketController : BaseController
    {
        /// <summary>
        /// 上线通知单接口
        /// </summary>
        private readonly IOnlineNoticeTicketService _OnlineNoticeTicketService;

        public OnlineNoticeTicketController(IOnlineNoticeTicketService OnlineNoticeTicketService)
        {
            _OnlineNoticeTicketService = OnlineNoticeTicketService;
        }

        /// <summary>
        /// 查询上线通知单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:list")]
        public IActionResult QueryOnlineNoticeTicket([FromQuery] OnlineNoticeTicketQueryDto parm)
        {
            var response = _OnlineNoticeTicketService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询上线通知单详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        [HttpGet("{TicketNo}")]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:query")]
        public IActionResult GetOnlineNoticeTicket(string TicketNo)
        {
            var response = _OnlineNoticeTicketService.GetInfo(TicketNo);

            var info = response.Adapt<OnlineNoticeTicketDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加上线通知单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:add")]
        [Log(Title = "上线通知单", BusinessType = BusinessType.INSERT)]
        public IActionResult AddOnlineNoticeTicket([FromBody] OnlineNoticeTicketDto parm)
        {
            var modal = parm.Adapt<OnlineNoticeTicket>().ToCreate(HttpContext);

            var response = _OnlineNoticeTicketService.AddOnlineNoticeTicket(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新上线通知单
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:edit")]
        [Log(Title = "上线通知单", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateOnlineNoticeTicket([FromBody] OnlineNoticeTicketDto parm)
        {
            var modal = parm.Adapt<OnlineNoticeTicket>().ToUpdate(HttpContext);
            var response = _OnlineNoticeTicketService.UpdateOnlineNoticeTicket(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除上线通知单
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:delete")]
        [Log(Title = "上线通知单", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteOnlineNoticeTicket([FromRoute] string id)
        {
            return ToResponse(_OnlineNoticeTicketService.DeleteOnlineNoticeTicket(id));
        }

        /// <summary>
        /// 查询上线通知单_设备概要
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        [HttpGet("{TicketNo}/equipment/summary")]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:query")]
        public IActionResult GetOnlineNoticeTicketEquipmentSummary(string TicketNo)
        {
            var response = _OnlineNoticeTicketService.GetEquipmentSummary(TicketNo);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询上线通知单_治具概要
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        [HttpGet("{TicketNo}/fixture/summary")]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:query")]
        public IActionResult GetOnlineNoticeTicketFixtureSummary(string TicketNo)
        {
            var response = _OnlineNoticeTicketService.GetFixtureSummary(TicketNo);
            return SUCCESS(response);
        }
    }
}