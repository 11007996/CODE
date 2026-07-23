namespace EAM.Model.Dto
{
    /// <summary>
    /// 耗品领用单_需求清单查询对象
    /// </summary>
    public class ConsumableReceiveTicketItemQueryDto : PagerInfo
    {
        public string TicketNo { get; set; }
        public int? ConsumableId { get; set; }
    }

    /// <summary>
    /// 耗品领用单_需求清单输入输出对象
    /// </summary>
    public class ConsumableReceiveTicketItemDto
    {
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "耗品ID不能为空")]
        public int ConsumableId { get; set; }

        public string ConsumablePart { get; set; }
        public string ConsumableName { get; set; }
        public string Spec { get; set; }

        public decimal Price { get; set; }

        [Required(ErrorMessage = "需求数量不能为空")]
        public int NeedQty { get; set; }
    }

    /// <summary>
    /// 耗品领用单_需求清单概要
    /// </summary>
    public class ConsumableReceiveTicketItemSummaryDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "耗品ID不能为空")]
        public int ConsumableId { get; set; }

        public string ConsumablePart { get; set; }

        [Required(ErrorMessage = "耗品名称不能为空")]
        public string ConsumableName { get; set; }

        public string Spec { get; set; }

        [Required(ErrorMessage = "需求数量不能为空")]
        public int NeedQty { get; set; }

        /// <summary>
        /// 领用数量
        /// </summary>
        public int ReceiveQty { get; set; }

        /// <summary>
        /// 归还数量
        /// </summary>
        public int BackQty { get; set; }
    }

    /// <summary>
    /// 耗品领用单_需求清单详情
    /// </summary>
    public class ConsumableReceiveTicketItemReceiveDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "耗品ID不能为空")]
        public int ConsumableId { get; set; }

        [Required(ErrorMessage = "耗品名称不能为空")]
        public string ConsumableName { get; set; }

        public string ConsumablePart { get; set; }
        public string Spec { get; set; }

        [Required(ErrorMessage = "领用数量不能为空")]
        public int ReceiveQty { get; set; }

        //占用数量
        public int Qty { get; set; }

        //归还数量
        public int BackQty { get; set; }

        [Required(ErrorMessage = "储位ID不能为空")]
        public int StorageID { get; set; }

        [Required(ErrorMessage = "储位描述不能为空")]
        public string StorageFullName { get; set; }
    }
}