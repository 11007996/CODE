using EAM.Model.Dto;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-08
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品事件处理动作
    /// </summary>
    [Verify]
    [Route("iot/IotActionInvoke")]
    public class IotActionInvokeController : BaseController
    {
        /// <summary>
        /// 动作调用 接口
        /// </summary>
        private readonly IIotActionInvokeService _IotActionInvokeService;

        public IotActionInvokeController(IIotActionInvokeService IotActionInvokeService)
        {
            _IotActionInvokeService = IotActionInvokeService;
        }

        /// <summary>
        /// 动作调用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[ActionPermissionFilter(Permission = "iot:action:invoke")]
        [Log(Title = "事件动作调用", BusinessType = BusinessType.INSERT)]
        public IActionResult ExeIotActionInvoke([FromBody] IotActionInvokeDto parm)
        {
            var response = _IotActionInvokeService.ExeIotActionInvoke(parm);

            return SUCCESS(response);
        }
    }
}