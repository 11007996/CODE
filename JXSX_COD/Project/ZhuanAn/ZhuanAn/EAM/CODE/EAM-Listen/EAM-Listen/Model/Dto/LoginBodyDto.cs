namespace EAM.Listen.Model.Dto
{
    public class LoginBodyDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 当前登入的厂区
        /// </summary>
        public string FactoryId { get; set; }

        /// <summary>
        /// 是否使用OA账号登入
        /// </summary>
        public bool UseOaAccount { get; set; }
    }
}