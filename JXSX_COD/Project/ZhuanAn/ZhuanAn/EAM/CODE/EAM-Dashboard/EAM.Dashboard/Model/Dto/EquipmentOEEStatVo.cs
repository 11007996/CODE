namespace EAM.Dashboard.Model.Dto
{
    /// <summary>
    /// 设备统计，大屏部门视图
    /// </summary>
    public class EquipmentOEEStatVo
    {
        /// <summary>
        /// 平均OEE
        /// </summary>
        public decimal OEE { get; set; }
        /// <summary>
        /// 统计个数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 平均 时间开动率，性能开动率，合格率
        /// </summary>
        public List<ChartBaseVo<string, decimal>> Rate { get; set; }

        /// <summary>
        /// 设备OEE
        /// </summary>
        public List<ChartBaseVo<string,decimal>> EquipmentOEE { get; set; }
    }

}