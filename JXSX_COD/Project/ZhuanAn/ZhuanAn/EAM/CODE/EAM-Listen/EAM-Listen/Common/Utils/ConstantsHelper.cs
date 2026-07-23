using System;
using System.Collections.Generic;
using System.Reflection;

namespace EAM.Listen.Common.Utils
{
    public class ConstantsHelper
    {
        public static Dictionary<string, string> GetConstantsDictionary<T>()
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            var constants = new Dictionary<string, string>();

            foreach (FieldInfo field in fields)
            {
                if (field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string)) // 检查是否为常量
                {
                    constants[field.Name] = Convert.ToString(field.GetValue(null)); // 常量是静态的，所以传递 null
                }
            }

            return constants;
        }
    }
}