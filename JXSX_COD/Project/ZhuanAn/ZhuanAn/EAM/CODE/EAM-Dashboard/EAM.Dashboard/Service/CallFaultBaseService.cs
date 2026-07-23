using EAM.Dashboard.Model.Dto;
using EAM.Dashboard.Service.IService;
using EAM.Model.Basic;
using EAM.Model.Call;
using EAM.Model.Constant;
using EAM.Model.Enums;
using EAM.ServiceCore;
using SqlSugar;
using System.Globalization;

namespace EAM.Dashboard.Service
{
    /// <summary>
    /// 故障记录Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ICallFaultBaseService), ServiceLifetime = LifeTime.Transient)]
    public class CallFaultBaseService : BaseService<CallFaultBase>, ICallFaultBaseService
    {
        public CallFaultBaseService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 获取指定日期所在周别的第一天（以星期一为第一天）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDayMon(DateTime dateTime)
        {
            int weeknow = Convert.ToInt32(dateTime.DayOfWeek);
            weeknow = (weeknow == 0 ? 6 : weeknow - 1); // 如果是星期天，则向前推6天
            int daydiff = (-1) * weeknow;
            return dateTime.AddDays(daydiff);
        }

        public List<CallArea> GetAreaList()
        {
            return Context.Queryable<CallArea>().ToList();
        }

        /// <summary>
        /// 查询区域未完结的呼叫
        /// </summary>
        /// <param name="LineId"></param>
        /// <returns></returns>
        public List<CallFaultBaseVo> GetUnsolvedCallFaultBase(int AreaId)
        {
            List<int> lineIds = new List<int>();
            if (AreaId > 0)
            {
                //获取产线信息
                lineIds.AddRange(GetLinesByArea(AreaId));
            }

            //故障信息
            var response = Queryable()
                 .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在 && it.FaultStatus != CallFaultStatusConstant.已完成 && it.FaultStatus != CallFaultStatusConstant.已中止)
                 .WhereIF(AreaId > 0, it => lineIds.Contains((int)it.LineId))
                 .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                 .LeftJoin<Station>((it, l, s) => it.StationId == s.StationId)
                 .LeftJoin<Employee>((it, l, s, e1) => it.HandlerNo == e1.EmpCode)
                 .LeftJoin<Employee>((it, l, s, e1, e2) => it.HelperNo == e2.EmpCode)
                 .LeftJoin<Employee>((it, l, s, e1, e2, e3) => it.SolverNo == e3.EmpCode)
                 .LeftJoin<Employee>((it, l, s, e1, e2, e3, e4) => it.QcNo == e4.EmpCode)
                 .OrderBy(it => it.CreateTime, OrderByType.Desc)
                 .Select((it, l, s, e1, e2, e3, e4) => new CallFaultBaseVo()
                 {
                     LineName = l.LineName,
                     StationName = s.StationName,
                     HandlerName = e1.EmpName,
                     HelperName = e2.EmpName,
                     SolverName = e3.EmpName,
                     QcName = e4.EmpName,
                     CreateTime = it.CreateTime,
                 }, true)
                 .ToList();

            if (response != null)
                foreach (var item in response)
                {
                    if (item.CallTargetType == CallTargetTypeConstant.生技)
                        item.CallTargetTypeLabel = "生技";
                    else if (item.CallTargetType == CallTargetTypeConstant.品质)
                        item.CallTargetTypeLabel = "品质";
                    else if (item.CallTargetType == CallTargetTypeConstant.生产)
                        item.CallTargetTypeLabel = "生产";
                    else if (item.CallTargetType == CallTargetTypeConstant.物料)
                        item.CallTargetTypeLabel = "物料";
                }

            return response;
        }

        /// <summary>
        /// 月次数统计
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public List<ChartBaseVo<string, int>> GetMonthCountStat(int AreaId)
        {
            List<int> lineIds = new List<int>();
            if (AreaId > 0)
            {
                //获取产线信息
                lineIds.AddRange(GetLinesByArea(AreaId));
            }

            var time = DateTime.Now;
            var startDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00")).AddMonths(-5);
            var monthArray = Enumerable.Range(0, 6).Select(it => time.AddMonths(-it).ToString("yyyy-MM")).ToList();//转成月份数组
            monthArray.Reverse();
            var queryableLeft = Context.Reportable(monthArray)
                .ToQueryable<string>();

            var queryableRight = Context.Queryable<CallFaultBase>()
               .Where(x2 => x2.DelFlag == (int)DeleteFlagEnum.存在 && x2.CreateTime >= startDate)
               .WhereIF(AreaId > 0, x2 => lineIds.Contains((int)x2.LineId))
               .GroupBy(x2 => x2.CreateTime.Value.ToString("yyyy-MM"))
               .Select(x2 => new ChartBaseVo<string, int>()
               {
                   Name = x2.CreateTime.Value.ToString("yyyy-MM"),
                   Value = SqlFunc.AggregateCount(x2.CallId)
               });

            var list = Context.Queryable(queryableLeft, queryableRight, JoinType.Left, (x1, x2)
                 => x1.ColumnName == x2.Name)
                .Select((x1, x2) => new ChartBaseVo<string, int>()
                {
                    Name = x1.ColumnName,
                    Value = x2.Value,
                }).ToList();

            if (list != null && list.Count > 0)
            {
                //从新排序并修改名称
                list = list.OrderBy(it => it.Name).ToList();
                foreach (var item in list)
                {
                    item.Name = item.Name.Split('-')[1] + "月";
                }
                ;
            }

            return list;
        }

        /// <summary>
        /// 周次数统计
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public List<ChartBaseVo<string, int>> GetWeekCountStat(int AreaId)
        {
            List<int> lineIds = new List<int>();
            if (AreaId > 0)
            {
                //获取产线信息
                lineIds.AddRange(GetLinesByArea(AreaId));
            }
            //本周星期一
            DateTime monday = GetMonday();
            // 使用公历日历
            GregorianCalendar calendar = new GregorianCalendar();

            //sql集合
            List<ISugarQueryable<ChartBaseVo<string, int>>> sqls = new List<ISugarQueryable<ChartBaseVo<string, int>>>();
            for (int i = 0; i < 7; i++)
            {
                DateTime startTime = monday.AddDays(-i * 7);
                DateTime endTime = monday.AddDays(-(i - 1) * 7);
                // 计算该日期是一年中的第几周，以 Monday 为一周的开始
                int weekOfYear = calendar.GetWeekOfYear(startTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                sqls.Add(Context.Queryable<CallFaultBase>()
                                .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在 && it.CreateTime >= startTime && it.CreateTime < endTime)
                                .WhereIF(AreaId > 0, it => lineIds.Contains((int)it.LineId))
                                .Select(it => new ChartBaseVo<string, int>() { Name = weekOfYear.ToString() + "周", Value = SqlFunc.AggregateCount(it.CallId) }));
            }
            sqls.Reverse();//调整顺序
            List<ChartBaseVo<string, int>> list = Context.UnionAll(sqls).ToList();

            return list;
        }

        /// <summary>
        /// 24小时次数统计
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public List<ChartBaseVo<string, int>> GetHourCountStat(int AreaId)
        {
            List<int> lineIds = new List<int>();
            if (AreaId > 0)
            {
                //获取产线信息
                lineIds.AddRange(GetLinesByArea(AreaId));
            }

            var time = DateTime.Now;
            var startTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:00:00")).AddHours(-24);
            List<string> hourArray = Enumerable.Range(0, 24).Select(it => time.AddHours(-it)).Select(it => it.ToString("HH")).ToList();//转成小时数组
            hourArray.Reverse();

            var list = Context.Queryable<CallFaultBase>()
            .Where(x2 => x2.DelFlag == (int)DeleteFlagEnum.存在 && x2.CreateTime >= startTime)
            .WhereIF(AreaId > 0, x2 => lineIds.Contains((int)x2.LineId))
            .GroupBy(x2 => x2.CreateTime.Value.ToString("HH"))
            .Select(x2 => new ChartBaseVo<string, int>()
            {
                Name = x2.CreateTime.Value.ToString("HH"),
                Value = SqlFunc.AggregateCount(x2.CallId)
            }).ToList();

            List<ChartBaseVo<string, int>> res = new List<ChartBaseVo<string, int>>();
            foreach (string hour in hourArray)
            {
                var item = list.Where(it => it.Name == hour).FirstOrDefault();
                res.Add(new ChartBaseVo<string, int>()
                {
                    Name = hour,
                    Value = item == null ? 0 : item.Value
                });
            }
            return res;
        }

        /// <summary>
        /// 当前周别数据分析
        /// </summary>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        public WeekCallFaultStat GetWeekDataAnalyseStat(int AreaId)
        {
            WeekCallFaultStat r = new WeekCallFaultStat();

            //区域产线
            List<int> lineIds = new List<int>();
            if (AreaId > 0)
            {
                //获取产线信息
                lineIds.AddRange(GetLinesByArea(AreaId));
            }
            //本周起始日期时间
            DateTime startTime = GetWeekFirstDayMon(DateTime.Now.Date);

            //本周呼叫数据
            var callList = Queryable()
                .LeftJoin<Employee>((it, e1) => it.HandlerNo == e1.EmpCode)
                .Where((it, e1) => it.DelFlag == (int)DeleteFlagEnum.存在 && it.EndTime != null && it.EquipmentType != null && it.HandlerNo != null && it.CreateTime >= startTime && it.EndTime <= DateTime.Now)
                .WhereIF(AreaId > 0, (it, e1) => lineIds.Contains((int)it.LineId))
                .Select((it, e1) => new CallFaultBaseVo()
                {
                    HandlerName = e1.EmpName,
                    CreateTime = it.CreateTime
                }, true)
                .ToList();

            //人员时间(Top5)
            var empData = callList.GroupBy(it => new { it.HandlerNo, it.HandlerName }).Select(g => new ChartBaseVo<string, int>() { Name = g.Key.HandlerName, Value = g.Sum(it => it.HandleMinute ?? 0) }).OrderByDescending(s => s.Value).Take(5).ToList();

            //设备故障次数(Top5)
            var equipmentData = callList.GroupBy(it => new { it.EquipmentName }).Select(g => new ChartBaseVo<string, int>() { Name = g.Key.EquipmentName, Value = g.Count() }).OrderByDescending(s => s.Value).Take(5).ToList();

            //设备故障TOP5的故障类别分析
            List<string> faultTypeYdata = new List<string>();
            List<List<ChartBaseVo<string, double>>> faultTypeData = new List<List<ChartBaseVo<string, double>>>();

            if (equipmentData.Any())
            {
                for (int j = 0; j < equipmentData.Count; j++)
                {
                    var equipment = equipmentData[j];
                    string key = equipment.Name + "(" + equipment.Value + ")";
                    faultTypeYdata.Add(key);
                    faultTypeData.Add(new List<ChartBaseVo<string, double>>());
                    var list = callList.Where(it => it.EquipmentName == equipment.Name).GroupBy(it => new { it.FaultContent }).Select(g => new ChartBaseVo<string, int>() { Name = g.Key.FaultContent, Value = g.Count() }).OrderByDescending(s => s.Value).Take(3).ToList();
                    int total = list.Sum(it => it.Value);
                    for (int i = 0; i < list.Count; i++)
                    {
                        faultTypeData[j].Add(new ChartBaseVo<string, double>()
                        {
                            Name = list[i].Name,
                            Value = Math.Round((double)list[i].Value / total, 2),
                        });
                    }
                }
            }

            r.EmpTimeData = empData;
            r.EquipmentData = equipmentData;
            r.FaultTypeYData = faultTypeYdata;
            r.FaultTypeData = faultTypeData;

            return r;
        }

        /// <summary>
        /// 获取指定秒数之后需要定时播放的任务
        /// </summary>
        /// <param name="AreaId"></param>
        /// <param name="intervalSeconds">间隔秒数，查询当前时间之后多少秒内需要执行的任务</param>
        /// <returns></returns>
        public List<ReadyCallScheduledTaskDto> GetCallScheduledTask(int AreaId, int intervalSeconds)
        {
            if (intervalSeconds == 0 || intervalSeconds < 60)
                throw new CustomException("刷新间隔时间不要小于60秒");
            if (intervalSeconds > 60 * 60 * 24)
                throw new CustomException("刷新间隔时间不要大于1天");

            DateTime now = DateTime.Now;
            DateTime startTime = now;
            DateTime endTime = now.AddSeconds(intervalSeconds);

            List<CallScheduledTask> taskList = Context.Queryable<CallScheduledTask>().Where(it => (it.AreaId == AreaId || it.AreaId == null) && it.Enable == true).ToList();
            List<ReadyCallScheduledTaskDto> response = new List<ReadyCallScheduledTaskDto>();
            //当日
            foreach (CallScheduledTask task in taskList)
            {
                DateTime tempTime = DateTime.Parse(now.Date.ToString("yyyy-MM-dd") + " " + task.TaskTime);
                if (tempTime >= startTime && tempTime < endTime)
                    response.Add(new ReadyCallScheduledTaskDto()
                    {
                        CallTaskId = task.CallTaskId,
                        TaskName = task.TaskName,
                        TaskTime = task.TaskTime,
                        PlayCount = task.PlayCount,
                        PlayMedium = task.PlayMedium,
                        FileId = task.FileId,
                        TextContent = task.TextContent,
                        ExecuteWaitSeconds = (int)((TimeSpan)(tempTime - startTime)).TotalSeconds,
                        ExecuteTaskTime = tempTime,
                    });
            }

            //次日，跨天处理
            foreach (CallScheduledTask task in taskList)
            {
                DateTime tempTime = DateTime.Parse(now.Date.AddDays(1).ToString("yyyy-MM-dd") + " " + task.TaskTime);
                if (tempTime >= startTime && tempTime < endTime)
                    response.Add(new ReadyCallScheduledTaskDto()
                    {
                        CallTaskId = task.CallTaskId,
                        TaskName = task.TaskName,
                        TaskTime = task.TaskTime,
                        PlayCount = task.PlayCount,
                        PlayMedium = task.PlayMedium,
                        FileId = task.FileId,
                        TextContent = task.TextContent,
                        ExecuteWaitSeconds = (int)((TimeSpan)(tempTime - startTime)).TotalSeconds,
                        ExecuteTaskTime = tempTime,
                    });
            }

            return response;
        }

        private DateTime GetMonday()
        {
            //获取本周一日期
            DateTime today = DateTime.Today; // 获取今天的日期（时间部分为 00:00:00）
            // 计算今天是星期几（DayOfWeek 枚举：Sunday=0, Monday=1, ..., Saturday=6）
            int dayOfWeek = (int)today.DayOfWeek;
            // 计算距离本周一的天数差
            // 如果今天是周日 (DayOfWeek.Sunday = 0)，则需要往前推 6 天
            int daysToMonday = dayOfWeek == 0 ? 6 : dayOfWeek - 1;
            // 得到本周一的日期
            return today.AddDays(-daysToMonday);
        }

        private List<int> GetLinesByArea(int areaId)
        {
            return Context.Queryable<CallAreaLine>().Where(it => it.AreaId == areaId).Select(it => it.LineId).ToList();
        }
    }
}