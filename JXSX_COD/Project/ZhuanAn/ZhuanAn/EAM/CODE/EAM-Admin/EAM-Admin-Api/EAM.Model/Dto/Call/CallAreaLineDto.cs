namespace EAM.Model.Dto
{
    /// <summary>
    /// 产线区域绑定输入输出对象
    /// </summary>
    public class CallAreaLineDto
    {
        [Required(ErrorMessage = "区域Id不能为空")]
        public int AreaId { get; set; }

        public string AreaName { get; set; }

        [Required(ErrorMessage = "产线ID不能为空")]
        public int LineId { get; set; }

        public string LineName { get; set; }
    }
}