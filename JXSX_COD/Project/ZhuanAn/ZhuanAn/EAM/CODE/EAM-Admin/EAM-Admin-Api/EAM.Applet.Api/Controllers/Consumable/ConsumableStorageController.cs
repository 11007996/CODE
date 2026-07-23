using EAM.Model.Dto;
using EAM.Service.Consumable.IConsumableService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-05-18
namespace EAM.Applet.Api.Controllers
{
    /// <summary>
    /// 耗品存储表
    /// </summary>
    [Verify]
    [Route("consumable/ConsumableStorage")]
    public class ConsumableStorageController : BaseController
    {
        /// <summary>
        /// 耗品存储表接口
        /// </summary>
        private readonly IConsumableStorageService _ConsumableStorageService;

        public ConsumableStorageController(IConsumableStorageService ConsumableStorageService)
        {
            _ConsumableStorageService = ConsumableStorageService;
        }

        /// <summary>
        /// 查询耗品存储表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "consumable:storage:list")]
        public IActionResult QueryConsumableStorage([FromQuery] ConsumableStorageQueryDto parm)
        {
            var response = _ConsumableStorageService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品存储表详情
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("info")]
        [ActionPermissionFilter(Permission = "consumable:storage:query")]
        public IActionResult GetConsumableStorage([FromQuery] ConsumableStorageInfoDto parm)
        {
            var response = _ConsumableStorageService.GetInfo(parm);

            var info = response.Adapt<ConsumableStorageDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        [HttpPut("in")]
        [ActionPermissionFilter(Permission = "consumable:storage:in")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.INSERT)]
        public IActionResult InConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.InConsumableStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <returns></returns>
        [HttpPut("out")]
        [ActionPermissionFilter(Permission = "consumable:storage:out")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult OutConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.OutConsumableStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 报废
        /// </summary>
        /// <returns></returns>
        [HttpPut("scrapped")]
        [ActionPermissionFilter(Permission = "consumable:storage:scrapped")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult ScrappedConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.ScrappedConsumableStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 领用
        /// </summary>
        /// <returns></returns>
        [HttpPut("receive")]
        [ActionPermissionFilter(Permission = "consumable:storage:receive")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult ReceiveConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.ReceiveConsumableStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <returns></returns>
        [HttpPut("back")]
        [ActionPermissionFilter(Permission = "consumable:storage:back")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult BackConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.BackConsumableStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 转移
        /// </summary>
        /// <returns></returns>
        [HttpPut("transfer")]
        [ActionPermissionFilter(Permission = "consumable:storage:transfer")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult TransferConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.TransferConsumableStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品出入库记录表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("record/list")]
        [ActionPermissionFilter(Permission = "consumable:storage:list")]
        public IActionResult QueryConsumableStorageRecord([FromQuery] ConsumableStorageRecordQueryDto parm)
        {
            parm = parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.GetRecordList(parm);
            return SUCCESS(response);
        }
      
    }
}