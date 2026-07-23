using EAM.Model.Dto;
using EAM.Service.Consumable.IConsumableService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-05-07
namespace EAM.Applet.Api.Controllers
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
        /// 查询储位信息列表树
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("treeList")]
        public IActionResult QueryTreeStorageSpace([FromQuery] ConsumableStorageSpaceTreeQueryDto parm)
        {
            var response = _StorageSpaceService.GetTreeList(parm);
            return SUCCESS(response);
        }
    }
}