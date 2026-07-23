using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-08-20
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 保养通知配置
    /// </summary>
    [Verify]
    [Route("equipment/MaintainNoticeConfig")]
    public class MaintainNoticeConfigController : BaseController
    {
        /// <summary>
        /// 保养通知配置接口
        /// </summary>
        private readonly IMaintainNoticeConfigService _MaintainNoticeConfigService;

        public MaintainNoticeConfigController(IMaintainNoticeConfigService MaintainNoticeConfigService)
        {
            _MaintainNoticeConfigService = MaintainNoticeConfigService;
        }

        /// <summary>
        /// 查询保养通知配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "maintain:notice:config:list")]
        public IActionResult QueryMaintainNoticeConfig([FromQuery] MaintainNoticeConfigQueryDto parm)
        {
            var response = _MaintainNoticeConfigService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询保养通知配置详情
        /// </summary>
        /// <param name="NoticeConfigId"></param>
        /// <returns></returns>
        [HttpGet("{NoticeConfigId}")]
        [ActionPermissionFilter(Permission = "maintain:notice:config:query")]
        public IActionResult GetMaintainNoticeConfig(int NoticeConfigId)
        {
            var response = _MaintainNoticeConfigService.GetInfo(NoticeConfigId);

            var info = response.Adapt<MaintainNoticeConfigDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加保养通知配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "maintain:notice:config:add")]
        [Log(Title = "保养通知配置", BusinessType = BusinessType.INSERT)]
        public IActionResult AddMaintainNoticeConfig([FromBody] MaintainNoticeConfigDto parm)
        {
            var modal = parm.Adapt<MaintainNoticeConfig>().ToCreate(HttpContext);

            var response = _MaintainNoticeConfigService.AddMaintainNoticeConfig(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新保养通知配置
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "maintain:notice:config:edit")]
        [Log(Title = "保养通知配置", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateMaintainNoticeConfig([FromBody] MaintainNoticeConfigDto parm)
        {
            var modal = parm.Adapt<MaintainNoticeConfig>().ToUpdate(HttpContext);
            var response = _MaintainNoticeConfigService.UpdateMaintainNoticeConfig(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除保养通知配置
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "maintain:notice:config:delete")]
        [Log(Title = "保养通知配置", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteMaintainNoticeConfig([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_MaintainNoticeConfigService.DeleteMaintainNoticeConfig(idArr));
        }
    }
}