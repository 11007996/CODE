namespace EAM.Listen.Common.Config
{
    public class MaintainConfigDto
    {
        /// <summary>
        /// 【日】保养检查开关，false 表示返回的保养状态都是正常，true表示需要返回实际保养状态
        /// </summary>
        public bool DayMaintainFlag { get; set; } = false;

        /// <summary>
        /// 【周】保养检查开关，false 表示返回的保养状态都是正常，true表示需要返回实际保养状态
        /// </summary>
        public bool WeekMaintainFlag { get; set; } = false;

        /// <summary>
        /// 【月】保养检查开关，false 表示返回的保养状态都是正常，true表示需要返回实际保养状态
        /// </summary>
        public bool MonthMaintainFlag { get; set; } = false;
    }
}