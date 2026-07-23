using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Service.Call.ICallService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-05-08
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 呼叫盒信息
    /// </summary>
    [Verify]
    [Route("call/CallBoxBase")]
    public class CallBoxBaseController : BaseController
    {
        /// <summary>
        /// 呼叫盒信息接口
        /// </summary>
        private readonly ICallBoxBaseService _CallBoxBaseService;

        public CallBoxBaseController(ICallBoxBaseService CallBoxBaseService)
        {
            _CallBoxBaseService = CallBoxBaseService;
        }

        /// <summary>
        /// 查询呼叫盒信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "call:box:base:list")]
        public IActionResult QueryCallBoxBase([FromQuery] CallBoxBaseQueryDto parm)
        {
            var response = _CallBoxBaseService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询呼叫盒信息详情
        /// </summary>
        /// <param name="BoxId"></param>
        /// <returns></returns>
        [HttpGet("{BoxId}")]
        [ActionPermissionFilter(Permission = "call:box:base:query")]
        public IActionResult GetCallBoxBase(int BoxId)
        {
            var response = _CallBoxBaseService.GetInfo(BoxId);

            var info = response.Adapt<CallBoxBaseDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加呼叫盒信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "call:box:base:add")]
        [Log(Title = "呼叫盒信息", BusinessType = BusinessType.INSERT)]
        public IActionResult AddCallBoxBase([FromBody] CallBoxBaseDto parm)
        {
            var modal = parm.Adapt<CallBoxBase>().ToCreate(HttpContext);

            var response = _CallBoxBaseService.AddCallBoxBase(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新呼叫盒信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "call:box:base:edit")]
        [Log(Title = "呼叫盒信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateCallBoxBase([FromBody] CallBoxBaseDto parm)
        {
            var modal = parm.Adapt<CallBoxBase>().ToUpdate(HttpContext);
            var response = _CallBoxBaseService.UpdateCallBoxBase(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除呼叫盒信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "call:box:base:delete")]
        [Log(Title = "呼叫盒信息", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteCallBoxBase([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_CallBoxBaseService.Delete(idArr));
        }

        /// <summary>
        /// 查询呼叫盒信息字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictCallBoxBase([FromQuery] CallBoxBaseQueryDto parm)
        {
            var response = _CallBoxBaseService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}