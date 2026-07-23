using EAM.Model.System;
using EAM.Model.System.Dto;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-20
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 微信聊天群
    /// </summary>
    [Verify]
    [Route("system/WxChatGroup")]
    public class WxChatGroupController : BaseController
    {
        /// <summary>
        /// 微信聊天群接口
        /// </summary>
        private readonly IWxChatGroupService _WxChatGroupService;

        public WxChatGroupController(IWxChatGroupService WxChatGroupService)
        {
            _WxChatGroupService = WxChatGroupService;
        }

        /// <summary>
        /// 查询微信聊天群列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "wx:chat:group:list")]
        public IActionResult QueryWxChatGroup([FromQuery] WxChatGroupQueryDto parm)
        {
            var response = _WxChatGroupService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询微信聊天群详情
        /// </summary>
        /// <param name="ChatId"></param>
        /// <returns></returns>
        [HttpGet("{ChatId}")]
        [ActionPermissionFilter(Permission = "wx:chat:group:query")]
        public IActionResult GetWxChatGroup(string ChatId)
        {
            var response = _WxChatGroupService.GetInfo(ChatId);

            var info = response.Adapt<WxChatGroupDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加微信聊天群
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "wx:chat:group:add")]
        [Log(Title = "微信聊天群", BusinessType = BusinessType.INSERT)]
        public IActionResult AddWxChatGroup([FromBody] WxChatGroupDto parm)
        {
            var modal = parm.Adapt<WxChatGroup>().ToCreate(HttpContext);

            var response = _WxChatGroupService.AddWxChatGroup(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新微信聊天群
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "wx:chat:group:edit")]
        [Log(Title = "微信聊天群", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateWxChatGroup([FromBody] WxChatGroupDto parm)
        {
            var modal = parm.Adapt<WxChatGroup>().ToUpdate(HttpContext);
            var response = _WxChatGroupService.UpdateWxChatGroup(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除微信聊天群
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "wx:chat:group:delete")]
        [Log(Title = "微信聊天群", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteWxChatGroup([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<string>(ids);

            return ToResponse(_WxChatGroupService.Delete(idArr));
        }

        /// <summary>
        /// 查询微信聊天群字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictWxChatGroup([FromQuery] WxChatGroupQueryDto parm)
        {
            var response = _WxChatGroupService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}