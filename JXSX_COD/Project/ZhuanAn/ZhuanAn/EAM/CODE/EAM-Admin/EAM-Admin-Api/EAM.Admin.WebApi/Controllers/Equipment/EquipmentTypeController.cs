using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-12-09
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 机台类型
    /// </summary>
    [Verify]
    [Route("equipment/EquipmentType")]
    public class EquipmentTypeController : BaseController
    {
        /// <summary>
        /// 机台类型接口
        /// </summary>
        private readonly IEquipmentTypeService _EquipmentTypeService;

        public EquipmentTypeController(IEquipmentTypeService EquipmentTypeService)
        {
            _EquipmentTypeService = EquipmentTypeService;
        }

        /// <summary>
        /// 查询机台类型列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "equipment:type:list")]
        public IActionResult QueryEquipmentType([FromQuery] EquipmentTypeQueryDto parm)
        {
            var response = _EquipmentTypeService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询机台类型详情
        /// </summary>
        /// <param name="EquipmentTypeName"></param>
        /// <returns></returns>
        [HttpGet("{EquipmentTypeName}")]
        [ActionPermissionFilter(Permission = "equipment:type:query")]
        public IActionResult GetEquipmentType(string EquipmentTypeName)
        {
            var response = _EquipmentTypeService.GetInfo(EquipmentTypeName);

            var info = response.Adapt<EquipmentTypeDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加机台类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "equipment:type:add")]
        [Log(Title = "机台类型", BusinessType = BusinessType.INSERT)]
        public IActionResult AddEquipmentType([FromBody] EquipmentTypeDto parm)
        {
            var modal = parm.Adapt<EquipmentType>().ToCreate(HttpContext);

            var response = _EquipmentTypeService.AddEquipmentType(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新机台类型
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "equipment:type:edit")]
        [Log(Title = "机台类型", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateEquipmentType([FromBody] EquipmentTypeDto parm)
        {
            var modal = parm.Adapt<EquipmentType>().ToUpdate(HttpContext);
            var response = _EquipmentTypeService.UpdateEquipmentType(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除机台类型
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "equipment:type:delete")]
        [Log(Title = "机台类型", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteEquipmentType([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<string>(ids);

            return ToResponse(_EquipmentTypeService.Delete(idArr));
        }

        /// <summary>
        /// 查询设备类型字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictEquipmentType([FromQuery] EquipmentTypeQueryDto parm)
        {
            var response = _EquipmentTypeService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}