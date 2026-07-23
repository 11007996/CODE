using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-02-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 保养计划班次
    /// </summary>
    [Verify]
    [Route("equipment/MaintainPlanClass")]
    public class MaintainPlanClassController : BaseController
    {
        /// <summary>
        /// 保养计划班次接口
        /// </summary>
        private readonly IMaintainPlanClassService _MaintainPlanClassService;

        public MaintainPlanClassController(IMaintainPlanClassService MaintainPlanClassService)
        {
            _MaintainPlanClassService = MaintainPlanClassService;
        }

        /// <summary>
        /// 查询保养计划班次列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "maintain:plan:class:list")]
        public IActionResult QueryMaintainPlanClass([FromQuery] MaintainPlanClassQueryDto parm)
        {
            var response = _MaintainPlanClassService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询保养计划班次详情
        /// </summary>
        /// <param name="planClassId"></param>
        /// <returns></returns>
        [HttpGet("{planClassId}")]
        [ActionPermissionFilter(Permission = "maintain:plan:class:query")]
        public IActionResult GetMaintainPlanClass(int planClassId)
        {
            var response = _MaintainPlanClassService.GetInfo(planClassId);

            var info = response.Adapt<MaintainPlanClassDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加保养计划班次
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "maintain:plan:class:add")]
        [Log(Title = "保养计划班次", BusinessType = BusinessType.INSERT)]
        public IActionResult AddMaintainPlanClass([FromBody] MaintainPlanClassDto parm)
        {
            var modal = parm.Adapt<MaintainPlanClass>().ToCreate(HttpContext);
            var response = _MaintainPlanClassService.AddMaintainPlanClass(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新保养计划班次
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "maintain:plan:class:edit")]
        [Log(Title = "保养计划班次", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateMaintainPlanClass([FromBody] MaintainPlanClassDto parm)
        {
            var modal = parm.Adapt<MaintainPlanClass>().ToCreate(HttpContext);
            var response = _MaintainPlanClassService.UpdateMaintainPlanClass(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除保养计划班次
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "maintain:plan:class:delete")]
        [Log(Title = "保养计划班次", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteMaintainPlanClass([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_MaintainPlanClassService.DeleteMaintainPlanClass(idArr));
        }

        /// <summary>
        /// 查询计划班次字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult GetDict([FromQuery] MaintainPlanClassQueryDto parm)
        {
            var response = _MaintainPlanClassService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}