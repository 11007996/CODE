using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Statistics;
using EAM.Repository;
using EAM.Service.Statistics.IStatisticsService;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Statistics
{
    /// <summary>
    /// 统计数据Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IStatisticsDataService), ServiceLifetime = LifeTime.Transient)]
    public class StatisticsDataService : BaseService<StatisticsData>, IStatisticsDataService
    {
        public StatisticsDataService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询统计数据列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<StatisticsDataDto> GetList(StatisticsDataQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<StatisticsData, StatisticsDataDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public StatisticsData GetInfo(int Id)
        {
            var response = Queryable()
                .Where(x => x.Id == Id)
                .First();

            return response;
        }

        /// <summary>
        /// 添加统计数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public StatisticsData AddStatisticsData(StatisticsData model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改统计数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateStatisticsData(StatisticsData model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 获取最新的数据
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public List<StatisticsData> GetNewestStatisticsData(StatisticsDataNewestQueryDto parm)
        {
            string[] names = parm.Names.Split(",");
            var r = Queryable()
                 .Where(it => names.Contains(it.MetricName))
                 .WhereIF(!string.IsNullOrEmpty(parm.Key), it => it.MetricKey == parm.Key)
                 .OrderBy(it => it.UpdateTime, OrderByType.Desc)
                 .Take(1)
                 .PartitionBy(it => it.MetricName)
                 .ToList();

            return r;
        }

        /// <summary>
        /// 获取指定天数的最新的数据
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public Dictionary<string, List<StatisticsData>> GetNewestStatisticsDataForDays(StatisticsDataNewestQueryDto parm)
        {
            var days = Enumerable.Range(0, (int)parm.Days).Select(it => DateTime.Now.Date.AddDays(it * -1)).ToList();

            Dictionary<string, List<StatisticsData>> r = new();
            string[] names = parm.Names.Split(",");
            foreach (var name in names)
            {
                var queryableLeft = Context.Reportable(days).ToQueryable<DateTime>();
                var queryableRight = Queryable()
                 .Where(it => it.MetricName == name)
                 .WhereIF(!string.IsNullOrEmpty(parm.Key), it => it.MetricKey == parm.Key);
                var sr = queryableLeft
                         .LeftJoin(queryableRight, (x1, x2) => x1.ColumnName.ToString("yyyy-MM-dd") == ((DateTime)x2.StatDate).ToString("yyyy-MM-dd"))
                         .Select((x1, x2) => new
                         {
                             rowNum = SqlFunc.RowNumber($"{x2.UpdateTime} desc", x1.ColumnName),
                             StatDate = x1.ColumnName,
                             MetricValue = SqlFunc.IIF(x2.MetricValue > 0, x2.MetricValue, 0)
                         })
                        .MergeTable()//将结果合并成一个表
                        .Where(it => it.rowNum == 1) //相同的name只取一条记录
                        .Select(it => new StatisticsData()
                        {
                            MetricName = it.StatDate.ToString("MM-dd"),
                            MetricValue = it.MetricValue
                        })
                       .ToList();
                r.Add(name, sr);
            }
            return r;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<StatisticsData> QueryExp(StatisticsDataQueryDto parm)
        {
            var predicate = Expressionable.Create<StatisticsData>();

            return predicate;
        }
    }
}