using System.ComponentModel.DataAnnotations;

namespace EAM.Model.System.Dto
{
    /// <summary>
    /// 用户关联厂区查询对象
    /// </summary>
    public class SysUserFactoryQueryDto : PagerInfo
    {
        public string FactoryId { get; set; }
        public string UserName { get; set; }
    }

    /// <summary>
    /// 用户关联厂区输入输出对象
    /// </summary>
    public class SysUserFactoryDto
    {
        [Required(ErrorMessage = "用户ID不能为空")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "工厂代码不能为空")]
        public string FactoryId { get; set; }
    }

    /// <summary>
    /// 厂区用户添加dto
    /// </summary>
    public class FactoryUsersOperateDto
    {
        /// <summary>
        /// 厂区id
        /// </summary>
        [Display(Name = "厂区id")]
        [Required(ErrorMessage = "factoryId 不能为空")]
        public string FactoryId { get; set; }

        /// <summary>
        /// 用户编码 [1,2,3,4]
        /// </summary>
        [Display(Name = "用户编码 [1,2,3,4]")]
        public List<long> UserIds { get; set; }
    }
}