namespace EAM.Model.Dto
{
    /// <summary>
    /// 盒子操作记录查询对象
    /// </summary>
    public class CallBoxOperateQueryDto : PagerInfo
    {
        public int? BoxId { get; set; }
        public DateTime? BeginCreateTime { get; set; }
        public DateTime? EndCreateTime { get; set; }
    }

    /// <summary>
    /// 盒子操作记录输入输出对象
    /// </summary>
    public class CallBoxOperateDto
    {
        [Required(ErrorMessage = "操作记录Id不能为空")]
        public long OperateId { get; set; }

        [Required(ErrorMessage = "盒子ID不能为空")]
        public int BoxId { get; set; }

        public string BoxName { get; set; }

        [Required(ErrorMessage = "操作类型不能为空")]
        public int OperateType { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}