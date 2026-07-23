using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string backPath = @"D:\work\SFC_MES数据备份\EUT重传\back";
            string filePath = @"D:\work\SFC_MES数据备份\EUT重传\2DP5EUT";
            string[] files = Directory.GetFiles(filePath, "*.json");



            //根据线程数分配文件
            List<List<string>> listGroup = DistributeList(files.ToList<string>(), 4);
            //创建线程去发送文件
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < listGroup.Count; i++)
            {
                List<string> lists = listGroup[i];
                Task ts = Task.Factory.StartNew(() => { changeFile(lists, backPath); });
                tasks.Add(ts);
            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("OK");
            Console.ReadKey();

        }

        public static void changeFile(string[] files, string backPath)
        {
            foreach (string file in files)
            {
                try
                {
                    string jsonString = File.ReadAllText(file, Encoding.Default);
                    JObject jo = JObject.Parse(jsonString);
                    jo["results"] = new JArray();
                    string convertString = Convert.ToString(jo);//将json装换为string
                    File.WriteAllText(backPath, convertString, System.Text.Encoding.ASCII);//将内容写进jon文件中
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public static void changeFile(List<string> files, string backPath)
        {
            foreach (string file in files)
            {
                try
                {
                    string jsonString = File.ReadAllText(file, Encoding.Default);
                    JObject jo = JObject.Parse(jsonString);

                    jo["results"] = new JArray();
                    string convertString = Convert.ToString(jo);//将json装换为string
                    string fi = file.Substring(file.LastIndexOf('\\') + 1);
                    string fileName = Path.Combine(backPath, fi);
                    File.WriteAllText(fileName, convertString, System.Text.Encoding.ASCII);//将内容写进jon文件中
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        /// <summary>
        /// 分配列表
        /// </summary>
        /// <param name="originalList"></param>
        /// <param name="targetCount"></param>
        /// <returns></returns>
        private static List<List<string>> DistributeList(List<string> originalList, int targetCount)
        {
            // 创建目标列表容器
            List<List<string>> result = new List<List<string>>();

            // 计算每个子列表的基础大小和剩余元素
            int totalItems = originalList.Count;
            int baseSize = (int)Math.Ceiling((double)totalItems / targetCount); // 每个子列表的基础大小

            // 分配元素到每个子列表
            for (int i = 0; i < totalItems; i += baseSize)
            {
                result.Add(originalList.Skip(i).Take(baseSize).ToList());
            }

            return result;
        }
    }
}
