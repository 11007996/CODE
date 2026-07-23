namespace EAM.Model.Dto
{
    /// <summary>
    /// 解决方案配置输入输出对象
    /// </summary>
    public class CallConfigFaultSolutionDto
    {
        public int? FaultConfigId { get; set; }

        [Required(ErrorMessage = "故障内容不能为空")]
        public string FaultContent { get; set; }

        [Required(ErrorMessage = "解决方案不能为空")]
        public string SolutionContent { get; set; }
    }
}