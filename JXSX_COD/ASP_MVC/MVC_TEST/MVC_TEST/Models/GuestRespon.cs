using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_TEST.Models
{
    public class GuestRespon
    {
        [Required(ErrorMessage="请输入名称")]   //验证数据是否有效
        public string Name { get; set; }
        [Required(ErrorMessage = "邮件地址")]
        [EmailAddress(ErrorMessage="请输入邮件地址")]
        [Display(Name="邮件")]        
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage="请输入是否参加")]
        public bool? WillAttend { get; set; }
    }
}