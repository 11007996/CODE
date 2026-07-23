using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-02-27
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 传输通道
    /// </summary>
    [Verify]
    [Route("iot/IotCommonChannel")]
    public class IotCommonChannelController : BaseController
    {
        /// <summary>
        /// 传输通道接口
        /// </summary>
        private readonly IIotCommonChannelService _IotCommonChannelService;

        public IotCommonChannelController(IIotCommonChannelService IotCommonChannelService)
        {
            _IotCommonChannelService = IotCommonChannelService;
        }

        /// <summary>
        /// 查询传输通道列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:common:channel:list")]
        public IActionResult QueryIotCommonChannel([FromQuery] IotCommonChannelQueryDto parm)
        {
            var response = _IotCommonChannelService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询传输通道详情
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        [HttpGet("{ChannelId}")]
        [ActionPermissionFilter(Permission = "iot:common:channel:query")]
        public IActionResult GetIotCommonChannel(int ChannelId)
        {
            var response = _IotCommonChannelService.GetInfo(ChannelId);

            var info = response.Adapt<IotCommonChannelDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加传输通道
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:common:channel:add")]
        [Log(Title = "传输通道", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotCommonChannel([FromBody] IotCommonChannelDto parm)
        {
            var modal = parm.Adapt<IotCommonChannel>().ToCreate(HttpContext);

            var response = _IotCommonChannelService.AddIotCommonChannel(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新传输通道
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:common:channel:edit")]
        [Log(Title = "传输通道", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotCommonChannel([FromBody] IotCommonChannelDto parm)
        {
            var modal = parm.Adapt<IotCommonChannel>().ToUpdate(HttpContext);
            var response = _IotCommonChannelService.UpdateIotCommonChannel(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除传输通道
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:common:channel:delete")]
        [Log(Title = "传输通道", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotCommonChannel([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotCommonChannelService.Delete(idArr));
        }

        /// <summary>
        /// 查询传输通道字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictIotCommonChannel([FromQuery] IotCommonChannelQueryDto parm)
        {
            var response = _IotCommonChannelService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}