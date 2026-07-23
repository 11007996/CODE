using System.Data;
using EAM.Model.Business;

namespace EAM.Model.Dto
{
    /// <summary>
    /// 治具尺寸量测验收单查询对象
    /// </summary>
    public class SizeMeasureTicketQueryDto : PagerInfo
    {
        public string TicketNo { get; set; }
        public string ProcessInstanceId { get; set; }
    }

    /// <summary>
    /// 治具尺寸量测验收单输入输出对象
    /// </summary>
    public class SizeMeasureTicketDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "发起人不能为空")]
        public string InitiatorId { get; set; }

        public string InitiatorName { get; set; }

        [Required(ErrorMessage = "创建模式不能为空")]
        public string CreateMode { get; set; }

        [Required(ErrorMessage = "产品料号不能为空")]
        public int? PartId { get; set; }

        public int? FixtureId { get; set; }

        [Required(ErrorMessage = "治具名称不能为空")]
        public string FixtureName { get; set; }

        [Required(ErrorMessage = "治具图号不能为空")]
        public string DrawingNo { get; set; }

        [Required(ErrorMessage = "治具编号描述不能为空")]
        public string FixtureNoDesc { get; set; }

        [Required(ErrorMessage = "版本不能为空")]
        public string Version { get; set; }

        public string ProcessInstanceId { get; set; }

        public string EngineerId { get; set; }

        public string EngineerName { get; set; }

        public string EngineerLeaderId { get; set; }

        public string EngineerLeaderName { get; set; }

        public string QcId { get; set; }

        public string QcName { get; set; }

        public string QcLeaderId { get; set; }

        public string QcLeaderName { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "删除标志不能为空")]
        public int DelFlag { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remark { get; set; }



        public List<SizeMeasureTicketItemDto> SizeMeasureTicketItemNav { get; set; }

        public List<SizeMeasureTicketItemDefineDto> SizeMeasureTicketItemDefineNav { get; set; }
        public List<SizeMeasureTicketItemResultDto> SizeMeasureTicketItemResultNav { get; set; }
        public List<SizeMeasureTicketItemOtherDto> SizeMeasureTicketItemOtherNav { get; set; }
        [ExcelColumn(Name = "业务状态")]
        public string StatusLabel { get; set; }

    }

    //批量入库，输入对象
    public class SizeMeasureTicketInStorageDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "治具Id不能为空")]
        public string FixtureId { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<CheckedFixtureInfo> CheckedFixtureList { get; set; }
    }

    /// <summary>
    /// 批量入库选中治具
    /// </summary>
    public class CheckedFixtureInfo
    {
        [Required(ErrorMessage = "治具编号不能为空")]
        public string FixtureNo { get; set; }

        [Required(ErrorMessage = "储位不能为空")]
        public string StorageId { get; set; }
    }
}