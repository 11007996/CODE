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
    [Route("fixture/FixturePart")]
    public class FixturePartController : BaseController
    {
        /// <summary>
        /// 治具料号关联表接口
        /// </summary>
        private readonly IFixturePartService _FixturePartService;

        public FixturePartController(IFixturePartService FixturePartService)
        {
            _FixturePartService = FixturePartService;
        }

        /// <summary>
        /// 查询治具料号关联表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "fixture:part:list")]
        public IActionResult QueryFixturePart([FromQuery] FixturePartQueryDto parm)
        {
            var response = _FixturePartService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询治具料号关联表详情
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionPermissionFilter(Permission = "fixture:part:query")]
        public IActionResult GetFixturePart(FixturePartQueryDto parm)
        {
            var response = _FixturePartService.GetInfo(parm);

            var info = response.Adapt<FixturePartDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加治具料号关联表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "fixture:part:add")]
        [Log(Title = "治具料号关联表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddFixturePart([FromBody] FixturePartDto parm)
        {
            var modal = parm.Adapt<FixturePart>().ToCreate(HttpContext);

            var response = _FixturePartService.AddFixturePart(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新治具料号关联表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "fixture:part:edit")]
        [Log(Title = "治具料号关联表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateFixturePart([FromBody] FixturePartDto parm)
        {
            var modal = parm.Adapt<FixturePart>().ToUpdate(HttpContext);
            var response = _FixturePartService.UpdateFixturePart(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除治具料号关联表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        [ActionPermissionFilter(Permission = "fixture:part:delete")]
        [Log(Title = "治具料号关联表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteFixturePart([FromBody] FixturePartDto parm)
        {
            //var idArr = Tools.SplitAndConvert<string>(ids);
            var modal = parm.Adapt<FixturePart>().ToUpdate(HttpContext);
            return ToResponse(_FixturePartService.DeleteFixturePart(modal));
        }
    }
}