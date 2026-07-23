namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备保管查询对象
    /// </summary>
    public class EquipmentStorageUsingQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public int? LineId { get; set; }
        public string TicketNo { get; set; }
    }

    /// <summary>
    /// 设备保管输入输出对象
    /// </summary>
    public class EquipmentStorageUsingDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        public int? EquipmentId { get; set; }

        public string AssetNo { get; set; }

        public string AssetName { get; set; }

        public int? LineId { get; set; }
        public string LineName { get; set; }

        public string ReceiverId { get; set; }

        public string ReceiverName { get; set; }

        public string StorageChangeType { get; set; }

        public string TicketNo { get; set; }

        public string TicketType { get; set; }

        public string Remark { get; set; }

        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? CreateTime { get; set; }

        public string CreateBy { get; set; }
    }

    /// <summary>
    /// 设备操作输入对象
    /// </summary>
    public class OperateEquipmentStorageDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        public int? EquipmentId { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string TicketNo { get; set; }

        public string TicketType { get; set; }

        public string Remark { get; set; }

        #region 领用时使用

        public string ReceiverId { get; set; }

        public int? LineId { get; set; }

        #endregion 领用时使用
    }
}