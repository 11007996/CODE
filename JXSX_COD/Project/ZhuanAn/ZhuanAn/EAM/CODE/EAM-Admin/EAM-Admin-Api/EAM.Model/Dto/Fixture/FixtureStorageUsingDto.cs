namespace EAM.Model.Dto
{
    /// <summary>
    /// 产线领用中的治具查询对象
    /// </summary>
    public class FixtureStorageUsingQueryDto : PagerInfo
    {
        public int? FixtureId { get; set; }
        public int? LineId { get; set; }
        public string TicketNo { get; set; }
    }

    /// <summary>
    /// 产线领用中的治具输入输出对象
    /// </summary>
    public class FixtureStorageUsingDto
    {
        [Required(ErrorMessage = "ID不能为空")]
        public int FixtureUsingId { get; set; }

        [Required(ErrorMessage = "治具ID不能为空")]
        public int FixtureId { get; set; }

        public string Series {  get; set; }
        public string FixtureName { get; set; }

        [Required(ErrorMessage = "数量不能为空")]
        public int ReceiveQty { get; set; }

        public int Qty { get; set; }

        [Required(ErrorMessage = "储位ID不能为空")]
        public int StorageId { get; set; }

        [Required(ErrorMessage = "储位描述不能为空")]
        public string StorageFullName { get; set; }

        public string RelatedUser { get; set; }
        public string RelatedUserName { get; set; }

        public int LineId { get; set; }
        public string LineName { get; set; }

        public string TicketNo { get; set; }
        public string TicketType { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}