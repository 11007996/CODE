using System.ComponentModel.DataAnnotations;

namespace EAM.Model.System.Dto
{
    public class RegisterDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string Username { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        [Required(ErrorMessage = "确认密码不能为空")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Uuid { get; set; } = "";

        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; }

        public string UserIP { get; set; }
    }

    public class OAUserRegisterDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string Username { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Uuid { get; set; } = "";

        /// <summary>
        /// 微信用户ID
        /// </summary>
        public string WxUserId { get; set; }

        /// <summary>
        /// 微信临时Code
        /// </summary>
        public string WxCode { get; set; }

        ///// <summary>
        ///// 关联的厂区
        ///// </summary>
        //[Required(ErrorMessage = "厂区不能为空")]
        //public string FactoryId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        //[Required(ErrorMessage = "姓名不能为空")]
        //public string NickName { get; set; }

        public string UserIP { get; set; }
    }
}