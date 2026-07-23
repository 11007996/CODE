using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ComTools.Util
{
    public class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }

        /// <summary>
        /// 获取枚举的值与描述属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<KeyValuePair<T, string>> GetEnumDescriptions<T>() where T : Enum
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            return fields
                .Where(f => f.IsLiteral && !f.IsInitOnly).Select(
                    f =>
                    {
                        var attribute = f.GetCustomAttribute<DescriptionAttribute>();
                        KeyValuePair<T, string> ky = new KeyValuePair<T, string>((T)f.GetValue(null), attribute?.Description);
                        return ky;
                    }).ToList();
        }

        public static List<string> GetEnumDescStr<T>() where T : Enum
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            return fields
                .Where(f => f.IsLiteral && !f.IsInitOnly).Select(
                    f =>
                    {
                        var attribute = f.GetCustomAttribute<DescriptionAttribute>();
                        string desc = attribute?.Description;
                        return desc;
                    }).ToList();
        }

        /// <summary>
        /// 根据名称获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetValueFromName<T>(string name) where T : struct, Enum
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException($"{typeof(T).FullName} 必须是一个枚举类型。");
            }

            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (value.ToString() == name)
                {
                    return value;
                }
            }

            throw new ArgumentException($"枚举类型 {typeof(T).FullName} 中没有找到名称为 '{name}' 的成员。");
        }
    }
}