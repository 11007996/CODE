using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Service.Call.ICallService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-07-30
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 呼叫通知配置
    /// </summary>
    [Verify]
    [Route("call/CallConfigNotice")]
    public class CallConfigNoticeController : BaseController
    {
        /// <summary>
        /// 呼叫通知配置接口
        /// </summary>
        private readonly ICallConfigNoticeService _CallConfigNoticeService;

        public CallConfigNoticeController(ICallConfigNoticeService CallConfigNoticeService)
        {
            _CallConfigNoticeService = CallConfigNoticeService;
        }

        /// <summary>
        /// 查询呼叫通知配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "call:config:notice:list")]
        public IActionResult QueryCallConfigNotice([FromQuery] CallConfigNoticeQueryDto parm)
        {
            var response = _CallConfigNoticeService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询呼叫通知配置详情
        /// </summary>
        /// <param name="NoticeConfigId"></param>
        /// <returns></returns>
        [HttpGet("{NoticeConfigId}")]
        [ActionPermissionFilter(Permission = "call:config:notice:query")]
        public IActionResult GetCallConfigNotice(int NoticeConfigId)
        {
            var response = _CallConfigNoticeService.GetInfo(NoticeConfigId);

            var info = response.Adapt<CallConfigNoticeDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加呼叫通知配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "call:config:notice:add")]
        [Log(Title = "呼叫通知配置", BusinessType = BusinessType.INSERT)]
        public IActionResult AddCallConfigNotice([FromBody] CallConfigNoticeDto parm)
        {
            var modal = parm.Adapt<CallConfigNotice>().ToCreate(HttpContext);

            var response = _CallConfigNoticeService.AddCallConfigNotice(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新呼叫通知配置
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "call:config:notice:edit")]
        [Log(Title = "呼叫通知配置", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateCallConfigNotice([FromBody] CallConfigNoticeDto parm)
        {
            var modal = parm.Adapt<CallConfigNotice>().ToUpdate(HttpContext);
            var response = _CallConfigNoticeService.UpdateCallConfigNotice(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除呼叫通知配置
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ActionPermissionFilter(Permission = "call:config:notice:delete")]
        [Log(Title = "呼叫通知配置", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteCallConfigNotice([FromRoute] int id)
        {
            return SUCCESS(_CallConfigNoticeService.DeleteCallConfigNotice(id));
        }
    }
}