using EAM.Model.Dto;
using EAM.Service.Statistics.IStatisticsService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-09-10
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 统计设备运行记录
    /// </summary>
    [Verify]
    [Route("statistics/StatEquipmentRuningRecord")]
    public class StatEquipmentRuningRecordController : BaseController
    {
        /// <summary>
        /// 统计设备运行记录接口
        /// </summary>
        private readonly IStatEquipmentRuningRecordService _StatEquipmentRuningRecordService;

        public StatEquipmentRuningRecordController(IStatEquipmentRuningRecordService StatEquipmentRuningRecordService)
        {
            _StatEquipmentRuningRecordService = StatEquipmentRuningRecordService;
        }

        /// <summary>
        /// 查询统计设备运行记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "stat:equipment:runing:record:list")]
        public IActionResult QueryStatEquipmentRuningRecord([FromQuery] StatEquipmentRuningRecordQueryDto parm)
        {
            var response = _StatEquipmentRuningRecordService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询统计设备运行记录详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "stat:equipment:runing:record:query")]
        public IActionResult GetStatEquipmentRuningRecord(int Id)
        {
            var response = _StatEquipmentRuningRecordService.GetInfo(Id);

            var info = response.Adapt<StatEquipmentRuningRecordDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 统计设备运行数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("stat")]
        [ActionPermissionFilter(Permission = "stat:equipment:runing:record:stat")]
        public IActionResult StatEquipmentRuningRecord()
        {
            var response = _StatEquipmentRuningRecordService.StatEquipmentRunData(null, DateTime.Now, DateTime.Now);
            return SUCCESS(response);
        }
    }
}