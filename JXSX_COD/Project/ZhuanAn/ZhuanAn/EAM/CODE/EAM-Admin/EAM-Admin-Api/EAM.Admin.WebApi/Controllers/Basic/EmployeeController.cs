using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Model.System;
using EAM.Service.Basic.IBasicService;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

//创建时间：2024-06-18
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 员工信息
    /// </summary>
    [Verify]
    [Route("basic/Employee")]
    public class EmployeeController : BaseController
    {
        /// <summary>
        /// 员工信息接口
        /// </summary>
        private readonly IEmployeeService _EmployeeService;

        private readonly ISysPostService _PostService;
        private readonly ISysUserPostService _UserPostService;
        private readonly ISysUserService _UserService;

        public EmployeeController(IEmployeeService EmployeeService, ISysPostService postService, ISysUserPostService userPostService, ISysUserService userService)
        {
            _EmployeeService = EmployeeService;
            _PostService = postService;
            _UserPostService = userPostService;
            _UserService = userService;
        }

        /// <summary>
        /// 查询员工信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "employee:list")]
        public IActionResult QueryEmployee([FromQuery] EmployeeQueryDto parm)
        {
            var response = _EmployeeService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询员工信息详情
        /// </summary>
        /// <param name="empCode"></param>
        /// <returns></returns>
        [HttpGet("{empCode}")]
        [ActionPermissionFilter(Permission = "employee:query")]
        public IActionResult GetEmployee(string empCode)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("posts", _PostService.GetAll().OrderBy(it => it.PostName));

            //编辑
            if (empCode != "0")
            {
                var response = _EmployeeService.GetInfo(empCode);
                var info = response.Adapt<EmployeeDto>();
                dic.Add("employee", info);
                SysUser user = _UserService.SelectUserByName(empCode);
                if (user != null && user.UserId > 0)
                {
                    info.PostIds = _UserPostService.GetUserPostsByUserId(user.UserId)?.ToArray();
                }
            }
            return SUCCESS(dic);
        }

        /// <summary>
        /// 添加员工信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "employee:add")]
        [Log(Title = "员工信息", BusinessType = BusinessType.INSERT)]
        public IActionResult AddEmployee([FromBody] EmployeeDto parm)
        {
            var modal = parm.Adapt<Employee>().ToCreate(HttpContext);

            var response = _EmployeeService.AddEmployee(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "employee:edit")]
        [Log(Title = "员工信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateEmployee([FromBody] EmployeeDto parm)
        {
            var modal = parm.Adapt<Employee>().ToUpdate(HttpContext);
            var response = _EmployeeService.UpdateEmployee(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{empCode}")]
        [ActionPermissionFilter(Permission = "employee:delete")]
        [Log(Title = "员工信息", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteEmployee([FromRoute] string empCode)
        {
            return ToResponse(_EmployeeService.DeleteEmployee(empCode));
        }

        /// <summary>
        /// 查询员工字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult QueryEmployeeDict([FromQuery] EmployeeQueryDto parm)
        {
            var response = _EmployeeService.GetDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importData")]
        [Log(Title = "员工信息导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "employee:import")]
        public IActionResult ImportData([FromForm(Name = "file")] IFormFile formFile)
        {
            List<EmployeeDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<EmployeeDto>(startCell: "A1").ToList();
            }

            return SUCCESS(_EmployeeService.ImportEmployee(list.Adapt<List<Employee>>()));
        }

        /// <summary>
        /// 员工信息导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        [Log(Title = "员工信息模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [AllowAnonymous]
        public IActionResult ImportTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<EmployeeDto>() { }, "员工信息导入模板");
            return ExportExcel(result.Item2, result.Item1);
        }
    }
}