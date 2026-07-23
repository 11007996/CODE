using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-02-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 保养计划
    /// </summary>
    [Verify]
    [Route("equipment/MaintainPlan")]
    public class MaintainPlanController : BaseController
    {
        /// <summary>
        /// 保养计划接口
        /// </summary>
        private readonly IMaintainPlanService _MaintainPlanService;

        public MaintainPlanController(IMaintainPlanService MaintainPlanService)
        {
            _MaintainPlanService = MaintainPlanService;
        }

        /// <summary>
        /// 查询保养计划列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "maintain:plan:list")]
        public IActionResult QueryMaintainPlan([FromQuery] MaintainPlanQueryDto parm)
        {
            var response = _MaintainPlanService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询保养计划详情
        /// </summary>
        /// <param name="PlanId"></param>
        /// <returns></returns>
        [HttpGet("{PlanId}")]
        [ActionPermissionFilter(Permission = "maintain:plan:query")]
        public IActionResult GetMaintainPlan(int PlanId)
        {
            var response = _MaintainPlanService.GetInfo(PlanId);

            var info = response.Adapt<MaintainPlanDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加保养计划
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "maintain:plan:add")]
        [Log(Title = "保养计划", BusinessType = BusinessType.INSERT)]
        public IActionResult AddMaintainPlan([FromBody] MaintainPlanBatchAddDto parm)
        {
            var modal = parm.ToCreate(HttpContext);

            var response = _MaintainPlanService.AddMaintainPlan(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新保养计划
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "maintain:plan:edit")]
        [Log(Title = "保养计划", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateMaintainPlan([FromBody] MaintainPlanDto parm)
        {
            var modal = parm.Adapt<MaintainPlan>().ToUpdate(HttpContext);
            var response = _MaintainPlanService.UpdateMaintainPlan(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除保养计划
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "maintain:plan:delete")]
        [Log(Title = "保养计划", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteMaintainPlan([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_MaintainPlanService.Delete(idArr));
        }

        /// <summary>
        /// 获取没有保养计划的设备
        /// </summary>
        /// <returns></returns>
        [HttpGet("excludeEquipment")]
        public IActionResult GetExcludeEquipment([FromQuery] ExcludeEquipmentQueryDto parm)
        {
            return SUCCESS(_MaintainPlanService.GetExcludeEquipment(parm));
        }
    }
}