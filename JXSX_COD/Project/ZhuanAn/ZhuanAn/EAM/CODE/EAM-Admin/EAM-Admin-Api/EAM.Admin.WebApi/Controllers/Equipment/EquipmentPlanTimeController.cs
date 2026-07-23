using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-05-12
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备计划停机时间
    /// </summary>
    [Verify]
    [Route("equipment/EquipmentPlanTime")]
    public class EquipmentPlanTimeController : BaseController
    {
        /// <summary>
        /// 设备计划停机时间接口
        /// </summary>
        private readonly IEquipmentPlanTimeService _EquipmentPlanTimeService;

        public EquipmentPlanTimeController(IEquipmentPlanTimeService EquipmentPlanTimeService)
        {
            _EquipmentPlanTimeService = EquipmentPlanTimeService;
        }

        /// <summary>
        /// 查询设备计划停机时间列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "equipment:plantime:list")]
        public IActionResult QueryEquipmentPlanTime([FromQuery] EquipmentPlanTimeQueryDto parm)
        {
            var response = _EquipmentPlanTimeService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备计划停机时间详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "equipment:plantime:query")]
        public IActionResult GetEquipmentPlanTime(int Id)
        {
            var response = _EquipmentPlanTimeService.GetInfo(Id);

            var info = response.Adapt<EquipmentPlanTimeDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备计划停机时间
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "equipment:plantime:add")]
        [Log(Title = "设备计划停机时间", BusinessType = BusinessType.INSERT)]
        public IActionResult AddEquipmentPlanTime([FromBody] EquipmentPlanTimeDto parm)
        {
            var modal = parm.Adapt<EquipmentPlanTime>().ToCreate(HttpContext);

            var response = _EquipmentPlanTimeService.AddEquipmentPlanTime(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新设备计划停机时间
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "equipment:plantime:edit")]
        [Log(Title = "设备计划停机时间", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateEquipmentPlanTime([FromBody] EquipmentPlanTimeDto parm)
        {
            var modal = parm.Adapt<EquipmentPlanTime>().ToUpdate(HttpContext);
            var response = _EquipmentPlanTimeService.UpdateEquipmentPlanTime(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除设备计划停机时间
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "equipment:plantime:delete")]
        [Log(Title = "设备计划停机时间", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteEquipmentPlanTime([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_EquipmentPlanTimeService.Delete(idArr));
        }
    }
}