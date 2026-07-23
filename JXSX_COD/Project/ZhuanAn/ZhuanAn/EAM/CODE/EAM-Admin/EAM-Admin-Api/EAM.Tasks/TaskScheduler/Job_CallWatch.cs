using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Call;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.Service.Call;
using EAM.ServiceCore.Model.Enums;
using EAM.ServiceCore.Services;
using Infrastructure;
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
    /// 定时任务：呼叫监控,监控呼叫是否超时，通知相关人员
    /// 使用如下注册后TaskExtensions里面不用再注册了
    /// </summary>
    [AppService(ServiceType = typeof(Job_CallWatch), ServiceLifetime = LifeTime.Scoped)]
    public class Job_CallWatch : JobBase, IJob
    {
        private readonly IWxMessageService _wxMessageService;

        private List<string> JobParams = null;//厂区ID集合

        public Job_CallWatch(IWxMessageService wxMessageService)
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
                        SqlSugarScopeProvider provider = DbScoped.SugarScope.GetConnectionScope(factoryId);
                        CallFaultBaseService callFaultBaseService = new CallFaultBaseService(DbScoped.SugarScope.GetConnectionScope(factoryId), _wxMessageService);
                        CheckCallFaultHandleTime(provider, callFaultBaseService);
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
        /// 检查故障是否处理超时，并通知相关人员
        /// </summary>
        private void CheckCallFaultHandleTime(SqlSugarScopeProvider provider, CallFaultBaseService callFaultBaseService)
        {
            //获取所有通知配置
            var noticeConfigs = provider.Queryable<CallConfigNotice>().ToList();

            //获取接单超时配置
            var config = provider.Queryable<FactoryConfig>().Where(it => it.ConfigKey == FactoryConfigKeyConstant.呼叫接单超时时间).First();
            int orderTimeout = config != null ? Convert.ToInt32(config.ConfigValue) : 10;//分钟
            string[] status = new string[] { CallFaultStatusConstant.待处理, CallFaultStatusConstant.处理中, CallFaultStatusConstant.支援中 };
            //未结案呼叫记录
            var callList = provider.Queryable<CallFaultBase>()
                 .LeftJoin<CallAreaLine>((it, al) => it.LineId == al.LineId)
                 .LeftJoin<Line>((it, al, l) => it.LineId == l.LineId)
                 .LeftJoin<Station>((it, al, l, s) => it.StationId == s.StationId)
                 .LeftJoin<Employee>((it, al, l, s, e1) => it.HandlerNo == e1.EmpCode)
                 .LeftJoin<Employee>((it, al, l, s, e1, e2) => it.HelperNo == e2.EmpCode)
                 .Where(it => status.Contains(it.FaultStatus))
                 .Select((it, al, l, s, e1, e2) => new CallFaultBaseDto()
                 {
                     AreaId = al.AreaId,
                     LineName = l.LineName,
                     StationName = s.StationName,
                     HandlerName = e1.EmpName,
                     HelperName = e2.EmpName,
                 }, true)
                 .ToList();

            if (noticeConfigs == null || callList == null || callList.Count <= 0) return;

            //遍历呼叫记录
            foreach (var call in callList)
            {
                try
                {
                    CallStageTypeEnum stageType = CallStageTypeEnum.接单超时;
                    int sendCount = 0;

                    if (call.FaultStatus == CallFaultStatusConstant.待处理 && call.CreateTime.Value < DateTime.Now.AddMinutes(-orderTimeout))
                    { //接单超时
                        stageType = CallStageTypeEnum.接单超时;
                        sendCount = provider.Queryable<CallFaultNotice>().Where(it => it.CallId == call.CallId && it.CallStageType == ((int)CallStageTypeEnum.接单超时).ToString()).Count();
                    }
                    else if (call.FaultStatus == CallFaultStatusConstant.处理中 && call.MaxHandleTimes > 0 && call.HandleTime.Value.AddMinutes(call.MaxHandleTimes.Value) < DateTime.Now)
                    {//处理超时
                        stageType = CallStageTypeEnum.处理超时;
                        sendCount = provider.Queryable<CallFaultNotice>().Where(it => it.CallId == call.CallId && it.CallStageType == ((int)CallStageTypeEnum.处理超时).ToString()).Count();
                        AutoCallHelp(provider, call, callFaultBaseService);
                    }
                    else if (call.FaultStatus == CallFaultStatusConstant.支援中 && call.MaxHelpTimes > 0 && call.HelpTime.Value.AddMinutes(call.MaxHelpTimes.Value) < DateTime.Now)
                    {//支援超时
                        stageType = CallStageTypeEnum.支援超时;
                        sendCount = provider.Queryable<CallFaultNotice>().Where(it => it.CallId == call.CallId && it.CallStageType == ((int)CallStageTypeEnum.支援超时).ToString()).Count();
                    }
                    else
                    {//其他情况不通知
                        continue;
                    }

                    //检查是否已发送，防止重复发送
                    if (sendCount > 0)
                        continue;

                    //获取优先级最高的通知配置
                    CallConfigNotice noticeConfig = noticeConfigs.AsEnumerable()
                         .Where(it => it.CallStageType == Convert.ToInt32(stageType).ToString())
                         .Where(it => it.AreaId == call.AreaId || it.AreaId == null)
                         .Where(it => it.CallTargetType == call.CallTargetType || it.CallTargetType == null)
                         .OrderByDescending(it => it.CallTargetType) //先以【呼叫目标】排序
                         .OrderByDescending(it => it.AreaId) //再以【区域】排序
                         .FirstOrDefault();

                    //发送消息
                    if (sendCount <= 0 && noticeConfig != null)
                    {
                        SendMsg(provider, stageType, call, noticeConfig);
                    }
                }
                catch (Exception)
                {
                    //防止其中一次出现问题，导致其他的推送不执行。
                }
            }
        }

        /// <summary>
        /// 自动呼叫支援
        /// </summary>
        private void AutoCallHelp(SqlSugarScopeProvider provider, CallFaultBaseDto call, CallFaultBaseService callFaultBaseService)
        {
            if (call == null) return;
            //检查配置
            var configFault = provider.Queryable<CallConfigFault>().Where(it => it.EquipmentType == call.EquipmentType).First();

            //判断是否开启自动呼叫支援
            if (configFault != null && configFault.AutoHelpFlag == SysYesNoConstant.是)
            {
                CallOperateDto parm = new CallOperateDto()
                {
                    CallId = call.CallId,
                    OperatorNo = GlobalConstant.System,
                    CallHelpWay = CallHelpWayConstant.超时触发,
                    UpdateBy = call.HandlerNo,
                    UpdateTime = DateTime.Now,
                };
                callFaultBaseService.RequestHelpCallFaultBase(parm);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="stageType"></param>
        /// <param name="callFaultBase"></param>
        /// <param name="empCodes"></param>
        private void SendMsg(SqlSugarScopeProvider provider, CallStageTypeEnum stageType, CallFaultBaseDto callFaultBase, CallConfigNotice noticeConfig)
        {
            string helperName = string.IsNullOrEmpty(callFaultBase.HelperName) ? null : "," + callFaultBase.HelperName;
            string content = string.Empty;
            if (callFaultBase.CallPointType == CallPointTypeConstant.设备)
                content = $"EAM 【呼叫提醒：{stageType}】\n线别：{callFaultBase.LineName}\n原因：{callFaultBase.CallReason}\n目标：{callFaultBase.CallTargetTypeLabel}\n机台：{callFaultBase.EquipmentName}\n人员：{callFaultBase.HandlerName}{helperName}\n时间：{callFaultBase.CreateTime}\n备注：{callFaultBase.Remark}";
            else if (callFaultBase.CallPointType == CallPointTypeConstant.工站)
                content = $"EAM 【呼叫提醒：{stageType}】\n线别：{callFaultBase.LineName}\n原因：{callFaultBase.CallReason}\n目标：{callFaultBase.CallTargetTypeLabel}\n工站：{callFaultBase.StationName}\n人员：{callFaultBase.HandlerName}{helperName}\n时间：{callFaultBase.CreateTime}\n备注：{callFaultBase.Remark}";
            else
                content = $"EAM 【呼叫提醒：{stageType}】\n线别：{callFaultBase.LineName}\n原因：{callFaultBase.CallReason}\n目标：{callFaultBase.CallTargetTypeLabel}\n人员：{callFaultBase.HandlerName}{helperName}\n时间：{callFaultBase.CreateTime}\n备注：{callFaultBase.Remark}";//{callFaultBase.HandlerName}{helperName}

            WxMessage wxMessage = null;
            if (!string.IsNullOrEmpty(noticeConfig.WxChatId))
            {//群消息
                wxMessage = _wxMessageService.SendWxChatMessage(noticeConfig.WxChatId, content);
            }
            else
            {//人员消息
                wxMessage = _wxMessageService.SendWxEmpMessage(noticeConfig.EmpCodes, content);
            }

            CallFaultNotice model = new CallFaultNotice()
            {
                CallId = callFaultBase.CallId,
                CallStageType = ((int)stageType).ToString(),
                WxNoticeId = wxMessage.Id,
                CreateTime = DateTime.Now
            };
            provider.Insertable<CallFaultNotice>(model).ExecuteCommand();
        }
    }
}