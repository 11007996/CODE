using EAM.Model.Business;
using EAM.Model.Dto;
using EAM.Service.Business.IBusinessService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-07-05
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 耗品领用单
    /// </summary>
    [Verify]
    [Route("business/ConsumableReceiveTicket")]
    public class ConsumableReceiveTicketController : BaseController
    {
        /// <summary>
        /// 耗品领用单接口
        /// </summary>
        private readonly IConsumableReceiveTicketService _ConsumableReceiveTicketService;

        public ConsumableReceiveTicketController(IConsumableReceiveTicketService ConsumableReceiveTicketService)
        {
            _ConsumableReceiveTicketService = ConsumableReceiveTicketService;
        }

        /// <summary>
        /// 查询耗品领用单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "consumableReceiveTicket:list")]
        public IActionResult QueryConsumableReceiveTicket([FromQuery] ConsumableReceiveTicketQueryDto parm)
        {
            var response = _ConsumableReceiveTicketService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品领用单详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        [HttpGet("{TicketNo}")]
        [ActionPermissionFilter(Permission = "consumableReceiveTicket:query")]
        public IActionResult GetConsumableReceiveTicket(string TicketNo)
        {
            var response = _ConsumableReceiveTicketService.GetInfo(TicketNo);

            var info = response.Adapt<ConsumableReceiveTicketDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加耗品领用单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "consumableReceiveTicket:add")]
        [Log(Title = "耗品领用单", BusinessType = BusinessType.INSERT)]
        public IActionResult AddConsumableReceiveTicket([FromBody] ConsumableReceiveTicketDto parm)
        {
            var modal = parm.Adapt<ConsumableReceiveTicket>().ToCreate(HttpContext);

            var response = _ConsumableReceiveTicketService.AddConsumableReceiveTicket(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新耗品领用单
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "consumableReceiveTicket:edit")]
        [Log(Title = "耗品领用单", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateConsumableReceiveTicket([FromBody] ConsumableReceiveTicketDto parm)
        {
            var modal = parm.Adapt<ConsumableReceiveTicket>().ToUpdate(HttpContext);
            var response = _ConsumableReceiveTicketService.UpdateConsumableReceiveTicket(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除耗品领用单
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{TicketNo}")]
        [ActionPermissionFilter(Permission = "consumableReceiveTicket:delete")]
        [Log(Title = "耗品领用单", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteConsumableReceiveTicket([FromRoute] string TicketNo)
        {
            return ToResponse(_ConsumableReceiveTicketService.DeletConsumableReceiveTicket(TicketNo));
        }

        /// <summary>
        /// 查询耗品领用_耗品概要
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        [HttpGet("{TicketNo}/summary")]
        [ActionPermissionFilter(Permission = "consumableReceiveTicket:query")]
        public IActionResult GetOnlineNoticeTicketSummary(string TicketNo)
        {
            var response = _ConsumableReceiveTicketService.GetItemSummary(TicketNo);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品领用单_可归还清单
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        [HttpGet("{TicketNo}/receive")]
        [ActionPermissionFilter(Permission = "consumableReceiveTicket:query")]
        public IActionResult GetConsumableReceiveTicketReceive(string TicketNo)
        {
            var response = _ConsumableReceiveTicketService.GetItemReceive(TicketNo);
            return SUCCESS(response);
        }

        /// <summary>
        /// 耗品领用单_领用操作
        /// </summary>
        /// <returns></returns>
        [HttpPost("receive")]
        [ActionPermissionFilter(Permission = "consumable:storage:receive")]
        [Log(Title = "耗品领用单", BusinessType = BusinessType.INSERT)]
        public IActionResult BatchReceiveConsumable([FromBody] BatchReceiveConsumableDto parm)
        {
            var modal = parm.ToCreate(HttpContext);

            var response = _ConsumableReceiveTicketService.BatchReceiveConsumable(modal);

            return SUCCESS(response);
        }
    }
}