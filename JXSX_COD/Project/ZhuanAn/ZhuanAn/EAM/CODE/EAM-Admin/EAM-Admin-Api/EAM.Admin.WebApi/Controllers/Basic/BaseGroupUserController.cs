using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Service.Basic.IBasicService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-02-08
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 分组用户
    /// </summary>
    [Verify]
    [Route("basic/BaseGroupUser")]
    public class BaseGroupUserController : BaseController
    {
        /// <summary>
        /// 分组用户接口
        /// </summary>
        private readonly IBaseGroupUserService _BaseGroupUserService;

        public BaseGroupUserController(IBaseGroupUserService BaseGroupUserService)
        {
            _BaseGroupUserService = BaseGroupUserService;
        }

        /// <summary>
        /// 查询分组用户列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "group:user:list")]
        public IActionResult QueryBaseGroupUser([FromQuery] BaseGroupUserQueryDto parm)
        {
            var response = _BaseGroupUserService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询分组用户详情
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpGet("{GroupId}")]
        [ActionPermissionFilter(Permission = "group:user:query")]
        public IActionResult GetBaseGroupUser(int GroupId)
        {
            var response = _BaseGroupUserService.GetInfo(GroupId);

            var info = response.Adapt<BaseGroupUserDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加分组用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "group:user:add")]
        [Log(Title = "分组用户", BusinessType = BusinessType.INSERT)]
        public IActionResult AddBaseGroupUser([FromBody] BaseGroupUserDto parm)
        {
            var modal = parm.Adapt<BaseGroupUser>().ToCreate(HttpContext);

            var response = _BaseGroupUserService.AddBaseGroupUser(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 批量添加分组用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("batch")]
        [ActionPermissionFilter(Permission = "group:user:add")]
        [Log(Title = "分组用户", BusinessType = BusinessType.INSERT)]
        public IActionResult BatchAddBaseGroupUser([FromBody] BatchBaseGroupUserDto parm)
        {
            var response = _BaseGroupUserService.BatchAddBaseGroupUser(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 删除分组用户
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        [ActionPermissionFilter(Permission = "group:user:delete")]
        [Log(Title = "分组用户", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteBaseGroupUser([FromBody] BatchBaseGroupUserDto parm)
        {
            return ToResponse(_BaseGroupUserService.BatchDeleteBaseGroupUser(parm));
        }

        /// <summary>
        /// 获取分组未分配用户
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("GetExcludeUsers")]
        public IActionResult GetExcludeUsers([FromQuery] BaseGroupUserQueryDto parm)
        {
            if (parm.GroupId <= 0)
            {
                throw new CustomException(ResultCode.PARAM_ERROR, "GroupId不能为空");
            }

            // 获取未添加用户
            var list = _BaseGroupUserService.GetExcludedUsersByGroupId(parm);

            return SUCCESS(list, TIME_FORMAT_FULL);
        }
    }
}