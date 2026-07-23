using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Service.Basic.IBasicService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-06-27
namespace EAM.Applet.Api.Controllers
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