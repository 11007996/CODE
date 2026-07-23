using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品物模型属性
    /// </summary>
    [Verify]
    [Route("iot/IotProductThingProperty")]
    public class IotProductThingPropertyController : BaseController
    {
        /// <summary>
        /// 产品物模型属性接口
        /// </summary>
        private readonly IIotProductThingPropertyService _IotProductThingPropertyService;

        public IotProductThingPropertyController(IIotProductThingPropertyService IotProductThingPropertyService)
        {
            _IotProductThingPropertyService = IotProductThingPropertyService;
        }

        /// <summary>
        /// 查询产品物模型属性列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:product:thing:property:list")]
        public IActionResult QueryIotProductThingProperty([FromQuery] IotProductThingPropertyQueryDto parm)
        {
            var response = _IotProductThingPropertyService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产品物模型属性详情
        /// </summary>
        /// <param name="PropertyId"></param>
        /// <returns></returns>
        [HttpGet("{PropertyId}")]
        [ActionPermissionFilter(Permission = "iot:product:thing:property:query")]
        public IActionResult GetIotProductThingProperty(int PropertyId)
        {
            var response = _IotProductThingPropertyService.GetInfo(PropertyId);

            var info = response.Adapt<IotProductThingPropertyDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产品物模型属性
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:product:thing:property:add")]
        [Log(Title = "产品物模型属性", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotProductThingProperty([FromBody] IotProductThingPropertyDto parm)
        {
            var modal = parm.Adapt<IotProductThingProperty>().ToCreate(HttpContext);

            var response = _IotProductThingPropertyService.AddIotProductThingProperty(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产品物模型属性
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:product:thing:property:edit")]
        [Log(Title = "产品物模型属性", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotProductThingProperty([FromBody] IotProductThingPropertyDto parm)
        {
            var modal = parm.Adapt<IotProductThingProperty>().ToUpdate(HttpContext);
            var response = _IotProductThingPropertyService.UpdateIotProductThingProperty(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产品物模型属性
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:product:thing:property:delete")]
        [Log(Title = "产品物模型属性", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotProductThingProperty([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotProductThingPropertyService.Delete(idArr));
        }


        #region 属性扩展
        /// <summary>
        /// 查询属性扩展描述详情
        /// </summary>
        /// <param name="PropertyId"></param>
        /// <returns></returns>
        [HttpGet("extend/{PropertyId}")]
        [ActionPermissionFilter(Permission = "iot:product:thing:property:query")]
        public IActionResult GetIotProductThingPropertyExtend(int PropertyId)
        {
            var response = _IotProductThingPropertyService.GetExtendInfo(PropertyId);

            var info = response.Adapt<IotProductThingPropertyExtendDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加属性扩展描述
        /// </summary>
        /// <returns></returns>
        [HttpPost("extend")]
        [ActionPermissionFilter(Permission = "iot:product:thing:property:add")]
        [Log(Title = "属性扩展描述", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotProductThingPropertyExtend([FromBody] IotProductThingPropertyExtendDto parm)
        {
            var modal = parm.Adapt<IotProductThingPropertyExtend>().ToCreate(HttpContext);

            var response = _IotProductThingPropertyService.AddIotProductThingPropertyExtend(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新属性扩展描述
        /// </summary>
        /// <returns></returns>
        [HttpPut("extend")]
        [ActionPermissionFilter(Permission = "iot:product:thing:property:edit")]
        [Log(Title = "属性扩展描述", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotProductThingPropertyExtend([FromBody] IotProductThingPropertyExtendDto parm)
        {
            var modal = parm.Adapt<IotProductThingPropertyExtend>().ToUpdate(HttpContext);
            var response = _IotProductThingPropertyService.UpdateIotProductThingPropertyExtend(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除属性扩展描述
        /// </summary>
        /// <returns></returns>
        [HttpDelete("extend/delete/{id}")]
        [ActionPermissionFilter(Permission = "iot:product:thing:property:delete")]
        [Log(Title = "属性扩展描述", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotProductThingPropertyExtend([FromRoute] int id)
        {
            return ToResponse(_IotProductThingPropertyService.DeleteIotProductThingPropertyExtend(id));
        }
        #endregion
    }
}