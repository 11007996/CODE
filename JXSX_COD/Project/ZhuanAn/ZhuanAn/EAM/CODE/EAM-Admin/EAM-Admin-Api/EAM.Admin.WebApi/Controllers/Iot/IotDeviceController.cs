using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品设备表
    /// </summary>
    [Verify]
    [Route("iot/IotDevice")]
    public class IotDeviceController : BaseController
    {
        /// <summary>
        /// 产品设备表接口
        /// </summary>
        private readonly IIotDeviceService _IotDeviceService;

        public IotDeviceController(IIotDeviceService IotDeviceService)
        {
            _IotDeviceService = IotDeviceService;
        }

        /// <summary>
        /// 查询产品设备表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:device:list")]
        public IActionResult QueryIotDevice([FromQuery] IotDeviceQueryDto parm)
        {
            var response = _IotDeviceService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产品设备表详情
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        [HttpGet("{DeviceId}")]
        [ActionPermissionFilter(Permission = "iot:device:query")]
        public IActionResult GetIotDevice(int DeviceId)
        {
            var response = _IotDeviceService.GetInfo(DeviceId);

            var info = response.Adapt<IotDeviceDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产品设备表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:device:add")]
        [Log(Title = "产品设备表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotDevice([FromBody] IotDeviceDto parm)
        {
            var modal = parm.Adapt<IotDevice>().ToCreate(HttpContext);

            var response = _IotDeviceService.AddIotDevice(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产品设备表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:device:edit")]
        [Log(Title = "产品设备表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotDevice([FromBody] IotDeviceDto parm)
        {
            var modal = parm.Adapt<IotDevice>().ToUpdate(HttpContext);
            var response = _IotDeviceService.UpdateIotDevice(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产品设备表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:device:delete")]
        [Log(Title = "产品设备表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotDevice([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotDeviceService.Delete(idArr));
        }

        /// <summary>
        /// 查询产品设备表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictIotDevice([FromQuery] IotDeviceQueryDto parm)
        {
            var response = _IotDeviceService.GetDict(parm);
            return SUCCESS(response);
        }
        #region 设备绑定相关

        /// <summary>
        /// 产品设备绑定
        /// </summary>
        /// <returns></returns>
        [HttpPost("bind")]
        [ActionPermissionFilter(Permission = "iot:device:bind")]
        [Log(Title = "产品设备绑定表", BusinessType = BusinessType.INSERT)]
        public IActionResult BindIotDevice([FromBody] IotDeviceBindDto parm)
        {
            var modal = parm.Adapt<IotDeviceBind>().ToCreate(HttpContext);

            var response = _IotDeviceService.BindIotDevice(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 产品设备解绑
        /// </summary>
        /// <returns></returns>
        [HttpDelete("unbind/{deviceId}")]
        [ActionPermissionFilter(Permission = "iot:device:unbind")]
        [Log(Title = "产品设备绑定表", BusinessType = BusinessType.DELETE)]
        public IActionResult UnBindIotDevice([FromRoute] int deviceId)
        {
            var response = _IotDeviceService.UnBindIotDevice(deviceId);

            return SUCCESS(response);
        }

        #endregion 设备绑定相关
    }
}