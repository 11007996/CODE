using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Service.Call.ICallService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-07-30
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 广播区域
    /// </summary>
    [Verify]
    [Route("call/CallArea")]
    public class CallAreaController : BaseController
    {
        /// <summary>
        /// 广播区域接口
        /// </summary>
        private readonly ICallAreaService _CallAreaService;

        public CallAreaController(ICallAreaService CallAreaService)
        {
            _CallAreaService = CallAreaService;
        }

        /// <summary>
        /// 查询广播区域列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "call:area:list")]
        public IActionResult QueryCallArea([FromQuery] CallAreaQueryDto parm)
        {
            var response = _CallAreaService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询广播区域详情
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        [HttpGet("{AreaId}")]
        [ActionPermissionFilter(Permission = "call:area:query")]
        public IActionResult GetCallArea(int AreaId)
        {
            var response = _CallAreaService.GetInfo(AreaId);

            var info = response.Adapt<CallAreaDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加广播区域
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "call:area:add")]
        [Log(Title = "广播区域", BusinessType = BusinessType.INSERT)]
        public IActionResult AddCallArea([FromBody] CallAreaDto parm)
        {
            var modal = parm.Adapt<CallArea>().ToCreate(HttpContext);

            var response = _CallAreaService.AddCallArea(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新广播区域
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "call:area:edit")]
        [Log(Title = "广播区域", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateCallArea([FromBody] CallAreaDto parm)
        {
            var modal = parm.Adapt<CallArea>().ToUpdate(HttpContext);
            var response = _CallAreaService.UpdateCallArea(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除广播区域
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ActionPermissionFilter(Permission = "call:area:delete")]
        [Log(Title = "广播区域", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteCallArea([FromRoute] int id)
        {
            return SUCCESS(_CallAreaService.DeleteCallArea(id));
        }

        /// <summary>
        /// 查询区域字典信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult QueryCallAreaDict([FromQuery] CallAreaQueryDto parm)
        {
            var response = _CallAreaService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}