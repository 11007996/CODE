using EAM.Dashboard.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace EAM.Dashboard.Controller
{
    [Verify]
    [Route("api/callScheduledTask")]
    public class CallScheduledTaskController : BaseController
    {
        private readonly ICallFaultBaseService service;

        public CallScheduledTaskController(ICallFaultBaseService callService)
        {
            service = callService;
        }

        /// <summary>
        /// 获取定时播放的任务
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpGet("{areaId}")]
        public IActionResult GetCallScheduledTask([FromRoute] int areaId, [FromQuery] int second)
        {
            var response = service.GetCallScheduledTask(areaId, second);
            return SUCCESS(response);
        }
    }
}