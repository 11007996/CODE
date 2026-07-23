using BigData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BigData.DAL
{
    public class MachineDAL
    {

        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <returns></returns>
        public IList<MachineState> GetAllMachineState()
        {
            string sql = "Select * From O_MachineState_T";
            DataTable dt = DBUtil.GetDataTable(sql);
            return TableToListModel<MachineState>(dt);
        }


        /// <summary>
        /// 获取设备分布
        /// </summary>
        /// <returns></returns>
        public IList<MachineDistribute> GetAllMachineDistribute()
        {
            string sql = "Select * From O_MachineDistribute_T";
            DataTable dt = DBUtil.GetDataTable(sql);
            return TableToListModel<MachineDistribute>(dt);
        }

        /// <summary>
        /// 获取最近30天呼叫次数
        /// </summary>
        /// <returns></returns>
        public DataTable GetCallStatByMonth()
        {
            string sql = @"SELECT convert(VARCHAR(100),StartTime,23) as StartTime,
                            COUNT(1) as ErrorCount
                            FROM M_ErrorRecord_T(NOLOCK) WHERE
                            StartTime >= DATEADD(DAY, -29, GETDATE())
                         GROUP BY CONVERT(VARCHAR(100),StartTime,23) ORDER BY StartTime asc";
            return DBUtil.GetDataTable(sql);
        }

        /// <summary>
        /// 获取呼叫统计
        /// </summary>
        /// <param name="monthNum">月数</param>
        /// <param name="weekNum">周数</param>
        /// <returns></returns>
        public DataSet GetCallStatForMonthAndWeek(int monthNum, int weekNum)
        {
            string sql = string.Format(@"SELECT CAST(MONTH( a.StartTime) AS VARCHAR)+'月' MonthNo, COUNT(1) ErrorCount,CONVERT(VARCHAR(7),  a.StartTime,23) sortMark FROM (SELECT StartTime FROM M_ErrorRecord_T WHERE StartTime>=CONVERT(VARCHAR(7), DATEADD(MONTH, -{0},  GETDATE()),23)+'-01' AND Machine IS NOT NULL ) AS a  
GROUP BY MONTH( a.StartTime), CONVERT(VARCHAR(7),  a.StartTime,23) ORDER BY sortMark ASC;


SELECT  CAST(DATEPART(wk, a.StartTime) AS VARCHAR)+'周' AS WeekNo , COUNT(1) AS ErrorCount,DATEDIFF(wk, 0, a.StartTime) AS sortMark FROM (SELECT StartTime FROM M_ErrorRecord_T WHERE StartTime>=DATEADD(wk, DATEDIFF(wk, 0, GETDATE())-{1},0) AND Machine IS NOT NULL ) AS a  
GROUP BY DATEPART(wk, a.StartTime),DATEDIFF(wk, 0, a.StartTime) ORDER BY sortMark ASC;", monthNum, weekNum);
            string[] tableName = { "MonthDT", "WeekDT" };
            return DBUtil.GetDataSet(sql, tableName);
        }


        public DataTable CallWeekAndMonthCountStat()
        {
            DayOfWeek week = DateTime.Now.DayOfWeek;
            int diffDay = week == DayOfWeek.Sunday ? (7 - 1) : (int)week - 1;
            DateTime currWeekFirstDay = DateTime.Now.AddDays(-diffDay).Date;
            DateTime preOndWeekFirstDay = currWeekFirstDay.AddDays(-7);
            DateTime preTwoWeekFirstDay = currWeekFirstDay.AddDays(-14);
            DateTime currMonthFirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime preOneMonthFirstDay = currMonthFirstDay.AddMonths(-1);
            DateTime preTwoMonthFirstDay = currMonthFirstDay.AddMonths(-2);
            string sql = string.Format(@"SELECT  COUNT(1) ErrorCount FROM M_ErrorRecord_T WHERE StartTime>='{0}' AND Machine IS NOT NULL UNION ALL
                                         SELECT  COUNT(1) ErrorCount FROM M_ErrorRecord_T WHERE StartTime>='{1}' AND StartTime<='{0}' AND Machine IS NOT NULL UNION ALL
                                         SELECT  COUNT(1) ErrorCount FROM M_ErrorRecord_T WHERE StartTime>='{2}' AND StartTime<='{1}' AND Machine IS NOT NULL UNION ALL
                                         SELECT  COUNT(1) ErrorCount FROM M_ErrorRecord_T WHERE StartTime>='{4}' AND StartTime<='{3}' AND Machine IS NOT NULL UNION ALL
                                         SELECT  COUNT(1) ErrorCount FROM M_ErrorRecord_T WHERE StartTime>='{5}' AND StartTime<='{4}' AND Machine IS NOT NULL", currWeekFirstDay, preOndWeekFirstDay, preTwoWeekFirstDay, currMonthFirstDay, preOneMonthFirstDay, preTwoMonthFirstDay );
            return DBUtil.GetDataTable(sql);
        }

        /// <summary>
        /// 获取最近24小时呼叫次数
        /// </summary>
        /// <returns></returns>
        public DataTable GetCallStatByOneDay()
        {
            string sql = @"SELECT  COUNT(1) as [count], DATEPART(HH, StartTime) as [Hour]
                            FROM [M_ErrorRecord_T](NOLOCK) WHERE StartTime>=DATEADD(Hour, -23, GETDATE())
                            GROUP BY  DATEPART(HH, StartTime) 
                            ORDER BY [Hour] asc";
            return DBUtil.GetDataTable(sql);
        }



        /// <summary>
        /// 获取24小时内设备的精简上报记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetRefineReportOneDay(DateTime startTimeMark)
        {

            string sql = string.Format(@"WITH cte AS (
				SELECT *,
					LAG ( RunState,1,0 ) OVER (PARTITION BY MachineCode ORDER BY  CreateTime  ) AS PreRunState,
					LAG ( WarnState,1,0 ) OVER (PARTITION BY MachineCode ORDER BY  CreateTime  ) AS PreWarnState,
					LEAD ( RunState,1,0 ) OVER (PARTITION BY MachineCode  ORDER BY  CreateTime  ) AS NextRunState,
					LEAD ( ProductCount,1,0 ) OVER (PARTITION BY MachineCode  ORDER BY  CreateTime  ) AS NextProductCount,
					LEAD ( FailedCount,1,0 ) OVER (PARTITION BY MachineCode  ORDER BY  CreateTime  ) AS NextFailedCount
				FROM
					dbo.M_MachineReport_T WITH(NOLOCK) WHERE  CreateTime>='{0}' AND CreateTime<=GETDATE()
		)
			SELECT
				*,
				LEAD ( CreateTime,1,NULL ) OVER (PARTITION BY MachineCode  ORDER BY  CreateTime  ) AS EndTime
			FROM
				cte WHERE RunState!=PreRunState OR WarnState!=PreWarnState  OR NextProductCount<ProductCount OR NextFailedCount<FailedCount  ", startTimeMark);
            return DBUtil.GetDataTable(sql);
        }

        public DataTable GetMachineStat()
        {
            string sql = @"SELECT  MachineCode,MachineName+'('+CONVERT(varchar, MachineNo)  +')' AS MachineName,Line,TheoryCT,
                            0.0 AS [OEE],0 AS [ErrorCount],0 AS[ProductCount],0 AS [LastProductCount],0 AS [FailedCount],
                            0 AS [RunState],0 AS [WarnState],0.0 AS [TimeUR],0.0 AS [EfficacyUR],0.0 AS [PassR],'' LastReportTime,0 AS [WarnCode] FROM M_Machine_T(NOLOCK) ";
            return DBUtil.GetDataTable(sql);
        }

        public DataTable GetPlanRestTimes()
        {
            string sql = "SELECT * FROM M_PlanTime_T";
            return DBUtil.GetDataTable(sql);
        }


        /// <summary>
        /// 获取最新的5分钟内的每个设备上报记录
        /// </summary>
        /// <returns></returns>
        public IList<MachineReport> GetCurrLastReport()
        {
            DateTime startTime = DateTime.Now.AddMinutes(100);
            string sql = string.Format("SELECT MachineCode,ProductCount,CONVERT(varchar(100), CreateTime, 24) CreateTime,RunState,WarnState FROM M_MachineReport_T(NOLOCK)  WHERE  CreateTime>=DATEADD(mi, -5, GETDATE()) order by CreateTime Desc");
            DataTable dt = DBUtil.GetDataTable(sql);
            return TableToListModel<MachineReport>(dt);
        }

        public IList<Machine> GetMachineBaseInfo()
        {
            DateTime startTime = DateTime.Now.AddMinutes(100);
            string sql = string.Format("SELECT  Line,MachineCode,CONCAT(MachineName,'(',MachineNo,')') MachineName,0 ProductCount FROM M_Machine_T");
            DataTable dt = DBUtil.GetDataTable(sql);
            return TableToListModel<Machine>(dt);
        }

        public IList<MachinePower> GetMachinePowerInfo()
        {
            DateTime startTime = DateTime.Now.AddMinutes(100);
            string sql = string.Format("SELECT  MachineCode,Power FROM M_Machine_T");
            DataTable dt = DBUtil.GetDataTable(sql);
            return TableToListModel<MachinePower>(dt);
        }



        public Dictionary<string, IList<MachineReportCount>> GetMachineRunTime()
        {
            Dictionary<string, IList<MachineReportCount>> dic = new Dictionary<string, IList<MachineReportCount>>();
            string sql = @"with cte_90 as( SELECT  MachineCode FROM M_MachineReport_T(NOLOCK) WHERE CreateTime >=DATEADD(DAY, -90, GETDATE()) GROUP BY MachineCode,Convert(varchar(16),CreateTime,25))
                                SELECT MachineCode,Count(*) count FROM cte_90 GROUP BY MachineCode;
                            with cte_30 as( SELECT  MachineCode FROM M_MachineReport_T(NOLOCK) WHERE CreateTime >=DATEADD(DAY, -30, GETDATE()) GROUP BY MachineCode,Convert(varchar(16),CreateTime,25))
                                SELECT MachineCode,Count(*) count FROM cte_30 GROUP BY MachineCode;
                            with cte_1 as( SELECT  MachineCode FROM M_MachineReport_T(NOLOCK) WHERE CreateTime >=DATEADD(DAY, -1, GETDATE()) GROUP BY MachineCode,Convert(varchar(16),CreateTime,25))
                                SELECT MachineCode,Count(*) count FROM cte_1 GROUP BY MachineCode;";
            string[] tableNames = new string[] { "day90", "day30", "day1" };
            DataSet ds = DBUtil.GetDataSet(sql, tableNames);
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    dic.Add(dt.TableName, TableToListModel<MachineReportCount>(dt));
                }
            }
            return dic;
        }

        /// <summary>
        /// DataTable转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>

        public static List<T> TableToListModel<T>(DataTable dt) where T : new()
        {
            if (dt == null) return null;
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
