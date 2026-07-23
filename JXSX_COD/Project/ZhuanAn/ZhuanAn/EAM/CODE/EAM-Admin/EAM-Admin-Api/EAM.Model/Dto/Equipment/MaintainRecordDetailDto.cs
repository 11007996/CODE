namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备保养记录详情查询对象
    /// </summary>
    public class MaintainRecordDetailQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 设备保养记录详情输入输出对象
    /// </summary>
    public class MaintainRecordDetailDto
    {
        [Required(ErrorMessage = "保养记录ID不能为空")]
        public int RecordId { get; set; }

        [Required(ErrorMessage = "保养项目不能为空")]
        public string ItemName { get; set; }

        public string ItemValue { get; set; }

        [Required(ErrorMessage = "保养项目ID不能为空")]
        public int ItemId { get; set; }
    }
}