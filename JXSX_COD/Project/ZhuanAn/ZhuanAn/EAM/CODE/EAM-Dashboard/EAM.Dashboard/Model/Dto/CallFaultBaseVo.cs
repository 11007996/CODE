namespace EAM.Dashboard.Model.Dto
{
    /// <summary>
    /// 故障记录输入输出对象
    /// </summary>
    public class CallFaultBaseVo
    {
        public int? AreaId { get; set; }

        public int CallId { get; set; }

        public int? LineId { get; set; }

        public string LineName { get; set; }

        public string CallReason { get; set; }

        public string CallTargetType {  get; set; }

        public string CallTargetTypeLabel { get; set; }

        public string CallPointType {  get; set; }

        public string StationName {  get; set; }

        public string FaultStatus { get; set; }

        public string EquipmentType { get; set; }

        public string EquipmentNo { get; set; }

        public int? DeptId { get; set; }

        public string DeptName { get; set; }

        public int? MaxHandleTimes { get; set; }

        public int? MaxHelpTimes { get; set; }

        public DateTime? HandleTime { get; set; }

        public DateTime? CallHelpTime { get; set; }

        public DateTime? HelpTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string HandlerNo { get; set; }

        public string HandlerName { get; set; }

        public string HelperNo { get; set; }

        public string HelperName { get; set; }

        public string SolverNo { get; set; }

        public string SolverName { get; set; }

        public string QcNo { get; set; }

        public string QcName { get; set; }

        public string FaultType { get; set; }

        public string FaultContent { get; set; }

        public string SolutionContent { get; set; }

        public int? ProdCount { get; set; }

        public int? PassCount { get; set; }

        public int? NgCount { get; set; }

        public string CallHelpWay { get; set; }

        public string PcIp { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string IsVisible { get; set; }

        #region 计算属性

        //设备名
        public string EquipmentName
        {
            get
            {
                if (string.IsNullOrEmpty(EquipmentNo))
                    return EquipmentType;
                else
                    return EquipmentType + "*" + EquipmentNo;
            }
        }

        //到场时长
        public int? ComeMinute
        {
            get
            {
                if (CreateTime == null) return null;
                else if (HandleTime == null)
                {
                    TimeSpan ts = DateTime.Now - CreateTime.Value;
                    return (int)ts.TotalMinutes;
                }
                else
                {
                    TimeSpan ts = HandleTime.Value - CreateTime.Value;
                    return (int)ts.TotalMinutes;
                }
            }
        }

        //处理时长
        public int? HandleMinute
        {
            get
            {
                if (CreateTime == null || HandleTime == null) return null;
                else if (CallHelpTime == null && EndTime == null)
                {//未结束
                    TimeSpan ts = DateTime.Now - HandleTime.Value;
                    return (int)ts.TotalMinutes;
                }
                else if (CallHelpTime != null)
                {
                    TimeSpan ts = CallHelpTime.Value - HandleTime.Value;
                    return (int)ts.TotalMinutes;
                }
                else if (EndTime != null)
                {
                    TimeSpan ts = EndTime.Value - HandleTime.Value;
                    return (int)ts.TotalMinutes;
                }
                return null;
            }
        }

        //支援时长
        public int? HelpMinute
        {
            get
            {
                if (CreateTime == null || HandleTime == null || CallHelpTime == null || HelpTime == null) return null;
                else if (EndTime == null)
                {
                    TimeSpan ts = DateTime.Now - HelpTime.Value;
                    return (int)ts.TotalMinutes;
                }
                else
                {
                    TimeSpan ts = EndTime.Value - HelpTime.Value;
                    return (int)ts.TotalMinutes;
                }
            }
        }

        //总计时长
        public int? TotalMinute
        {
            get
            {
                if (CreateTime == null) return null;
                else if (EndTime == null)
                {
                    TimeSpan ts = DateTime.Now - CreateTime.Value;
                    return (int)ts.TotalMinutes;
                }
                else
                {
                    TimeSpan ts = EndTime.Value - CreateTime.Value;
                    return (int)ts.TotalMinutes;
                }
            }
        }

        //当前处理或支援的剩余时长
        public int? RemainMinute
        {
            get
            {
                if (CreateTime == null || HandleTime == null || EndTime != null) return null;
                else if (HandleTime != null && CallHelpTime == null && MaxHandleTimes != null && MaxHandleTimes > 0)
                {//处理中
                    TimeSpan ts = HandleTime.Value.AddMinutes(MaxHandleTimes.Value) - DateTime.Now;
                    return (int)Math.Ceiling(ts.TotalMinutes);
                }
                else if (HelpTime != null && MaxHelpTimes != null && MaxHelpTimes > 0)
                {//支援中
                    TimeSpan ts = HelpTime.Value.AddMinutes(MaxHelpTimes.Value) - DateTime.Now;
                    return (int)Math.Ceiling(ts.TotalMinutes);
                }
                return null;
            }
        }

        #endregion 计算属性
    }

    public class CallFaultCountStat
    {
        public List<ChartBaseVo<string, int>> MonthData;
        public List<ChartBaseVo<string, int>> WeekData;
        public List<ChartBaseVo<string, int>> HourData;
    }

    /// <summary>
    /// 一周的数据分析
    /// </summary>
    public class WeekCallFaultStat
    {
        public List<ChartBaseVo<string, int>> EmpTimeData;
        public List<ChartBaseVo<string, int>> EquipmentData;
        public List<string> FaultTypeYData;
        public List<List<ChartBaseVo<string, double>>> FaultTypeData;
    }
}