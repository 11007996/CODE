using EAM.Model.Constant;
using EAM.Model.Equipment;
using EAM.Model.System;
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAM.Tasks.TaskScheduler
{
    /// <summary>
    /// 定时任务：设备保养提醒
    /// 使用如下注册后TaskExtensions里面不用再注册了
    /// </summary>
    [AppService(ServiceType = typeof(Job_MaintainRemind), ServiceLifetime = LifeTime.Scoped)]
    public class Job_MaintainRemind : JobBase, IJob
    {
        private readonly IWxMessageService _wxMessageService;

        private List<string> JobParams = null; //厂区Id集合

        public Job_MaintainRemind(IWxMessageService wxMessageService)
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

                        //获取通知配置
                        List<MaintainNoticeConfig> noticeConfigs = provider.Queryable<MaintainNoticeConfig>().ToList();
                        if (noticeConfigs == null || noticeConfigs.Count <= 0)
                            return;

                        //TODO 日保养检查

                        //周保养检查
                        CheckWeekMaintain(provider, noticeConfigs);

                        //TODO 月保养检查
                        CheckMaintainByPlan(provider, DateMarkConstant.月, noticeConfigs);

                        //TODO 季保养检查
                        CheckMaintainByPlan(provider, DateMarkConstant.季, noticeConfigs);

                        //TODO 年保养检查
                        CheckMaintainByPlan(provider, DateMarkConstant.年, noticeConfigs);

                        //关闭连接
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
        /// 周保养检查
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="noticeConfigs"></param>
        private void CheckWeekMaintain(SqlSugarScopeProvider provider, List<MaintainNoticeConfig> noticeConfigs)
        {
            //判断今天是否周6
            // 判断是否为周六或周日
            if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday)
                return;

            string dateMark = DateMarkConstant.周;
            // 检查哪些设备未保养
            int year = DateTime.Now.Year;
            int dataMarkStamp = GetDateMarkStamp(dateMark, DateTime.Now);

            //获取当前运行的设备
            List<EquipmentBase> equipments = GetRuningEquipments(provider);
            if (equipments == null || equipments.Count == 0)
                return;
            //获取未保养的设备
            List<EquipmentBase> unMaintainEquipments = CheckMaintainStatus(provider, equipments, year, dateMark, dataMarkStamp);
            //获取未通知过的设备
            List<EquipmentBase> unNoticeEquipments = CheckNoticeStatus(provider, equipments, year, dateMark, dataMarkStamp);
            //获取配置的通知人员
            MaintainNoticeConfig noticeConfig = noticeConfigs.Where(it => it.EnableFlag == SysYesNoConstant.是 && (it.DateMark == dateMark || it.DateMark == null)).OrderBy(it => it.DateMark).FirstOrDefault();

            //根据
            if (unNoticeEquipments != null && unNoticeEquipments.Count > 0 && noticeConfig != null)
            {
                //  List<string> emps = noticeConfig.EmpCodes.Split(',').ToList();
                SendMsg(provider, unNoticeEquipments, year, dateMark, dataMarkStamp, noticeConfig);
            }
        }

        /// <summary>
        /// 根据保养计划检查
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="dateMark"></param>
        /// <param name="noticeConfigs"></param>
        private void CheckMaintainByPlan(SqlSugarScopeProvider provider, string dateMark, List<MaintainNoticeConfig> noticeConfigs)
        {
            // 检查哪些设备未保养
            int year = DateTime.Now.Year;
            int dataMarkStamp = GetDateMarkStamp(dateMark, DateTime.Now);
            DateTime now = DateTime.Now;
            List<int> equipmentIds = provider.Queryable<MaintainPlan>()
                .LeftJoin<MaintainPlanClassItem>((p, pc) => p.PlanClassId == pc.PlanClassId)
                .Where((p, pc) => pc.StartDate < now && pc.EndDate > now)
                .Select((p, pc) => p.EquipmentId).ToList();
            if (equipmentIds == null || equipmentIds.Count <= 0) { return; }

            List<EquipmentBase> equipments = provider.Queryable<EquipmentBase>().Where(it => equipmentIds.Contains(it.EquipmentId) && it.DelFlag == 0).ToList();
            if (equipmentIds == null || equipmentIds.Count <= 0) { return; }

            //获取未保养的设备
            List<EquipmentBase> unMaintainEquipments = CheckMaintainStatus(provider, equipments, year, dateMark, dataMarkStamp);
            //获取未通知过的设备
            List<EquipmentBase> unNoticeEquipments = CheckNoticeStatus(provider, equipments, year, dateMark, dataMarkStamp);
            //获取配置的通知人员
            MaintainNoticeConfig noticeConfig = noticeConfigs.Where(it => it.EnableFlag == SysYesNoConstant.是 && (it.DateMark == dateMark || it.DateMark == null)).OrderBy(it => it.DateMark).FirstOrDefault();

            //根据
            if (unNoticeEquipments != null && unNoticeEquipments.Count > 0 && noticeConfig != null)
            {
                //  List<string> emps = noticeConfig.EmpCodes.Split(',').ToList();
                SendMsg(provider, unNoticeEquipments, year, dateMark, dataMarkStamp, noticeConfig);
            }
        }

        /// <summary>
        /// 获取当天所有正在使用的设备(有上报记录)
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private List<EquipmentBase> GetRuningEquipments(SqlSugarScopeProvider provider)
        {
            //运行设备的编码
            List<int> equipmentIds = provider.Queryable<EquipmentRuningRecord>()
                 .Where(r => r.CreateTime >= DateTime.Now.Date)
                 .Select(r => r.EquipmentId).Distinct().ToList();

            if (equipmentIds != null && equipmentIds.Count() > 0)
            {
                //运行的设备信息
                return provider.Queryable<EquipmentBase>()
                     .Select(e => new EquipmentBase()
                     {
                         EquipmentId = e.EquipmentId,
                         AssetName = e.AssetName,
                         AssetNo = e.AssetNo,
                     })
                     .ToList();
            }
            return null;
        }

        /// <summary>
        /// 根据日期标记与日期计算出对应时间戳
        /// </summary>
        /// <param name="dateMark"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private int GetDateMarkStamp(string dateMark, DateTime date)
        {
            int dateMarkStamp = 0;
            switch (dateMark)
            {
                case DateMarkConstant.日:
                    dateMarkStamp = date.DayOfYear;
                    break;

                case DateMarkConstant.周:
                    GregorianCalendar gregorianCalendar = new GregorianCalendar();
                    dateMarkStamp = gregorianCalendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//当天所属周
                    break;

                case DateMarkConstant.月:
                    dateMarkStamp = date.Month;
                    break;

                case DateMarkConstant.季:
                    dateMarkStamp = (int)Math.Ceiling((double)date.Month / 3);
                    break;

                case DateMarkConstant.年:
                    dateMarkStamp = 1;
                    break;

                default:
                    throw new CustomException($"未知日期标记:{dateMark}");
            }
            return dateMarkStamp;
        }

        /// <summary>
        /// 检查设备保养
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="equipments">要检查的设备</param>
        /// <param name="year">年份</param>
        /// <param name="dateMark">日期标记</param>
        /// <param name="dateMarkStamp">日期标记戳</param>
        /// <returns>未保养设备列表</returns>
        private List<EquipmentBase> CheckMaintainStatus(SqlSugarScopeProvider provider, List<EquipmentBase> equipments, int year, string dateMark, int dateMarkStamp)
        {
            // 检查设备
            if (equipments == null || equipments.Count <= 0)
                return null;

            List<int> equipmentIds = equipments.Select(it => (int)it.EquipmentId).ToList();
            List<int> maintainEquIds = provider.Queryable<MaintainRecord>().Where(it => equipmentIds.Contains((int)it.EquipmentId) && it.Year == year && it.DateMark == dateMark && it.DateMarkStamp == dateMarkStamp)
                 .Select(it => (int)it.EquipmentId).ToList();

            return equipments.Where(it => !maintainEquIds.Contains((int)it.EquipmentId)).ToList();
        }

        /// <summary>
        /// 检查是否已发送提醒
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="equipments">要检查的设备</param>
        /// <param name="year">年份</param>
        /// <param name="dateMark">日期标记</param>
        /// <param name="dateMarkStamp">日期标记戳</param>
        /// <returns>未保养设备列表</returns>
        private List<EquipmentBase> CheckNoticeStatus(SqlSugarScopeProvider provider, List<EquipmentBase> equipments, int year, string dateMark, int dateMarkStamp)
        {
            // 检查设备
            if (equipments == null || equipments.Count <= 0)
                return null;

            List<int> equipmentIds = equipments.Select(it => (int)it.EquipmentId).ToList();
            List<int> noticeEquIds = provider.Queryable<MaintainNotice>().Where(it => equipmentIds.Contains((int)it.EquipmentId) && it.Year == year && it.DateMark == dateMark && it.DateMarkStamp == dateMarkStamp)
                 .Select(it => (int)it.EquipmentId).ToList();

            return equipments.Where(it => !noticeEquIds.Contains((int)it.EquipmentId)).ToList();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="equipments"></param>
        /// <param name="year"></param>
        /// <param name="dateMark"></param>
        /// <param name="dateMarkStamp"></param>
        /// <param name="noticeConfig"></param>
        private void SendMsg(SqlSugarScopeProvider provider, List<EquipmentBase> equipments, int year, string dateMark, int dateMarkStamp, MaintainNoticeConfig noticeConfig)
        {
            if (string.IsNullOrEmpty(noticeConfig.WxChatId) && string.IsNullOrEmpty(noticeConfig.EmpCodes)) return;
            StringBuilder content = new StringBuilder();
            List<EquipmentBase> tempEquments = new List<EquipmentBase>();
            string dateMarkLabel = "";
            switch (dateMark)
            {
                case DateMarkConstant.日:
                    dateMarkLabel = "日";
                    break;

                case DateMarkConstant.周:
                    dateMarkLabel = "周";
                    break;

                case DateMarkConstant.月:
                    dateMarkLabel = "月";
                    break;

                case DateMarkConstant.季:
                    dateMarkLabel = "季";
                    break;

                case DateMarkConstant.年:
                    dateMarkLabel = "年";
                    break;
            }

            //遍历设备，发送消息
            for (int i = 0; i < equipments.Count; i++)
            {
                tempEquments.Add(equipments[i]);
                if (content.Length <= 0)//没有内容，追加标题
                    content.Append($"EAM 【{dateMarkLabel}保养提醒：{dateMarkStamp}{dateMarkLabel}】");
                content.Append($"\n{equipments[i].AssetNo}：{equipments[i].AssetName}");

                if (content.Length > 1000 || i == equipments.Count - 1)
                {//超过1000个字符，或循环到最后一条，再发送信息
                 //消息发送
                    WxMessage wxMessage = null;
                    if (!string.IsNullOrEmpty(noticeConfig.WxChatId))
                    {//群消息
                        wxMessage = _wxMessageService.SendWxChatMessage(noticeConfig.WxChatId, content.ToString());
                    }
                    else
                    {//人员消息
                        wxMessage = _wxMessageService.SendWxEmpMessage(noticeConfig.EmpCodes, content.ToString());
                    }

                    //插入通知记录
                    List<MaintainNotice> maintainNotices = new List<MaintainNotice>();
                    foreach (EquipmentBase equ in tempEquments)
                    {
                        MaintainNotice model = new MaintainNotice()
                        {
                            EquipmentId = (int)equ.EquipmentId,
                            Year = year,
                            DateMark = dateMark,
                            DateMarkStamp = dateMarkStamp,
                            WxNoticeId = wxMessage.Id,
                            CreateTime = DateTime.Now
                        };
                        maintainNotices.Add(model);
                    }
                    provider.Insertable<MaintainNotice>(maintainNotices).ExecuteCommand();

                    //清空内容
                    content.Clear();
                    tempEquments.Clear();
                }
            }
        }
    }
}