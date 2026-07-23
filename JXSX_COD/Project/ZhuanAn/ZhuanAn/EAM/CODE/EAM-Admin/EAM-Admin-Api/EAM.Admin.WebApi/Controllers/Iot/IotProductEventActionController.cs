using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-08
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品事件处理动作
    /// </summary>
    [Verify]
    [Route("iot/IotProductEventAction")]
    public class IotProductEventActionController : BaseController
    {
        /// <summary>
        /// 产品事件处理动作接口
        /// </summary>
        private readonly IIotProductEventActionService _IotProductEventActionService;

        public IotProductEventActionController(IIotProductEventActionService IotProductEventActionService)
        {
            _IotProductEventActionService = IotProductEventActionService;
        }

        /// <summary>
        /// 查询产品事件处理动作列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:product:event:action:list")]
        public IActionResult QueryIotProductEventAction([FromQuery] IotProductEventActionQueryDto parm)
        {
            var response = _IotProductEventActionService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产品事件处理动作详情
        /// </summary>
        /// <param name="ActionId"></param>
        /// <returns></returns>
        [HttpGet("{ActionId}")]
        [ActionPermissionFilter(Permission = "iot:product:event:action:query")]
        public IActionResult GetIotProductEventAction(int ActionId)
        {
            var response = _IotProductEventActionService.GetInfo(ActionId);

            var info = response.Adapt<IotProductEventActionDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产品事件处理动作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:product:event:action:add")]
        [Log(Title = "产品事件处理动作", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotProductEventAction([FromBody] IotProductEventActionDto parm)
        {
            var modal = parm.Adapt<IotProductEventAction>().ToCreate(HttpContext);

            var response = _IotProductEventActionService.AddIotProductEventAction(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产品事件处理动作
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:product:event:action:edit")]
        [Log(Title = "产品事件处理动作", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotProductEventAction([FromBody] IotProductEventActionDto parm)
        {
            var modal = parm.Adapt<IotProductEventAction>().ToUpdate(HttpContext);
            var response = _IotProductEventActionService.UpdateIotProductEventAction(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产品事件处理动作
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:product:event:action:delete")]
        [Log(Title = "产品事件处理动作", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotProductEventAction([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotProductEventActionService.Delete(idArr));
        }
    }
}