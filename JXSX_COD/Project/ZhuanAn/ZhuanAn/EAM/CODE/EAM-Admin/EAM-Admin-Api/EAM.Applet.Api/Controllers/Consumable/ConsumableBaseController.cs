using EAM.Model.Dto;
using EAM.Service.Consumable.IConsumableService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-05-18
namespace EAM.Applet.Api.Controllers
{
    /// <summary>
    /// 耗品表
    /// </summary>
    [Verify]
    [Route("consumable/ConsumableBase")]
    [ApiExplorerSettings(GroupName = "sys")]
    public class ConsumableBaseController : BaseController
    {
        /// <summary>
        /// 耗品表接口
        /// </summary>
        private readonly IConsumableBaseService _ConsumableService;

        public ConsumableBaseController(IConsumableBaseService ConsumableService)
        {
            _ConsumableService = ConsumableService;
        }

        /// <summary>
        /// 查询耗品字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult QueryConsumableBaseDict([FromQuery] ConsumableBaseQueryDto parm)
        {
            var response = _ConsumableService.GetDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品类别字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict/category")]
        public IActionResult QueryConsumableCategoryDict([FromQuery] ConsumableBaseQueryDto parm)
        {
            var response = _ConsumableService.GetCategoryDict(parm);
            return SUCCESS(response);
        }
    }
}