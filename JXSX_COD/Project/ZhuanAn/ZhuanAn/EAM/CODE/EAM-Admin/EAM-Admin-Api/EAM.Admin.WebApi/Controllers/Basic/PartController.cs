using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Service.Basic.IBasicService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-05-15
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 料号
    /// </summary>
    [Verify]
    [Route("basic/part")]
    public class PartController : BaseController
    {
        /// <summary>
        /// 料号接口
        /// </summary>
        private readonly IPartService _PartService;

        public PartController(IPartService PartService)
        {
            _PartService = PartService;
        }

        /// <summary>
        /// 查询料号列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "part:list")]
        public IActionResult QueryPart([FromQuery] PartQueryDto parm)
        {
            var response = _PartService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询料号详情
        /// </summary>
        /// <param name="PartId"></param>
        /// <returns></returns>
        [HttpGet("{PartId}")]
        [ActionPermissionFilter(Permission = "part:query")]
        public IActionResult GetPart(int PartId)
        {
            var response = _PartService.GetInfo(PartId);

            var info = response.Adapt<PartDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加料号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "part:add")]
        [Log(Title = "料号", BusinessType = BusinessType.INSERT)]
        public IActionResult AddPart([FromBody] PartDto parm)
        {
            var modal = parm.Adapt<Part>().ToCreate(HttpContext);

            var response = _PartService.AddPart(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新料号
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "part:edit")]
        [Log(Title = "产线信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateLine([FromBody] PartDto parm)
        {
            var modal = parm.Adapt<Part>().ToUpdate(HttpContext);
            var response = _PartService.UpdatePart(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除料号
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "part:delete")]
        [Log(Title = "料号", BusinessType = BusinessType.DELETE)]
        public IActionResult DeletePart([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<string>(ids);

            return ToResponse(_PartService.Delete(idArr));
        }

        /// <summary>
        /// 查询料号字典列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult QueryPartDict([FromQuery] PartQueryDto parm)
        {
            var response = _PartService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}