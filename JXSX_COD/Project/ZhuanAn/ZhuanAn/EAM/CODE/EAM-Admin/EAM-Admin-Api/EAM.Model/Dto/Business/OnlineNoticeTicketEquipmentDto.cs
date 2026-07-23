namespace EAM.Model.Dto
{
    /// <summary>
    /// 上线通知单_设备需求清单查询对象
    /// </summary>
    public class OnlineNoticeTicketEquipmentQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 上线通知单_设备需求清单输入输出对象
    /// </summary>
    public class OnlineNoticeTicketEquipmentDto
    {
        public string TicketNo { get; set; }

        public string EquipmentName { get; set; }

        public int NeedQty { get; set; }
    }

    public class OnlineNoticeTicketEquipmentSummaryDto
    {
        public string TicketNo { get; set; }
        public string TicketType { get; set; }

        public List<EquipmentDemandDto> DemandList { get; set; }

        public List<EquipmentReceiveDto> ReceiveList { get; set; }

        public List<EquipmentStorageRecordDto> StorageRecordList { get; set; }
    }

    public class EquipmentDemandDto
    {
        public string EquipmentName { get; set; }
        public int NeedQty { get; set; } = 0;
    }

    public class EquipmentReceiveDto
    {
        public int EquipmentId { get; set; }
        public string AssetName { get; set; }
        public string EquipmentName { get; set; }
        public int ReceiveQty { get; set; }
        public int BackQty { get; set; }
    }

    /// <summary>
    /// 上线通知单，批量领取设备
    /// </summary>
    public class BatchReceiveEquipmentDto
    {
        public string TicketNo { get; set; }
        public List<OperateEquipmentStorageDto> Equipments { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}