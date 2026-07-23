using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Controllers;
using Infrastructure.Enums;
using LxMail.Models;
using LxMail.Models.Dto;
using LxMail.Service;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace LxMail.Controllers
{
    [Route("SysAutoMail")]
    public class SysAutoMailController : BaseController
    {
        private readonly ISysAutoMailService AutoMailService;

        public SysAutoMailController(ISysAutoMailService autoMailService)
        {
            AutoMailService = autoMailService;
        }

        [HttpGet("{id}")]
        public IActionResult GetSysAutoMail(long id)
        {
            var res = AutoMailService.GetSysSendMail(id);
            return SUCCESS(res);
        }

        [HttpPost("add")]
        [Log(Title = "系统邮件", BusinessType = BusinessType.INSERT)]
        public IActionResult AddSysAutoMail([FromBody] SysAutoMailDto parm)
        {
            var model = parm.Adapt<SysAutoMail>().ToCreate(context: HttpContext);
            var response = AutoMailService.AddSysAutoMail(model);
            return SUCCESS(response.MailID);
        }
    }
}