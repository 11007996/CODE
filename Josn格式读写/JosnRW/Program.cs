using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace JosnRW
{
    class Program
    {
        static void Main(string[] args)
        {
            /*读josn文件方式一
            string fileName = @"..\..\test.json";
            StreamReader file = File.OpenText(fileName);
            JsonTextReader reader = new JsonTextReader(file);
            JObject jsonObject = (JObject)JToken.ReadFrom(reader);
            Console.WriteLine((jsonObject["Information"]).ToList()[1]["Points"][0][0].ToString());
            file.Close();
            Console.ReadKey();*/
             
            /*//读josn文件方式二
            //string jsonText = @"{""input"" : ""value"", ""output"" : ""result""}";
            string fileName = @"..\..\test.json";
            StreamReader file = File.OpenText(fileName);
            string cont = file.ReadToEnd();
            //JsonReader reader = new JsonTextReader(new StringReader(jsonText));
            JsonReader reader = new JsonTextReader(new StringReader(cont));
            while (reader.Read())
            {
                Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value + "\r\n");
            }
            Console.ReadKey();*/
            


            /*读josn文件方式三
            string jsonText = @"{""input"" : ""value1"", ""output"" : ""result""}";
            JObject jo = JObject.Parse(jsonText);
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();   //item.Name读取键名
            Console.WriteLine(values[0]);
            Console.ReadKey();
            //4.2.4 使用JsonSerializer读写对象(基于JsonWriter与JsonReader)
            //4.2.4.1数组型数据
            string jsonArrayText1 = "[{'a':'a1','b':'b1'},{'a':'a2','b':'b2'}]";
            JArray ja = (JArray)JsonConvert.DeserializeObject(jsonArrayText1);
            string ja1a = ja[0]["a"].ToString();
            Console.WriteLine(ja1a);
            Console.ReadKey();
            //或者
            JObject o = (JObject)ja[1];
            string oa = o["a"].ToString();
            Console.WriteLine(oa);
            Console.ReadKey();
            */

            /*读josn文件方式四
            string jsonText3 = "{\"beijing\":{\"zone\":\"海淀\",\"zone_en\":\"haidian\"}}";
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(jsonText3);
            string zone = jo1["beijing"]["zone"].ToString();
            string zone_en = jo1["beijing"]["zone_en"].ToString();
            Console.WriteLine(zone);
            Console.WriteLine(zone_en);
            Console.ReadKey();
            */


            /*写josn文件
            StringWriter sw = new StringWriter();
            JsonWriter writer = new JsonTextWriter(sw);
            writer.WriteStartObject();
            writer.WritePropertyName("input");
            writer.WriteValue("value");
            writer.WritePropertyName("output");
            writer.WriteValue("result");
            writer.WriteEndObject();
            writer.Flush();
            string jsonText2 = sw.GetStringBuilder().ToString();
            Console.WriteLine(jsonText2);
            Console.ReadKey();
            */


            //改josn文件
            /*
            string fileName = @"..\..\test.json";
            string jsonString = File.ReadAllText(fileName, System.Text.Encoding.UTF8);//读取文件
            JObject jobject = JObject.Parse(jsonString);//解析成json
            jobject["Information"][1]["LocationName"] = "通道2";//替换需要的文件
            string convertString = Convert.ToString(jobject);//将json装换为string
            File.WriteAllText(fileName, convertString, System.Text.Encoding.UTF8);//将内容写进jon文件中
            */

            string fileName = @"..\..\testKpsn.json";
            StringBuilder response = new StringBuilder();
            response.Append("{\"HEAD\": [");
            for (int i=0;i<20000;i++)
            {
                response.Append("{\"SHIPPINGID\": \"00001\",\"USN\": \""+i.ToString("D6")+"\",\"BOXID\": \"02\",\"PALLET_ID\": \"03\",\"CUSTOMER_PN\": \"00001\",\"CUSTOMER_PO\": \"00003\",\"PACK_PALLET_NO\": \"00001\",\"CREATE_DT\": \"00001\",\"CHECK_STATUS\": \"00001\"}," + Environment.NewLine);
            }
            response.Remove(response.Length - 2, 1);
            response.Append("]}");
            File.WriteAllText(fileName, response.ToString(), System.Text.Encoding.UTF8);


            /*序列化DataTable为Json格式
            DataTable dt = new DataTable();
            dt.Columns.Add("Age", Type.GetType("System.Int32"));
            dt.Columns.Add("Name", Type.GetType("System.String"));
            dt.Columns.Add("Sex", Type.GetType("System.String"));
            dt.Columns.Add("IsMarry", Type.GetType("System.Boolean"));
            for (int i = 0; i < 4; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Age"] = i + 1;
                dr["Name"] = "Name" + i;
                dr["Sex"] = i % 2 == 0 ? "男" : "女";
                dr["IsMarry"] = i % 2 > 0 ? true : false;
                dt.Rows.Add(dr);
            }
            Console.WriteLine(JsonConvert.SerializeObject(dt));
            Console.ReadKey();
             */

            /*Person p = new Person { room = null, Age = 10, Name = "张三丰", Sex = "男", IsMarry = false, Birthday = new DateTime(1991, 1, 2) };
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.NullValueHandling = NullValueHandling.Ignore;
            Console.WriteLine(JsonConvert.SerializeObject(p, Formatting.Indented, jsetting));
            Console.ReadKey();*/
        }
    }


    /*读josn文件方式五
    class Program
    {
        static void Main(string[] args)
        {
            Project p = new Project() { Input = "stone", Output = "gold" };
            JsonSerializer serializer = new JsonSerializer();
            StringWriter sw = new StringWriter();
            serializer.Serialize(new JsonTextWriter(sw), p);
            Console.WriteLine(sw.GetStringBuilder().ToString());

            StringReader sr = new StringReader(@"{""Input"":""stone"", ""Output"":""gold""}");
            Project p1 = (Project)serializer.Deserialize(new JsonTextReader(sr), typeof(Project));
            Console.WriteLine(p1.Input + "=>" + p1.Output);
            Console.ReadKey();
        }
    }


    class Project
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }
     */

    [JsonObject(MemberSerialization.OptOut)]
    public class Person
    {
        public int Age { get; set; }

        public string Name { get; set; }
        public string room { get; set; }
        public string Sex { get; set; }

        [JsonIgnore]
        public bool IsMarry { get; set; }

        public DateTime Birthday { get; set; }
    }
}
