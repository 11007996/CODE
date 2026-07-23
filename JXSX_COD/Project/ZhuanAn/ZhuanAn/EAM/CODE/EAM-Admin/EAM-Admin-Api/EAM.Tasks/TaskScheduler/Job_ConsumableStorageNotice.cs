using EAM.Model.Consumable;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.ServiceCore.Model.Enums;
using EAM.ServiceCore.Services;
using Infrastructure.Attribute;
using Newtonsoft.Json;
using Quartz;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAM.Tasks.TaskScheduler
{
    /// <summary>
    /// 定时任务：耗品库存数量不足，提醒相关人员
    /// 使用如下注册后TaskExtensions里面不用再注册了
    /// </summary>
    [AppService(ServiceType = typeof(Job_ConsumableStorageNotice), ServiceLifetime = LifeTime.Scoped)]
    public class Job_ConsumableStorageNotice : JobBase, IJob
    {
        private readonly IWxMessageService _wxMessageService;

        private List<string> JobParams = null;//厂区ID集合

        private SqlSugarScopeProvider provider;

        public Job_ConsumableStorageNotice(IWxMessageService wxMessageService)
        {
            _wxMessageService = wxMessageService;
        }

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
                        provider = DbScoped.SugarScope.GetConnectionScope(factoryId);

                        List<ConsumableDetailDto> consumables = GetOutOfSafeStorageConsumables();
                        if (consumables != null)
                        {
                            List<int> noticeConIds = QueryConsumableNotice();
                            ConsumableConfigNotice noticeConfig = QueryNoticeEmp();
                            consumables = consumables.Where(it => !noticeConIds.Contains(it.ConsumableId)).ToList();
                            SendMsg(consumables, noticeConfig);
                        }

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
        /// 获取超出安全库存的耗品
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private List<ConsumableDetailDto> GetOutOfSafeStorageConsumables()
        {
            var sqlcs = provider.Queryable<ConsumableStorage>()
                .GroupBy(cs => cs.ConsumableId)
                .Select(cs => new
                {
                    cs.ConsumableId,

                    TotalStackQty = SqlFunc.AggregateSumNoNull(cs.Qty),
                });

            var response = provider.Queryable<ConsumableBase>()
                .LeftJoin(sqlcs, (it, cs) => it.ConsumableId == cs.ConsumableId)
                .Where((it, cs) => it.SafetyQty > cs.TotalStackQty && it.DelFlag == (int)DeleteFlagEnum.存在 && it.Status == SysStatusConstant.正常)
                .Select((it, cs) => new ConsumableDetailDto
                {
                    ConsumableId = (int)it.ConsumableId,
                    ConsumablePart = it.ConsumablePart,
                    ConsumableName = it.ConsumableName,
                    Spec = it.Spec,
                    SafetyQty = (int)it.SafetyQty,
                    TotalStackQty = cs.TotalStackQty,
                })
                .ToList();

            return response;
        }

        /// <summary>
        /// 返回已通知过的耗品ID
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="noticeInterval"></param>
        /// <returns></returns>
        private List<int> QueryConsumableNotice()
        {
            DateTime start = DateTime.Now.AddDays(-1).AddSeconds(30);//加30秒，防止执行过程耗时导致时间范围内包含了时间临界的数据
            return provider.Queryable<ConsumableStorageNotice>().Where(it => it.CreateTime > start).Select(it => it.ConsumableId).Distinct().ToList();
        }

        /// <summary>
        /// 获取通知人员工号
        /// </summary>
        /// <returns></returns>
        private ConsumableConfigNotice QueryNoticeEmp()
        {
            return provider.Queryable<ConsumableConfigNotice>().First();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="empCodes"></param>
        private void SendMsg(List<ConsumableDetailDto> datas, ConsumableConfigNotice noticeConfig)
        {
            if (datas == null || datas.Count <= 0 || noticeConfig == null) return;

            string content = $"EAM【耗品库存提醒】\n当前有{datas.Count}个耗品需要补充，请登入EAM管理平台查看。";

            WxMessage wxMessage = null;
            if (!string.IsNullOrEmpty(noticeConfig.WxChatId))
            {//群消息
                wxMessage = _wxMessageService.SendWxChatMessage(noticeConfig.WxChatId, content);
            }
            else
            {//人员消息
                wxMessage = _wxMessageService.SendWxEmpMessage(noticeConfig.EmpCodes, content);
            }

            //保存发磅发送记录
            List<ConsumableStorageNotice> notices = new List<ConsumableStorageNotice>();
            foreach (ConsumableDetailDto con in datas)
            {
                notices.Add(new ConsumableStorageNotice()
                {
                    ConsumableId = con.ConsumableId,
                    WxNoticeId = wxMessage.Id,
                    CreateTime = DateTime.Now
                });
            }

            provider.Insertable(notices).ExecuteCommand();
        }
    }
}