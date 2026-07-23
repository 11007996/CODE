using System.Data;
using System.Xml.Serialization;
using EAM.Model.Business;

namespace EAM.Model.Business
{
    /// <summary>
    /// 治具尺寸量测验收单
    /// </summary>
    [SugarTable("BU_Size_Measure_Ticket")]
    public class SizeMeasureTicket
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        [SugarColumn(ColumnName = "initiator_ID")]
        public string InitiatorId { get; set; }

        /// <summary>
        /// 发起人姓名
        /// </summary>
        [SugarColumn(ColumnName = "initiator_Name")]
        public string InitiatorName { get; set; }

        /// <summary>
        /// 创建模式
        /// </summary>
        [SugarColumn(ColumnName = "create_Mode")]
        public string CreateMode { get; set; }

        /// <summary>
        /// 产品料号
        /// </summary>
        [SugarColumn(ColumnName = "part_ID")]
        public int? PartId { get; set; }

        /// <summary>
        /// 关联治具ID (新开时为入库时创建的治具,增开、维修时为原关联治具)
        /// </summary>
        [SugarColumn(ColumnName = "fixture_Id")]
        public int? FixtureId { get; set; }

        /// <summary>
        /// 治具名称
        /// </summary>
        [SugarColumn(ColumnName = "fixture_Name")]
        public string FixtureName { get; set; }

        /// <summary>
        /// 治具图号
        /// </summary>
        [SugarColumn(ColumnName = "drawing_No")]
        public string DrawingNo { get; set; }

        /// <summary>
        /// 治具编号描述
        /// </summary>
        [SugarColumn(ColumnName = "fixture_No_Desc")]
        public string FixtureNoDesc { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        [SugarColumn(ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 工程师ID
        /// </summary>
        [SugarColumn(ColumnName = "engineer_ID")]
        public string EngineerId { get; set; }

        /// <summary>
        /// 工程师
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string EngineerName { get; set; }

        /// <summary>
        /// 工程师主管ID
        /// </summary>
        [SugarColumn(ColumnName = "engineer_Leader_ID")]
        public string EngineerLeaderId { get; set; }

        /// <summary>
        /// 工程师主管
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string EngineerLeaderName { get; set; }

        /// <summary>
        /// 品保ID
        /// </summary>
        [SugarColumn(ColumnName = "qC_ID")]
        public string QcId { get; set; }

        /// <summary>
        /// 品保
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string QcName { get; set; }

        /// <summary>
        /// 品保主管ID
        /// </summary>
        [SugarColumn(ColumnName = "qC_Leader_ID")]
        public string QcLeaderId { get; set; }

        /// <summary>
        /// 品保主管
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string QcLeaderName { get; set; }

        /// <summary>
        /// 业务状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public int DelFlag { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(SizeMeasureTicketItem.TicketNo), nameof(TicketNo))] //自定义关系映射
        public List<SizeMeasureTicketItem> SizeMeasureTicketItemNav { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(SizeMeasureTicketItemDefine.TicketNo), nameof(TicketNo))] //自定义关系映射
        public List<SizeMeasureTicketItemDefine> SizeMeasureTicketItemDefineNav { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(SizeMeasureTicketItemResult.TicketNo), nameof(TicketNo))] //自定义关系映射
        public List<SizeMeasureTicketItemResult> SizeMeasureTicketItemResultNav { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(SizeMeasureTicketItemOther.TicketNo), nameof(TicketNo))] //自定义关系映射
        public List<SizeMeasureTicketItemOther> SizeMeasureTicketItemOtherNav { get; set; }

        [SugarColumn(IsIgnore = true)]
        public DataTable DynamicStatisticalReport { get; set; }
    }
}