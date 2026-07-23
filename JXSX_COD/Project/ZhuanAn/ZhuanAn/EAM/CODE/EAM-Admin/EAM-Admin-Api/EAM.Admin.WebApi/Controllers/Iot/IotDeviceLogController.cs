using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备日志
    /// </summary>
    [Verify]
    [Route("iot/IotDeviceLog")]
    public class IotDeviceLogController : BaseController
    {
        /// <summary>
        /// 设备日志接口
        /// </summary>
        private readonly IIotDeviceLogService _IotDeviceLogService;

        public IotDeviceLogController(IIotDeviceLogService IotDeviceLogService)
        {
            _IotDeviceLogService = IotDeviceLogService;
        }

        /// <summary>
        /// 查询设备日志列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:device:log:list")]
        public IActionResult QueryIotDeviceLog([FromQuery] IotDeviceLogQueryDto parm)
        {
            var response = _IotDeviceLogService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备日志详情
        /// </summary>
        /// <param name="LogId"></param>
        /// <returns></returns>
        [HttpGet("{LogId}")]
        [ActionPermissionFilter(Permission = "iot:device:log:query")]
        public IActionResult GetIotDeviceLog(long LogId)
        {
            var response = _IotDeviceLogService.GetInfo(LogId);

            var info = response.Adapt<IotDeviceLogDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备日志
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:device:log:add")]
        [Log(Title = "设备日志", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotDeviceLog([FromBody] IotDeviceLogDto parm)
        {
            var modal = parm.Adapt<IotDeviceLog>().ToCreate(HttpContext);

            var response = _IotDeviceLogService.AddIotDeviceLog(modal);

            return SUCCESS(response);
        }
    }
}