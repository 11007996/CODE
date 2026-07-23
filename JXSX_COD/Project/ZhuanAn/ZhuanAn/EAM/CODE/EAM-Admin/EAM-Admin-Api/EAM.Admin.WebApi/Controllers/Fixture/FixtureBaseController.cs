using EAM.Model.Dto;
using EAM.Model.Fixture;
using EAM.Service.Fixture.IFixtureService;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

//创建时间：2024-04-24
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 治具信息
    /// </summary>
    [Verify]
    [Route("fixture/FixtureBase")]
    public class FixtureBaseController : BaseController
    {
        /// <summary>
        /// 治具信息接口
        /// </summary>
        private readonly IFixtureBaseService _FixtureService;

        public FixtureBaseController(IFixtureBaseService FixtureService)
        {
            _FixtureService = FixtureService;
        }

        /// <summary>
        /// 查询治具信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "fixture:list")]
        public IActionResult QueryFixtureBase([FromQuery] FixtureBaseQueryDto parm)
        {
            var response = _FixtureService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询治具信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("detailList")]
        [ActionPermissionFilter(Permission = "fixture:list")]
        public IActionResult QueryFixtureDetail([FromQuery] FixtureBaseQueryDto parm)
        {
            var response = _FixtureService.GetFixtureDetailList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询治具信息详情
        /// </summary>
        /// <param name="FixtureId"></param>
        /// <returns></returns>
        [HttpGet("{FixtureId}")]
        [ActionPermissionFilter(Permission = "fixture:query")]
        public IActionResult GetFixtureBase(int FixtureId)
        {
            var response = _FixtureService.GetInfo(FixtureId);

            var info = response.Adapt<FixtureBaseDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加治具信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "fixture:add")]
        [Log(Title = "治具信息", BusinessType = BusinessType.INSERT)]
        public IActionResult AddFixtureBase([FromBody] FixtureBaseDto parm)
        {
            var modal = parm.Adapt<FixtureBase>().ToCreate(HttpContext);

            var response = _FixtureService.AddFixtureBase(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新治具信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "fixture:edit")]
        [Log(Title = "治具信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateFixtureBase([FromBody] FixtureBaseDto parm)
        {
            var modal = parm.Adapt<FixtureBase>().ToUpdate(HttpContext);
            var response = _FixtureService.UpdateFixtureBase(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除治具信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "fixture:delete")]
        [Log(Title = "治具信息", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteFixtureBase([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_FixtureService.DeleteFixtureBase(idArr));
        }

        /// <summary>
        /// 导出治具表
        /// </summary>
        /// <returns></returns>
        [Log(Title = "治具管理", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "fixture:export")]
        public IActionResult Export([FromQuery] FixtureBaseQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _FixtureService.ExportList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "治具表", "治具表");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importData")]
        [Log(Title = "治具信息导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "fixture:import")]
        public IActionResult ImportData([FromForm(Name = "file")] IFormFile formFile)
        {
            List<FixtureBaseDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<FixtureBaseDto>(startCell: "A1").ToList();
            }

            return SUCCESS(_FixtureService.ImportFixtureBase(list.Adapt<List<FixtureBase>>()));
        }

        /// <summary>
        /// 治具信息导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        [Log(Title = "治具信息模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        public IActionResult ImportTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<FixtureBaseDto>() { }, "Fixture");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 查询治具字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictFixtureBase([FromQuery] FixtureBaseQueryDto parm)
        {
            var response = _FixtureService.GetDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询闲置治具
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("idle")]
        public IActionResult IdleFixtureBase([FromQuery] FixtureBaseQueryDto parm)
        {
            var response = _FixtureService.IdleFixtureList(parm);
            return SUCCESS(response);
        }
    }
}