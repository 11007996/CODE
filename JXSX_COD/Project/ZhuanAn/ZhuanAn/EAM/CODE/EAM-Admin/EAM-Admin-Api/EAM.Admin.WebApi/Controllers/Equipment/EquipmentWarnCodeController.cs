using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

//创建时间：2024-12-09
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备报警编码
    /// </summary>
    [Verify]
    [Route("equipment/EquipmentWarnCode")]
    public class EquipmentWarnCodeController : BaseController
    {
        /// <summary>
        /// 设备报警编码接口
        /// </summary>
        private readonly IEquipmentWarnCodeService _EquipmentWarnCodeService;

        public EquipmentWarnCodeController(IEquipmentWarnCodeService EquipmentWarnCodeService)
        {
            _EquipmentWarnCodeService = EquipmentWarnCodeService;
        }

        /// <summary>
        /// 查询设备报警编码列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "equipment:warn:code:list")]
        public IActionResult QueryEquipmentWarnCode([FromQuery] EquipmentWarnCodeQueryDto parm)
        {
            var response = _EquipmentWarnCodeService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备报警编码说情
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("info")]
        [ActionPermissionFilter(Permission = "equipment:warn:code:query")]
        public IActionResult GetEquipmentWarnCode([FromQuery] EquipmentWarnCodeQueryDto parm)
        {
            var response = _EquipmentWarnCodeService.GetInfo(parm.EquipmentId, parm.WarnCode);

            var info = response.Adapt<EquipmentWarnCodeDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备报警编码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "equipment:warn:code:add")]
        [Log(Title = "设备报警编码", BusinessType = BusinessType.INSERT)]
        public IActionResult AddEquipmentWarnCode([FromBody] EquipmentWarnCodeDto parm)
        {
            var modal = parm.Adapt<EquipmentWarnCode>().ToCreate(HttpContext);

            var response = _EquipmentWarnCodeService.AddEquipmentWarnCode(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新设备报警代码
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "equipment:warn:code:edit")]
        [Log(Title = "设备报警代码", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateEquipmentWarnCode([FromBody] EquipmentWarnCodeDto parm)
        {
            var modal = parm.Adapt<EquipmentWarnCode>().ToUpdate(HttpContext);
            var response = _EquipmentWarnCodeService.UpdateEquipmentWarnCode(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除设备报警编码
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        [ActionPermissionFilter(Permission = "equipment:warn:code:delete")]
        [Log(Title = "设备报警编码", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteEquipmentWarnCode([FromBody] EquipmentWarnCode modal)
        {
            return ToResponse(_EquipmentWarnCodeService.DeleteEquipmentWarnCode(modal));
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importData")]
        [Log(Title = "设备报警编码导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "equipment:warn:code:import")]
        public IActionResult ImportData([FromForm(Name = "file")] IFormFile formFile)
        {
            List<EquipmentWarnCodeDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<EquipmentWarnCodeDto>(startCell: "A1").ToList();
            }

            return SUCCESS(_EquipmentWarnCodeService.ImportEquipmentWarnCode(list.Adapt<List<EquipmentWarnCode>>()));
        }

        /// <summary>
        /// 设备报警代码导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        [Log(Title = "设备报警代码模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [AllowAnonymous]
        public IActionResult ImportTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<EquipmentWarnCodeDto>() { }, "EquipmentWarnCode");
            return ExportExcel(result.Item2, result.Item1);
        }
    }
}