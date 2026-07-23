using EAM.Dashboard.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace EAM.Dashboard.Controller
{
    [Verify]
    [Route("api/business")]
    public class BusinessController : BaseController
    {
        private readonly IBusinessService service;

        public BusinessController(IBusinessService businessService)
        {
            service = businessService;
        }

        [HttpGet("simpleOnlineNoticeTicket/list")]
        public IActionResult ListSimpleOnlineNoticeTicket()
        {
            var r = service.GetSimpleOnlineNoticeTicketList();
            return SUCCESS(r);
        }

        [HttpGet("simpleOnlineNoticeTicket/{ticketNo}")]
        public IActionResult GetSimpleOnlineNoticeTicketInfo([FromRoute] string ticketNo)
        {
            var r = service.GetSimpleOnlineNoticeTicketInfo(ticketNo);
            return SUCCESS(r);
        }
    }
}