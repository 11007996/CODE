using EAM.Model.Constant;
using EAM.Model.Consumable;
using EAM.Model.Enums;
using EAM.Model.Equipment;
using EAM.Model.Fixture;
using EAM.Model.Statistics;
using Infrastructure.Attribute;
using Newtonsoft.Json;
using Quartz;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAM.Tasks.TaskScheduler
{
    /// <summary>
    /// 定时任务：统计数据
    /// 使用如下注册后TaskExtensions里面不用再注册了
    /// </summary>
    [AppService(ServiceType = typeof(Job_StatisticsData), ServiceLifetime = LifeTime.Scoped)]
    public class Job_StatisticsData : JobBase, IJob
    {
        private List<string> JobParams = null; //厂区ID,集合

        public async Task Execute(IJobExecutionContext context)
        {
            var jobParam = context.JobDetail.JobDataMap["JobParam"];

            if (jobParam != null)
            {
                JobParams = JsonConvert.DeserializeObject<List<string>>(jobParam.ToString());
            }
            await ExecuteJob(context, Run);
        }

        /// <summary>
        /// 任务使用中注意：所有方法都需要使用异步，并且不能少了await
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            await Task.Delay(1);
            //TODO 业务逻辑
            if (JobParams != null && JobParams.Count > 0)
            {
                string errorMsg = string.Empty;
                foreach (var factoryId in JobParams)
                {
                    try
                    {
                        SqlSugarScopeProvider provider = DbScoped.SugarScope.GetConnectionScope(factoryId);
                        StatisticsEquipmentCount(provider);
                        StatisticsFixtureCount(provider);
                        StatisticsConsumableCount(provider);
                        StatisticsAssetTotalCost(provider);
                        StatisticsFixtureChangeForDay(provider);
                        StatisticsConsumableChangeForDay(provider);
                        provider.Close();
                    }
                    catch (Exception ex)
                    {
                        errorMsg += $"厂区[{factoryId}]执行异常,异常消息：${ex.Message}{Environment.NewLine}";
                    }
                }
                //抛出异常
                if (errorMsg != null && errorMsg.Length > 0)
                {
                    throw new Exception(errorMsg);
                }
            }
            else
            {
                throw new Exception("未传递有效的任务参数");
            }
        }

        /// <summary>
        /// 统计设备总数
        /// </summary>
        /// <param name="provider"></param>
        private void StatisticsEquipmentCount(SqlSugarScopeProvider provider)
        {
            int count = provider.Queryable<EquipmentBase>().Where(it => it.DelFlag == (int)DeleteFlagEnum.存在).Count();
            StatisticsData statistics = new()
            {
                StatDate = DateTime.Now.Date,
                UpdateTime = DateTime.Now,
                MetricName = StatisticsItemConstant.设备总数,
                MetricValue = count
            };
            var x = provider.Storageable(statistics)
                 .WhereColumns(it => new { it.StatDate, it.MetricName }, date => date.ToString("yyyy-MM-dd"))
                 .ToStorage();
            x.AsInsertable.ExecuteCommand(); //执行插入
            x.AsUpdateable.ExecuteCommand(); //执行更新
        }

        /// <summary>
        /// 统计治具总数
        /// </summary>
        /// <param name="provider"></param>
        private void StatisticsFixtureCount(SqlSugarScopeProvider provider)
        {
            int totalIdleQty = provider.Queryable<FixtureStorage>().Sum(it => it.Qty);
            int totalUsingQty = provider.Queryable<FixtureStorageUsing>().Sum(it => it.Qty);
            StatisticsData statistics = new()
            {
                StatDate = DateTime.Now.Date,
                UpdateTime = DateTime.Now,
                MetricName = StatisticsItemConstant.治具总数,
                MetricValue = totalIdleQty + totalUsingQty,
            };
            var x = provider.Storageable(statistics)
                 .WhereColumns(it => new { it.StatDate, it.MetricName }, date => date.ToString("yyyy-MM-dd"))
                 .ToStorage();
            x.AsInsertable.ExecuteCommand(); //执行插入
            x.AsUpdateable.ExecuteCommand(); //执行更新
        }

        /// <summary>
        /// 统计耗品总数
        /// </summary>
        private void StatisticsConsumableCount(SqlSugarScopeProvider provider)
        {
            int totalQty = provider.Queryable<ConsumableStorage>().Sum(it => it.Qty);
            StatisticsData statistics = new()
            {
                StatDate = DateTime.Now.Date,
                UpdateTime = DateTime.Now,
                MetricName = StatisticsItemConstant.耗品总数,
                MetricValue = totalQty,
            };
            var x = provider.Storageable(statistics)
            .WhereColumns(it => new { it.StatDate, it.MetricName }, date => date.ToString("yyyy-MM-dd"))
            .ToStorage();
            x.AsInsertable.ExecuteCommand(); //执行插入
            x.AsUpdateable.ExecuteCommand(); //执行更新
        }

        /// <summary>
        /// 统计总资产价值
        /// </summary>
        private void StatisticsAssetTotalCost(SqlSugarScopeProvider provider)
        {
            decimal totalCost = 0;
            //治具
            decimal? fixtureIdleCost = provider.Queryable<FixtureStorage>()
                .LeftJoin<FixtureBase>((fs, f) => fs.FixtureId == f.FixtureId)
                .Sum((fs, f) => fs.Qty * f.Price);
            decimal? fixtureUsingCost = provider.Queryable<FixtureStorageUsing>()
                .LeftJoin<FixtureBase>((fs, f) => fs.FixtureId == f.FixtureId)
                .Sum((fs, f) => fs.Qty * f.Price);
            if (fixtureIdleCost != null)
            {
                totalCost += (decimal)fixtureIdleCost;
            }
            if (fixtureUsingCost != null)
            {
                totalCost += (decimal)fixtureUsingCost;
            }
            //耗品
            decimal? consumabelCost = provider.Queryable<ConsumableStorage>()
                .LeftJoin<ConsumableBase>((cs, f) => cs.ConsumableId == f.ConsumableId)
                .Sum((cs, f) => cs.Qty * f.Price);
            if (consumabelCost != null)
            {
                totalCost += (decimal)consumabelCost;
            }

            StatisticsData statistics = new()
            {
                StatDate = DateTime.Now.Date,
                UpdateTime = DateTime.Now,
                MetricName = StatisticsItemConstant.总资产,
                MetricValue = totalCost,
            };
            var x = provider.Storageable(statistics)
             .WhereColumns(it => new { it.StatDate, it.MetricName }, date => date.ToString("yyyy-MM-dd"))
             .ToStorage();
            x.AsInsertable.ExecuteCommand(); //执行插入
            x.AsUpdateable.ExecuteCommand(); //执行更新
        }

        /// <summary>
        /// 统计当日治具变动
        /// </summary>
        private void StatisticsFixtureChangeForDay(SqlSugarScopeProvider provider)
        {
            DateTime startTime = DateTime.Now.Date;
            DateTime endTime = startTime.AddDays(1);
            //当日领用
            int receiveCount = provider.Queryable<FixtureStorageRecord>()
                .Where(it => it.CreateTime >= startTime && it.CreateTime < endTime && it.StorageChangeType == StorageChangeTypeConstant.领用)
                .Sum(it => it.ChangeQty);
            StatisticsData statisticsReceive = new()
            {
                StatDate = DateTime.Now.Date,
                UpdateTime = DateTime.Now,
                MetricName = StatisticsItemConstant.治具领用,
                MetricValue = Math.Abs(receiveCount),
            };
            var x = provider.Storageable(statisticsReceive)
                 .WhereColumns(it => new { it.StatDate, it.MetricName }, date => date.ToString("yyyy-MM-dd"))
                 .ToStorage();
            x.AsInsertable.ExecuteCommand(); //执行插入
            x.AsUpdateable.ExecuteCommand(); //执行更新

            //当日归还
            int backCount = provider.Queryable<FixtureStorageRecord>()
                .Where(it => it.CreateTime >= startTime && it.CreateTime < endTime && it.StorageChangeType == StorageChangeTypeConstant.归还)
                .Sum(it => it.ChangeQty);
            StatisticsData statisticsBack = new()
            {
                StatDate = DateTime.Now.Date,
                UpdateTime = DateTime.Now,
                MetricName = StatisticsItemConstant.治具归还,
                MetricValue = backCount,
            };
            x = provider.Storageable(statisticsBack)
                 .WhereColumns(it => new { it.StatDate, it.MetricName }, date => date.ToString("yyyy-MM-dd"))
                 .ToStorage();
            x.AsInsertable.ExecuteCommand(); //执行插入
            x.AsUpdateable.ExecuteCommand(); //执行更新
        }

        /// <summary>
        /// 统计当日耗品变动
        /// </summary>
        private void StatisticsConsumableChangeForDay(SqlSugarScopeProvider provider)
        {
            DateTime startTime = DateTime.Now.Date;
            DateTime endTime = startTime.AddDays(1);
            //当日领用
            int receiveCount = provider.Queryable<ConsumableStorageRecord>()
                .Where(it => it.CreateTime >= startTime && it.CreateTime < endTime && it.StorageChangeType == StorageChangeTypeConstant.领用)
                .Sum(it => it.ChangeQty);
            StatisticsData statisticsReceive = new()
            {
                StatDate = DateTime.Now.Date,
                UpdateTime = DateTime.Now,
                MetricName = StatisticsItemConstant.耗品领用,
                MetricValue = Math.Abs(receiveCount),
            };
            var x = provider.Storageable(statisticsReceive)
                 .WhereColumns(it => new { it.StatDate, it.MetricName }, date => date.ToString("yyyy-MM-dd"))
                 .ToStorage();
            x.AsInsertable.ExecuteCommand(); //执行插入
            x.AsUpdateable.ExecuteCommand(); //执行更新

            //当日入库
            int inCount = provider.Queryable<ConsumableStorageRecord>()
                .Where(it => it.CreateTime >= startTime && it.CreateTime < endTime && it.StorageChangeType == StorageChangeTypeConstant.入库)
                .Sum(it => it.ChangeQty);
            StatisticsData statisticsIn = new()
            {
                StatDate = DateTime.Now.Date,
                UpdateTime = DateTime.Now,
                MetricName = StatisticsItemConstant.耗品入库,
                MetricValue = inCount,
            };
            x = provider.Storageable(statisticsIn)
                 .WhereColumns(it => new { it.StatDate, it.MetricName }, date => date.ToString("yyyy-MM-dd"))
                 .ToStorage();
            x.AsInsertable.ExecuteCommand(); //执行插入
            x.AsUpdateable.ExecuteCommand(); //执行更新
        }
    }
}