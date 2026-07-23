using EAM.Model.Consumable;
using EAM.Model.Dto;
using EAM.Service.Consumable.IConsumableService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-05-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 耗品储位信息
    /// </summary>
    [Verify]
    [Route("consumable/storageSpace")]
    public class ConsumableStorageSpaceController : BaseController
    {
        /// <summary>
        /// 储位信息接口
        /// </summary>
        private readonly IConsumableStorageSpaceService _StorageSpaceService;

        public ConsumableStorageSpaceController(IConsumableStorageSpaceService StorageSpaceService)
        {
            _StorageSpaceService = StorageSpaceService;
        }

        /// <summary>
        /// 查询储位信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "consumable:storageSpace:list")]
        public IActionResult QueryStorageSpace([FromQuery] ConsumableStorageSpaceQueryDto parm)
        {
            var response = _StorageSpaceService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询储位信息列表树
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("treeList")]
        [ActionPermissionFilter(Permission = "consumable:storageSpace:list")]
        public IActionResult QueryTreeStorageSpace([FromQuery] ConsumableStorageSpaceTreeQueryDto parm)
        {
            var response = _StorageSpaceService.GetTreeList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询储位信息详情
        /// </summary>
        /// <param name="StorageId"></param>
        /// <returns></returns>
        [HttpGet("{StorageId}")]
        [ActionPermissionFilter(Permission = "consumable:storageSpace:query")]
        public IActionResult GetStorageSpace(int StorageId)
        {
            var response = _StorageSpaceService.GetInfo(StorageId);

            var info = response.Adapt<ConsumableStorageSpaceDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加储位信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "consumable:storageSpace:add")]
        [Log(Title = "储位信息", BusinessType = BusinessType.INSERT)]
        public IActionResult AddStorageSpace([FromBody] ConsumableStorageSpaceDto parm)
        {
            var modal = parm.Adapt<ConsumableStorageSpace>().ToCreate(HttpContext);

            var response = _StorageSpaceService.AddStorageSpace(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新储位信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "consumable:storageSpace:edit")]
        [Log(Title = "储位信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateStorageSpace([FromBody] ConsumableStorageSpaceDto parm)
        {
            var modal = parm.Adapt<ConsumableStorageSpace>().ToUpdate(HttpContext);
            var response = _StorageSpaceService.UpdateStorageSpace(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除储位信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ActionPermissionFilter(Permission = "consumable:storageSpace:delete")]
        [Log(Title = "储位信息", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteStorageSpace([FromRoute] int id)
        {
            return ToResponse(_StorageSpaceService.DeleteStorageSpace(id));
        }
    }
}