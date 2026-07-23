using EAM.Model.Dto;
using EAM.Model.Fixture;
using EAM.Service.Fixture.IFixtureService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-04-24
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 治具信息
    /// </summary>
    [Verify]
    [Route("fixture/FixtureFile")]
    public class FixtureFileController : BaseController
    {
        /// <summary>
        /// 治具文件关联接口
        /// </summary>
        private readonly IFixtureFileService _FixtureFileService;

        public FixtureFileController(IFixtureFileService FixtureFileService)
        {
            _FixtureFileService = FixtureFileService;
        }

        /// <summary>
        /// 查询治具文件关联列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "fixture:file:list")]
        public IActionResult QueryFixtureFile([FromQuery] FixtureFileQueryDto parm)
        {
            var response = _FixtureFileService.GetFileList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 添加治具文件关联
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "fixture:file:add")]
        [Log(Title = "治具文件关联", BusinessType = BusinessType.INSERT)]
        public IActionResult AddFixtureFile([FromBody] FixtureFileDto parm)
        {
            var modal = parm.Adapt<FixtureFile>().ToCreate(HttpContext);

            var response = _FixtureFileService.AddFixtureFile(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 添加治具文件关联
        /// </summary>
        /// <returns></returns>
        [HttpPost("batch")]
        [ActionPermissionFilter(Permission = "fixture:file:add")]
        [Log(Title = "治具文件关联", BusinessType = BusinessType.INSERT)]
        public IActionResult BatchAddFixtureFile([FromBody] List<FixtureFileDto> parm)
        {
            var modal = parm.Adapt<List<FixtureFile>>();

            var response = _FixtureFileService.BatchAddFixtureFile(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新治具文件关联
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "fixture:file:edit")]
        [Log(Title = "治具文件关联", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateFixtureFile([FromBody] FixtureFileDto parm)
        {
            var modal = parm.Adapt<FixtureFile>().ToUpdate(HttpContext);
            var response = _FixtureFileService.UpdateFixtureFile(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除治具文件关联
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        [ActionPermissionFilter(Permission = "fixture:file:delete")]
        [Log(Title = "治具文件关联", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteFixtureFile([FromBody] FixtureFileDto parm)
        {
            return ToResponse(_FixtureFileService.DeleteFixtureFile(parm));
        }
    }
}