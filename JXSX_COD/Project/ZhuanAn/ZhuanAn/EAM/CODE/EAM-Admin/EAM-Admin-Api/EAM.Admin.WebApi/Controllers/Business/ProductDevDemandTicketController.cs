using EAM.Model.Business;
using EAM.Model.Dto;
using EAM.Service.Business.IBusinessService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-07-16
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品开发需求单
    /// </summary>
    [Verify]
    [Route("business/ProductDevDemandTicket")]
    public class ProductDevDemandTicketController : BaseController
    {
        /// <summary>
        /// 产品开发需求单接口
        /// </summary>
        private readonly IProductDevDemandTicketService _ProductDevDemandTicketService;

        public ProductDevDemandTicketController(IProductDevDemandTicketService ProductDevDemandTicketService)
        {
            _ProductDevDemandTicketService = ProductDevDemandTicketService;
        }

        /// <summary>
        /// 查询产品开发需求单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "productDevDemandTicket:list")]
        public IActionResult QueryProductDevDemandTicket([FromQuery] ProductDevDemandTicketQueryDto parm)
        {
            var response = _ProductDevDemandTicketService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产品开发需求单详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        [HttpGet("{TicketNo}")]
        [ActionPermissionFilter(Permission = "productDevDemandTicket:query")]
        public IActionResult GetProductDevDemandTicket(string TicketNo)
        {
            var response = _ProductDevDemandTicketService.GetInfo(TicketNo);

            var info = response.Adapt<ProductDevDemandTicketDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产品开发需求单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "productDevDemandTicket:add")]
        [Log(Title = "产品开发需求单", BusinessType = BusinessType.INSERT)]
        public IActionResult AddProductDevDemandTicket([FromBody] ProductDevDemandTicketDto parm)
        {
            var modal = parm.Adapt<ProductDevDemandTicket>().ToCreate(HttpContext);

            var response = _ProductDevDemandTicketService.AddProductDevDemandTicket(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产品开发需求单
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "productDevDemandTicket:edit")]
        [Log(Title = "产品开发需求单", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateProductDevDemandTicket([FromBody] ProductDevDemandTicketDto parm)
        {
            var modal = parm.Adapt<ProductDevDemandTicket>().ToUpdate(HttpContext);
            var response = _ProductDevDemandTicketService.UpdateProductDevDemandTicket(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产品开发需求单
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ticketNo}")]
        [ActionPermissionFilter(Permission = "productDevDemandTicket:delete")]
        [Log(Title = "产品开发需求单", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteProductDevDemandTicket([FromRoute] string ticketNo)
        {
            return ToResponse(_ProductDevDemandTicketService.DeleteProductDevDemandTicket(ticketNo));
        }

        /// <summary>
        /// 同步产品开发需求单
        /// </summary>
        /// <returns></returns>
        [HttpPut("{ticketNo}/async")]
        [ActionPermissionFilter(Permission = "productDevDemandTicket:edit")]
        [Log(Title = "产品开发需求单", BusinessType = BusinessType.UPDATE)]
        public IActionResult AsyncProductDevDemandTicket([FromRoute] string ticketNo)
        {
            var response = _ProductDevDemandTicketService.AsyncProductDevDemandTicket(ticketNo);

            return ToResponse(response);
        }
    }
}