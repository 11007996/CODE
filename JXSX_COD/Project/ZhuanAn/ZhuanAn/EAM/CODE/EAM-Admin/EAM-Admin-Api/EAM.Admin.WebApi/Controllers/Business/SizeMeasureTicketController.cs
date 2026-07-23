using EAM.Model.Business;
using EAM.Model.Dto;
using EAM.Service.Business.IBusinessService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-07-17
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 治具尺寸量测验收单
    /// </summary>
    [Verify]
    [Route("business/SizeMeasureTicket")]
    public class SizeMeasureTicketController : BaseController
    {
        /// <summary>
        /// 治具尺寸量测验收单接口
        /// </summary>
        private readonly ISizeMeasureTicketService _SizeMeasureTicketService;

        public SizeMeasureTicketController(ISizeMeasureTicketService SizeMeasureTicketService)
        {
            _SizeMeasureTicketService = SizeMeasureTicketService;
        }

        /// <summary>
        /// 查询治具尺寸量测验收单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "sizeMeasureTicket:list")]
        public IActionResult QuerySizeMeasureTicket([FromQuery] SizeMeasureTicketQueryDto parm)
        {
            var response = _SizeMeasureTicketService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询治具尺寸量测验收单详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        [HttpGet("{TicketNo}")]
        [ActionPermissionFilter(Permission = "sizeMeasureTicket:query")]
        public IActionResult GetSizeMeasureTicket(string TicketNo)
        {
            var response = _SizeMeasureTicketService.GetInfo(TicketNo);

            //var info = response.Adapt<SizeMeasureTicketDto>();
            return SUCCESS(response);
        }

        /// <summary>
        /// 添加治具尺寸量测验收单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "sizeMeasureTicket:add")]
        [Log(Title = "治具尺寸量测验收单", BusinessType = BusinessType.INSERT)]
        public IActionResult AddSizeMeasureTicket([FromBody] SizeMeasureTicketDto parm)
        {
            var modal = parm.Adapt<SizeMeasureTicket>().ToCreate(HttpContext);

            var response = _SizeMeasureTicketService.AddSizeMeasureTicket(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新治具尺寸量测验收单
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "sizeMeasureTicket:edit")]
        [Log(Title = "治具尺寸量测验收单", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateSizeMeasureTicket([FromBody] SizeMeasureTicketDto parm)
        {
            var modal = parm.Adapt<SizeMeasureTicket>().ToUpdate(HttpContext);
            var response = _SizeMeasureTicketService.UpdateSizeMeasureTicket(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除治具尺寸量测验收单
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ticketNo}")]
        [ActionPermissionFilter(Permission = "sizeMeasureTicket:delete")]
        [Log(Title = "治具尺寸量测验收单", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteSizeMeasureTicket([FromRoute] string ticketNo)
        {
            return ToResponse(_SizeMeasureTicketService.DeleteSizeMeasureTicket(ticketNo));
        }

        /// <summary>
        /// 治具尺寸量测验收单_批量入库
        /// </summary>
        /// <returns></returns>
        [HttpPost("inStorage")]
        [ActionPermissionFilter(Permission = "fixture:storage:in")]
        [Log(Title = "治具尺寸量测验收单_批量入库", BusinessType = BusinessType.INSERT)]
        public IActionResult SizeMeasureTicketInStorage([FromBody] SizeMeasureTicketInStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);

            var response = _SizeMeasureTicketService.SizeMeasureTicketInStorage(modal);

            return SUCCESS(response);
        }
    }
}