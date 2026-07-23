using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 数据解析脚本
    /// </summary>
    [Verify]
    [Route("iot/IotProductParserScript")]
    public class IotProductParserScriptController : BaseController
    {
        /// <summary>
        /// 数据解析脚本接口
        /// </summary>
        private readonly IIotProductParserScriptService _IotProductParserScriptService;

        public IotProductParserScriptController(IIotProductParserScriptService IotProductParserScriptService)
        {
            _IotProductParserScriptService = IotProductParserScriptService;
        }

        /// <summary>
        /// 查询数据解析脚本列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:product:parser:script:list")]
        public IActionResult QueryIotProductParserScript([FromQuery] IotProductParserScriptQueryDto parm)
        {
            var response = _IotProductParserScriptService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询数据解析脚本详情
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [HttpGet("{ProductId}")]
        [ActionPermissionFilter(Permission = "iot:product:parser:script:query")]
        public IActionResult GetIotProductParserScript(int ProductId)
        {
            var response = _IotProductParserScriptService.GetInfo(ProductId);

            var info = response.Adapt<IotProductParserScriptDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加数据解析脚本
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:product:parser:script:add")]
        [Log(Title = "数据解析脚本", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotProductParserScript([FromBody] IotProductParserScriptDto parm)
        {
            var modal = parm.Adapt<IotProductParserScript>().ToCreate(HttpContext);

            var response = _IotProductParserScriptService.AddIotProductParserScript(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新数据解析脚本
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:product:parser:script:edit")]
        [Log(Title = "数据解析脚本", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotProductParserScript([FromBody] IotProductParserScriptDto parm)
        {
            var modal = parm.Adapt<IotProductParserScript>().ToUpdate(HttpContext);
            var response = _IotProductParserScriptService.UpdateIotProductParserScript(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除数据解析脚本
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:product:parser:script:delete")]
        [Log(Title = "数据解析脚本", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotProductParserScript([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotProductParserScriptService.Delete(idArr));
        }
    }
}