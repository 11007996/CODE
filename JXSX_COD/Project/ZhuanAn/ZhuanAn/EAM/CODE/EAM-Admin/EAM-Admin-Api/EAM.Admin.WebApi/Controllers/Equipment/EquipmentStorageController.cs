using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-12-04
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备保管
    /// </summary>
    [Verify]
    [Route("equipment/EquipmentStorage")]
    public class EquipmentStorageController : BaseController
    {
        /// <summary>
        /// 设备保管接口
        /// </summary>
        private readonly IEquipmentStorageService _EquipmentStorageService;

        public EquipmentStorageController(IEquipmentStorageService EquipmentStorageService)
        {
            _EquipmentStorageService = EquipmentStorageService;
        }

        /// <summary>
        /// 查询设备保管列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "equipment:storage:list")]
        public IActionResult QueryEquipmentStorage([FromQuery] EquipmentStorageUsingQueryDto parm)
        {
            var response = _EquipmentStorageService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备保管详情
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpGet("{equipmentId}")]
        [ActionPermissionFilter(Permission = "equipment:storage:query")]
        public IActionResult GetEquipmentStorage(int equipmentId)
        {
            var response = _EquipmentStorageService.GetInfo(equipmentId);

            var info = response.Adapt<EquipmentStorageUsingDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备保管
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "equipment:storage:add")]
        [Log(Title = "设备保管", BusinessType = BusinessType.INSERT)]
        public IActionResult AddEquipmentStorage([FromBody] EquipmentStorageUsingDto parm)
        {
            var modal = parm.Adapt<EquipmentStorageUsing>().ToCreate(HttpContext);

            var response = _EquipmentStorageService.AddEquipmentStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新设备保管
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "equipment:storage:edit")]
        [Log(Title = "设备保管", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateEquipmentStorage([FromBody] EquipmentStorageUsingDto parm)
        {
            var modal = parm.Adapt<EquipmentStorageUsing>().ToUpdate(HttpContext);
            var response = _EquipmentStorageService.UpdateEquipmentStorage(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除设备保管
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "equipment:storage:delete")]
        [Log(Title = "设备保管", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteEquipmentStorage([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_EquipmentStorageService.Delete(idArr));
        }

        /// <summary>
        /// 领用
        /// </summary>
        /// <returns></returns>
        [HttpPut("receive")]
        [ActionPermissionFilter(Permission = "equipment:storage:receive")]
        [Log(Title = "设备保管", BusinessType = BusinessType.UPDATE)]
        public IActionResult ReceiveEquipmentStorage([FromBody] OperateEquipmentStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);

            var response = _EquipmentStorageService.ReceiveEquipmentStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 设备批量领用
        /// </summary>
        /// <returns></returns>
        [HttpPost("receive/batch")]
        [ActionPermissionFilter(Permission = "equipment:storage:receive")]
        [Log(Title = "设备保管", BusinessType = BusinessType.UPDATE)]
        public IActionResult BatchReceiveEquipment([FromBody] List<OperateEquipmentStorageDto> parm)
        {
            if (parm.Count > 0)
            {
                foreach (var item in parm)
                {
                    item.ToCreate(HttpContext);
                }
            }
            var response = _EquipmentStorageService.BatchReceiveEquipment(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <returns></returns>
        [HttpPut("back")]
        [ActionPermissionFilter(Permission = "equipment:storage:back")]
        [Log(Title = "设备保管", BusinessType = BusinessType.UPDATE)]
        public IActionResult BackEquipmentStorage([FromBody] OperateEquipmentStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);

            var response = _EquipmentStorageService.BackEquipmentStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备保管记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("record")]
        [ActionPermissionFilter(Permission = "equipment:storage:list")]
        public IActionResult QueryEquipmentStorageRecord([FromQuery] EquipmentStorageRecordQueryDto parm)
        {
            var response = _EquipmentStorageService.GetRecordList(parm);
            return SUCCESS(response);
        }
    }
}