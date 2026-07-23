using EAM.Model;
using EAM.Model.Consumable;
using EAM.Model.Dto;
using EAM.Service.Consumable.IConsumableService;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

//创建时间：2024-05-18
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 耗品表
    /// </summary>
    [Verify]
    [Route("consumable/ConsumableBase")]
    [ApiExplorerSettings(GroupName = "sys")]
    public class ConsumableBaseController : BaseController
    {
        /// <summary>
        /// 耗品表接口
        /// </summary>
        private readonly IConsumableBaseService _ConsumableService;

        public ConsumableBaseController(IConsumableBaseService ConsumableService)
        {
            _ConsumableService = ConsumableService;
        }

        /// <summary>
        /// 查询耗品表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "consumable:list")]
        public IActionResult QueryConsumableBase([FromQuery] ConsumableBaseQueryDto parm)
        {
            var response = _ConsumableService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品详情列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("detailList")]
        public IActionResult QueryConsumableDetail([FromQuery] ConsumableBaseQueryDto parm)
        {
            var response = _ConsumableService.GetDetailList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品表详情
        /// </summary>
        /// <param name="consumableId"></param>
        /// <returns></returns>
        [HttpGet("{consumableId}")]
        [ActionPermissionFilter(Permission = "consumable:query")]
        public IActionResult GetConsumableBase(int consumableId)
        {
            var response = _ConsumableService.GetInfo(consumableId);

            var info = response.Adapt<ConsumableBaseDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加耗品表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "consumable:add")]
        [Log(Title = "耗品管理", BusinessType = BusinessType.INSERT)]
        public IActionResult AddConsumableBase([FromBody] ConsumableBaseDto parm)
        {
            var modal = parm.Adapt<ConsumableBase>().ToCreate(HttpContext);

            var response = _ConsumableService.AddConsumableBase(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新耗品表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "consumable:edit")]
        [Log(Title = "耗品管理", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateConsumableBase([FromBody] ConsumableBaseDto parm)
        {
            var modal = parm.Adapt<ConsumableBase>().ToUpdate(HttpContext);
            var response = _ConsumableService.UpdateConsumableBase(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除耗品表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "consumable:delete")]
        [Log(Title = "耗品管理", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteConsumableBase([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);
            return ToResponse(_ConsumableService.DeleteConsumableBase(idArr));
        }

        /// <summary>
        /// 导出耗品表
        /// </summary>
        /// <returns></returns>
        [Log(Title = "耗品管理", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "consumable:export")]
        public IActionResult Export([FromQuery] ConsumableBaseQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _ConsumableService.ExportList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "耗品表", "耗品表");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importData")]
        [Log(Title = "耗品表导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "consumable:import")]
        public IActionResult ImportData([FromForm(Name = "file")] IFormFile formFile)
        {
            List<ConsumableBaseDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<ConsumableBaseDto>(startCell: "A1").ToList();
            }

            return SUCCESS(_ConsumableService.ImportConsumableBase(list.Adapt<List<ConsumableBase>>()));
        }

        /// <summary>
        /// 耗品表导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        [Log(Title = "耗品表模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        public IActionResult ImportTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<ConsumableBaseDto>() { }, "Consumable");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 查询耗品字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult QueryConsumableBaseDict([FromQuery] ConsumableBaseQueryDto parm)
        {
            var response = _ConsumableService.GetDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品类别字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict/category")]
        public IActionResult QueryConsumableCategoryDict([FromQuery] ConsumableBaseQueryDto parm)
        {
            var response = _ConsumableService.GetCategoryDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询闲置耗品
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("idle")]
        public IActionResult QueryIdleConsumableBase([FromQuery] ConsumableBaseQueryDto parm)
        {
            var list = _ConsumableService.GetDetailList(parm);
            var response = list.Adapt<PagedInfo<IdleConsumableDto>>();
            return SUCCESS(response);
        }
    }
}