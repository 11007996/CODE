using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Service.Basic.IBasicService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-02-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 基础分组
    /// </summary>
    [Verify]
    [Route("basic/BaseGroup")]
    public class BaseGroupController : BaseController
    {
        /// <summary>
        /// 基础分组接口
        /// </summary>
        private readonly IBaseGroupService _BaseGroupService;

        public BaseGroupController(IBaseGroupService BaseGroupService)
        {
            _BaseGroupService = BaseGroupService;
        }

        /// <summary>
        /// 查询基础分组列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "base:group:list")]
        public IActionResult QueryBaseGroup([FromQuery] BaseGroupQueryDto parm)
        {
            var response = _BaseGroupService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询基础分组详情
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpGet("{GroupId}")]
        [ActionPermissionFilter(Permission = "base:group:query")]
        public IActionResult GetBaseGroup(int GroupId)
        {
            var response = _BaseGroupService.GetInfo(GroupId);

            var info = response.Adapt<BaseGroupDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加基础分组
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "base:group:add")]
        [Log(Title = "基础分组", BusinessType = BusinessType.INSERT)]
        public IActionResult AddBaseGroup([FromBody] BaseGroupDto parm)
        {
            var modal = parm.Adapt<BaseGroup>().ToCreate(HttpContext);

            var response = _BaseGroupService.AddBaseGroup(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新基础分组
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "base:group:edit")]
        [Log(Title = "基础分组", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateBaseGroup([FromBody] BaseGroupDto parm)
        {
            var modal = parm.Adapt<BaseGroup>().ToUpdate(HttpContext);
            var response = _BaseGroupService.UpdateBaseGroup(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除基础分组
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ActionPermissionFilter(Permission = "base:group:delete")]
        [Log(Title = "基础分组", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteBaseGroup([FromRoute] int id)
        {
            return ToResponse(_BaseGroupService.DeleteBaseGroup(id));
        }
    }
}