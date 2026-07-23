using Listen.Base;
using Listen.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Utils
{
    public class AssetHelper
    {
        private static int Year;
        private static int DayStamp;
        private static int WeekStamp;
        private static int MonthStamp;
        private static int StartCheckHour = 8;
        private static int EndCheckHour = 7;

        /// <summary>
        ///  每日检查过的设备的编码
        /// </summary>
        private static List<int> MaintenanceCheckedMachines = new List<int>();
        /// <summary>
        /// 设备的保养状态字典
        /// </summary>
        private static Dictionary<int, MaintenanceStatus> MachineMaintenanceStatus = new Dictionary<int, MaintenanceStatus>();
        /// <summary>
        /// 每台设备最后的上报记录
        /// </summary>
        private static Dictionary<int, ReportInfo> MachineReport = new Dictionary<int, ReportInfo>();

        public static int _StartCheckHour { get { return StartCheckHour; } }
        /// <summary>
        /// 初始化所有的资产状态
        /// </summary>
        public static void InitAssetStatus()
        {
            //初始化资产保养状态
            MaintenanceCheckedMachines.Clear();
            MachineMaintenanceStatus.Clear();
            MachineReport.Clear();

            //时间标记计算
            DateTime now = DateTime.Now;
            //如果小于指定开始检查小时，则表示前一天
            if (now.Hour < EndCheckHour)
                now.AddDays(-1);

            Year = now.Year;
            DayStamp = now.DayOfYear;
            GregorianCalendar gregorianCalendar = new GregorianCalendar();
            WeekStamp = gregorianCalendar.GetWeekOfYear(now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//当天所属周
            MonthStamp = now.Month;
        }


        //判断是否检查过最少一次保养
        public static bool IsCheckedMaintenance(int MachineCode)
        {
            return MaintenanceCheckedMachines.Contains(MachineCode);
        }

        /// <summary>
        /// 获取指定设备编码的资产保养状态
        ///     返回值：MaintenanceCode枚举，表示保养结果编码
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public static MaintenanceCode GetAssetStatus(int machineCode)
        {
            //判断是否开启保养检查，如果都开启，返回保养正常的结果
            if (!BaseInfo.DayMaintenanceCheckFlag && !BaseInfo.WeekMaintenanceCheckFlag && !BaseInfo.MonthMaintenanceCheckFlag)
                return MaintenanceCode.OK;

            //获取已有的保养结果
            MaintenanceStatus mStatus;
            MachineMaintenanceStatus.TryGetValue(machineCode, out mStatus);
            //已有结果保养正常直接返回
            if (CheckStatus(mStatus) == MaintenanceCode.OK) return MaintenanceCode.OK;

            //------------到数据库中实时查询-------------------------------------
            string sql = string.Format(@"
                                        DECLARE @MachineCode INT;
                                        SET @MachineCode='{0}'
                                        DECLARE @AssetNo VARCHAR(50);
                                        SELECT @AssetNo=AssetNo FROM M_Machine_T WHERE MachineCode=@MachineCode ;
                                        SELECT @MachineCode MachineCode,@AssetNo AssetNo,
                                        (SELECT Count(1) FROM A_MaintenanceReportD_T WHERE AssetNo=@AssetNo AND [Year]='{1}' AND TimeMark='D' AND TimeMarkStamp='{2}') DayMaintenanceFlag,
                                        (SELECT Count(1) FROM A_MaintenanceReportD_T WHERE AssetNo=@AssetNo AND [Year]='{1}' AND TimeMark='W' AND TimeMarkStamp='{3}') WeekMaintenanceFlag,
                                        (SELECT Count(1) FROM A_MaintenanceReportD_T WHERE AssetNo=@AssetNo AND [Year]='{1}' AND TimeMark='M' AND TimeMarkStamp='{4}') MonthMaintenanceFlag ", machineCode, Year, DayStamp, WeekStamp, MonthStamp);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                mStatus = new MaintenanceStatus();
                mStatus.MachineCode = machineCode;
                mStatus.AssetNo = Convert.ToString(dt.Rows[0]["AssetNo"]);
                mStatus.DayMaintenanceFlag = Convert.ToInt16(dt.Rows[0]["DayMaintenanceFlag"]) == 1 ? true : false;
                mStatus.WeekMaintenanceFlag = Convert.ToInt16(dt.Rows[0]["WeekMaintenanceFlag"]) == 1 ? true : false;
                mStatus.MonthMaintenanceFlag = Convert.ToInt16(dt.Rows[0]["MonthMaintenanceFlag"]) == 1 ? true : false;
                //保存保养结果的数据到基础数据中
                if (!MaintenanceCheckedMachines.Contains(machineCode))
                    MaintenanceCheckedMachines.Add(machineCode);

                if (!MachineMaintenanceStatus.Keys.Contains(machineCode))
                    MachineMaintenanceStatus.Add(machineCode, mStatus);
                else
                    MachineMaintenanceStatus[machineCode] = mStatus;
            }
            return CheckStatus(mStatus);
        }

        public static MaintenanceCode CheckStatus(MaintenanceStatus mStatus)
        {
            //未传值，返回日保养未做
            DateTime now = DateTime.Now;
            if (mStatus == null) return MaintenanceCode.DAY_FALSE;
            //日  :检查开启,且当前时间不是换班时间范围内
            if (BaseInfo.DayMaintenanceCheckFlag && (now.Hour < EndCheckHour || now.Hour >= StartCheckHour))
            {
                if (!mStatus.DayMaintenanceFlag) return MaintenanceCode.DAY_FALSE;
            }
            //周  :检查开启，且当前时间为星期六或星期天
            if (BaseInfo.WeekMaintenanceCheckFlag && (now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday))
            {
                if (!mStatus.WeekMaintenanceFlag) return MaintenanceCode.WEEK_FALSE;
            }
            //月 ：检查开启，且当前时间为本月28号及28号后
            if (BaseInfo.MonthMaintenanceCheckFlag && now.Day >= 28)
            {
                if (!mStatus.MonthMaintenanceFlag) return MaintenanceCode.MONTH_FALSE;
            }
            //如果已有保养结果通正常，直接返回保养正常
            return MaintenanceCode.OK;
        }

        //检查当前记录的数据，并返回数据是否合法；更新最后一条记录的数据。
        public static bool CheckCurrReport(int machineCode, int lineCode, int productCount, int failedCount)
        {
            bool result = true;
            if (failedCount > productCount)
            {
                result = false;
            }
            //获取上条记录数据
            ReportInfo report;
            MachineReport.TryGetValue(machineCode, out report);
            if (report == null)
                report = new ReportInfo();

            //增加的产量出现异常
            if (productCount > (report.ProductCount + 500))
            {
                result = false;
            }
            //增加的次品出现异常
            if (failedCount > (report.FailedCount + 100))
            {
                result = false;
            }
            if (lineCode != report.LineCode)
            {//产线代码发生变化
                string sql = string.Format("Update M_Machine_T SET Line=(SELECT  Line FROM S_LineInfo_T WHERE Id='{1}') WHERE MachineCode='{0}'", machineCode, lineCode);
                DBUtil.ExecSQL(sql);
            }

            //更新最后的上报记录
            report.MachineCode = machineCode;
            report.LineCode = lineCode;
            report.ProductCount = productCount;
            report.FailedCount = failedCount;

            if (!MachineReport.Keys.Contains(machineCode))
                MachineReport.Add(machineCode, report);
            else
                MachineReport[machineCode] = report;
            return result;
        }
    }
}
