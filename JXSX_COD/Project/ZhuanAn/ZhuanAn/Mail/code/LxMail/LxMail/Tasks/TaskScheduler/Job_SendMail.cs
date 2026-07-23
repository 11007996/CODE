using Infrastructure.Attribute;
using LxMail.CoreService.Constants;
using LxMail.Models;
using LxMail.Service;
using Newtonsoft.Json.Linq;
using Quartz;
using SqlSugar;
using SqlSugar.IOC;

namespace LxMail.Tasks.TaskScheduler
{
    /// <summary>
    /// 定时任务：统计数据
    /// 使用如下注册后TaskExtensions里面不用再注册了
    /// </summary>
    [AppService(ServiceType = typeof(Job_SendMail), ServiceLifetime = LifeTime.Scoped)]
    public class Job_SendMail : JobBase, IJob
    {
        private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private string dbConfigId = "0";
        private SqlSugarScopeProvider provider = null;
        private ISysAutoMailService SysAutoMailService;

        public Job_SendMail(ISysAutoMailService sysAutoMailService)
        {
            SysAutoMailService = sysAutoMailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobParam = context.JobDetail.JobDataMap["JobParam"];

            if (jobParam != null)
            {
                JObject jobject = JObject.Parse(jobParam.ToString());
                dbConfigId = jobject["dbConfigId"].ToString();
            }
            provider = DbScoped.SugarScope.GetConnectionScope(dbConfigId);
            await ExecuteJob(context, Run);
        }

        /// <summary>
        /// 任务使用中注意：所有方法都需要使用异步，并且不能少了await
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            await Task.Delay(1);
            try
            {
                //TODO 业务逻辑
                AutoSendMail();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 自动发送邮件
        /// </summary>
        private void AutoSendMail()
        {
            //第一次尝试
            List<SysAutoMail> mails0 = provider.Queryable<SysAutoMail>().Where(it => it.SendFlag == MailSendFlagConstant.未发送 && it.CreateTime > DateTime.Now.AddDays(-1) && it.SendFailedCount == 0).ToList();
            //第二次尝试
            List<SysAutoMail> mails1 = provider.Queryable<SysAutoMail>().Where(it => it.SendFlag == MailSendFlagConstant.未发送 && it.CreateTime > DateTime.Now.AddDays(-1) && it.CreateTime < DateTime.Now.AddHours(-1) && it.SendFailedCount == 1).ToList();
            //第三次尝试
            List<SysAutoMail> mails2 = provider.Queryable<SysAutoMail>().Where(it => it.SendFlag == MailSendFlagConstant.未发送 && it.CreateTime > DateTime.Now.AddDays(-1) && it.CreateTime < DateTime.Now.AddHours(-6) && it.SendFailedCount == 2).ToList();

            List<SysAutoMail> mails = new List<SysAutoMail>();
            mails.AddRange(mails0);
            mails.AddRange(mails1);
            mails.AddRange(mails2);
            foreach (SysAutoMail mail in mails)
            {
                //Task tes = new Task();
                SysAutoMailService.SendMail(mail);
            }
        }
    }
}