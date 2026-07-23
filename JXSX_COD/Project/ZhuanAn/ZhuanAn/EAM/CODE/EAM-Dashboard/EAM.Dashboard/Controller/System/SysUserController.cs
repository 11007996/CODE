using Microsoft.AspNetCore.Mvc;
using EAM.ServiceCore.Filters;

namespace EAM.Dashboard.Controller
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Verify]
    [Route("system/user")]
    [ApiExplorerSettings(GroupName = "sys")]
    public class SysUserController : BaseController
    {
        private readonly ISysUserFactoryService UserFactoryService;

        public SysUserController(
            ISysUserFactoryService userFactoryService)
        {
            UserFactoryService = userFactoryService;
        }

        /// <summary>
        /// 获取当前用户可用的厂区
        /// </summary>
        /// <returns></returns>
        [HttpGet("factorys")]
        public IActionResult GetFactoryInfo()
        {
            string username = HttpContextExtension.GetName(this.HttpContext);
            var factorys = UserFactoryService.GetUserFactorysByUserName(username);
            return SUCCESS(factorys);
        }

    }
}
