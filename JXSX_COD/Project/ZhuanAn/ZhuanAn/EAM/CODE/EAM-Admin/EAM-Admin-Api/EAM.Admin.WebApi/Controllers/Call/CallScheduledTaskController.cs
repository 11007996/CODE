using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Service.Call.ICallService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-05-15
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 广播定时任务
    /// </summary>
    [Verify]
    [Route("call/CallScheduledTask")]
    public class CallScheduledTaskController : BaseController
    {
        /// <summary>
        /// 广播定时任务接口
        /// </summary>
        private readonly ICallScheduledTaskService _CallScheduledTaskService;

        public CallScheduledTaskController(ICallScheduledTaskService CallScheduledTaskService)
        {
            _CallScheduledTaskService = CallScheduledTaskService;
        }

        /// <summary>
        /// 查询广播定时任务列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "call:scheduled:task:list")]
        public IActionResult QueryCallScheduledTask([FromQuery] CallScheduledTaskQueryDto parm)
        {
            var response = _CallScheduledTaskService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询广播定时任务详情
        /// </summary>
        /// <param name="CallTaskId"></param>
        /// <returns></returns>
        [HttpGet("{CallTaskId}")]
        [ActionPermissionFilter(Permission = "call:scheduled:task:query")]
        public IActionResult GetCallScheduledTask(int CallTaskId)
        {
            var response = _CallScheduledTaskService.GetInfo(CallTaskId);

            var info = response.Adapt<CallScheduledTaskDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加广播定时任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "call:scheduled:task:add")]
        [Log(Title = "广播定时任务", BusinessType = BusinessType.INSERT)]
        public IActionResult AddCallScheduledTask([FromBody] CallScheduledTaskDto parm)
        {
            var modal = parm.Adapt<CallScheduledTask>().ToCreate(HttpContext);

            var response = _CallScheduledTaskService.AddCallScheduledTask(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新广播定时任务
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "call:scheduled:task:edit")]
        [Log(Title = "广播定时任务", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateCallScheduledTask([FromBody] CallScheduledTaskDto parm)
        {
            var modal = parm.Adapt<CallScheduledTask>().ToUpdate(HttpContext);
            var response = _CallScheduledTaskService.UpdateCallScheduledTask(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除广播定时任务
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "call:scheduled:task:delete")]
        [Log(Title = "广播定时任务", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteCallScheduledTask([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_CallScheduledTaskService.Delete(idArr));
        }
    }
}