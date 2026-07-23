namespace EAM.Model.Constant
{
    public class CollectDeadbandTypeConstant
    {
        /// <summary>
        /// 当前值与上次上报值的绝对差值超过设定阈值时才上报。
        ///     公式：|CurrentValue - LastReportedValue| > Threshold
        /// </summary>
        public const string 绝对值 = "Absolute";

        /// <summary>
        /// 当前值与上次上报值的相对变化百分比超过设定阈值时才上报。
        ///     公式：|CurrentValue - LastReportedValue| / |LastReportedValue| > PercentThreshold
        /// </summary>
        public const string 百分比 = "Percent";

        /// <summary>
        /// 仅当数值变化跨越某个固定步长（step size）的整数倍时才上报。
        ///     例如：步长为 5，则只在 0、5、10、15... 等点位变化时上报。
        /// </summary>
        public const string 步进 = "Step";
    }
}
