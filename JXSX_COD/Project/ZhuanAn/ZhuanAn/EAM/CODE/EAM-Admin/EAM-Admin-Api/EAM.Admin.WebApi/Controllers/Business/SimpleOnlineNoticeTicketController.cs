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
    [Route("business/SimpleOnlineNoticeTicket")]
    public class SimpleOnlineNoticeTicketController : BaseController
    {
        /// <summary>
        /// 上线通知单接口
        /// </summary>
        private readonly ISimpleOnlineNoticeTicketService _SimpleOnlineNoticeTicketService;

        public SimpleOnlineNoticeTicketController(ISimpleOnlineNoticeTicketService SimpleOnlineNoticeTicketService)
        {
            _SimpleOnlineNoticeTicketService = SimpleOnlineNoticeTicketService;
        }

        /// <summary>
        /// 查询上线通知单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:list")]
        public IActionResult QueryOnlineNoticeTicket([FromQuery] SimpleOnlineNoticeTicketQueryDto parm)
        {
            var response = _SimpleOnlineNoticeTicketService.GetList(parm);
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
            var response = _SimpleOnlineNoticeTicketService.GetInfo(TicketNo);

            var info = response.Adapt<SimpleOnlineNoticeTicketDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加上线通知单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:add")]
        [Log(Title = "上线通知单", BusinessType = BusinessType.INSERT)]
        public IActionResult AddOnlineNoticeTicket([FromBody] SimpleOnlineNoticeTicketDto parm)
        {
            var modal = parm.Adapt<SimpleOnlineNoticeTicket>().ToCreate(HttpContext);

            var response = _SimpleOnlineNoticeTicketService.AddSimpleOnlineNoticeTicket(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新上线通知单
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:edit")]
        [Log(Title = "上线通知单", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateOnlineNoticeTicket([FromBody] SimpleOnlineNoticeTicketDto parm)
        {
            var modal = parm.Adapt<SimpleOnlineNoticeTicket>().ToUpdate(HttpContext);
            var response = _SimpleOnlineNoticeTicketService.UpdateSimpleOnlineNoticeTicket(modal);

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
            return ToResponse(_SimpleOnlineNoticeTicketService.DeleteSimpleOnlineNoticeTicket(id));
        }

        /// <summary>
        /// 结案上线通知单
        /// </summary>
        /// <returns></returns>
        [HttpPut("close/{id}")]
        [ActionPermissionFilter(Permission = "onlineNoticeTicket:close")]
        [Log(Title = "上线通知单", BusinessType = BusinessType.UPDATE)]
        public IActionResult CloseOnlineNoticeTicket([FromRoute] string id)
        {
            var response = _SimpleOnlineNoticeTicketService.CloseSimpleOnlineNoticeTicket(id);

            return ToResponse(response);
        }

        /// <summary>
        /// 查询上线通知单设备*治具名称字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("item/dict")]
        public IActionResult QueryOnlineNoticeTicketItemDict([FromQuery] SimpleOnlineNoticeTicketDictQueryDto parm)
        {
            var response = _SimpleOnlineNoticeTicketService.QueryOnlineNoticeTicketItemDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询上线通知单料号名称字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("partName/dict")]
        public IActionResult QueryOnlineNoticeTicketPartNameDict([FromQuery] SimpleOnlineNoticeTicketDictQueryDto parm)
        {
            var response = _SimpleOnlineNoticeTicketService.QueryOnlineNoticeTicketPartNameDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询历史上线通知单料号关联的项目名称列表
        /// </summary>
        /// <param name="partName"></param>
        /// <returns></returns>
        [HttpGet("itemsByPart")]
        public IActionResult QueryOnlineNoticeTicketItemsByPart([FromQuery] string partName)
        {
            var response = _SimpleOnlineNoticeTicketService.QueryOnlineNoticeTicketItemsByPart(partName);
            return SUCCESS(response);
        }
    }
}