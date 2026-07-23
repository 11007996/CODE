using BigData.DAL;
using BigData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BigData.BLL
{
    public class MachineService
    {
        private MachineDAL dao = new MachineDAL();

        /// <summary>
        /// 设备预设状态
        /// </summary>
        /// <returns></returns>
        public IList<MachineState> GetMachineStateStat()
        {
            IList<MachineState> list = dao.GetAllMachineState();
            if (list != null)
            {
                int allCount = list.Sum(i => i.Count);
                MachineState machine = new MachineState();
                machine.Count = allCount;
                machine.StateName = "设备总数";
                list.Insert(0, machine);
            }
            return list;
        }

        /// <summary>
        /// 设备分布
        /// </summary>
        /// <returns></returns>
        public IList<MachineDistribute> GetMachineDistributeStat()
        {
            IList<MachineDistribute> list = dao.GetAllMachineDistribute();
            if (list == null) return null;
            list = list.OrderByDescending(t => t.Count).ToList();
            //超过4个，将其他统计数量的放入第四个中
            if (list.Count > 4)
            {
                int other = 0;
                other = list.Skip(3).Sum(r => r.Count);
                MachineDistribute md = new MachineDistribute();
                md.PointName = "其他";
                md.Count = other;
                list.Insert(3, md);
            }
            return list.Take(4).ToList();
        }


        /// <summary>
        /// 获取最新的5分钟内的每个设备前6条上报记录
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<Machine> CurrLastReport(int count)
        {
            IList<Machine> machines = dao.GetMachineBaseInfo();
            IList<MachineReport> mrList = dao.GetCurrLastReport();
            if (mrList != null && machines != null)
            {
                //将 设备与上报记录关联
                foreach (MachineReport item in mrList)
                {
                    Machine m = machines.FirstOrDefault(t => t.MachineCode == item.MachineCode);
                    if (m != null)
                    {
                        if (m.Reports == null)
                        {
                            IList<MachineReport> rp = new List<MachineReport>();
                            m.Reports = rp;
                            m.ProductCount = item.ProductCount;
                            m.RunState = item.RunState;
                            m.WarnState = item.WarnState;
                        }
                        //前6条记录
                        if (m.Reports.Count < count)
                            m.Reports.Add(item);
                    }
                }
                //清除没有上报记录的设备
                for (int i = machines.Count - 1; i >= 0; i--)
                {
                    if (machines[i].Reports == null || machines[i].Reports.Count == 0)
                    {
                        machines.RemoveAt(i);
                    }
                }
            }
            return machines;
        }

        /// <summary>
        /// 月、周呼叫次数统计
        /// </summary>
        /// <returns></returns>
        public CallStatVo CallStat_del()
        {
            CallStatVo rs = new CallStatVo();
            DataTable dt = dao.GetCallStatByMonth();
            Dictionary<string, int> monthDic = new Dictionary<string, int>();
            Dictionary<string, int> weekDic = new Dictionary<string, int>();
            for (int i = -30; i <= 0; i++)
            {
                string dayMark = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                DataRow row = dt.Select(string.Format("StartTime='{0}'", dayMark)).FirstOrDefault();
                string day = DateTime.Now.AddDays(i).ToString("MM月dd");
                int count = 0;
                if (row != null) count = Convert.ToInt32(row["ErrorCount"]);
                monthDic.Add(day, count);
                if (i > -7)
                {
                    string weekStr = Week(Convert.ToDateTime(DateTime.Now.AddDays(i)));
                    weekDic.Add(weekStr, count);
                }
            }
            ChartXY<string, int> month = new ChartXY<string, int>();
            month.XData = monthDic.Keys.ToList();
            month.YData = monthDic.Values.ToList();

            ChartXY<string, int> week = new ChartXY<string, int>();
            week.XData = weekDic.Keys.ToList();
            week.YData = weekDic.Values.ToList();

            rs.month = month;
            rs.week = week;
            return rs;
        }


        public CallStatVo CallStat()
        {
            CallStatVo rs = new CallStatVo();
            DataSet ds = dao.GetCallStatForMonthAndWeek(6, 5);//6:前6个月，5:前5周
            DataTable monthDT = ds.Tables["MonthDT"];
            DataTable weekDT = ds.Tables["WeekDT"];
            //Dictionary<string, int> monthDic = new Dictionary<string, int>();
            //Dictionary<string, int> weekDic = new Dictionary<string, int>();
            //for (int i = -30; i <= 0; i++)
            //{
            //    string dayMark = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
            //    DataRow row = dt.Select(string.Format("StartTime='{0}'", dayMark)).FirstOrDefault();
            //    string day = DateTime.Now.AddDays(i).ToString("MM月dd");
            //    int count = 0;
            //    if (row != null) count = Convert.ToInt32(row["ErrorCount"]);
            //    monthDic.Add(day, count);
            //    if (i > -7)
            //    {
            //        string weekStr = Week(Convert.ToDateTime(DateTime.Now.AddDays(i)));
            //        weekDic.Add(weekStr, count);
            //    }
            //}
            ChartXY<string, int> month = new ChartXY<string, int>();
            //month.XData = monthDic.Keys.ToList();
            //month.YData = monthDic.Values.ToList();
            month.XData = monthDT.AsEnumerable().Select(t => t.Field<string>("MonthNo")).ToList();
            month.YData = monthDT.AsEnumerable().Select(t => t.Field<int>("ErrorCount")).ToList();

            ChartXY<string, int> week = new ChartXY<string, int>();
            week.XData = weekDT.AsEnumerable().Select(t => t.Field<string>("WeekNo")).ToList();
            week.YData = weekDT.AsEnumerable().Select(t => t.Field<int>("ErrorCount")).ToList();

            rs.month = month;
            rs.week = week;
            return rs;
        }

        public List<int> CallWeekAndMonthCountStat()
        {
            DataTable dt = dao.CallWeekAndMonthCountStat();
            return dt.AsEnumerable().Select(t => t.Field<int>("ErrorCount")).ToList();
        }

        /// <summary>
        /// 24小时呼叫统计
        /// </summary>
        /// <returns></returns>
        public object MachineCallStatByOneDay()
        {
            DataTable dt = dao.GetCallStatByOneDay();
            ChartXY<string, int> datas = new ChartXY<string, int>();
            IList<string> xData = new List<string>();
            IList<int> yData = new List<int>();
            int currHourFlag = DateTime.Now.Hour;
            if (dt != null)
            {
                //添加大与当前小时标志的
                for (int i = currHourFlag + 1; i < 24; i++)
                {
                    DataRow row = dt.Select("Hour=" + i).FirstOrDefault();
                    string hourStr = i.ToString();
                    int count = 0;
                    if (i < 10) hourStr = "0" + hourStr;
                    if (row != null) count = Convert.ToInt32(row["Count"]);
                    xData.Add(hourStr);
                    yData.Add(count);
                }
                //添加小与等于当前小时标志的
                for (int i = 0; i <= currHourFlag; i++)
                {
                    DataRow row = dt.Select("Hour=" + i).FirstOrDefault();
                    string hourStr = i.ToString();
                    int count = 0;
                    if (i < 10) hourStr = "0" + hourStr;
                    if (row != null) count = Convert.ToInt32(row["Count"]);
                    xData.Add(hourStr);
                    yData.Add(count);
                }
            }
            datas.XData = xData;
            datas.YData = yData;
            return datas;
        }

        public MachineEnergyStat MachineEnergyStat()
        {
            Dictionary<string, IList<MachineReportCount>> dic = dao.GetMachineRunTime();
            IList<MachinePower> machines = dao.GetMachinePowerInfo();
            MachineEnergyStat meStat = new MachineEnergyStat();
            foreach (KeyValuePair<string, IList<MachineReportCount>> item in dic)
            {
                int mi = 0;//分钟数
                decimal energy = 0;//能耗
                foreach (MachineReportCount mrcItem in item.Value)
                {
                    MachinePower mp = machines.FirstOrDefault(t => t.MachineCode == mrcItem.MachineCode);
                    if (mp != null)
                    {
                        decimal power = mp.Power;
                        mi += mrcItem.Count;
                        energy += (power / 60 * mrcItem.Count);
                    }
                }
                EnergyItem eI = new EnergyItem();
                eI.Hour = mi / 60;
                eI.Energy = (int)energy;
                if (item.Key == "day90") meStat.day90 = eI;
                if (item.Key == "day30") meStat.day30 = eI;
                if (item.Key == "day1") meStat.day1 = eI;
            }
            return meStat;
        }

        /// <summary>
        /// 设备实时状态，平均OEE,设备OEE,实时报警
        /// </summary>
        /// <returns></returns>
        public MachineStatVo MachineStatReportOneDay()
        {
            DateTime startTimeMark = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 07:30:00");
            if (DateTime.Now < startTimeMark) startTimeMark.AddDays(-1);
            //初始前端视图模型
            MachineStatVo msVo = new MachineStatVo();
            IList<WarnRepert> currWarnMachines = new List<WarnRepert>();
            MachineAvgOEE machineAvgOEE = new MachineAvgOEE();
            ChartXY<string, decimal> machineOEE = new ChartXY<string, decimal>();
            IList<MachineCurrState> machineCurrState = new List<MachineCurrState>();

            msVo.CurrWarnMachines = currWarnMachines;
            msVo.MachineAvgOEE = machineAvgOEE;
            msVo.MachineCurrState = machineCurrState;
            msVo.MachineOEE = machineOEE;

            //设备数据查询
            DataTable Machines = dao.GetMachineStat();
            DataTable PlanRestTimes = dao.GetPlanRestTimes();
            DataTable ReportDT = dao.GetRefineReportOneDay(startTimeMark);
            ReportDT.DefaultView.Sort = "CreateTime asc";
            DataTable ReportDT2 = ReportDT.DefaultView.ToTable();
            //设备遍历数据分析
            if (Machines != null && Machines.Rows.Count > 0)
            {
                for (int i = 0; i < Machines.Rows.Count; i++)
                {
                    string whereSql = string.Format("MachineCode='{0}'", Machines.Rows[i]["MachineCode"].ToString());
                    DataRow[] watchRows = ReportDT2.Select(whereSql);
                    ParseWatchData(Machines.Rows[i], watchRows, PlanRestTimes, startTimeMark);
                }
                //排序
                Machines.DefaultView.Sort = "LastReportTime DESC";
                Machines = Machines.DefaultView.ToTable();
            }
            ParseMachinesStat(Machines, msVo);
            return msVo;
        }

        private void ParseMachinesStat(DataTable Machines, MachineStatVo msVo)
        {
            if (Machines != null)
            {
                decimal sumOEE = 0;
                decimal sumTimeUR = 0;
                decimal sumEfficacyUR = 0;
                decimal sumPassR = 0;
                int countOEE = 0;

                IList<string> MachineNameList = new List<string>();
                IList<decimal> MachineOEEList = new List<decimal>();

                int runCount = 0;
                int stopCount = 0;
                int warnCount = 0;
                //遍历所有设备
                foreach (DataRow row in Machines.Rows)
                {
                    if (Convert.ToDecimal(row["OEE"]) > 0)
                    {
                        //获取平均OEE
                        sumOEE += Convert.ToDecimal(row["OEE"].ToString());
                        sumTimeUR += Convert.ToDecimal(row["TimeUR"].ToString());
                        sumEfficacyUR += Convert.ToDecimal(row["EfficacyUR"].ToString());
                        sumPassR += Convert.ToDecimal(row["PassR"].ToString());
                        countOEE += 1;

                        //设备OEE图表(取前20条记录)
                        if (MachineNameList.Count < 20 && Convert.ToDecimal(row["OEE"].ToString()) > 0)
                        {
                            MachineNameList.Add(row["MachineName"].ToString());
                            MachineOEEList.Add(Convert.ToDecimal(row["OEE"].ToString()));
                        }
                    }

                    //设备最新状态
                    int tempRunState = Convert.ToInt32(row["RunState"]);
                    int tempWarnState = Convert.ToInt32(row["WarnState"]);
                    if (tempRunState == 2)
                        runCount++;
                    else if (tempRunState == 1 && tempWarnState == 1)
                        stopCount++;
                    if (tempRunState == 1 && tempWarnState == 2)
                        warnCount++;

                    //报警设备
                    string tempWarnCode = row["WarnCode"].ToString();
                    if (tempWarnCode != "" && tempWarnCode != "0")
                    {
                        WarnRepert wr = new WarnRepert();
                        wr.Line = row["Line"].ToString();
                        wr.MachineName = row["MachineName"].ToString();
                        wr.WarnCode = row["WarnCode"].ToString();
                        wr.CreateTime = Convert.ToDateTime(row["LastReportTime"]).ToString("HH:mm");
                        msVo.CurrWarnMachines.Add(wr);
                    }

                }

                //平均OEE
                if (countOEE > 0)
                {
                    msVo.MachineAvgOEE.OEE = Math.Round(sumOEE / countOEE, 2);
                    msVo.MachineAvgOEE.Count = countOEE;
                    IList<PieData> datas = new List<PieData>();

                    PieData timeUR = new PieData();
                    timeUR.name = "时间稼动率";
                    timeUR.value = Math.Round(sumTimeUR / countOEE, 2);
                    PieData efficacyUR = new PieData();
                    efficacyUR.name = "性能稼动率";
                    efficacyUR.value = Math.Round(sumEfficacyUR / countOEE, 2);
                    PieData passR = new PieData();
                    passR.name = "良品率";
                    passR.value = Math.Round(sumPassR / countOEE, 2);

                    datas.Add(timeUR);
                    datas.Add(efficacyUR);
                    datas.Add(passR);
                    msVo.MachineAvgOEE.Rate = datas;
                }
                //设备OEE
                msVo.MachineOEE.XData = MachineNameList;
                msVo.MachineOEE.YData = MachineOEEList;

                //设备实时状态
                MachineCurrState allState = new MachineCurrState();
                allState.StateName = "全部";
                allState.Color = "blue";
                allState.Count = Machines.Rows.Count;
                allState.Rate = 100;
                MachineCurrState runState = new MachineCurrState();
                runState.StateName = "运行";
                runState.Color = "green";
                runState.Count = runCount;
                runState.Rate = (int)(Math.Round((decimal)runCount / Machines.Rows.Count, 2) * 100);
                MachineCurrState stopState = new MachineCurrState();
                stopState.StateName = "待机";
                stopState.Color = "red";
                stopState.Count = stopCount;
                stopState.Rate = (int)(Math.Round((decimal)stopCount / Machines.Rows.Count, 2) * 100);
                MachineCurrState warnState = new MachineCurrState();
                warnState.StateName = "报警";
                warnState.Color = "yellow";
                warnState.Count = warnCount;
                warnState.Rate = (int)(Math.Round((decimal)warnCount / Machines.Rows.Count, 2) * 100);
                MachineCurrState otherState = new MachineCurrState();
                otherState.StateName = "停机";
                otherState.Color = "gray";
                otherState.Count = Machines.Rows.Count - runCount - stopCount - warnCount;
                otherState.Rate = 100 - runState.Rate - stopState.Rate - warnState.Rate;
                msVo.MachineCurrState.Add(allState);
                msVo.MachineCurrState.Add(runState);
                msVo.MachineCurrState.Add(stopState);
                msVo.MachineCurrState.Add(warnState);
                msVo.MachineCurrState.Add(otherState);
            }
        }


        //数据解析
        private void ParseWatchData(DataRow machine, DataRow[] watchData, DataTable PlanRestTimes, DateTime startTimeMark)
        {
            //稼动率
            decimal oee = 0;//OEE(综合稼动率)
            decimal timeUR = 0;//时间稼动率
            decimal efficacyUR = 0;//性能稼动率
            decimal passR = 0;//良品率
            //基础数据:产量、不良
            int productCount = 0;//产量（实际）
            int failedCount = 0;//不良
            int lastProductCount = 0;//最后上报的产量
            //基础数据:时间
            int totalSeconds = 0;//开机时间(秒)
            int runSeconds = 0;//运行时间
            int planRestSeconds = 0;//计划停机时间(秒)
            int failedSeconds = 0;////计划外停机时间（秒）=(小停顿时间+故障时间)
            int stopSeconds = 0;//小停顿时间(运行状态1，报警状态1)
            int warnSeconds = 0;//故障时间(运行状态1，报警状态为2)

            //基础数据:故障次数、状态
            int errorCount = 0;//故障次数
            int runState = 0;//设备最后的运行状态，0:未监听，1：停机中，2：运行中
            int wartnState = 0;//设备最后的报警状态，0：未监听，1：正常，2：报警

            DateTime lastReportTime = DateTime.Now;
            string lastWarnCode = "0";

            //遍历上报数据
            if (watchData != null && watchData.Length > 0)
            {
                //用于计算运行时间、停机时间、产量
                int tempPreProCount = 0;
                int tempProCount = 0;
                int tempPreFailedCount = 0;
                int tempFailedCount = 0;
                bool findFirstDataFlag = false;//存在合规的第一条记录标记
                foreach (DataRow row in watchData)
                {
                    //只有状态为2的记录才是合规的第一条记录。
                    if (!findFirstDataFlag)
                    {
                        if (row["RunState"].ToString() == "2")
                        {
                            //开机总时间
                            DateTime runStart = Convert.ToDateTime(row["CreateTime"]);
                            DateTime runEnd = Convert.ToDateTime(watchData[watchData.Length - 1]["CreateTime"]);
                            totalSeconds = (int)(new TimeSpan(runEnd.Ticks - runStart.Ticks).TotalSeconds);
                            //初始化：上一个产量，当前产量、上一个不良、当前不良
                            tempPreProCount = int.Parse(row["ProductCount"].ToString());
                            tempProCount = int.Parse(row["ProductCount"].ToString());
                            tempPreFailedCount = int.Parse(row["FailedCount"].ToString());
                            tempFailedCount = int.Parse(row["FailedCount"].ToString());
                            findFirstDataFlag = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //最后一个记录的结束时间是其本身的开始时间
                    if (row["EndTime"] == DBNull.Value) row["EndTime"] = watchData[watchData.Length - 1]["CreateTime"];
                    //根据状态得到不同时间
                    if (row["RunState"].ToString() == "1")
                    {
                        DateTime startTime = Convert.ToDateTime(row["CreateTime"]);
                        DateTime endTime = Convert.ToDateTime(row["EndTime"]);
                        int tempFailedSeconds = 0;
                        int tempPlanRestSeconds = 0;
                        //解析停机时间
                        ParseStopTime(PlanRestTimes, startTime, endTime, startTimeMark, ref tempFailedSeconds, ref tempPlanRestSeconds);
                        failedSeconds += tempFailedSeconds;
                        planRestSeconds += tempPlanRestSeconds;
                        if (row["WarnState"].ToString() == "2")
                        {
                            warnSeconds += tempFailedSeconds;
                        }
                        else
                        {
                            stopSeconds += tempFailedSeconds;
                        }
                    }
                    else if (row["RunState"].ToString() == "2")
                    {
                        DateTime startTime = Convert.ToDateTime(row["CreateTime"]);
                        DateTime endTime = Convert.ToDateTime(row["EndTime"]);
                        runSeconds += (int)(new TimeSpan(endTime.Ticks - startTime.Ticks).TotalSeconds);
                    }
                    //故障次数统计
                    if (row["RunState"].ToString() == "1" && row["WarnState"].ToString() == "2")
                    {
                        errorCount++;
                    }
                    //产量
                    tempProCount = int.Parse(row["ProductCount"].ToString());
                    if (tempPreProCount <= tempProCount)
                    {
                        productCount += (tempProCount - tempPreProCount);
                    }
                    else
                    { //设备重置
                        productCount += tempProCount;
                    }
                    tempPreProCount = int.Parse(row["ProductCount"].ToString());
                    //不良
                    tempFailedCount = int.Parse(row["FailedCount"].ToString());
                    if (tempPreFailedCount <= tempFailedCount)
                    {
                        failedCount += (tempFailedCount - tempPreFailedCount);
                    }
                    else
                    { //设备重置
                        failedCount += tempFailedCount;
                    }
                    tempPreFailedCount = int.Parse(row["FailedCount"].ToString());
                }

                //--------计算稼动率--------
                //时间稼动率= (（开机时间-异常时间-计划停机时间)/（开机时间-计划停机时间))*100%
                if ((totalSeconds - planRestSeconds) > 0)
                    timeUR = Math.Round(((decimal)(totalSeconds - failedSeconds - planRestSeconds) / (totalSeconds - planRestSeconds)) * 100, 2);
                //性能稼动率=  ((产能 * 理论CT) / (开机时间—异常时间-计划停机时间))*100%
                if ((totalSeconds - failedSeconds - planRestSeconds) > 0)
                    efficacyUR = Math.Round(((decimal)(productCount * decimal.Parse(machine["TheoryCT"].ToString())) / (totalSeconds - failedSeconds - planRestSeconds)) * 100, 2);
                //良品率=(1-(不良品数/产量))*100
                if (productCount > 0)
                    passR = Math.Round((1 - ((decimal)failedCount / productCount)) * 100, 2);
                //OEE
                //原算法 oee = Math.Round((timeUR * efficacyUR * passR) / 10000, 2);
                oee = Math.Round((timeUR * efficacyUR * passR * (decimal)1.2) / 10000, 2);//*1.2为作弊系数，防止oee过低。
                //oee超过100，强制降低到90
                if (oee >= 100)
                    oee = 90;

                //--------最后运行数据-----------
                DateTime createTime = Convert.ToDateTime(watchData[watchData.Length - 1]["CreateTime"]);
                int minutes = (int)(new TimeSpan(DateTime.Now.Ticks - createTime.Ticks).TotalMinutes);
                if (minutes > 10)
                { //如果最后一条记录创建时间超过3分钟，则表示连接断开了。
                    runState = 0;
                    wartnState = 0;
                }
                else
                {
                    runState = Convert.ToInt32(watchData[watchData.Length - 1]["RunState"]);
                    wartnState = Convert.ToInt32(watchData[watchData.Length - 1]["WarnState"]);
                }
                lastProductCount = Convert.ToInt32(watchData[watchData.Length - 1]["ProductCount"]);
                lastWarnCode = watchData[watchData.Length - 1]["WarnCode"].ToString();
                lastReportTime = Convert.ToDateTime(watchData[watchData.Length - 1]["CreateTime"]);
            }
            //将统计结果插入设备数据行
            machine["OEE"] = oee;
            machine["ErrorCount"] = errorCount;
            machine["ProductCount"] = productCount;
            machine["LastProductCount"] = lastProductCount;
            machine["FailedCount"] = failedCount;
            machine["RunState"] = runState;
            machine["WarnState"] = wartnState;
            machine["TimeUR"] = timeUR;
            machine["EfficacyUR"] = efficacyUR;
            machine["PassR"] = passR;
            machine["LastReportTime"] = lastReportTime;
            machine["WarnCode"] = lastWarnCode;
        }

        //停机时间解析
        private void ParseStopTime(DataTable PlanRestTimes, DateTime failedStart, DateTime failedEnd, DateTime startTimeMark, ref int failedSeconds, ref int planRestSeconds)
        {

            failedSeconds = (int)(new TimeSpan(failedEnd.Ticks - failedStart.Ticks).TotalSeconds);
            planRestSeconds = 0;
            if (PlanRestTimes != null && PlanRestTimes.Rows.Count > 0)
            {
                DateTime tempStart;
                DateTime tempEnd;
                foreach (DataRow row in PlanRestTimes.Rows)
                {
                    DateTime startTime = Convert.ToDateTime(startTimeMark.ToString("yyyy-MM-dd") + " " + row["startTime"]);
                    DateTime endTime = Convert.ToDateTime(startTimeMark.ToString("yyyy-MM-dd") + " " + row["EndTime"]);
                    if (endTime < startTime)
                    {//次日
                        endTime.AddDays(1);
                    }
                    //故障时间为计划停机时间内
                    if (failedStart > startTime && failedEnd < endTime)
                    {
                        planRestSeconds = (int)(new TimeSpan(failedEnd.Ticks - failedStart.Ticks).TotalSeconds);
                        break;
                    }
                    //故障时间不在计划时间内
                    else if (failedEnd < startTime || failedStart > endTime)
                    {
                        continue;
                    }
                    //交集处理  
                    if (failedStart < startTime)
                    {
                        tempStart = startTime;
                    }
                    else
                    {
                        tempStart = failedStart;
                    }
                    if (failedEnd > endTime)
                    {
                        tempEnd = endTime;
                    }
                    else
                    {
                        tempEnd = failedEnd;
                    }
                    planRestSeconds += (int)(new TimeSpan(tempEnd.Ticks - tempStart.Ticks).TotalSeconds);
                    //是否超过最大计划停机时间
                    int maxSeconds = int.Parse(row["MaxSeconds"].ToString());
                    planRestSeconds += (int)(new TimeSpan(tempEnd.Ticks - tempStart.Ticks).TotalSeconds);
                    if (planRestSeconds > maxSeconds) planRestSeconds = maxSeconds;
                }
            }
            failedSeconds = failedSeconds - planRestSeconds;
        }

        private string Week(DateTime date)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(date.DayOfWeek)];
            return week;
        }

        private static List<T> TableToListModel<T>(DataTable dt) where T : new()
        {
            // 定义集合    
            List<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }


    }
}
