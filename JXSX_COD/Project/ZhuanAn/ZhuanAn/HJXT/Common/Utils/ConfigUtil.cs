using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common.Utils
{
    public class ConfigUtil
    {
        private static string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
        //读取
        public static XmlDocument ReadConfigFile()
        {
            if (!File.Exists(filePath))
            {
                CreateDefaultXmlConfig();
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            return doc;
        }

        //保存
        public static void SaveConfigFile(XmlDocument doc)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            doc.Save(filePath);
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
                if (!CheckConfigNode())
                {
                    doc = CorrectionConfigFile(doc);
                }
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
                    else if (item.Name == "systemInfo")
                    {//系统信息
                        XmlNodeList sbNodes = item.ChildNodes;
                        foreach (XmlNode item2 in sbNodes)
                        {
                            if (item2.Name == "picCachePath") BaseInfo.PicCachePath = item2.InnerText;
                            else if (item2.Name == "autoUpdate") BaseInfo.AutoUpdate = item2.InnerText == "Y" ? true : false;
                        }
                    }
                    else if (item.Name == "baseInfo")
                    { //基本信息
                        XmlNodeList sbNodes = item.ChildNodes;
                        foreach (XmlNode item2 in sbNodes)
                        {
                            if (item2.Name == "factoryName") BaseInfo.Factory = item2.InnerText;
                            else if (item2.Name == "areaName") BaseInfo.Area = item2.InnerText;
                            else if (item2.Name == "lineName") BaseInfo.Line = item2.InnerText;
                            else if (item2.Name == "machineName") BaseInfo.Machine = item2.InnerText;
                        }
                    }
                    else if (item.Name == "screenPanel")
                    { //看板信息
                        XmlNodeList sbNodes = item.ChildNodes;
                        foreach (XmlNode item2 in sbNodes)
                        {
                            if (item2.Name == "deptName") BaseInfo.Dept = item2.InnerText;
                            else if (item2.Name == "machineNum") BaseInfo.MachineNum = int.Parse(item2.InnerText.Trim());
                            else if (item2.Name == "preWeekNum") BaseInfo.PreWeekNum = int.Parse(item2.InnerText.Trim());
                        }
                    }
                    else if (item.Name == "callSet")
                    { //呼叫设置
                        XmlNodeList sbNodes = item.ChildNodes;
                        foreach (XmlNode item2 in sbNodes)
                        {
                            if (item2.Name == "callLimit") BaseInfo.CallLimit = item2.InnerText == "Y" ? true : false;
                        }
                    }
                    else if (item.Name == "machineListen")
                    { //机台监听设置
                        XmlNodeList sbNodes = item.ChildNodes;
                        foreach (XmlNode item2 in sbNodes)
                        {
                            if (item2.Name == "listenOpenFlag") BaseInfo.OpenFlag = item2.InnerText == "Y" ? true : false;
                            if (item2.Name == "listenPort") BaseInfo.Port = Convert.ToInt32(item2.InnerText);
                            if (item2.Name == "receiveTimeout") BaseInfo.ReceiveTimeout = Convert.ToInt32(item2.InnerText);
                        }
                    }
                }
                //头像缓存目录是否存在
                if (!Directory.Exists(BaseInfo.PicCachePath))
                {
                    try
                    {
                        Directory.CreateDirectory(BaseInfo.PicCachePath);
                    }
                    catch (Exception)
                    {
                        string tempPath = Path.GetTempPath() + "CallSys\\HandlerPic";
                        Directory.CreateDirectory(tempPath);
                        XmlNodeList nodeList = doc.GetElementsByTagName("picCachePath");
                        if (nodeList != null && nodeList.Count > 0)
                            nodeList.Item(0).InnerText = tempPath;
                        SaveConfigFile(doc);
                        BaseInfo.PicCachePath = tempPath;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(ConfigUtil), "加载配置文件发生异常", ex);
                return;
            }
        }

        //检查配置节点
        public static bool CheckConfigNode()
        {
            //加载xml
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings rs = new XmlReaderSettings();
            rs.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(filePath, rs);
            doc.Load(reader);
            reader.Close();
            //根节点
            XmlNodeList nodeList = doc.GetElementsByTagName("configuration");
            if (nodeList != null && nodeList.Count == 0)
                return false;
            //configuration下子节点
            XmlNodeList nodes = doc.SelectSingleNode("configuration").ChildNodes;
            if (nodes == null || nodes.Count < 6 || nodes[0].Name != "database" || nodes[1].Name != "systemInfo" || nodes[2].Name != "baseInfo" ||
                nodes[3].Name != "screenPanel" || nodes[4].Name != "callSet" || nodes[5].Name != "machineListen")
            {
                return false;
            }

            //数据库子节点
            XmlNodeList nodes_0 = nodes[0].ChildNodes;
            if (nodes_0 == null || nodes_0.Count < 1 || nodes_0[0].Name != "connectionString" || nodes_0[0].InnerText == "")
            {
                return false;
            }
            //系统信息子节点
            XmlNodeList nodes_1 = nodes[1].ChildNodes;
            if (nodes_1 == null || nodes_1.Count < 2 || nodes_1[0].Name != "picCachePath" || nodes_1[1].Name != "autoUpdate" ||
                nodes_1[0].InnerText == "" || nodes_1[1].InnerText == "")
            {
                return false;
            }
            //基本信息子节点
            XmlNodeList nodes_2 = nodes[2].ChildNodes;
            if (nodes_2 == null || nodes_2.Count < 4 || nodes_2[0].Name != "factoryName" || nodes_2[1].Name != "areaName" ||
                nodes_2[2].Name != "lineName" || nodes_2[3].Name != "machineName")
            {
                return false;
            }
            //看板设置子节点  
            XmlNodeList nodes_3 = nodes[3].ChildNodes;
            if (nodes_3 == null || nodes_3.Count < 3 || nodes_3[0].Name != "deptName" || nodes_3[1].Name != "machineNum" || nodes_3[2].Name != "preWeekNum" ||
                nodes_3[1].InnerText == "" || nodes_3[2].InnerText == "")
            {
                return false;
            }
            //呼叫设置
            XmlNodeList nodes_4 = nodes[4].ChildNodes;
            if (nodes_4 == null || nodes_4.Count < 1 || nodes_4[0].Name != "callLimit" || nodes_4[0].InnerText == "")
            {
                return false;
            }
            //设备监听设置
            XmlNodeList nodes_5 = nodes[5].ChildNodes;
            if (nodes_5 == null || nodes_5.Count < 3 || nodes_5[0].Name != "listenOpenFlag" || nodes_5[1].Name != "listenPort" ||
                nodes_5[2].Name != "receiveTimeout" || nodes_5[0].InnerText == "")
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 创建默认配置文件
        /// </summary>
        public static void CreateDefaultXmlConfig()
        {
            string dirPath = System.AppDomain.CurrentDomain.BaseDirectory;
            XmlDocument doc = GenerateConfigStructure();
            //生成并保存文件
            if (!Directory.Exists(dirPath))
            {
                /* 如果待保存的文件夹目录不存在就创建文件夹目录 */
                Directory.CreateDirectory(dirPath);
            }
            if (!File.Exists(filePath))
            {
                FileStream fs = File.Create(filePath);
                fs.Close();
            }
            SaveConfigFile(doc);
        }

        //修正配置文件结构，原有配置转移到新配置结构文件中
        public static XmlDocument CorrectionConfigFile(XmlDocument doc)
        {
            XmlDocument newDoc = GenerateConfigStructure();
            string[] nodesName = new string[] {
                "connectionString", "picCachePath", "autoUpdate", 
                "factoryName", "areaName", "lineName", "machineName", 
                "deptName","machineNum", "preWeekNum", 
                "callLimit", 
                "listenOpenFlag", "listenPort", "receiveTimeout"};
            foreach (string name in nodesName)
            {
                XmlNodeList nodes = doc.GetElementsByTagName(name);
                if (nodes.Count > 0 && !string.IsNullOrWhiteSpace(nodes[0].InnerText))
                {
                    newDoc.GetElementsByTagName(name)[0].InnerText = nodes[0].InnerText;
                }
            }
            SaveConfigFile(newDoc);
            return newDoc;
        }


        /// <summary>
        /// 生成配置文件结构
        /// </summary>
        public static XmlDocument GenerateConfigStructure()
        {
            //首先创建XmlDocument xml文档
            XmlDocument doc = new XmlDocument();
            //<?xml version="1.0" encoding="UTF-8">
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));
            //创建根节点 <configuration>
            XmlElement rootNode = doc.CreateElement("configuration");
            //把根节点加入到xml文档中
            doc.AppendChild(rootNode);
            //0：数据库连接
            XmlElement ele_0 = AddChildNode(doc, rootNode, "database", null, "==========数据库设置==========");
            XmlElement ele_0_1 = AddChildNode(doc, ele_0, "connectionString", "server=127.0.0.1;database=HJDB;uid=cs;pwd=iMvyh5JHLzv3uy+vYsrOkw==", "数据库连接");

            //1：系统设置
            XmlElement ele_1 = AddChildNode(doc, rootNode, "systemInfo", null, "==========系统设置==========");
            XmlElement ele_1_1 = AddChildNode(doc, ele_1, "picCachePath", Path.GetTempPath() + "CallSys\\HandlerPic", "头像文件缓存目录");
            XmlElement ele_1_2 = AddChildNode(doc, ele_1, "autoUpdate", "Y", "是否自动更新(Y:是，N:否)");

            //2：基本信息设置
            XmlElement ele_2 = AddChildNode(doc, rootNode, "baseInfo", null, "==========基本信息设置==========");
            XmlElement ele_2_1 = AddChildNode(doc, ele_2, "factoryName", "", "工厂名称");
            XmlElement ele_2_2 = AddChildNode(doc, ele_2, "areaName", "", "厂区名称");
            XmlElement ele_2_3 = AddChildNode(doc, ele_2, "lineName", "", "线别名称");
            XmlElement ele_2_4 = AddChildNode(doc, ele_2, "machineName", "", "机台名称");


            //3：看板信息设置
            XmlElement ele_3 = AddChildNode(doc, rootNode, "screenPanel", null, "==========看板相关设置==========");
            XmlElement ele_3_1 = AddChildNode(doc, ele_3, "deptName", "生技", "看板显示的人员属性部门");
            XmlElement ele_3_2 = AddChildNode(doc, ele_3, "machineNum", "20", "看板统计图表显示的机台数(1~30,默认20)");
            XmlElement ele_3_3 = AddChildNode(doc, ele_3, "preWeekNum", "2", "看板统计图表显示前几周数据（0当前周，1前一周，以此类推，最大5）");

            //4：呼叫设置
            XmlElement ele_4 = AddChildNode(doc, rootNode, "callSet", null, "==========呼叫相关设置==========");
            XmlElement ele_4_1 = AddChildNode(doc, ele_4, "callLimit", "N", "呼叫数量限制(Y:限制，N:不限制)");

            //5.机台监听设置
            XmlElement ele_5 = AddChildNode(doc, rootNode, "machineListen", null, "==========机台监听==========");
            XmlElement ele_5_1 = AddChildNode(doc, ele_5, "listenOpenFlag", "N", "监听开关（Y:开启，N:关闭）");
            XmlElement ele_5_2 = AddChildNode(doc, ele_5, "listenPort", "10409", "监听端口");
            XmlElement ele_5_3 = AddChildNode(doc, ele_5, "receiveTimeout", "10", "接收超时（分钟）");

            return doc;
        }


        /// <summary>
        /// 修改指定配置
        /// </summary>
        /// <param name="eleName"></param>
        /// <param name="innerText"></param>
        public static void ModifyXmlConfig(string eleName, string innerText)
        {
            XmlDocument doc = ReadConfigFile();
            XmlNodeList nodeList = doc.GetElementsByTagName(eleName);
            if (nodeList != null && nodeList.Count > 0)
                nodeList.Item(0).InnerText = innerText;
            SaveConfigFile(doc);
            LoadXmlConfig();
        }

        /// <summary>
        /// 批量修改配置文件
        /// </summary>
        /// <param name="dic"></param>
        public static void ModifyXmlConfig(Dictionary<string, string> dic)
        {
            if (dic == null || dic.Count <= 0) return;
            XmlDocument doc = ReadConfigFile();
            foreach (KeyValuePair<string, string> item in dic)
            {
                XmlNodeList nodeList = doc.GetElementsByTagName(item.Key);
                if (nodeList != null && nodeList.Count > 0)
                    nodeList.Item(0).InnerText = item.Value;
            }
            SaveConfigFile(doc);
            LoadXmlConfig();
        }

        /// <summary>
        /// 创建一个节点并挂载到父节点
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="elementName"></param>
        /// <param name="innerText"></param>
        /// <returns></returns>
        private static XmlElement AddChildNode(XmlDocument doc, XmlElement parentEle, string elementName, string innerText)
        {
            XmlElement ele = AddChildNode(doc, parentEle, elementName, innerText, null);
            return ele;
        }

        /// <summary>
        /// 创建一个节点并挂载到父节点（带注释）
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="elementName"></param>
        /// <param name="innerText"></param>
        /// <returns></returns>
        private static XmlElement AddChildNode(XmlDocument doc, XmlElement parentEle, string elementName, string innerText, string comment)
        {
            if (!string.IsNullOrWhiteSpace(comment))
            {
                XmlComment xmlcomment = doc.CreateComment(comment);
                parentEle.AppendChild(xmlcomment);
            }
            //创建一个节点<ElementName>
            XmlElement ele = CreateNode(doc, elementName, innerText);
            parentEle.AppendChild(ele);
            return ele;
        }


        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="ElementName"></param>
        /// <param name="innerText"></param>
        /// <returns></returns>
        private static XmlElement CreateNode(XmlDocument doc, string ElementName, string innerText)
        {
            //创建一个节点<ElementName>
            XmlElement ele = doc.CreateElement(ElementName);
            //<ElementName>节点的内容
            ele.InnerText = innerText;
            return ele;
        }
    }
}
