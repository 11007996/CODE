using EAM.Model.Dto;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-09-30
namespace EAM.Applet.Api.Controllers
{
    /// <summary>
    /// 设备资产信息
    /// </summary>
    [Verify]
    [Route("equipment/EquipmentBase")]
    public class EquipmentBaseController : BaseController
    {
        /// <summary>
        /// 设备资产信息接口
        /// </summary>
        private readonly IEquipmentBaseService _EquipmentService;

        public EquipmentBaseController(IEquipmentBaseService EquipmentService)
        {
            _EquipmentService = EquipmentService;
        }

        /// <summary>
        /// 查询设备资产信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult QueryEquipmentBase([FromQuery] EquipmentBaseQueryDto parm)
        {
            var response = _EquipmentService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备资产信息详情
        /// </summary>
        /// <param name="AssetNo"></param>
        /// <returns></returns>
        [HttpGet("{AssetNo}")]
        public IActionResult GetEquipmentBase(string AssetNo)
        {
            var response = _EquipmentService.GetInfoByAssetNo(AssetNo);

            var info = response.Adapt<EquipmentBaseDto>();
            return SUCCESS(info);
        }
    }
}