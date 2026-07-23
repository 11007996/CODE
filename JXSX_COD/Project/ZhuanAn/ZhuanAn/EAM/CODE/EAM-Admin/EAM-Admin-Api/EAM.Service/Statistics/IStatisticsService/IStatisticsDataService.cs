using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Statistics;

namespace EAM.Service.Statistics.IStatisticsService
{
    /// <summary>
    /// 统计数据service接口
    /// </summary>
    public interface IStatisticsDataService : IBaseService<StatisticsData>
    {
        PagedInfo<StatisticsDataDto> GetList(StatisticsDataQueryDto parm);

        StatisticsData GetInfo(int Id);

        StatisticsData AddStatisticsData(StatisticsData parm);

        int UpdateStatisticsData(StatisticsData parm);

        List<StatisticsData> GetNewestStatisticsData(StatisticsDataNewestQueryDto parm);

        Dictionary<string, List<StatisticsData>> GetNewestStatisticsDataForDays(StatisticsDataNewestQueryDto parm);
    }
}