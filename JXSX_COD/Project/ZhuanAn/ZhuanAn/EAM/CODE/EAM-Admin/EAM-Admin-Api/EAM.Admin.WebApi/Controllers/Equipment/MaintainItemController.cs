using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

//创建时间：2024-10-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备保养项目
    /// </summary>
    [Verify]
    [Route("equipment/MaintainItem")]
    public class MaintainItemController : BaseController
    {
        /// <summary>
        /// 设备保养项目接口
        /// </summary>
        private readonly IMaintainItemService _MaintainItemService;

        public MaintainItemController(IMaintainItemService MaintainItemService)
        {
            _MaintainItemService = MaintainItemService;
        }

        /// <summary>
        /// 查询设备保养项目列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "maintain:item:list")]
        public IActionResult QueryMaintainItem([FromQuery] MaintainItemQueryDto parm)
        {
            var response = _MaintainItemService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备保养项目详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "maintain:item:query")]
        public IActionResult GetMaintainItem(int Id)
        {
            var response = _MaintainItemService.GetInfo(Id);

            var info = response.Adapt<MaintainItemDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备保养项目
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "maintain:item:add")]
        [Log(Title = "设备保养项目", BusinessType = BusinessType.INSERT)]
        public IActionResult AddMaintainItem([FromBody] MaintainItemDto parm)
        {
            var modal = parm.Adapt<MaintainItem>().ToCreate(HttpContext);
            var response = _MaintainItemService.AddMaintainItem(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新设备保养项目
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "maintain:item:edit")]
        [Log(Title = "设备保养项目", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateMaintainItem([FromBody] MaintainItemDto parm)
        {
            var modal = parm.Adapt<MaintainItem>().ToUpdate(HttpContext);
            var response = _MaintainItemService.UpdateMaintainItem(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除设备保养项目
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "maintain:item:delete")]
        [Log(Title = "设备保养项目", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteMaintainItem([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_MaintainItemService.DeleteMaintainItem(idArr));
        }

        /// <summary>
        /// 导出设备保养项目
        /// </summary>
        /// <returns></returns>
        [Log(Title = "设备保养项目", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "maintain:item:export")]
        public IActionResult Export([FromQuery] MaintainItemQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _MaintainItemService.ExportList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "设备保养项目", "设备保养项目");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importData")]
        [Log(Title = "设备保养项目导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "maintain:item:import")]
        public IActionResult ImportData([FromForm(Name = "file")] IFormFile formFile)
        {
            List<MaintainItemDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<MaintainItemDto>(startCell: "A1").ToList();
            }

            return SUCCESS(_MaintainItemService.ImportMaintainItem(list.Adapt<List<MaintainItem>>()));
        }

        /// <summary>
        /// 设备保养项目导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        [Log(Title = "设备保养项目模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [AllowAnonymous]
        public IActionResult ImportTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<MaintainItemDto>() { }, "MaintainItem");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 添加设备保养项目
        /// </summary>
        /// <returns></returns>
        [HttpPost("clone")]
        [ActionPermissionFilter(Permission = "maintain:item:add")]
        [Log(Title = "设备保养项目克隆", BusinessType = BusinessType.INSERT)]
        public IActionResult AddMaintainItem([FromBody] MaintainItemCloneDto parm)
        {
            var response = _MaintainItemService.CloneMaintainItem(parm);

            return SUCCESS(response);
        }
    }
}