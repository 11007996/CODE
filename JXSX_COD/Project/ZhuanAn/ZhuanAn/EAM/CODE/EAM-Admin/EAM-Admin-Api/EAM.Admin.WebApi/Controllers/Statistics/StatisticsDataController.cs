using EAM.Model.Dto;
using EAM.Model.Statistics;
using EAM.Service.Statistics.IStatisticsService;

using Microsoft.AspNetCore.Mvc;

//创建时间：2024-08-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 统计数据
    /// </summary>
    [Verify]
    [Route("statistics/StatStatisticsData")]
    public class StatisticsDataController : BaseController
    {
        /// <summary>
        /// 统计数据接口
        /// </summary>
        private readonly IStatisticsDataService _StatisticsDataService;

        public StatisticsDataController(IStatisticsDataService StatisticsDataService)
        {
            _StatisticsDataService = StatisticsDataService;
        }

        /// <summary>
        /// 查询统计数据列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "statisticsdata:list")]
        public IActionResult QueryStatisticsData([FromQuery] StatisticsDataQueryDto parm)
        {
            var response = _StatisticsDataService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询统计数据详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "statisticsdata:query")]
        public IActionResult GetStatisticsData(int Id)
        {
            var response = _StatisticsDataService.GetInfo(Id);

            var info = response.Adapt<StatisticsDataDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加统计数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "statisticsdata:add")]
        [Log(Title = "统计数据", BusinessType = BusinessType.INSERT)]
        public IActionResult AddStatisticsData([FromBody] StatisticsDataDto parm)
        {
            var modal = parm.Adapt<StatisticsData>().ToCreate(HttpContext);

            var response = _StatisticsDataService.AddStatisticsData(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新统计数据
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "statisticsdata:edit")]
        [Log(Title = "统计数据", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateStatisticsData([FromBody] StatisticsDataDto parm)
        {
            var modal = parm.Adapt<StatisticsData>().ToUpdate(HttpContext);
            var response = _StatisticsDataService.UpdateStatisticsData(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除统计数据
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "statisticsdata:delete")]
        [Log(Title = "统计数据", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteStatisticsData([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_StatisticsDataService.Delete(idArr));
        }

        /// <summary>
        /// 查询最新统计数据
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("newest")]
        public IActionResult GetNewestStatisticsData(StatisticsDataNewestQueryDto parm)
        {
            var response = _StatisticsDataService.GetNewestStatisticsData(parm);

            var list = response.Adapt<List<StatisticsDataDto>>();
            return SUCCESS(list);
        }

        /// <summary>
        /// 根据[天数]查询最新统计数据
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("days")]
        public IActionResult GetNewestStatisticsDataForDays(StatisticsDataNewestQueryDto parm)
        {
            var response = _StatisticsDataService.GetNewestStatisticsDataForDays(parm);

            var list = response.Adapt<Dictionary<string, List<StatisticsDataDto>>>();
            return SUCCESS(list);
        }
    }
}