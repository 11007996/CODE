using System.ComponentModel.DataAnnotations;

namespace EAM.Model.System.Dto
{
    /// <summary>
    /// 厂区管理查询对象
    /// </summary>
    public class SysFactoryQueryDto : PagerInfo
    {
        public string FactoryId { get; set; }
        public string FactoryName { get; set; }
    }

    /// <summary>
    /// 厂区管理输入输出对象
    /// </summary>
    public class SysFactoryDto
    {
        [Required(ErrorMessage = "厂区代码不能为空")]
        public string FactoryId { get; set; }

        [Required(ErrorMessage = "厂区名称不能为空")]
        public string FactoryName { get; set; }

        public int? RootDeptId { get; set; }

        public string RootDeptName { get; set; }

        public int? DefaultRoleId { get; set; }

        public string DefaultRoleName { get; set; }

        public int? UserCount { get; set; }
    }
}