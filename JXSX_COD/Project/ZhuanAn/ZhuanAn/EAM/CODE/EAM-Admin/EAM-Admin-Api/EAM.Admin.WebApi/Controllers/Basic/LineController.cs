using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Service.Basic.IBasicService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-06-27
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产线信息
    /// </summary>
    [Verify]
    [Route("basic/Line")]
    public class LineController : BaseController
    {
        /// <summary>
        /// 产线信息接口
        /// </summary>
        private readonly ILineService _LineService;

        public LineController(ILineService LineService)
        {
            _LineService = LineService;
        }

        /// <summary>
        /// 查询产线信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "line:list")]
        public IActionResult QueryLine([FromQuery] LineQueryDto parm)
        {
            var response = _LineService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产线信息详情
        /// </summary>
        /// <param name="LineId"></param>
        /// <returns></returns>
        [HttpGet("{LineId}")]
        [ActionPermissionFilter(Permission = "line:query")]
        public IActionResult GetLine(int LineId)
        {
            var response = _LineService.GetInfo(LineId);

            var info = response.Adapt<LineDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产线信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "line:add")]
        [Log(Title = "产线信息", BusinessType = BusinessType.INSERT)]
        public IActionResult AddLine([FromBody] LineDto parm)
        {
            var modal = parm.Adapt<Line>().ToCreate(HttpContext);

            var response = _LineService.AddLine(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产线信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "line:edit")]
        [Log(Title = "产线信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateLine([FromBody] LineDto parm)
        {
            var modal = parm.Adapt<Line>().ToUpdate(HttpContext);
            var response = _LineService.UpdateLine(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产线信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "line:delete")]
        [Log(Title = "产线信息", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteLine([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<string>(ids);

            return ToResponse(_LineService.Delete(idArr));
        }

        /// <summary>
        /// 查询产线字典信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult QueryLineDict([FromQuery] LineQueryDto parm)
        {
            var response = _LineService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}