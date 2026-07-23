using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Update.Utils
{
    public class ConfigUtil
    {
        private static string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
        //读取
        public static XmlDocument ReadConfigFile()
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("未找到Config.xml配置文件","错误提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                System.Environment.Exit(0);
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            return doc;
        }


        /// <summary>
        /// 加载xml配置文件
        /// </summary>
        public static void LoadXmlConfig()
        {
            try
            {
                XmlDocument doc = ReadConfigFile();
                //检查结构
                XmlNodeList list = doc.SelectSingleNode("configuration").ChildNodes;
                foreach (XmlNode item in list)
                {

                    if (item.Name == "database")
                    { //数据库连接
                        XmlNodeList sbNodes = item.ChildNodes;
                        foreach (XmlNode item2 in sbNodes)
                        {
                            if (item2.Name == "connectionString") DBUtil.ConnectionStr = DBUtil.GetDeConnectionStr(item.InnerText);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

     
    }
}
