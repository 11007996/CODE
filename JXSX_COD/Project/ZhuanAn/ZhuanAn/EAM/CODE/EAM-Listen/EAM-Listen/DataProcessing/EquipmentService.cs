using EAM.Listen.Common;
using EAM.Listen.Common.Config;
using EAM.Listen.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace EAM.Listen.DataProcessing
{
    public class EquipmentService
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
        private static List<int> MaintenCheckedEquipmentList = new List<int>();

        /// <summary>
        /// 设备的保养状态字典
        /// </summary>
        private static Dictionary<int, MaintenanceStatusDto> EquipmentMaintenStatusDict = new Dictionary<int, MaintenanceStatusDto>();

        /// <summary>
        /// 每台设备最后的上报记录
        /// </summary>
        private static Dictionary<int, ReceiveData> ReceiveDataRecordDict = new Dictionary<int, ReceiveData>();

        /// <summary>
        /// 设备的保养状态字典
        /// </summary>
        private static List<EquipmentExtend> EquipmentExtends = new List<EquipmentExtend>();

        static EquipmentService()
        {
            InitEquipmentStatus();
            InitEquipmentExtend();
        }

        public static int _StartCheckHour
        { get { return StartCheckHour; } }

        /// <summary>
        /// 初始化所有的设备状态
        /// </summary>
        public static void InitEquipmentStatus()
        {
            //初始化设备保养状态
            MaintenCheckedEquipmentList.Clear();
            EquipmentMaintenStatusDict.Clear();
            ReceiveDataRecordDict.Clear();

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

        public static void InitEquipmentExtend()
        {
            EquipmentExtends = SqlSugarUtil.Conn().Context.Queryable<EquipmentExtend>().ToList();
        }

        /// <summary>
        /// 通过编码获取设备ID
        /// </summary>
        /// <param name="equipmentCode"></param>
        /// <returns></returns>
        public static int? GetEquipmentIdByCode(int equipmentCode)
        {
            if (EquipmentExtends == null)
                return null;
            return EquipmentExtends.Where(it => it.EquipmentCode == equipmentCode).Select(it => it.EquipmentId).FirstOrDefault();
        }

        //判断是否检查过最少一次保养
        public static bool IsCheckedMaintenance(int equipmentId)
        {
            return MaintenCheckedEquipmentList.Contains(equipmentId);
        }

        public static MaintenanceCodeEnum GetEquipmentMaintanStatusByEquId(int equipmentId)
        {
            return GetEquipmentMaintanStatus(equipmentId);
        }

        /// <summary>
        /// 获取指定设备的资产保养状态
        ///     返回值：MaintenanceCode枚举，表示保养结果编码
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public static MaintenanceCodeEnum GetEquipmentMaintanStatus(int equipmentId)
        {
            //判断是否开启保养检查，如果都不开启，返回保养正常的结果
            if (!FactoryBaseConfig.MaintainConfig.DayMaintainFlag && !FactoryBaseConfig.MaintainConfig.WeekMaintainFlag && !FactoryBaseConfig.MaintainConfig.MonthMaintainFlag)
                return MaintenanceCodeEnum.OK;

            //获取已有的保养结果
            MaintenanceStatusDto mStatus;
            EquipmentMaintenStatusDict.TryGetValue(equipmentId, out mStatus);
            //已有结果保养正常直接返回
            if (CheckStatus(mStatus) == MaintenanceCodeEnum.OK) return MaintenanceCodeEnum.OK;

            //------------到数据库中实时查询-------------------------------------
            string sql = $@" SELECT
                            (SELECT Count(1) FROM EQU_Maintain_Record WHERE Equipment_Id={equipmentId} AND [Year]='{Year}' AND Date_Mark='D' AND Date_Mark_Stamp='{DayStamp}') DayMaintenanceFlag,
                            (SELECT Count(1) FROM EQU_Maintain_Record WHERE Equipment_Id={equipmentId} AND [Year]='{Year}' AND Date_Mark='W' AND Date_Mark_Stamp='{WeekStamp}') WeekMaintenanceFlag,
                            (SELECT Count(1) FROM EQU_Maintain_Record WHERE Equipment_Id={equipmentId} AND [Year]='{Year}' AND Date_Mark='M' AND Date_Mark_Stamp='{MonthStamp}') MonthMaintenanceFlag ";

            DataTable dt = SqlSugarUtil.Conn().Ado.GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                mStatus = new MaintenanceStatusDto();
                mStatus.EquipmentId = equipmentId;
                mStatus.DayMaintenanceFlag = Convert.ToInt16(dt.Rows[0]["DayMaintenanceFlag"]) > 0 ? true : false;
                mStatus.WeekMaintenanceFlag = Convert.ToInt16(dt.Rows[0]["WeekMaintenanceFlag"]) > 0 ? true : false;
                mStatus.MonthMaintenanceFlag = Convert.ToInt16(dt.Rows[0]["MonthMaintenanceFlag"]) > 0 ? true : false;
                //保存保养结果的数据到基础数据中
                if (!MaintenCheckedEquipmentList.Contains(equipmentId))
                    MaintenCheckedEquipmentList.Add(equipmentId);

                if (!EquipmentMaintenStatusDict.Keys.Contains(equipmentId))
                    EquipmentMaintenStatusDict.Add(equipmentId, mStatus);
                else
                    EquipmentMaintenStatusDict[equipmentId] = mStatus;
            }
            return CheckStatus(mStatus);
        }

        public static MaintenanceCodeEnum CheckStatus(MaintenanceStatusDto mStatus)
        {
            //未传值，返回日保养未做
            DateTime now = DateTime.Now;
            if (mStatus == null) return MaintenanceCodeEnum.DAY_FALSE;
            //日  :检查开启,且当前时间不是换班时间范围内
            if (FactoryBaseConfig.MaintainConfig.DayMaintainFlag && (now.Hour < EndCheckHour || now.Hour >= StartCheckHour))
            {
                if (!mStatus.DayMaintenanceFlag) return MaintenanceCodeEnum.DAY_FALSE;
            }
            //周  :检查开启，且当前时间为星期六或星期天
            if (FactoryBaseConfig.MaintainConfig.WeekMaintainFlag && (now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday))
            {
                if (!mStatus.WeekMaintenanceFlag) return MaintenanceCodeEnum.WEEK_FALSE;
            }
            //月 ：检查开启，且当前时间为本月28号及28号后
            if (FactoryBaseConfig.MaintainConfig.MonthMaintainFlag && now.Day >= 28)
            {
                if (!mStatus.MonthMaintenanceFlag) return MaintenanceCodeEnum.MONTH_FALSE;
            }
            //如果已有保养结果通正常，直接返回保养正常
            return MaintenanceCodeEnum.OK;
        }

        //检查当前记录的数据，并返回数据是否合法；更新最后一条记录的数据。
        public static bool CheckReceiveData(ReceiveData data)
        {
            bool result = true;

            //获取前一条记录数据
            ReceiveData preData;
            ReceiveDataRecordDict.TryGetValue(data.equipmentId, out preData);
            //产线代码发生变化
            if (preData == null || data.lineCode != preData.lineCode || data.ip != preData.ip)
            {
                SqlSugarUtil.Conn().Updateable<EquipmentExtend>()
                           .SetColumns(it => it.Ip == data.ip)
                           .SetColumns(it => it.IsOnline == true)
                           .SetColumns(it => it.LastOnlineTime == DateTime.Now)
                           .Where(it => it.EquipmentId == data.equipmentId)
                           .ExecuteCommand();
            }
            if (preData == null)
            {
                preData = data;
                ReceiveDataRecordDict.Add(data.equipmentId, preData);
            }
            else
            {
                ReceiveDataRecordDict[data.equipmentId] = data;
            }

            if (data.defectCount > data.productCount)
            {
                result = false;
            }
            //两条记录之间，增加的产量出现异常
            if (data.productCount > (preData.productCount + 500))
            {
                result = false;
            }
            //两条记录之间，增加的次品出现异常
            if (data.defectCount > (preData.defectCount + 100))
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 上传接收数据
        /// </summary>
        /// <param name="data"></param>
        public static void UploadReceiveData(ReceiveData data)
        {
            if (FactoryBaseConfig.SignalConfig.UploadDataFlag)
            {
                EquipmentRuningRecord record = new EquipmentRuningRecord()
                {
                    EquipmentId = data.equipmentId,
                    RunState = data.runState,
                    ProductCount = data.productCount,
                    DefectCount = data.defectCount,
                    WarnState = data.warnState,
                    WarnCode = data.warnCode,
                    CreateTime = DateTime.Now,
                };
                SqlSugarUtil.Conn().Insertable(record).ExecuteCommand();
            }
        }
    }
}