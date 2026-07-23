using EAM.Model.System.Dto;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-05-21
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 厂区用户
    /// </summary>
    [Verify]
    [Route("system/userFactory")]
    public class SysUserFactoryController : BaseController
    {
        /// <summary>
        /// 厂区管理接口
        /// </summary>
        private readonly ISysUserFactoryService _UserFactoryService;

        public SysUserFactoryController(ISysUserFactoryService UserFactoryService)
        {
            _UserFactoryService = UserFactoryService;
        }

        /// <summary>
        /// 根据厂区获取已分配的用户
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "system:factory:user:list")]
        public IActionResult GetFactroyUser([FromQuery] SysUserFactoryQueryDto parm)
        {
            var list = _UserFactoryService.GetSysUserByFactroy(parm);

            return SUCCESS(list, TIME_FORMAT_FULL);
        }

        /// <summary>
        /// 添加厂区用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "system:factory:user:add")]
        [Log(Title = "添加厂区用户", BusinessType = BusinessType.INSERT)]
        public IActionResult AddUser([FromBody] FactoryUsersOperateDto parm)
        {
            var response = _UserFactoryService.AddFactoryUser(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 删除厂区用户
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionPermissionFilter(Permission = "system:factory:user:delete")]
        [Log(Title = "删除厂区用户", BusinessType = BusinessType.DELETE)]
        public IActionResult Delete([FromBody] FactoryUsersOperateDto parm)
        {
            return SUCCESS(_UserFactoryService.DeleteFactoryUserByUserIds(parm.FactoryId, parm.UserIds));
        }

        /// <summary>
        /// 获取厂区未分配用户
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("GetExcludeUsers")]
        public IActionResult GetExcludeUsers([FromQuery] SysUserFactoryQueryDto parm)
        {
            if (string.IsNullOrEmpty(parm.FactoryId))
            {
                throw new CustomException(ResultCode.PARAM_ERROR, "factory不能为空");
            }

            // 获取未添加用户
            var list = _UserFactoryService.GetExcludedSysUsersByFactory(parm);

            return SUCCESS(list, TIME_FORMAT_FULL);
        }
    }
}