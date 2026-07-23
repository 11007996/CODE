namespace EAM.Model.Dto
{
    /// <summary>
    /// 广播区域查询对象
    /// </summary>
    public class CallAreaQueryDto : PagerInfo
    {
        public string AreaName { get; set; }
    }

    /// <summary>
    /// 广播区域输入输出对象
    /// </summary>
    public class CallAreaDto
    {
        [Required(ErrorMessage = "区域ID不能为空")]
        public int AreaId { get; set; }

        [Required(ErrorMessage = "区域名称不能为空")]
        public string AreaName { get; set; }

        public List<CallAreaLineDto> CallAreaLineNav { get; set; }
    }
}