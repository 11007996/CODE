using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util
{
    /// <summary>
    /// 枚举类扩展方法
    /// </summary>
    public static class EnumExtension
    {
        public static string ToDescription(this Enum val)
        {
            var type = val.GetType();

            var memberInfo = type.GetMember(val.ToString());

            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes == null || attributes.Length != 1)
            {
                //如果没有定义描述，就把当前枚举值的对应名称返回
                return val.ToString();
            }

            return (attributes.Single() as DescriptionAttribute).Description;
        }
    }
}
