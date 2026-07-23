using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Manager.Util
{
    class FileUtil
    {
        /// <summary>
        /// 从文件中反序列化对象
        /// </summary>
        public static T DeserializeFromFile<T>(string path)
        {
            if (File.Exists(path))
            {
                string text = "";
                using (StreamReader sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<T>(text);
            }
            return default(T);
        }

        /// <summary>
        /// 对象序列化并保存到文件
        /// </summary>
        public static void SerializeToFile(string path, object obj)
        {
            string text = JsonConvert.SerializeObject(obj);
            string dir = new FileInfo(path).DirectoryName;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (File.Exists(path)) File.Delete(path);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(text);
            }
        }
    }
}
