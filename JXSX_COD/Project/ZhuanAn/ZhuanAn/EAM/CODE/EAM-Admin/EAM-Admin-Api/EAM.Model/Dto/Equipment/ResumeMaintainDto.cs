
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace EAM.Model.Dto
{
    /// <summary>
    /// 履历保养记录查询对象
    /// </summary>
    public class ResumeMaintainQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public DateTime? BeginExecuteDate { get; set; }
        public DateTime? EndExecuteDate { get; set; }
    }

    /// <summary>
    /// 履历保养记录输入输出对象
    /// </summary>
    public class ResumeMaintainDto
    {
        [Required(ErrorMessage = "履历保养ID不能为空")]
        [ExcelColumnName("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "设备ID不能为空")]
        [ExcelColumnName("设备ID")]
        public int? EquipmentId { get; set; }

        [ExcelColumnName("资产编号")]
        public string AssetNo { get; set; }

        [ExcelColumnName("资产名称")]
        public string AssetName { get; set; }

        [Required(ErrorMessage = "实施类别不能为空")]
        [ExcelColumnName("实施类别")]
        public string ExecuteCategory { get; set; }

        [ExcelColumnName("实施日期")]
        public DateTime? ExecuteDate { get; set; }

        [ExcelColumnName("保管部门")]
        public string TakenDept { get; set; }

        [ExcelColumnName("实施状况")]
        public string ExecuteState { get; set; }

        [ExcelColumnName("实施人员ID")]
        public string ExecuteUser { get; set; }

        [ExcelColumnName("实施人员姓名")]
        public string ExecuteUserName { get; set; }


        [ExcelColumnName("下次实施日期")]
        public DateTime? NextExecuteDate { get; set; }

        [ExcelColumnName("备注/校验报告编号")]
        public string Remark { get; set; }

        [ExcelIgnore]
        public string CreateBy { get; set; }

        [ExcelIgnore]
        public DateTime? CreateTime { get; set; }

        [ExcelIgnore]
        public string UpdateBy { get; set; }

        [ExcelIgnore]
        public DateTime? UpdateTime { get; set; }

        [ExcelIgnore]
        public string ExecuteCategoryLabel { get; set; }
    }
}