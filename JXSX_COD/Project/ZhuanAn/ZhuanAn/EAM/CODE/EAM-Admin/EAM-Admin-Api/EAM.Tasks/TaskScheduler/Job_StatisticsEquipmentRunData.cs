using EAM.Service.Statistics;
using Infrastructure.Attribute;
using Newtonsoft.Json;
using Quartz;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAM.Tasks.TaskScheduler
{
    /// <summary>
    /// 定时任务：统计分析设备运行数据
    /// 使用如下注册后TaskExtensions里面不用再注册了
    /// </summary>
    [AppService(ServiceType = typeof(Job_StatisticsEquipmentRunData), ServiceLifetime = LifeTime.Scoped)]
    public class Job_StatisticsEquipmentRunData : JobBase, IJob
    {
        private List<string> JobParams = null; //厂区ID集合

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
                foreach (string factoryId in JobParams)
                {
                    try
                    {
                        StatisticsEquipmentRunData(factoryId);
                    }
                    catch (Exception ex)
                    {
                        errorMsg += $"厂区[{factoryId}]执行异常,异常消息：${ex.Message}{Environment.NewLine}";
                    }
                }
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
        /// 统计设备运行数据
        /// </summary>
        private void StatisticsEquipmentRunData(string factoryId)
        {
            DateTime startDate = DateTime.Now.AddDays(-1);
            DateTime endDate = DateTime.Now;
            StatEquipmentRuningRecordService service = new StatEquipmentRuningRecordService(DbScoped.SugarScope.GetConnectionScope(factoryId));
            service.StatEquipmentRunData(null, startDate, endDate);
        }
    }
}