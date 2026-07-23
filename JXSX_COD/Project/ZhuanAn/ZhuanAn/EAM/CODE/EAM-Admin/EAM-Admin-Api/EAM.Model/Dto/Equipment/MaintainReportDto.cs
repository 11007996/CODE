using EAM.Model.Equipment;
using System.Data;

namespace EAM.Model.Dto
{
    /// <summary>
    /// 资产保养报表查询对象
    /// </summary>
    public class MaintainReportQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public int? Year { get; set; }
    }

    public class MaintainReportSheetQueryDto
    {
        public int? EquipmentId { get; set; }
        public int Year { get; set; }
        public int? Month { get; set; }
        public string TimeMark { get; set; }
    }

    public class MaintainReportSheetDto
    {
        public int? EquipmentId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public string Tip { get; set; }

        /// <summary>
        /// 设备信息
        /// </summary>
        public EquipmentBase Equipment { get; set; }

        /// <summary>
        /// 报表部份
        /// </summary>
        public List<ReportSheetPart> SheetPart { get; set; }

        public class ReportSheetPart
        {
            public string DateMark { get; set; }
            public List<SheetPartColumn> PartColumn { get; set; }
            public List<SheetPartItem> PartRow { get; set; }
            public DataTable SheetTable { get; set; }
        }

        /// <summary>
        /// 报表列说明
        /// </summary>
        public class SheetPartColumn
        {
            /// <summary>
            /// 列名
            /// </summary>
            public string ColumnName { get; set; }

            /// <summary>
            /// 日期标记
            /// </summary>
            public string DateMark { get; set; }

            /// <summary>
            /// 所属日期戳
            /// </summary>
            public int DateMarkStamp { get; set; }

            /// <summary>
            /// 前端宽度占比
            ///     说明：日期标记为周别的特殊处理,使前端的周保养与日保养的显示位置能对应上
            /// </summary>
            public float? WidthPercent { get; set; }

            /// <summary>
            /// 单元格跨列
            /// </summary>
            public int SpanColumn { get; set; }
        }

        /// <summary>
        /// 保养项目
        /// </summary>
        public class SheetPartItem
        {
            public int? ItemId { get; set; }
            public string ItemName { get; set; }
        }
    }

    /// <summary>
    /// 批量添加输入对象
    /// </summary>
    public class MaintainReportBatchAddDto
    {
        [Required(ErrorMessage = "年标记不能为空")]
        public int Year { get; set; }
    }

    /// <summary>
    /// 保养报表概况查询
    /// </summary>
    public class MaintainReportOverviewQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }

        [Required(ErrorMessage = "年份不能为空")]
        public int? Year { get; set; }
        public int? Month { get; set; }

        [Required(ErrorMessage = "日期标记不能为空")]
        public string DateMark { get; set; }

        public string TimeMark { get; set; }

        public string CostCenter {  get; set; }
    }

    /// <summary>
    /// 资产保养报表输入输出对象
    /// </summary>
    public class MaintainReportDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        public int? EquipmentId { get; set; }

        public string AssetNo { get; set; }

        public string AssetName { get; set; }

        [Required(ErrorMessage = "年标记不能为空")]
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