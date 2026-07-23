using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-02-27
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备配置
    /// </summary>
    [Verify]
    [Route("iot/IotDeviceConfig")]
    public class IotDeviceConfigController : BaseController
    {
        /// <summary>
        /// 设备配置接口
        /// </summary>
        private readonly IIotDeviceConfigService _IotDeviceConfigService;

        public IotDeviceConfigController(IIotDeviceConfigService IotDeviceConfigService)
        {
            _IotDeviceConfigService = IotDeviceConfigService;
        }

        /// <summary>
        /// 查询设备配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:device:config:list")]
        public IActionResult QueryIotDeviceConfig([FromQuery] IotDeviceConfigQueryDto parm)
        {
            var response = _IotDeviceConfigService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备配置详情
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        [HttpGet("{DeviceId}")]
        [ActionPermissionFilter(Permission = "iot:device:config:query")]
        public IActionResult GetIotDeviceConfig(int DeviceId)
        {
            var response = _IotDeviceConfigService.GetInfo(DeviceId);

            var info = response.Adapt<IotDeviceConfigDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:device:config:add")]
        [Log(Title = "设备配置", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotDeviceConfig([FromBody] IotDeviceConfigDto parm)
        {
            var modal = parm.Adapt<IotDeviceConfig>().ToCreate(HttpContext);

            var response = _IotDeviceConfigService.AddIotDeviceConfig(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新设备配置
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:device:config:edit")]
        [Log(Title = "设备配置", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotDeviceConfig([FromBody] IotDeviceConfigDto parm)
        {
            var modal = parm.Adapt<IotDeviceConfig>().ToUpdate(HttpContext);
            var response = _IotDeviceConfigService.UpdateIotDeviceConfig(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除设备配置
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:device:config:delete")]
        [Log(Title = "设备配置", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotDeviceConfig([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotDeviceConfigService.Delete(idArr));
        }
    }
}