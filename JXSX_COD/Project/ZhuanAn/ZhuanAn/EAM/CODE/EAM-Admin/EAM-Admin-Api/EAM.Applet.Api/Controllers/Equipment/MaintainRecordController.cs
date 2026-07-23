using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-10-11
namespace EAM.Applet.Api.Controllers
{
    /// <summary>
    /// 设备保养记录
    /// </summary>
    [Verify]
    [Route("equipment/MaintainRecord")]
    public class MaintainRecordController : BaseController
    {
        /// <summary>
        /// 设备保养记录接口
        /// </summary>
        private readonly IMaintainRecordService _MaintainRecordService;

        public MaintainRecordController(IMaintainRecordService MaintainRecordService)
        {
            _MaintainRecordService = MaintainRecordService;
        }

        /// <summary>
        /// 添加设备保养记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[ActionPermissionFilter(Permission = "maintain:record:add")]
        [Log(Title = "设备保养记录", BusinessType = BusinessType.INSERT)]
        public IActionResult AddMaintainRecord([FromBody] MaintainRecordDto parm)
        {
            var modal = parm.Adapt<MaintainRecord>().ToCreate(HttpContext).ToUpdate(HttpContext);

            var response = _MaintainRecordService.AddMaintainRecord(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新设备保养记录
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        //[ActionPermissionFilter(Permission = "maintain:record:edit")]
        [Log(Title = "设备保养记录", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateMaintainRecord([FromBody] MaintainRecordDto parm)
        {
            var modal = parm.Adapt<MaintainRecord>().ToUpdate(HttpContext);
            var response = _MaintainRecordService.UpdateMaintainRecord(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 查询设备保养记录详情
        /// </summary>
        /// <param name = "param" ></param>
        /// <returns></returns>
        [HttpGet("detail")]
        public IActionResult DetailMaintainRecord(MaintainRecordQueryDetailDto param)
        {
            var response = _MaintainRecordService.GetDetail(param);

            var info = response.Adapt<MaintainRecordDto>();
            return SUCCESS(info);
        }
    }
}