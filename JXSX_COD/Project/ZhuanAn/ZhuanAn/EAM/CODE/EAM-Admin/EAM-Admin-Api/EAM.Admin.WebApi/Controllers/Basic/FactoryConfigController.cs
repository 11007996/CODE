using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Service.Basic.IBasicService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-09-24
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 厂区配置
    /// </summary>
    [Verify]
    [Route("basic/FactoryConfig")]
    public class FactoryConfigController : BaseController
    {
        /// <summary>
        /// 厂区配置接口
        /// </summary>
        private readonly IFactoryConfigService _FactoryConfigService;

        public FactoryConfigController(IFactoryConfigService FactoryConfigService)
        {
            _FactoryConfigService = FactoryConfigService;
        }

        /// <summary>
        /// 查询厂区配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "factory:config:list")]
        public IActionResult QueryFactoryConfig([FromQuery] FactoryConfigQueryDto parm)
        {
            var response = _FactoryConfigService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询厂区配置详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "factory:config:query")]
        public IActionResult GetFactoryConfig(int Id)
        {
            var response = _FactoryConfigService.GetInfo(Id);

            var info = response.Adapt<FactoryConfigDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 查询厂区配置详情
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [HttpGet("key/{keys}")]
        public IActionResult GetFactoryConfig(string keys)
        {
            string[] keyArr = Tools.SplitAndConvert<string>(keys);
            List<FactoryConfig> response = new();
            foreach (string key in keyArr)
            {
                FactoryConfig config = _FactoryConfigService.GetInfoByKey(key);
                if (config != null)
                    response.Add(config);
            }

            var info = response.Adapt<List<FactoryConfigDto>>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加厂区配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "factory:config:add")]
        [Log(Title = "厂区配置", BusinessType = BusinessType.INSERT)]
        public IActionResult AddFactoryConfig([FromBody] FactoryConfigDto parm)
        {

            var modal = parm.Adapt<FactoryConfig>().ToUpdate(HttpContext);

            var response = _FactoryConfigService.AddFactoryConfig(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新厂区配置
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "factory:config:edit")]
        [Log(Title = "厂区配置", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateFactoryConfig([FromBody] FactoryConfigDto parm)
        {
            var modal = parm.Adapt<FactoryConfig>().ToUpdate(HttpContext);
            var response = _FactoryConfigService.UpdateFactoryConfig(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除厂区配置
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "factory:config:delete")]
        [Log(Title = "厂区配置", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteFactoryConfig([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_FactoryConfigService.Delete(idArr));
        }
    }
}