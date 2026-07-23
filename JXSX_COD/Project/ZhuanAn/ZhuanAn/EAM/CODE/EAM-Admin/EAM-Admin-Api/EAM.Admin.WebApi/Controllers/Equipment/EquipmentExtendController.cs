using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-12-09
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备扩展信息
    /// </summary>
    [Verify]
    [Route("equipment/EquipmentExtend")]
    public class EquipmentExtendController : BaseController
    {
        /// <summary>
        /// 设备扩展信息接口
        /// </summary>
        private readonly IEquipmentExtendService _EquipmentExtendService;

        public EquipmentExtendController(IEquipmentExtendService EquipmentExtendService)
        {
            _EquipmentExtendService = EquipmentExtendService;
        }

        /// <summary>
        /// 查询设备扩展信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "equipment:extend:list")]
        public IActionResult QueryEquipmentExtend([FromQuery] EquipmentExtendQueryDto parm)
        {
            var response = _EquipmentExtendService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备扩展信息详情
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}")]
        [ActionPermissionFilter(Permission = "equipment:extend:query")]
        public IActionResult GetEquipmentExtend(int equipmentId)
        {
            var response = _EquipmentExtendService.GetInfo(equipmentId);

            var info = response.Adapt<EquipmentExtendDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备扩展信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "equipment:extend:add")]
        [Log(Title = "设备扩展信息", BusinessType = BusinessType.INSERT)]
        public IActionResult AddEquipmentExtend([FromBody] EquipmentExtendDto parm)
        {
            var modal = parm.Adapt<EquipmentExtend>().ToCreate(HttpContext);

            var response = _EquipmentExtendService.AddEquipmentExtend(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新设备扩展信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "equipment:extend:edit")]
        [Log(Title = "设备扩展信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateEquipmentExtend([FromBody] EquipmentExtendDto parm)
        {
            var modal = parm.Adapt<EquipmentExtend>().ToUpdate(HttpContext);
            var response = _EquipmentExtendService.UpdateEquipmentExtend(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除设备扩展信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "equipment:extend:delete")]
        [Log(Title = "设备扩展信息", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteEquipmentExtend([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<string>(ids);

            return ToResponse(_EquipmentExtendService.Delete(idArr));
        }
    }
}