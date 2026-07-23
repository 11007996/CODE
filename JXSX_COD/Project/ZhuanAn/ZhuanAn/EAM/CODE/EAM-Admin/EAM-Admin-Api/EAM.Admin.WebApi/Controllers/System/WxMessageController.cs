using EAM.Model.Dto;
using EAM.Model.System;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-08-05
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 企业微信发送记录表
    /// </summary>
    [Verify]
    [Route("system/WxMessage")]
    public class WxMessageController : BaseController
    {
        /// <summary>
        /// 企业微信发送记录表接口
        /// </summary>
        private readonly IWxMessageService _WxMessageService;

        public WxMessageController(IWxMessageService WxMessageService)
        {
            _WxMessageService = WxMessageService;
        }

        /// <summary>
        /// 查询企业微信发送记录表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "wxmessage:list")]
        public IActionResult QueryWxMessage([FromQuery] WxMessageQueryDto parm)
        {
            var response = _WxMessageService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询企业微信发送记录表详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "wxmessage:query")]
        public IActionResult GetWxMessage(int Id)
        {
            var response = _WxMessageService.GetInfo(Id);

            var info = response.Adapt<WxMessageDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加企业微信发送记录表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "wxmessage:add")]
        [Log(Title = "企业微信发送记录表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddWxMessage([FromBody] WxMessageDto parm)
        {
            var modal = parm.Adapt<WxMessage>().ToCreate(HttpContext);

            var response = _WxMessageService.AddWxMessage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新企业微信发送记录表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "wxmessage:edit")]
        [Log(Title = "企业微信发送记录表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateWxMessage([FromBody] WxMessageDto parm)
        {
            var modal = parm.Adapt<WxMessage>().ToUpdate(HttpContext);
            var response = _WxMessageService.UpdateWxMessage(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除企业微信发送记录表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "wxmessage:delete")]
        [Log(Title = "企业微信发送记录表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteWxMessage([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_WxMessageService.Delete(idArr));
        }
    }
}