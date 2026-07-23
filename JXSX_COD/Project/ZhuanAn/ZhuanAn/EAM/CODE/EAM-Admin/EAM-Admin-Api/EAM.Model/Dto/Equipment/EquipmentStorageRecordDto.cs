namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备保管记录查询对象
    /// </summary>
    public class EquipmentStorageRecordQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public string TicketNo { get; set; }
        public DateTime? BeginCreateTime { get; set; }
        public DateTime? EndCreateTime { get; set; }
    }

    /// <summary>
    /// 设备保管记录输入输出对象
    /// </summary>
    public class EquipmentStorageRecordDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        public int? EquipmentId { get; set; }

        public string AssetNo { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public int? LineId { get; set; }

        public string LineName { get; set; }

        public string AssetName { get; set; }

        [Required(ErrorMessage = "变动类型不能为空")]
        public string StorageChangeType { get; set; }

        public string TicketNo { get; set; }

        public string TicketType { get; set; }

        public string Remark { get; set; }

        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? CreateTime { get; set; }

        public string CreateBy { get; set; }

        [ExcelColumn(Name = "变动类型")]
        public string StorageChangeTypeLabel { get; set; }

        [ExcelColumn(Name = "单据类型")]
        public string TicketTypeLabel { get; set; }
    }
}