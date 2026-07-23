using EAM.Model.Consumable;
using EAM.Model.Dto;
using EAM.Service.Consumable.IConsumableService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-10-16
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 耗品通知配置
    /// </summary>
    [Verify]
    [Route("consumable/ConsumableConfigNotice")]
    public class ConsumableConfigNoticeController : BaseController
    {
        /// <summary>
        /// 耗品通知配置接口
        /// </summary>
        private readonly IConsumableConfigNoticeService _ConsumableConfigNoticeService;

        public ConsumableConfigNoticeController(IConsumableConfigNoticeService ConsumableConfigNoticeService)
        {
            _ConsumableConfigNoticeService = ConsumableConfigNoticeService;
        }

        /// <summary>
        /// 查询耗品通知配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "consumable:config:notice:list")]
        public IActionResult QueryConsumableConfigNotice([FromQuery] ConsumableConfigNoticeQueryDto parm)
        {
            var response = _ConsumableConfigNoticeService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品通知配置详情
        /// </summary>
        /// <param name="NoticeConfigId"></param>
        /// <returns></returns>
        [HttpGet("{NoticeConfigId}")]
        [ActionPermissionFilter(Permission = "consumable:config:notice:query")]
        public IActionResult GetConsumableConfigNotice(int NoticeConfigId)
        {
            var response = _ConsumableConfigNoticeService.GetInfo(NoticeConfigId);

            var info = response.Adapt<ConsumableConfigNoticeDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加耗品通知配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "consumable:config:notice:add")]
        [Log(Title = "耗品通知配置", BusinessType = BusinessType.INSERT)]
        public IActionResult AddConsumableConfigNotice([FromBody] ConsumableConfigNoticeDto parm)
        {
            var modal = parm.Adapt<ConsumableConfigNotice>().ToCreate(HttpContext);

            var response = _ConsumableConfigNoticeService.AddConsumableConfigNotice(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新耗品通知配置
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "consumable:config:notice:edit")]
        [Log(Title = "耗品通知配置", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateConsumableConfigNotice([FromBody] ConsumableConfigNoticeDto parm)
        {
            var modal = parm.Adapt<ConsumableConfigNotice>().ToUpdate(HttpContext);
            var response = _ConsumableConfigNoticeService.UpdateConsumableConfigNotice(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除耗品通知配置
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "consumable:config:notice:delete")]
        [Log(Title = "耗品通知配置", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteConsumableConfigNotice([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_ConsumableConfigNoticeService.Delete(idArr));
        }
    }
}