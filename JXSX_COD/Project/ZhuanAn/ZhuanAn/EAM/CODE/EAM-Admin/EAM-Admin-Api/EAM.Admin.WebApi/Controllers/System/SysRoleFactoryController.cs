using EAM.Model.Dto;
using EAM.Model.System.Dto;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

//创建时间：2026-02-02
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 角色厂区绑定
    /// </summary>
    [Verify]
    [Route("system/RoleFactory")]
    public class SysRoleFactoryController : BaseController
    {
        /// <summary>
        /// 角色厂区绑定接口
        /// </summary>
        private readonly ISysUserRoleService _ISysUserRoleService;

        private readonly ISysRoleService _SysRoleService;

        public SysRoleFactoryController(ISysUserRoleService SysUserRoleService, ISysRoleService sysRoleService)
        {
            _ISysUserRoleService = SysUserRoleService;
            _SysRoleService = sysRoleService;
        }

        /// <summary>
        /// 查询角色厂区绑定列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "system:rolefactory:list")]
        public IActionResult QuerySysRoleFactory([FromQuery] SysRoleQueryDto parm)
        {
            if (!HttpContextExtension.IsAdmin(HttpContext))
            {
                parm.FactoryId = HttpContextExtension.GetFactoryId(HttpContext);
            }
            var response = _SysRoleService.SelectRoleList(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 添加厂区角色用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("addUsers")]
        [ActionPermissionFilter(Permission = "system:rolefactory:user:add")]
        [Log(Title = "厂区角色用户", BusinessType = BusinessType.INSERT)]
        public IActionResult AddFactoryRoleUser([FromBody] FactoryRoleUserOpeateDto parm)
        {
            parm.FactoryId = HttpContextExtension.GetFactoryId(HttpContext);
            var response = _ISysUserRoleService.AddFactoryRoleUser(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 删除厂区角色用户
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delUsers")]
        [ActionPermissionFilter(Permission = "system:rolefactory:user:del")]
        [Log(Title = "厂区角色用户", BusinessType = BusinessType.DELETE)]
        public IActionResult DelFactoryRoleUser([FromBody] FactoryRoleUserOpeateDto parm)
        {
            parm.FactoryId = HttpContextExtension.GetFactoryId(HttpContext);
            var response = _ISysUserRoleService.DelFactoryRoleUser(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importData/{roleId}")]
        [Log(Title = "角色员工绑定导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "system:rolefactory:user:import")]
        public IActionResult ImportData([FromRoute] int roleId, [FromForm(Name = "file")] IFormFile formFile)
        {
            string factoryId = HttpContextExtension.GetFactoryId(HttpContext);
            List<EmpSimpleDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<EmpSimpleDto>(startCell: "A1").ToList();
            }

            return SUCCESS(_ISysUserRoleService.ImportFactoryRoleUser(roleId, factoryId, list));
        }

        /// <summary>
        /// 员工信息导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        [Log(Title = "角色员工导入模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [AllowAnonymous]
        public IActionResult ImportTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<EmpSimpleDto>() { }, "角色员工导入模板");
            return ExportExcel(result.Item2, result.Item1);
        }
    }
}