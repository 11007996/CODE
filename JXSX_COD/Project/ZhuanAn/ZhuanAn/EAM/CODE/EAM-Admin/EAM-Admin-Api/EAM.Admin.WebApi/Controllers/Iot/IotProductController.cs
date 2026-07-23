using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品表
    /// </summary>
    [Verify]
    [Route("iot/IotProduct")]
    public class IotProductController : BaseController
    {
        /// <summary>
        /// 产品表接口
        /// </summary>
        private readonly IIotProductService _IotProductService;

        public IotProductController(IIotProductService IotProductService)
        {
            _IotProductService = IotProductService;
        }

        /// <summary>
        /// 查询产品表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:product:list")]
        public IActionResult QueryIotProduct([FromQuery] IotProductQueryDto parm)
        {
            var response = _IotProductService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产品表详情
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [HttpGet("{ProductId}")]
        [ActionPermissionFilter(Permission = "iot:product:query")]
        public IActionResult GetIotProduct(int ProductId)
        {
            var response = _IotProductService.GetInfo(ProductId);

            var info = response.Adapt<IotProductDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产品表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:product:add")]
        [Log(Title = "产品表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotProduct([FromBody] IotProductDto parm)
        {
            var modal = parm.Adapt<IotProduct>().ToCreate(HttpContext);

            var response = _IotProductService.AddIotProduct(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产品表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:product:edit")]
        [Log(Title = "产品表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotProduct([FromBody] IotProductDto parm)
        {
            var modal = parm.Adapt<IotProduct>().ToUpdate(HttpContext);
            var response = _IotProductService.UpdateIotProduct(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产品表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:product:delete")]
        [Log(Title = "产品表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotProduct([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotProductService.Delete(idArr));
        }

        /// <summary>
        /// 发布产品
        /// </summary>
        /// <returns></returns>
        [HttpPut("release/{id}")]
        [ActionPermissionFilter(Permission = "iot:product:release")]
        [Log(Title = "产品表", BusinessType = BusinessType.UPDATE)]
        public IActionResult ReleaseIotProduct([FromRoute] int id)
        {
            return ToResponse(_IotProductService.ReleaseIotProduct(id));
        }

        /// <summary>
        /// 查询Iot产品字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictIotProduct([FromQuery] IotProductQueryDto parm)
        {
            var response = _IotProductService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}