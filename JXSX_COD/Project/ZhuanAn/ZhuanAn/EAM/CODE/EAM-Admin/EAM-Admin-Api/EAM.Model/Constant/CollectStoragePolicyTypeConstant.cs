namespace EAM.Model.Constant
{
    public class CollectStoragePolicyTypeConstant
    {
        /// <summary>
        /// 每次采集到的数据点都无条件写入历史数据库。
        ///     适用于关键参数,存储开销最大。
        /// </summary>
        public const string 始终存储 = "Always";

        /// <summary>
        /// 仅当当前值与上一次存储值不同时才写入。
        ///     可显著减少重复数据（如开关量长时间保持“运行”状态）。
        ///     注意：需配合死区（Deadband）使用以避免高频微小波动。
        /// </summary>
        public const string 仅变化时存储 = "OnChange";

        /// <summary>
        /// 当前值与上次存储值的差值超过设定阈值（绝对或百分比）时才存储。
        ///     结合了 Deadband + OnChange 的思想。
        ///     常用于模拟量（如温度、压力）。
        /// </summary>
        public const string 基于偏差存储 = "OnDeviation";

        /// <summary>
        /// 按固定时间间隔（如每5秒、每分钟）强制写入一次，无论是否变化。
        ///     保证时间序列的完整性，便于后续对齐分析。
        ///     常用于需要做 FFT、频谱分析或机器学习训练的场景。
        /// </summary>
        public const string 周期性存储 = "Periodic";

        /// <summary>
        /// 仅在特定事件触发时存储（如报警发生、设备启停、手动操作）。
        ///     通常与状态变量或布尔信号关联。
        ///     存储稀疏但语义丰富。
        /// </summary>
        public const string 事件驱动存储 = "EventDriven";

        /// <summary>
        /// 使用算法（如 Swinging Door Compression, SDC）动态判断是否保留数据点，以保持曲线趋势精度的同时大幅压缩数据。
        /// 广泛用于 Historian 系统（如 OSIsoft PI、AVEVA Historian）。
        /// 特点：非等间隔存储，但能高保真还原原始波形。
        /// </summary>
        public const string 压缩存储 = "Compressed";

        /// <summary>
        /// 数据仅用于实时监控或计算，不持久化。适用于临时中间变量。
        /// </summary>
        public const string 从不存储 = "Never";
        
    }
}
