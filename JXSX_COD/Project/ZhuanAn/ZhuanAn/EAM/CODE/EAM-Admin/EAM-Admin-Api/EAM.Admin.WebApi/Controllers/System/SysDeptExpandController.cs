using EAM.Model.System;
using EAM.Model.System.Dto;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-29
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 系统部门扩展
    /// </summary>
    [Verify]
    [Route("system/SysDeptExpand")]
    public class SysDeptExpandController : BaseController
    {
        /// <summary>
        /// 系统部门扩展接口
        /// </summary>
        private readonly ISysDeptExpandService _SysDeptExpandService;

        public SysDeptExpandController(ISysDeptExpandService SysDeptExpandService)
        {
            _SysDeptExpandService = SysDeptExpandService;
        }

        /// <summary>
        /// 查询系统部门扩展列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "system:dept:expand:list")]
        public IActionResult QuerySysDeptExpand([FromQuery] SysDeptExpandQueryDto parm)
        {
            var response = _SysDeptExpandService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询系统部门扩展详情
        /// </summary>
        /// <param name="SysDeptId"></param>
        /// <returns></returns>
        [HttpGet("{SysDeptId}")]
        [ActionPermissionFilter(Permission = "system:dept:expand:query")]
        public IActionResult GetSysDeptExpand(long SysDeptId)
        {
            var response = _SysDeptExpandService.GetInfo(SysDeptId);

            var info = response.Adapt<SysDeptExpandDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加系统部门扩展
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "system:dept:expand:add")]
        [Log(Title = "系统部门扩展", BusinessType = BusinessType.INSERT)]
        public IActionResult AddSysDeptExpand([FromBody] SysDeptExpandDto parm)
        {
            var modal = parm.Adapt<SysDeptExpand>().ToCreate(HttpContext);

            var response = _SysDeptExpandService.AddSysDeptExpand(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新系统部门扩展
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "system:dept:expand:edit")]
        [Log(Title = "系统部门扩展", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateSysDeptExpand([FromBody] SysDeptExpandDto parm)
        {
            var modal = parm.Adapt<SysDeptExpand>().ToUpdate(HttpContext);
            var response = _SysDeptExpandService.UpdateSysDeptExpand(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除系统部门扩展
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "system:dept:expand:delete")]
        [Log(Title = "系统部门扩展", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteSysDeptExpand([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<long>(ids);

            return ToResponse(_SysDeptExpandService.Delete(idArr));
        }

        /// <summary>
        /// 同步系统部门名称
        /// </summary>
        /// <returns></returns>
        [HttpPut("syncDeptName")]
        [ActionPermissionFilter(Permission = "system:dept:expand:sync")]
        [Log(Title = "系统部门扩展", BusinessType = BusinessType.UPDATE)]
        public IActionResult SyncSysDeptExpand()
        {
            var response = _SysDeptExpandService.SyncSysDeptExpand();

            return SUCCESS(response);
        }
    }
}