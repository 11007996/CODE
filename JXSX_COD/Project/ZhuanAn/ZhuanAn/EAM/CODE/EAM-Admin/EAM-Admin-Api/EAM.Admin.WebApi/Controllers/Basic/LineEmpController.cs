using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Service.Basic.IBasicService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-09-15
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产线员工关联
    /// </summary>
    [Verify]
    [Route("basic/LineEmp")]
    public class LineEmpController : BaseController
    {
        /// <summary>
        /// 产线员工关联接口
        /// </summary>
        private readonly ILineEmpService _LineEmpService;

        public LineEmpController(ILineEmpService LineEmpService)
        {
            _LineEmpService = LineEmpService;
        }

        /// <summary>
        /// 查询产线员工关联列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "line:emp:list")]
        public IActionResult QueryLineEmp([FromQuery] LineEmpQueryDto parm)
        {
            var response = _LineEmpService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产线员工关联详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "line:emp:query")]
        public IActionResult GetLineEmp(int Id)
        {
            var response = _LineEmpService.GetInfo(Id);

            var info = response.Adapt<LineEmpDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产线员工关联
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "line:emp:add")]
        [Log(Title = "产线员工关联", BusinessType = BusinessType.INSERT)]
        public IActionResult AddLineEmp([FromBody] LineEmpDto parm)
        {
            var modal = parm.Adapt<LineEmp>().ToCreate(HttpContext);

            var response = _LineEmpService.AddLineEmp(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产线员工关联
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "line:emp:edit")]
        [Log(Title = "产线员工关联", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateLineEmp([FromBody] LineEmpDto parm)
        {
            var modal = parm.Adapt<LineEmp>().ToUpdate(HttpContext);
            var response = _LineEmpService.UpdateLineEmp(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产线员工关联
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "line:emp:delete")]
        [Log(Title = "产线员工关联", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteLineEmp([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_LineEmpService.Delete(idArr));
        }
    }
}