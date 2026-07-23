using EAM.Dashboard.Model;
using EAM.Dashboard.Model.Dto;
using EAM.Dashboard.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace EAM.Dashboard.Controller
{
    [Verify]
    [Route("api/equipment")]
    public class EquipmentBaseController : BaseController
    {
        private readonly IEquipmentBaseService _EquipmentBaseService;

        public EquipmentBaseController(IEquipmentBaseService equipmentBaseService)
        {
            _EquipmentBaseService = equipmentBaseService;
        }

        /// <summary>
        /// 统计设备的状态
        /// </summary>
        /// <returns></returns>
        [HttpGet("state")]
        public IActionResult StatEquipmentState()
        {
            List<KanBanEquipmentState> list = _EquipmentBaseService.GetEquipmentStateStat();
            return SUCCESS(list);
        }

        /// <summary>
        /// 统计设备分布
        /// </summary>
        /// <returns></returns>
        [HttpGet("distribute")]
        public IActionResult StatEquipmentDistribute()
        {
            var list = _EquipmentBaseService.GetEquipmentDistributeStat();
            return SUCCESS(list);
        }

        /// <summary>
        /// 获取实时上报记录，每个设备最后上报的6条记录
        /// </summary>
        /// <returns></returns>
        [HttpGet("record")]
        public IActionResult EquipmentLastRunRecord()
        {
            List<EquipmentRuningReportVo> list = _EquipmentBaseService.GetLastEquipmentRuningRecord(10,10);
            return SUCCESS(list);
        }

        /// <summary>
        /// 实时故障，平均oee,设备实时状态，设备oee
        /// </summary>
        /// <returns></returns>
        [HttpGet("oee")]
        public IActionResult EquipmentOEEStat()
        {
            var r = _EquipmentBaseService.StatEquipmentOEE();
            return SUCCESS(r);
        }

        /// <summary>
        /// 能耗统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("energy")]
        public IActionResult EquipmentEnergyStat()
        {
            var r = _EquipmentBaseService.StatEquipmentEnergy();
            return SUCCESS(r);
        }

        /// <summary>
        /// 不同设备状态数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("runState")]
        public IActionResult StatEquipmentStateCount()
        {
            var r = _EquipmentBaseService.StatEquipmentStateCount(10);
            return SUCCESS(r);
        }

        /// <summary>
        /// 获取当前报警设备
        /// </summary>
        /// <returns></returns>
        [HttpGet("warnEquipment")]
        public IActionResult GetWarnEquipment()
        {
            var r = _EquipmentBaseService.GetWarnEquipmentRuningRecord(10);
            return SUCCESS(r);
        }

        /// <summary>
        /// 当日产线设备的产能占比
        /// </summary>
        /// <returns></returns>
        [HttpGet("lineProductRate")]
        public IActionResult StatEquipmentProductRate()
        {
            var r = _EquipmentBaseService.StatEquipmentProductRate();
            return SUCCESS(r);
        }

        /// <summary>
        /// 产线的性能开动率
        /// </summary>
        /// <returns></returns>
        [HttpGet("linePerformanceRate")]
        public IActionResult StatLinePerformanceRate()
        {
            var r = _EquipmentBaseService.StatLinePerformanceRate(7);
            return SUCCESS(r);
        }

        /// <summary>
        /// 统计设备故障时长排序
        /// </summary>
        /// <returns></returns>
        [HttpGet("faultTime")]
        public IActionResult StatEquipmentFaultTime()
        {
            var r = _EquipmentBaseService.StatEquipmentFaultTime(7);
            return SUCCESS(r);
        }


        /// <summary>
        /// 统计每条线故障时间最少的生技组长排行榜
        /// </summary>
        /// <returns></returns>
        [HttpGet("topEmp")]
        public IActionResult StatTopEmp()
        {
            var r = _EquipmentBaseService.StatTopEmp(3);
            return SUCCESS(r);
        }
    }
}