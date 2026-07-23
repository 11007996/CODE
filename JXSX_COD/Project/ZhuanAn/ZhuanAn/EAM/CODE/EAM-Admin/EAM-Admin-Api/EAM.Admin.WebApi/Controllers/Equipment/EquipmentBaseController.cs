using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

//创建时间：2024-09-30
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备资产信息
    /// </summary>
    [Verify]
    [Route("equipment/EquipmentBase")]
    public class EquipmentBaseController : BaseController
    {
        /// <summary>
        /// 设备资产信息接口
        /// </summary>
        private readonly IEquipmentBaseService _EquipmentService;

        public EquipmentBaseController(IEquipmentBaseService EquipmentService)
        {
            _EquipmentService = EquipmentService;
        }

        /// <summary>
        /// 查询设备资产信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "equipment:list")]
        public IActionResult QueryEquipmentBase([FromQuery] EquipmentBaseQueryDto parm)
        {
            var response = _EquipmentService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备资产信息详情
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}")]
        [ActionPermissionFilter(Permission = "equipment:query")]
        public IActionResult GetEquipmentBase(int equipmentId)
        {
            var response = _EquipmentService.GetInfo(equipmentId);

            var info = response.Adapt<EquipmentBaseDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备资产信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "equipment:add")]
        [Log(Title = "设备资产信息", BusinessType = BusinessType.INSERT)]
        public IActionResult AddEquipmentBase([FromBody] EquipmentBaseDto parm)
        {
            var modal = parm.Adapt<EquipmentBase>().ToUpdate(HttpContext);

            var response = _EquipmentService.AddEquipmentBase(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新设备资产信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "equipment:edit")]
        [Log(Title = "设备资产信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateEquipmentBase([FromBody] EquipmentBaseDto parm)
        {
            var modal = parm.Adapt<EquipmentBase>().ToUpdate(HttpContext);
            var response = _EquipmentService.UpdateEquipmentBase(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除设备资产信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "equipment:delete")]
        [Log(Title = "设备资产信息", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteEquipmentBase([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_EquipmentService.DeleteEquipmentBase(idArr));
        }

        /// <summary>
        /// 导出设备资产信息
        /// </summary>
        /// <returns></returns>
        [Log(Title = "设备资产信息", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "equipment:export")]
        public IActionResult Export([FromQuery] EquipmentBaseQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _EquipmentService.ExportList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "设备资产信息", "设备资产信息");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importData")]
        [Log(Title = "设备资产信息导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "equipment:import")]
        public IActionResult ImportData([FromForm(Name = "file")] IFormFile formFile)
        {
            List<EquipmentBaseDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<EquipmentBaseDto>(startCell: "A1").ToList();
            }

            return SUCCESS(_EquipmentService.ImportEquipmentBase(list.Adapt<List<EquipmentBase>>()));
        }

        /// <summary>
        /// 设备资产信息导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        [Log(Title = "设备资产信息模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [AllowAnonymous]
        public IActionResult ImportTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<EquipmentBaseDto>() { }, "Equipment");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 查询设备字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictEquipmentBase([FromQuery] EquipmentBaseQueryDto parm)
        {
            var response = _EquipmentService.GetDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询闲置设备
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("idle")]
        public IActionResult IdleEquipmentBase([FromQuery] EquipmentBaseQueryDto parm)
        {
            var response = _EquipmentService.GetIdle(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询成本中心字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict/costCenter")]
        public IActionResult GetCostCenterDict([FromQuery] EquipmentBaseQueryDto parm)
        {
            var response = _EquipmentService.GetCostCenterDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询自定义机型字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict/customModel")]
        public IActionResult GetCustomModelDict([FromQuery] EquipmentBaseQueryDto parm)
        {
            var response = _EquipmentService.GetCustomModelDict(parm);
            return SUCCESS(response);
        }
    }
}