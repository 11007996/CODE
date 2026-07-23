namespace EAM.Model.System.Dto
{
    public class WxLoginDto
    {
        /// <summary>
        /// 微信授权code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 优先尝试登入的厂区
        /// </summary>
        public string FactoryId { get; set; }
    }
}