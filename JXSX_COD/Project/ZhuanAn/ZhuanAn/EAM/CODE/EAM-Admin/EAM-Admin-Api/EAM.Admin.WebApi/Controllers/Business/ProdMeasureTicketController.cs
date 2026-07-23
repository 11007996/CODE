using EAM.Model.Business;
using EAM.Model.Dto;
using EAM.Service.Business.IBusinessService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-07-17
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品测量报告
    /// </summary>
    [Verify]
    [Route("business/ProdMeasureTicket")]
    public class ProdMeasureTicketController : BaseController
    {
        /// <summary>
        /// 产品测量报告接口
        /// </summary>
        private readonly IProdMeasureTicketService _ProdMeasureTicketService;

        public ProdMeasureTicketController(IProdMeasureTicketService ProdMeasureTicketService)
        {
            _ProdMeasureTicketService = ProdMeasureTicketService;
        }

        /// <summary>
        /// 查询产品测量报告列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "prodMeasureTicket:list")]
        public IActionResult QueryProdMeasureTicket([FromQuery] ProdMeasureTicketQueryDto parm)
        {
            var response = _ProdMeasureTicketService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产品测量报告详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        [HttpGet("{TicketNo}")]
        [ActionPermissionFilter(Permission = "prodMeasureTicket:query")]
        public IActionResult GetProdMeasureTicket(string TicketNo)
        {
            var response = _ProdMeasureTicketService.GetInfo(TicketNo);

            //var info = response.Adapt<ProdMeasureTicketDto>();
            return SUCCESS(response);
        }

        /// <summary>
        /// 添加产品测量报告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "prodMeasureTicket:add")]
        [Log(Title = "产品测量报告", BusinessType = BusinessType.INSERT)]
        public IActionResult AddProdMeasureTicket([FromBody] ProdMeasureTicketDto parm)
        {
            var modal = parm.Adapt<ProdMeasureTicket>().ToCreate(HttpContext);

            var response = _ProdMeasureTicketService.AddProdMeasureTicket(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产品测量报告
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "prodMeasureTicket:edit")]
        [Log(Title = "产品测量报告", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateProdMeasureTicket([FromBody] ProdMeasureTicketDto parm)
        {
            var modal = parm.Adapt<ProdMeasureTicket>().ToUpdate(HttpContext);
            var response = _ProdMeasureTicketService.UpdateProdMeasureTicket(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产品测量报告
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ticketNo}")]
        [ActionPermissionFilter(Permission = "prodMeasureTicket:delete")]
        [Log(Title = "产品测量报告", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteProdMeasureTicket([FromRoute] string ticketNo)
        {
            return ToResponse(_ProdMeasureTicketService.DeleteProdMeasureTicket(ticketNo));
        }
    }
}