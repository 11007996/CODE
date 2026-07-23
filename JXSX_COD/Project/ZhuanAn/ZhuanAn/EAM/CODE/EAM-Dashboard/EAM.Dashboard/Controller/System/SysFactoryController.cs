using EAM.Model.System.Dto;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-05-21
namespace EAM.Dashboard.Controller
{
    /// <summary>
    /// 厂区管理
    /// </summary>
    [Verify]
    [Route("system/factory")]
    public class SysFactoryController : BaseController
    {
        /// <summary>
        /// 厂区管理接口
        /// </summary>
        private readonly ISysFactoryService _FactoryService;

        public SysFactoryController(ISysFactoryService FactoryService)
        {
            _FactoryService = FactoryService;
        }

        /// <summary>
        /// 查询工厂字典
        /// </summary>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult QueryFactoryDict(SysFactoryQueryDto parm)
        {
            var response = _FactoryService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}