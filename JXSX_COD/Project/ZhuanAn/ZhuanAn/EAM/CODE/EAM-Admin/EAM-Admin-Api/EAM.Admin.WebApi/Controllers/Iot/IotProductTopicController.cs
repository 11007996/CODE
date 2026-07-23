using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Service.Iot.IIotService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-01-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 产品主题类表
    /// </summary>
    [Verify]
    [Route("iot/IotProductTopic")]
    public class IotProductTopicController : BaseController
    {
        /// <summary>
        /// 产品主题类表接口
        /// </summary>
        private readonly IIotProductTopicService _IotProductTopicService;

        public IotProductTopicController(IIotProductTopicService IotProductTopicService)
        {
            _IotProductTopicService = IotProductTopicService;
        }

        /// <summary>
        /// 查询产品主题类表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "iot:product:topic:list")]
        public IActionResult QueryIotProductTopic([FromQuery] IotProductTopicQueryDto parm)
        {
            var response = _IotProductTopicService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询产品主题类表详情
        /// </summary>
        /// <param name="TopicId"></param>
        /// <returns></returns>
        [HttpGet("{TopicId}")]
        [ActionPermissionFilter(Permission = "iot:product:topic:query")]
        public IActionResult GetIotProductTopic(int TopicId)
        {
            var response = _IotProductTopicService.GetInfo(TopicId);

            var info = response.Adapt<IotProductTopicDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加产品主题类表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "iot:product:topic:add")]
        [Log(Title = "产品主题类表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddIotProductTopic([FromBody] IotProductTopicDto parm)
        {
            var modal = parm.Adapt<IotProductTopic>().ToCreate(HttpContext);

            var response = _IotProductTopicService.AddIotProductTopic(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新产品主题类表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "iot:product:topic:edit")]
        [Log(Title = "产品主题类表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateIotProductTopic([FromBody] IotProductTopicDto parm)
        {
            var modal = parm.Adapt<IotProductTopic>().ToUpdate(HttpContext);
            var response = _IotProductTopicService.UpdateIotProductTopic(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除产品主题类表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "iot:product:topic:delete")]
        [Log(Title = "产品主题类表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteIotProductTopic([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_IotProductTopicService.Delete(idArr));
        }
    }
}