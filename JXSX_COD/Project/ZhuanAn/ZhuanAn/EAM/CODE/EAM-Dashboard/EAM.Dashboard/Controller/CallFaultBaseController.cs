using EAM.Dashboard.Model.Dto;
using EAM.Dashboard.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace EAM.Dashboard.Controller
{
    [Verify]
    [Route("api/callFaultBase")]
    public class CallFaultBaseController : BaseController
    {
        private readonly ICallFaultBaseService service;

        public CallFaultBaseController(ICallFaultBaseService callService)
        {
            service = callService;
        }

        /// <summary>
        /// 获取广播区域信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("area/list")]
        public IActionResult GetAreaList()
        {
            var r = service.GetAreaList();
            return SUCCESS(r);
        }

        /// <summary>
        /// 未解决呼叫
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpGet("unsolved/{areaId}")]
        public IActionResult GetCallDiffCountStat([FromRoute] int areaId)
        {
            var r = service.GetUnsolvedCallFaultBase(areaId);
            return SUCCESS(r);
        }

        /// <summary>
        /// 月、周呼叫次数
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpGet("count/{areaId}")]
        public IActionResult CallCountStat([FromRoute] int areaId)
        {
            CallFaultCountStat r = new CallFaultCountStat();
            r.MonthData = service.GetMonthCountStat(areaId);
            r.WeekData = service.GetWeekCountStat(areaId);
            return SUCCESS(r);
        }

        /// <summary>
        /// 24小时呼叫次数
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpGet("count/hour/{areaId}")]
        public IActionResult CallCountStatForHour([FromRoute] int areaId)
        {
            CallFaultCountStat r = new CallFaultCountStat();
            r.HourData = service.GetHourCountStat(areaId);
            return SUCCESS(r);
        }

        /// <summary>
        /// 本周呼叫数据分析
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpGet("week/{areaId}")]
        public IActionResult WeekDataAnalyseStat([FromRoute] int areaId)
        {
            var response = service.GetWeekDataAnalyseStat(areaId);
            return SUCCESS(response);
        }
    }
}