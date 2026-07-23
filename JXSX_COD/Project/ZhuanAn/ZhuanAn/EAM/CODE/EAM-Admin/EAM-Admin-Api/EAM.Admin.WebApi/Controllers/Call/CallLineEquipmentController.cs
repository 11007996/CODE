using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Service.Call.ICallService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-07-30
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产线设备
    /// </summary>
    [Verify]
    [Route("call/CallLineEquipment")]
    public class CallLineEquipmentController : BaseController
    {
        /// <summary>
        /// 产线设备接口
        /// </summary>
        private readonly ICallLineEquipmentService _CallLineEquipmentService;

        public CallLineEquipmentController(ICallLineEquipmentService CallLineEquipmentService)
        {
            _CallLineEquipmentService = CallLineEquipmentService;
        }

        /// <summary>
        /// 查询产线设备列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "call:line:equipment:list")]
        public IActionResult QueryCallLineEquipment([FromQuery] CallLineEquipmentQueryDto parm)
        {
            var response = _CallLineEquipmentService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产线设备详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "call:line:equipment:query")]
        public IActionResult GetCallLineEquipment(int Id)
        {
            var response = _CallLineEquipmentService.GetInfo(Id);

            var info = response.Adapt<CallLineEquipmentDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产线设备
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "call:line:equipment:add")]
        [Log(Title = "产线设备", BusinessType = BusinessType.INSERT)]
        public IActionResult AddCallLineEquipment([FromBody] CallLineEquipmentDto parm)
        {
            var modal = parm.Adapt<CallLineEquipment>().ToCreate(HttpContext);

            var response = _CallLineEquipmentService.AddCallLineEquipment(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产线设备
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "call:line:equipment:edit")]
        [Log(Title = "产线设备", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateCallLineEquipment([FromBody] CallLineEquipmentDto parm)
        {
            var modal = parm.Adapt<CallLineEquipment>().ToUpdate(HttpContext);
            var response = _CallLineEquipmentService.UpdateCallLineEquipment(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产线设备
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "call:line:equipment:delete")]
        [Log(Title = "产线设备", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteCallLineEquipment([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_CallLineEquipmentService.Delete(idArr));
        }
    }
}