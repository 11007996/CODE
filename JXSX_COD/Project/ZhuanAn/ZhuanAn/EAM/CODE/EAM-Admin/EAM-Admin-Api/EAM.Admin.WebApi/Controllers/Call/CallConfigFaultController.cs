using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Service.Call.ICallService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-07-30
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 故障配置
    /// </summary>
    [Verify]
    [Route("call/CallConfigFault")]
    public class CallConfigFaultController : BaseController
    {
        /// <summary>
        /// 故障配置接口
        /// </summary>
        private readonly ICallConfigFaultService _CallConfigFaultService;

        public CallConfigFaultController(ICallConfigFaultService CallConfigFaultService)
        {
            _CallConfigFaultService = CallConfigFaultService;
        }

        /// <summary>
        /// 查询故障配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult QueryCallConfigFault([FromQuery] CallConfigFaultQueryDto parm)
        {
            var response = _CallConfigFaultService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询故障配置详情
        /// </summary>
        /// <param name="FaultConfigId"></param>
        /// <returns></returns>
        [HttpGet("{FaultConfigId}")]
        public IActionResult GetCallConfigFault(int FaultConfigId)
        {
            var response = _CallConfigFaultService.GetInfo(FaultConfigId);

            var info = response.Adapt<CallConfigFaultDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加故障配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "call:config:fault:add")]
        [Log(Title = "故障配置", BusinessType = BusinessType.INSERT)]
        public IActionResult AddCallConfigFault([FromBody] CallConfigFaultDto parm)
        {
            var modal = parm.Adapt<CallConfigFault>().ToCreate(HttpContext);

            var response = _CallConfigFaultService.AddCallConfigFault(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新故障配置
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "call:config:fault:edit")]
        [Log(Title = "故障配置", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateCallConfigFault([FromBody] CallConfigFaultDto parm)
        {
            var modal = parm.Adapt<CallConfigFault>().ToUpdate(HttpContext);
            var response = _CallConfigFaultService.UpdateCallConfigFault(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除故障配置
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ActionPermissionFilter(Permission = "call:config:fault:delete")]
        [Log(Title = "故障配置", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteCallConfigFault([FromRoute] int id)
        {
            return SUCCESS(_CallConfigFaultService.DeleteCallConfigFault(id));
        }
    }
}