using EAM.Model.System;
using EAM.Model.System.Dto;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-05-21
namespace EAM.Admin.WebApi.Controllers
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
        /// 查询厂区管理列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "factory:list")]
        public IActionResult QueryFactory([FromQuery] SysFactoryQueryDto parm)
        {
            var response = _FactoryService.GetList(parm);
            return SUCCESS(response);
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

        /// <summary>
        /// 查询厂区管理详情列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("detailList")]
        [ActionPermissionFilter(Permission = "factory:list")]
        public IActionResult QueryFactoryDetail([FromQuery] SysFactoryQueryDto parm)
        {
            var response = _FactoryService.GetDetailList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询厂区管理详情
        /// </summary>
        /// <param name="FactoryId"></param>
        /// <returns></returns>
        [HttpGet("{FactoryId}")]
        public IActionResult GetFactory(string FactoryId)
        {
            var response = _FactoryService.GetInfo(FactoryId);

            var info = response.Adapt<SysFactoryDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加厂区管理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "factory:add")]
        [Log(Title = "厂区管理", BusinessType = BusinessType.INSERT)]
        public IActionResult AddFactory([FromBody] SysFactoryDto parm)
        {
            var modal = parm.Adapt<SysFactory>().ToCreate(HttpContext);

            var response = _FactoryService.AddFactory(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新厂区管理
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "factory:edit")]
        [Log(Title = "厂区管理", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateFactory([FromBody] SysFactoryDto parm)
        {
            var modal = parm.Adapt<SysFactory>().ToUpdate(HttpContext);
            var response = _FactoryService.UpdateFactory(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除厂区管理
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "factory:delete")]
        [Log(Title = "厂区管理", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteFactory([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<string>(ids);

            return ToResponse(_FactoryService.Delete(idArr));
        }
    }
}