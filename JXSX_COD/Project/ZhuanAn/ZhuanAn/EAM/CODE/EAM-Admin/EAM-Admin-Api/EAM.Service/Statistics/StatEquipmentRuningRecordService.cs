using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.Equipment;
using EAM.Model.Statistics;
using EAM.Repository;
using EAM.Service.Statistics.IStatisticsService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace EAM.Service.Statistics
{
    /// <summary>
    /// 统计设备运行记录Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IStatEquipmentRuningRecordService), ServiceLifetime = LifeTime.Transient)]
    public class StatEquipmentRuningRecordService : BaseService<StatEquipmentRuningRecord>, IStatEquipmentRuningRecordService
    {
        public StatEquipmentRuningRecordService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public StatEquipmentRuningRecordService(ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        /// <summary>
        /// 查询统计设备运行记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<StatEquipmentRuningRecordDto> GetList(StatEquipmentRuningRecordQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<StatEquipmentRuningRecord, StatEquipmentRuningRecordDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public StatEquipmentRuningRecord GetInfo(int Id)
        {
            var response = Queryable()
                .Where(x => x.StatId == Id)
                .First();

            return response;
        }

        /// <summary>
        /// 添加统计设备运行记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public StatEquipmentRuningRecord AddStatEquipmentRuningRecord(StatEquipmentRuningRecord model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改统计设备运行记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateStatEquipmentRuningRecord(StatEquipmentRuningRecord model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<StatEquipmentRuningRecord> QueryExp(StatEquipmentRuningRecordQueryDto parm)
        {
            var predicate = Expressionable.Create<StatEquipmentRuningRecord>();

            return predicate;
        }

        #region 设备运行数据分析统计

        /// <summary>
        ///  单个设备数据分析
        /// </summary>
        /// <param name="equipmentId">指定设备编码</param>
        /// <param name="statStartTime">开始日期</param>
        /// <param name="statEndTime">结束日期</param>
        /// <returns></returns>
        public StatEquipmentRuningRecord StatOneEquipmentRunData(int equipmentId, DateTime statStartTime, DateTime statEndTime)
        {
            if (equipmentId <= 0)
                throw new CustomException("未传递设备Id参数");
            //参数检查
            if (statStartTime > statEndTime)
                throw new CustomException("开始时间大于结束时间");
            int days = (int)new TimeSpan(statEndTime.Ticks - statStartTime.Ticks).TotalDays;
            if (days > 3)
                throw new CustomException("时间跨度不能超过3天");

            //基础数据
            StatEquipmentRuningRecord serr = new StatEquipmentRuningRecord();//统计结果
            EquipmentExtend equExt = Context.Queryable<EquipmentExtend>().Where(it => it.EquipmentId == equipmentId).First();//设备扩展信息
            List<EquipmentPlanTime> planTimes = Context.Queryable<EquipmentPlanTime>().ToList();//计划停机时间配置

            List<EquipmentRuningRecordAnalyseDto> equRunDatas = GetRunData(new List<int> { equipmentId }, statStartTime, statEndTime);
            equRunDatas.Sort((a, b) => DateTime.Compare(a.CreateTime, b.CreateTime));//根据时间升序

            if (equExt != null && equExt.TheoryCT > 0 && equRunDatas != null && equRunDatas.Count > 0)
            {
                serr.EquipmentId = equExt.EquipmentId;
                serr.StatDate = statStartTime.Date;
                serr.StatStartTime = statStartTime;
                serr.StatEndTime = statEndTime;
                serr.TheoryCT = equExt.TheoryCT;
                serr.UpdateTime = DateTime.Now;
                //解析数据
                ParseRunData(serr, equRunDatas, planTimes);
            }

            return serr;
        }

        /// <summary>
        /// （每日班次）设备数据分析统计
        ///  注意：此服务最好不要给到外部接口使用，只给到定时任务调用，以免接口频繁调用影响性能
        /// </summary>
        /// <param name="equipmentIds">指定设备编码，可为空，表示全部设备</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public List<StatEquipmentRuningRecord> StatEquipmentRunData(List<int> equipmentIds, DateTime startDate, DateTime endDate)
        {
            //参数检查
            if (startDate.Date > endDate.Date)
                throw new CustomException("开始日期大于结束日期");
            int days = (int)new TimeSpan(endDate.Ticks - startDate.Ticks).TotalDays;
            if (days > 180)
                throw new CustomException("时间跨度不能超过180天");

            //厂区拆分时间配置
            FactoryConfig config = Context.Queryable<FactoryConfig>().Where(it => it.ConfigKey == FactoryConfigKeyConstant.设备监听数据统计拆分时间).First();
            string splitTime = config != null ? config.ConfigValue : "00:00:00";
            TimeSpan timeSpan = new TimeSpan(int.Parse(splitTime.Split(':')[0]), int.Parse(splitTime.Split(':')[1]), 0);
            //厂区班次时长配置
            FactoryConfig config2 = Context.Queryable<FactoryConfig>().Where(it => it.ConfigKey == FactoryConfigKeyConstant.设备监听数据统计班次时长).First();
            int classHour = config2 != null ? int.Parse(config2.ConfigValue) : 24;
            if (classHour > 24 || 24 % classHour != 0)
                throw new CustomException("系统配置的班次时长值非法");

            //修正开始与结束时间
            DateTime startTime = startDate;
            if (!DateTime.TryParse(startDate.Date.ToString("yyyy-MM-dd") + " " + splitTime, out startTime))
                throw new CustomException("系统配置的拆分时间值非法");
            DateTime endTime = DateTime.Parse(endDate.Date.AddDays(1).ToString("yyyy-MM-dd") + " " + splitTime);

            //基础数据
            List<StatEquipmentRuningRecord> statEquRunList = new List<StatEquipmentRuningRecord>();//统计结果
            List<EquipmentExtend> equExtends = Context.Queryable<EquipmentExtend>().ToList();//设备扩展信息
            List<EquipmentPlanTime> planTimes = Context.Queryable<EquipmentPlanTime>().ToList();//计划停机时间配置

            DateTime statStartTime = startTime;
            DateTime statEndTime = statStartTime.AddHours(classHour);
            DateTime statDate = DateTime.Now.Date;//统计数据所属日期
            int classSeq = 0;//班次序号
            //判断统计时间是否小于指定结束时间
            while (statEndTime <= endTime)
            {
                statDate = statStartTime.TimeOfDay >= timeSpan ? statStartTime.Date : statStartTime.Date.AddDays(-1);//如果小于拆分日期，则表示前一天的统计
                classSeq += 1;

                List<EquipmentRuningRecordAnalyseDto> runDatas = GetRunData(equipmentIds, statStartTime, statEndTime);
                runDatas.Sort((a, b) => DateTime.Compare(a.CreateTime, b.CreateTime));//根据时间升序
                List<int> equipmentIdList = runDatas.GroupBy(it => new { it.EquipmentId }).Select(it => it.Key.EquipmentId).Distinct().ToList();
                //设备编码遍历
                foreach (int equId in equipmentIdList)
                {
                    EquipmentExtend equExt = equExtends.Where(it => it.EquipmentId == equId).FirstOrDefault();
                    List<EquipmentRuningRecordAnalyseDto> equRunDatas = runDatas.Where(it => it.EquipmentId == equId && it.CreateTime >= statStartTime && it.CreateTime < statEndTime).ToList();
                    if (equExt != null && equExt.TheoryCT > 0 && equRunDatas != null && equRunDatas.Count > 0)
                    {
                        StatEquipmentRuningRecord serr = new StatEquipmentRuningRecord();
                        serr.EquipmentId = equExt.EquipmentId;
                        serr.StatDate = statDate;
                        serr.ClassSeq = classSeq;
                        serr.SplitTime = splitTime;
                        serr.StatStartTime = statStartTime;
                        serr.StatEndTime = statEndTime;
                        serr.TheoryCT = equExt.TheoryCT;
                        serr.UpdateTime = DateTime.Now;

                        ParseRunData(serr, equRunDatas, planTimes);
                        if (serr.OEE > 0)
                            statEquRunList.Add(serr);
                    }
                }
                //向后移动班次时长
                statStartTime = statStartTime.AddHours(classHour);
                statEndTime = statEndTime.AddHours(classHour);
                if (classSeq * classHour >= 24)
                    classSeq = 0;
            }

            SaveStatResult(statEquRunList);
            return statEquRunList;
        }

        ///// <summary>
        ///// （每日）设备数据分析统计
        /////  注意：此服务最好不要给到外部接口使用，只给到定时任务调用，以免接口频繁调用影响性能
        ///// </summary>
        ///// <param name="equipmentIds">指定设备编码，可为空，表示全部设备</param>
        ///// <param name="startDate">开始日期</param>
        ///// <param name="endDate">结束日期</param>
        ///// <returns></returns>
        //public List<StatEquipmentRuningRecord> StatEquipmentRunData(List<int> equipmentIds, DateTime startDate, DateTime endDate)
        //{
        //    //参数检查
        //    if (startDate.Date > endDate.Date)
        //        throw new CustomException("开始日期大于结束日期");
        //    int days = (int)new TimeSpan(endDate.Ticks - startDate.Ticks).TotalDays;
        //    if (days > 180)
        //        throw new CustomException("时间跨度不能超过180天");

        //    //厂区拆分时间配置
        //    FactoryConfig config = Context.Queryable<FactoryConfig>().Where(it => it.ConfigKey == FactoryConfigKeyConstant.设备监听数据统计拆分时间).First();
        //    string splitTime = config != null ? config.ConfigValue : "00:00:00";

        //    //修正开始与结束时间
        //    DateTime startTime = startDate;
        //    if (!DateTime.TryParse(startDate.Date.ToString("yyyy-MM-dd") + " " + splitTime, out startTime))
        //        throw new CustomException("系统配置的拆分时间值非法");
        //    DateTime endTime = DateTime.Parse(endDate.Date.AddDays(1).ToString("yyyy-MM-dd") + " " + splitTime);

        //    //基础数据
        //    List<StatEquipmentRuningRecord> statEquRunList = new List<StatEquipmentRuningRecord>();//统计结果
        //    List<EquipmentExtend> equExtends = Context.Queryable<EquipmentExtend>().ToList();//设备扩展信息
        //    List<EquipmentPlanTime> planTimes = Context.Queryable<EquipmentPlanTime>().ToList();//计划停机时间配置

        //    DateTime statStartTime = startTime;
        //    DateTime statEndTime = statStartTime.AddDays(1);
        //    //统计日期循环(每天)
        //    while (statEndTime <= endTime)
        //    {
        //        List<EquipmentRuningRecordAnalyseDto> runDatas = GetRunData(equipmentIds, statStartTime, statEndTime);
        //        runDatas.Sort((a, b) => DateTime.Compare(a.CreateTime, b.CreateTime));//根据时间升序
        //        List<int> equipmentIdList = runDatas.GroupBy(it => new { it.EquipmentId }).Select(it => it.Key.EquipmentId).Distinct().ToList();
        //        //设备编码遍历
        //        foreach (int equId in equipmentIdList)
        //        {
        //            EquipmentExtend equExt = equExtends.Where(it => it.EquipmentId == equId).FirstOrDefault();
        //            List<EquipmentRuningRecordAnalyseDto> equRunDatas = runDatas.Where(it => it.EquipmentId == equId && it.CreateTime >= statStartTime && it.CreateTime < statEndTime).ToList();
        //            if (equExt != null && equExt.TheoryCT > 0 && equRunDatas != null && equRunDatas.Count > 0)
        //            {
        //                StatEquipmentRuningRecord serr = new StatEquipmentRuningRecord();
        //                serr.EquipmentId = equExt.EquipmentId;
        //                serr.StatDate = statStartTime.Date;
        //                serr.SplitTime = splitTime;
        //                serr.StatStartTime = statStartTime;
        //                serr.StatEndTime = statEndTime;
        //                serr.TheoryCT = equExt.TheoryCT;
        //                serr.UpdateTime = DateTime.Now;

        //                ParseRunData(serr, equRunDatas, planTimes);
        //                if (serr.OEE > 0)
        //                    statEquRunList.Add(serr);
        //            }
        //        }
        //        //向后移动天
        //        statStartTime = statStartTime.AddHours(1);
        //        statEndTime = statEndTime.AddDays(1);
        //    }

        //    SaveStatResult(statEquRunList);
        //    return statEquRunList;
        //}

        /// <summary>
        /// 获取运行数据
        /// </summary>
        /// <param name="equipmentIds">指定设备编码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        private List<EquipmentRuningRecordAnalyseDto> GetRunData(List<int> equipmentIds, DateTime startTime, DateTime endTime)
        {
            string sql;
            if (equipmentIds != null && equipmentIds.Count > 0)
            {
                sql = $@"WITH cte AS (
	                        SELECT
		                        Equipment_Id EquipmentId,
		                        Run_State RunState,
		                        Product_Count ProductCount,
		                        Defect_Count DefectCount,
		                        Warn_State WarnState,
		                        Warn_Code WarnCode,
		                        Create_Time CreateTime,
		                        LAG ( Run_State, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS PreRunState,
		                        LAG ( Warn_State, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS PreWarnState,
		                        LEAD ( Run_State, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS NextRunState,
		                        LEAD ( Product_Count, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS NextProductCount,
		                        LEAD ( Defect_Count, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS NextDefectCount
	                        FROM
		                        dbo.EQU_Equipment_Runing_Record WITH ( NOLOCK )
	                        WHERE
		                        Create_Time >= '{startTime}'
		                        AND Create_Time < '{endTime}'
                                AND Equipment_Id IN ({string.Join(',', equipmentIds)})
	                        ) SELECT
	                        *,
	                        LEAD ( CreateTime, 1, NULL ) OVER ( PARTITION BY EquipmentId ORDER BY CreateTime ) AS EndTime
                        FROM
	                        cte
                        WHERE
	                        RunState != PreRunState
	                        OR WarnState != PreWarnState
	                        OR NextProductCount < ProductCount
	                        OR NextDefectCount < DefectCount";
            }
            else
            {
                sql = $@"WITH cte AS (
	                        SELECT
		                        Equipment_Id EquipmentId,
		                        Run_State RunState,
		                        Product_Count ProductCount,
		                        Defect_Count DefectCount,
		                        Warn_State WarnState,
		                        Warn_Code WarnCode,
		                        Create_Time CreateTime,
		                        LAG ( Run_State, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS PreRunState,
		                        LAG ( Warn_State, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS PreWarnState,
		                        LEAD ( Run_State, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS NextRunState,
		                        LEAD ( Product_Count, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS NextProductCount,
		                        LEAD ( Defect_Count, 1, 0 ) OVER ( PARTITION BY Equipment_Id ORDER BY Create_Time ) AS NextDefectCount
	                        FROM
		                        dbo.EQU_Equipment_Runing_Record WITH ( NOLOCK )
	                        WHERE
		                        Create_Time >= '{startTime}'
		                        AND Create_Time < '{endTime}'
	                        ) SELECT
	                        *,
	                        LEAD ( CreateTime, 1, NULL ) OVER ( PARTITION BY EquipmentId ORDER BY CreateTime ) AS EndTime
                        FROM
	                        cte
                        WHERE
	                        RunState != PreRunState
	                        OR WarnState != PreWarnState
	                        OR NextProductCount < ProductCount
	                        OR NextDefectCount < DefectCount";
            }

            return Context.SqlQueryable<EquipmentRuningRecordAnalyseDto>(sql).ToList();
        }

        /// <summary>
        /// 设备数据解析
        /// </summary>
        /// <param name="statEqument">设备统计信息</param>
        /// <param name="runDatas">运行数据</param>
        /// <param name="planRestTimes">计划停机时间配置</param>
        /// <returns></returns>
        private bool ParseRunData(StatEquipmentRuningRecord statEqument, List<EquipmentRuningRecordAnalyseDto> runDatas, List<EquipmentPlanTime> planRestTimes)
        {
            if (statEqument == null) return false;
            if (runDatas == null || runDatas.Count <= 0) return false;

            //基础数据
            DateTime? dataStartTime = null;//数据开始时间
            DateTime? dataEndTime = null;//数据结束时间
            int productCount = 0;//总产量（实际）
            int defectCount = 0;//不良数量
            int lastProductCount = 0;//最后上报的产量
            int runSeconds = 0;//开机时间运行(秒)
            int planEffectSeconds = 0;//生效的计划停机时间(秒)
            int lossSeconds = 0;//(损失时间)计划外停机时间（秒）=(小停顿时间+故障时间)
            int stopSeconds = 0;//小停顿时间
            int faultSeconds = 0;//故障时间
            int faultCount = 0;//故障次数
            //计算数据
            decimal oee = 0;//OEE(综合效率)
            decimal availabilityRate = 0;//时间开动率
            decimal performanceRate = 0;//性能开动率
            decimal qualityRate = 0;//合格率

            //临时计算数据
            int tempPreProCount = 0;//上一个产量
            int tempProCount = 0;//当前产量
            int tempPreDefectCount = 0;//上一个不良
            int tempDefectCount = 0;//当前不良
            bool findFirstDataFlag = false;//存在合规的第一条记录标记
            int tempLossSeconds = 0; //当前记录的损失时间(故障时间)
            int tempPlanEffectSeconds = 0;//当前记录的计划生效时间
            List<StatEquipmentRuningWarn> runWarns = new List<StatEquipmentRuningWarn>();

            //遍历运行数据
            foreach (EquipmentRuningRecordAnalyseDto row in runDatas)
            {
                //只有状态为【运行中】的记录才是合规的第一条记录,防止设备停机。
                if (!findFirstDataFlag)
                {
                    if (row.RunState == (int)EquipmentRunStateEnum.Runing)
                    {
                        //开机总时间
                        dataStartTime = row.CreateTime;
                        dataEndTime = runDatas[runDatas.Count - 1].CreateTime;
                        lastProductCount = runDatas[runDatas.Count - 1].ProductCount;
                        runSeconds = (int)new TimeSpan(dataEndTime.Value.Ticks - dataStartTime.Value.Ticks).TotalSeconds;
                        //初始化：上一个产量，当前产量、上一个不良、当前不良
                        tempPreProCount = row.ProductCount;
                        tempProCount = row.ProductCount;
                        tempPreDefectCount = row.DefectCount;
                        tempDefectCount = row.DefectCount;
                        findFirstDataFlag = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                //最后一个记录的结束时间是其本身的开始时间
                if (row.EndTime == null) row.EndTime = runDatas[runDatas.Count - 1].CreateTime;

                //根据状态得到不同时间
                if (row.RunState == (int)EquipmentRunStateEnum.Stop)
                {
                    tempLossSeconds = 0;
                    tempPlanEffectSeconds = 0;
                    //解析停机时间
                    ParseStopTime(planRestTimes, row.CreateTime, row.EndTime.Value, out tempLossSeconds, out tempPlanEffectSeconds);

                    planEffectSeconds += tempPlanEffectSeconds;
                    if (row.WarnState == (int)EquipmentWarnStateEnum.Warning)
                    {//故障停机
                        faultSeconds += tempLossSeconds;
                        //故障次数统计
                        faultCount++;
                        //添加设备报警统计记录
                        runWarns.Add(new StatEquipmentRuningWarn()
                        {
                            StatDate = statEqument.StatDate,
                            EquipmentId = statEqument.EquipmentId,
                            WarnCode = row.WarnCode,
                            DataStartTime = row.CreateTime,
                            DataEndTime = row.EndTime,
                            FaultSeconds = tempLossSeconds,
                            UpdateTime = DateTime.Now
                        });
                    }
                    else//小停顿
                        stopSeconds += tempLossSeconds;
                }

                //产量
                tempProCount = row.ProductCount;
                if (tempPreProCount <= tempProCount)
                {
                    productCount += tempProCount - tempPreProCount;
                }
                else
                { //设备重置
                    productCount += tempProCount;
                }
                tempPreProCount = row.ProductCount;

                //不良
                tempDefectCount = row.DefectCount;
                if (tempPreDefectCount <= tempDefectCount)
                {
                    defectCount += tempDefectCount - tempPreDefectCount;
                }
                else
                { //设备重置
                    defectCount += tempDefectCount;
                }
                tempPreDefectCount = row.DefectCount;
            }

            //--------计算效率--------
            //时间开动率= （开机时间-异常时间-计划停机时间)/（开机时间-计划停机时间)*100
            lossSeconds = faultSeconds + stopSeconds;
            if (runSeconds - planEffectSeconds > 0)
                availabilityRate = Math.Round((decimal)(runSeconds - lossSeconds - planEffectSeconds) / (runSeconds - planEffectSeconds) * 100, 2);
            //性能开动率=  ((产能 * 理论CT) / (开机时间—异常时间-计划停机时间))*100
            if (runSeconds - lossSeconds - planEffectSeconds > 0)
                performanceRate = Math.Round(productCount * statEqument.TheoryCT / (runSeconds - lossSeconds - planEffectSeconds) * 100, 2);
            //防止ct过小，导致性能超过100%
            if (performanceRate >= 100)
                performanceRate = 100;
            //合格率=(1-(不良品数/产量))*100
            if (productCount > 0)
                qualityRate = Math.Round((1 - (decimal)defectCount / productCount) * 100, 2);
            //OEE综合效率 = 时间开动率 * 性能开动率 * 合格率 /10000
            oee = Math.Round((availabilityRate * performanceRate * qualityRate) / 10000, 2);

            //将统计结果插入设备数据行
            statEqument.DataStartTime = dataStartTime;
            statEqument.DataEndTime = dataEndTime;
            statEqument.ProductCount = productCount;
            statEqument.DefectCount = defectCount;
            statEqument.LastProductCount = lastProductCount;
            statEqument.RunSeconds = runSeconds;
            statEqument.PlanEffectSeconds = planEffectSeconds;
            statEqument.StopSeconds = stopSeconds;
            statEqument.FaultSeconds = faultSeconds;
            statEqument.FaultCount = faultCount;
            statEqument.AvailabilityRate = availabilityRate;
            statEqument.PerformanceRate = performanceRate;
            statEqument.QualityRate = qualityRate;
            statEqument.OEE = oee;
            statEqument.StatEquipmentRuningWarnNav = runWarns;

            return statEqument.OEE > 0;
        }

        /// <summary>
        /// 停机时间解析
        /// </summary>
        /// <param name="planTimes">计划停机时间配置</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结果时间</param>
        /// <param name="lossSeconds">损失时间</param>
        /// <param name="planEffectSeconds">生效的计划停机时间</param>
        private void ParseStopTime(List<EquipmentPlanTime> planTimes, DateTime startTime, DateTime endTime, out int lossSeconds, out int planEffectSeconds)
        {
            lossSeconds = (int)new TimeSpan(endTime.Ticks - startTime.Ticks).TotalSeconds;
            planEffectSeconds = 0;//生效的计划停机时间
            int days = (int)new TimeSpan(endTime.Ticks - startTime.Ticks).TotalDays;//跨天数。
            if (planTimes != null && planTimes.Count > 0)
            {
                //如果跨天数
                if (days > 0)
                {
                    int dayMaxPlanSeconds = planTimes.Select(it => it.MaxSeconds).Sum();
                    planEffectSeconds += dayMaxPlanSeconds * days;
                    startTime = startTime.AddDays(days);//开始时间调整到对应日期
                }

                DateTime tempStart;
                DateTime tempEnd;
                int tempPlanEffectSeconds = 0;
                foreach (EquipmentPlanTime planTime in planTimes)
                {
                    tempPlanEffectSeconds = 0;
                    DateTime planStartTime = Convert.ToDateTime(startTime.ToString("yyyy-MM-dd") + " " + planTime.StartTime);
                    DateTime planEndTime = Convert.ToDateTime(startTime.ToString("yyyy-MM-dd") + " " + planTime.EndTime);
                    if (planEndTime < planStartTime)
                    {//计划的结束时间跨天,结束时间加一天。
                        planEndTime.AddDays(1);
                    }
                    //故障时间为计划停机时间内
                    if (startTime > planStartTime && endTime < planEndTime)
                    {
                        planEffectSeconds += planTime.MaxSeconds < lossSeconds ? planTime.MaxSeconds : lossSeconds;
                        break;
                    }
                    else if (startTime > planEndTime || endTime < planStartTime)
                    { //故障时间不在计划时间内
                        continue;
                    }
                    //交集处理
                    if (startTime < planStartTime)
                        tempStart = planStartTime;
                    else
                        tempStart = startTime;
                    if (endTime > planEndTime)
                        tempEnd = planEndTime;
                    else
                        tempEnd = endTime;

                    //故障时间包含多个计划时间的特殊处理
                    tempPlanEffectSeconds = (int)new TimeSpan(tempEnd.Ticks - tempStart.Ticks).TotalSeconds;
                    //是否超过最大计划停机时间
                    if (tempPlanEffectSeconds > planTime.MaxSeconds)
                        planEffectSeconds += planTime.MaxSeconds;
                    else
                        planEffectSeconds += tempPlanEffectSeconds;
                }
            }

            lossSeconds -= planEffectSeconds;
        }

        /// <summary>
        /// 保存统计结果
        /// </summary>
        /// <param name="datas"></param>
        private void SaveStatResult(List<StatEquipmentRuningRecord> datas)
        {
            foreach (var data in datas)
            {
                StatEquipmentRuningRecord oldStat = Queryable().Where(it => it.EquipmentId == data.EquipmentId && it.StatDate == data.StatDate && it.ClassSeq == data.ClassSeq).First();
                if (oldStat != null)
                {
                    data.StatId = oldStat.StatId;
                    Update(data, false);
                }
                else
                {
                    data.StatId = Context.Insertable(data).ExecuteReturnIdentity();
                }
                //更新故障报警统计数据
                Context.Deleteable<StatEquipmentRuningWarn>().Where(it => it.StatId == data.StatId).ExecuteCommand();
                if (data.StatEquipmentRuningWarnNav != null && data.StatEquipmentRuningWarnNav.Count() > 0)
                {
                    foreach (var item in data.StatEquipmentRuningWarnNav)
                    {
                        item.StatId = data.StatId;
                    }
                    Context.Insertable<StatEquipmentRuningWarn>(data.StatEquipmentRuningWarnNav).ExecuteCommand();
                }
            }
        }

        #endregion 设备运行数据分析统计
    }
}