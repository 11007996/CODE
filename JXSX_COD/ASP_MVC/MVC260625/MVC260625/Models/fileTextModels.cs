using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC260625.Models
{
    public class fileTextModels
    {
        [Display(Name = "姓名")]
        [RegularExpression("\\d[1-5]",ErrorMessage="名称由5位数据组成")]
        [StringLength(5, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 3)]
        public string name { set; get; }

        [Display(Name = "年纪")]
        [Required(ErrorMessage = "年纪不能为空")]
        [Range(10,20,ErrorMessage="年纪必须在10到20之间")]
        public int age { set; get; }

        [Display(Name = "登录密码", Order = 2, AutoGenerateField = false)]//自动表格里隐藏该列
        [DataType(DataType.Password)]
        [Required,MinLength(8)]
        public string pwd { set; get; }

        //public bool check { set; get; }
        //public bool check1 { set; get; }
    }
}