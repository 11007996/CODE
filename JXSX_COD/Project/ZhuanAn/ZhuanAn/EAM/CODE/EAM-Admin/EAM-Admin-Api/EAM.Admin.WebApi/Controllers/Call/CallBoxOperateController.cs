using Microsoft.AspNetCore.Mvc;
using EAM.Model.Dto;
using EAM.Model.Call;
using EAM.Service.Call.ICallService;

//创建时间：2026-05-08
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 盒子操作记录
    /// </summary>
    [Verify]
    [Route("call/CallBoxOperate")]
    public class CallBoxOperateController : BaseController
    {
        /// <summary>
        /// 盒子操作记录接口
        /// </summary>
        private readonly ICallBoxOperateService _CallBoxOperateService;

        public CallBoxOperateController(ICallBoxOperateService CallBoxOperateService)
        {
            _CallBoxOperateService = CallBoxOperateService;
        }

        /// <summary>
        /// 查询盒子操作记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "call:box:operate:list")]
        public IActionResult QueryCallBoxOperate([FromQuery] CallBoxOperateQueryDto parm)
        {
            var response = _CallBoxOperateService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询盒子操作记录详情
        /// </summary>
        /// <param name="RecordId"></param>
        /// <returns></returns>
        [HttpGet("{RecordId}")]
        [ActionPermissionFilter(Permission = "call:box:operate:query")]
        public IActionResult GetCallBoxOperate(long RecordId)
        {
            var response = _CallBoxOperateService.GetInfo(RecordId);

            var info = response.Adapt<CallBoxOperateDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加盒子操作记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "call:box:operate:add")]
        [Log(Title = "盒子操作记录", BusinessType = BusinessType.INSERT)]
        public IActionResult AddCallBoxOperate([FromBody] CallBoxOperateDto parm)
        {
            var modal = parm.Adapt<CallBoxOperate>().ToCreate(HttpContext);

            var response = _CallBoxOperateService.AddCallBoxOperate(modal);

            return SUCCESS(response);
        }
    }
}