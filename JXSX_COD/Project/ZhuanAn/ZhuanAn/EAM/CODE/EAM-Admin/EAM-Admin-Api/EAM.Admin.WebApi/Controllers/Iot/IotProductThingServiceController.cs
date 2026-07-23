using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品物模型服务
    /// </summary>
    [Verify]
    [Route("iot/IotProductThingService")]
    public class IotProductThingServiceController : BaseController
    {
        /// <summary>
        /// 产品物模型服务接口
        /// </summary>
        private readonly IIotProductThingServiceService _IotProductThingServiceService;

        public IotProductThingServiceController(IIotProductThingServiceService IotProductThingServiceService)
        {
            _IotProductThingServiceService = IotProductThingServiceService;
        }

        /// <summary>
        /// 查询产品物模型服务列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:product:thing:service:list")]
        public IActionResult QueryIotProductThingService([FromQuery] IotProductThingServiceQueryDto parm)
        {
            var response = _IotProductThingServiceService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产品物模型服务详情
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <returns></returns>
        [HttpGet("{ServiceId}")]
        [ActionPermissionFilter(Permission = "iot:product:thing:service:query")]
        public IActionResult GetIotProductThingService(int ServiceId)
        {
            var response = _IotProductThingServiceService.GetInfo(ServiceId);

            var info = response.Adapt<IotProductThingServiceDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产品物模型服务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:product:thing:service:add")]
        [Log(Title = "产品物模型服务", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotProductThingService([FromBody] IotProductThingServiceDto parm)
        {
            var modal = parm.Adapt<IotProductThingService>().ToCreate(HttpContext);

            var response = _IotProductThingServiceService.AddIotProductThingService(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产品物模型服务
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:product:thing:service:edit")]
        [Log(Title = "产品物模型服务", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotProductThingService([FromBody] IotProductThingServiceDto parm)
        {
            var modal = parm.Adapt<IotProductThingService>().ToUpdate(HttpContext);
            var response = _IotProductThingServiceService.UpdateIotProductThingService(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产品物模型服务
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:product:thing:service:delete")]
        [Log(Title = "产品物模型服务", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotProductThingService([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotProductThingServiceService.Delete(idArr));
        }
    }
}