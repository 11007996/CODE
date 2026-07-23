namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备文件查询对象
    /// </summary>
    public class EquipmentFileQueryDto : PagerInfo
    {
        public string RealName { get; set; }
    }

    public class EquipmentFileDto
    {
        public long FileId { get; set; }
    }

    public class EquipmentFileBindQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public long? FileId { get; set; }
    }

    /// <summary>
    /// 设备文件关联输入输出对象
    /// </summary>
    public class EquipmentFileBindDto
    {
        [Required(ErrorMessage = "设备ID不能为空")]
        public int? EquipmentId { get; set; }

        public string AssetNo { get; set; }

        [Required(ErrorMessage = "文件Id不能为空")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long FileId { get; set; }

        public string FileName { get; set; }

        public string AssetName { get; set; }

        public string AccessUrl { get; set; }
    }
}