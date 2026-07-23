namespace EAM.Model.Equipment
{
    /// <summary>
    /// 资产保养报表
    /// </summary>
    [SugarTable("EQU_Maintain_Report")]
    public class MaintainReport
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 年标记
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "year")]
        public int Year { get; set; }

        /// <summary>
        /// 1月
        /// </summary>
        public string Jan { get; set; }

        /// <summary>
        /// 2月
        /// </summary>
        public string Feb { get; set; }

        /// <summary>
        /// 3月
        /// </summary>
        public string Mar { get; set; }

        /// <summary>
        /// 4月
        /// </summary>
        public string Apr { get; set; }

        /// <summary>
        /// 5月
        /// </summary>
        public string May { get; set; }

        /// <summary>
        /// 6月
        /// </summary>
        public string Jun { get; set; }

        /// <summary>
        /// 7月
        /// </summary>
        public string Jul { get; set; }

        /// <summary>
        /// 8月
        /// </summary>
        public string Aug { get; set; }

        /// <summary>
        /// 9月
        /// </summary>
        public string Sept { get; set; }

        /// <summary>
        /// 10月
        /// </summary>
        public string Oct { get; set; }

        /// <summary>
        /// 11月
        /// </summary>
        public string Nov { get; set; }

        /// <summary>
        /// 12月
        /// </summary>
        public string Dec { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        public string Yearly { get; set; }
    }
}