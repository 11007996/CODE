using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备采集数据
    /// </summary>
    [Verify]
    [Route("iot/IotDeviceData")]
    public class IotDeviceDataController : BaseController
    {
        /// <summary>
        /// 设备采集数据接口
        /// </summary>
        private readonly IIotDeviceDataService _IotDeviceDataService;

        public IotDeviceDataController(IIotDeviceDataService IotDeviceDataService)
        {
            _IotDeviceDataService = IotDeviceDataService;
        }

        /// <summary>
        /// 查询设备采集数据列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:device:data:list")]
        public IActionResult QueryIotDeviceData([FromQuery] IotDeviceDataQueryDto parm)
        {
            var response = _IotDeviceDataService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备采集数据详情
        /// </summary>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        [HttpGet("{DeviceId}")]
        [ActionPermissionFilter(Permission = "iot:device:data:query")]
        public IActionResult GetIotDeviceData(int DeviceId)
        {
            var response = _IotDeviceDataService.GetInfo(DeviceId);

            var info = response.Adapt<IotDeviceDataDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备采集数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:device:data:add")]
        [Log(Title = "设备采集数据", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotDeviceData([FromBody] IotDeviceDataDto parm)
        {
            var modal = parm.Adapt<IotDeviceData>().ToCreate(HttpContext);

            var response = _IotDeviceDataService.AddIotDeviceData(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 删除设备采集数据
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:device:data:delete")]
        [Log(Title = "设备采集数据", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotDeviceData([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotDeviceDataService.Delete(idArr));
        }
    }
}