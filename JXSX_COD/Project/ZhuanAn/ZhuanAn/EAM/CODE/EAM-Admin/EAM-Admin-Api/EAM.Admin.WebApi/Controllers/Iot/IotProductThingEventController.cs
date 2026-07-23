using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品物模型事件
    /// </summary>
    [Verify]
    [Route("iot/IotProductThingEvent")]
    public class IotProductThingEventController : BaseController
    {
        /// <summary>
        /// 产品物模型事件接口
        /// </summary>
        private readonly IIotProductThingEventService _IotProductThingEventService;

        public IotProductThingEventController(IIotProductThingEventService IotProductThingEventService)
        {
            _IotProductThingEventService = IotProductThingEventService;
        }

        /// <summary>
        /// 查询产品物模型事件列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:product:thing:event:list")]
        public IActionResult QueryIotProductThingEvent([FromQuery] IotProductThingEventQueryDto parm)
        {
            var response = _IotProductThingEventService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产品物模型事件详情
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        [HttpGet("{EventId}")]
        [ActionPermissionFilter(Permission = "iot:product:thing:event:query")]
        public IActionResult GetIotProductThingEvent(int EventId)
        {
            var response = _IotProductThingEventService.GetInfo(EventId);

            var info = response.Adapt<IotProductThingEventDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产品物模型事件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:product:thing:event:add")]
        [Log(Title = "产品物模型事件", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotProductThingEvent([FromBody] IotProductThingEventDto parm)
        {
            var modal = parm.Adapt<IotProductThingEvent>().ToCreate(HttpContext);

            var response = _IotProductThingEventService.AddIotProductThingEvent(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产品物模型事件
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:product:thing:event:edit")]
        [Log(Title = "产品物模型事件", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotProductThingEvent([FromBody] IotProductThingEventDto parm)
        {
            var modal = parm.Adapt<IotProductThingEvent>().ToUpdate(HttpContext);
            var response = _IotProductThingEventService.UpdateIotProductThingEvent(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产品物模型事件
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:product:thing:event:delete")]
        [Log(Title = "产品物模型事件", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotProductThingEvent([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotProductThingEventService.Delete(idArr));
        }
    }
}