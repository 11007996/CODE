using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Service.Basic.IBasicService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-05-08
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 工站信息
    /// </summary>
    [Verify]
    [Route("basic/Station")]
    public class StationController : BaseController
    {
        /// <summary>
        /// 工站信息接口
        /// </summary>
        private readonly IStationService _StationService;

        public StationController(IStationService StationService)
        {
            _StationService = StationService;
        }

        /// <summary>
        /// 查询工站信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "station:list")]
        public IActionResult QueryStation([FromQuery] StationQueryDto parm)
        {
            var response = _StationService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询工站信息详情
        /// </summary>
        /// <param name="StationId"></param>
        /// <returns></returns>
        [HttpGet("{StationId}")]
        [ActionPermissionFilter(Permission = "station:query")]
        public IActionResult GetStation(int StationId)
        {
            var response = _StationService.GetInfo(StationId);

            var info = response.Adapt<StationDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加工站信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "station:add")]
        [Log(Title = "工站信息", BusinessType = BusinessType.INSERT)]
        public IActionResult AddStation([FromBody] StationDto parm)
        {
            var modal = parm.Adapt<Station>().ToCreate(HttpContext);

            var response = _StationService.AddStation(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新工站信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "station:edit")]
        [Log(Title = "工站信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateStation([FromBody] StationDto parm)
        {
            var modal = parm.Adapt<Station>().ToUpdate(HttpContext);
            var response = _StationService.UpdateStation(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除工站信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "station:delete")]
        [Log(Title = "工站信息", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteStation([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_StationService.Delete(idArr));
        }

        /// <summary>
        /// 查询工站信息字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictStation([FromQuery] StationQueryDto parm)
        {
            var response = _StationService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}