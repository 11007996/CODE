using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Asset.DAL.Util
{
    public class DataTableConvert
    {
        /// <summary>
        ///DataTable转List集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(DataTable dt) where T : new()
        {
            if (dt == null) return null;
            // 定义集合    
            List<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T model = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = model.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            Type realType = Nullable.GetUnderlyingType(pi.PropertyType);//Nullabe类型，取实际类型

                            if (pi.PropertyType.IsEnum || (realType != null && realType.IsEnum))
                            {//枚举类型的值特殊处理
                                object e = Enum.Parse(realType, value.ToString());
                                pi.SetValue(model, e, null);
                            }
                            else
                            {
                                pi.SetValue(model, value, null);
                            }
                        }
                    }
                }
                ts.Add(model);
            }
            return ts;
        }

    }
}
