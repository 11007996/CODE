using EAM.Model.Call;
using EAM.Model.Constant;

namespace EAM.Model.Dto
{
    /// <summary>
    /// 故障记录查询对象
    /// </summary>
    public class CallFaultBaseQueryDto : PagerInfo
    {
        public int? AreaId { get; set; }
        public int? LineId { get; set; }
        public string CallTargetType { get; set; }
        public string CallPointType {  get; set; }
        public string EquipmentType { get; set; }
        public DateTime? BeginCreateTime { get; set; }
        public DateTime? EndCreateTime { get; set; }
        public string FaultStatus { get; set; }
        public string HandlerNo { get; set; }
        public string HelperNo { get; set; }
        public string SolverNo { get; set; }
    }

    /// <summary>
    /// 故障记录输入输出对象
    /// </summary>
    public class CallFaultBaseDto
    {
        [Required(ErrorMessage = "呼叫记录ID不能为空")]
        [ExcelColumn(Name = "呼叫ID")]
        public int CallId { get; set; }

        [ExcelColumn(Name = "区域ID")]
        public int? AreaId { get; set; }

        [ExcelColumn(Name = "区域名称")]
        public string AreaName { get; set; }

        [ExcelColumn(Name = "产线ID")]
        public int? LineId { get; set; }

        [ExcelColumn(Name = "产线名称")]
        public string LineName { get; set; }

        [Required(ErrorMessage = "呼叫原因不能为空")]
        [ExcelColumn(Name = "呼叫原因")]
        public string CallReason { get; set; }

        [ExcelColumn(Name = "呼叫目标")]
        public string CallTargetType { get; set; }

        [ExcelColumn(Name = "点位类型")]
        public string CallPointType { get; set; }

        [ExcelColumn(Name = "工站Id")]
        public int? StationId { get; set; }

        [ExcelColumn(Name = "工站名称")]
        public string StationName { get; set; }

        [ExcelColumn(Name = "设备类型")]
        public string EquipmentType { get; set; }

        [ExcelColumn(Name = "设备编号")]
        public string EquipmentNo { get; set; }

        [ExcelColumn(Name = "备注")]
        public string Remark { get; set; }

        [ExcelColumn(Name = "故障状态")]
        public string FaultStatus { get; set; }

        [ExcelColumn(Name = "最大处理时间(分)")]
        public int? MaxHandleTimes { get; set; }

        [ExcelColumn(Name = "最大支援时间(分)")]
        public int? MaxHelpTimes { get; set; }

        [ExcelColumn(Name = "处理签到时间", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        public DateTime? HandleTime { get; set; }

        [ExcelColumn(Name = "呼叫支援时间", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        public DateTime? CallHelpTime { get; set; }

        [ExcelColumn(Name = "支援签到时间", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        public DateTime? HelpTime { get; set; }

        [ExcelColumn(Name = "解决时间", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        public DateTime? EndTime { get; set; }

        [ExcelColumn(Name = "处理人工号")]
        public string HandlerNo { get; set; }

        [ExcelColumn(Name = "处理人")]
        public string HandlerName { get; set; }

        [ExcelColumn(Name = "支援人工号")]
        public string HelperNo { get; set; }

        [ExcelColumn(Name = "支援人")]
        public string HelperName { get; set; }

        [ExcelColumn(Name = "解决人工号")]
        public string SolverNo { get; set; }

        [ExcelColumn(Name = "解决人")]
        public string SolverName { get; set; }

        [ExcelColumn(Name = "品质工号")]
        public string QcNo { get; set; }

        [ExcelColumn(Name = "品质")]
        public string QcName { get; set; }

        [ExcelColumn(Name = "故障类型")]
        public string FaultType { get; set; }

        [ExcelColumn(Name = "故障内容")]
        public string FaultContent { get; set; }

        [ExcelColumn(Name = "解决方案")]
        public string SolutionContent { get; set; }

        [ExcelColumn(Name = "制品跟踪数量")]
        public int? ProdCount { get; set; }

        [ExcelColumn(Name = "良品数量")]
        public int? PassCount { get; set; }

        [ExcelColumn(Name = "不良品数量")]
        public int? NgCount { get; set; }

        [ExcelColumn(Name = "呼叫支援方式")]
        public string CallHelpWay { get; set; }

        [ExcelColumn(Name = "呼叫电脑IP")]
        public string PcIp { get; set; }

        [ExcelColumn(Name = "呼叫人工号")]
        public string CreateBy { get; set; }

        [ExcelColumn(Name = "呼叫时间")]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Ignore = true)]
        public string UpdateBy { get; set; }

        [ExcelColumn(Ignore = true)]
        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Ignore = true)]
        public int? BoxId { get; set; }

        #region 计算属性

        //设备名
        [ExcelColumn(Name = "设备名称")]
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

        public string CallTargetTypeLabel
        {
            get
            {
                string targetLabel = string.Empty;
                switch (this.CallTargetType)
                {
                    case CallTargetTypeConstant.生技:
                        targetLabel = "生技";
                        break;
                    case CallTargetTypeConstant.品质:
                        targetLabel = "品质";
                        break;
                    case CallTargetTypeConstant.生产:
                        targetLabel = "生产";
                        break;
                    case CallTargetTypeConstant.物料:
                        targetLabel = "物料";
                        break;
                }
                return targetLabel;
            }
        }


        //到场时长
        [ExcelColumn(Name = "到场时长(分)")]
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
        [ExcelColumn(Name = "处理时长(分)")]
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
        [ExcelColumn(Name = "支援时长(分)")]
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
        [ExcelColumn(Name = "总计时长(分)")]
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
        [ExcelColumn(Ignore = true)]
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

        [ExcelColumn(Ignore = true)]
        public List<CallFaultOperateDto> CallFaultOperateNav { get; set; }
    }

    public class LineCallSummaryDto
    {
        public int? LineId { get; set; }
        public List<CallLineEquipmentDto> LineEquipmentList { get; set; }
        public List<StationDto> LineStationList { get; set; }
        public CallAreaLineDto CallArea { get; set; }
    }

    public class CallOperateDto
    {
        public int CallId { get; set; }

        public string OperatorNo { get; set; }

        public string CallHelpWay { get; set; }

        public string FaultType { get; set; }

        public string FaultContent { get; set; }

        public string SolutionContent { get; set; }

        public int? ProdCount { get; set; }

        public int? PassCount { get; set; }

        public int? NgCount { get; set; }

        public string QcNo { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateBy { get; set; }
    }

    /// <summary>
    /// 故障记录输入输出对象
    /// </summary>
    public class AddCallFaultBaseDto
    {
        [Required(ErrorMessage = "呼叫记录ID不能为空")]
        public int CallId { get; set; }

        [Required(ErrorMessage = "产线不能为空")]
        public int? LineId { get; set; }

        [Required(ErrorMessage = "呼叫原因不能为空")]
        public string CallReason { get; set; }

        [Required(ErrorMessage = "呼叫目标不能为空")]
        public string CallTargetType { get; set; }

        [Required(ErrorMessage = "呼叫点位类型不能为空")]
        public string CallPointType { get; set; }
        
        public int? StationId { get; set; }

        public string EquipmentType { get; set; }

        public string EquipmentNo { get; set; }

        public string Remark { get; set; }

        #region 呼叫盒呼叫扩展
        /// <summary>
        /// 呼叫盒ID
        /// </summary>
        public int? BoxId { get; set; }
        #endregion
    }
}