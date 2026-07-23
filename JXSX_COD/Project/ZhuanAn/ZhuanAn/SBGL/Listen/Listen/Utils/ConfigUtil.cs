using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Listen.Utils
{
    public class ConfigUtil
    {
        private static string FilePath = System.AppDomain.CurrentDomain.BaseDirectory + "Config.xml";

        private static ConfigNode ConfigNodeTree = null;

        /// <summary>
        /// 静态初始配置类结构
        ///     类加载时执行一次
        /// </summary>
        static ConfigUtil()
        {
            ConfigNodeTree = new ConfigNode("configuration");
            //1.数据库设置
            ConfigNode node1 = new ConfigNode("database", null, "==========数据库设置==========");
            node1.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("connectionString", "server=127.0.0.1;database=HJDB;uid=cs;pwd=iMvyh5JHLzv3uy+vYsrOkw==", "数据库连接"),
            };
            //2.TCP监听设置
            ConfigNode node2 = new ConfigNode("tcpListen", null, "==========TCP监听==========");
            node2.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("tcpListenFlag", "N", "监听开关（Y:开启，N:关闭）"),
                new ConfigNode("listenIP", "0.0.0.0", "监听IP"),
                new ConfigNode("listenPort", "10409", "监听端口"),
                new ConfigNode("receiveTimeout", "10", "接收超时（秒）")
            };
            //3.串口监听设置
            ConfigNode node3 = new ConfigNode("serialListen", null, "==========串口监听==========");
            node3.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("serialListenFlag", "N", "监听开关（Y:开启，N:关闭）"),
                new ConfigNode("listenPortName", "COM3", "串口名称"),
                new ConfigNode("baudRate", "9600", "波特率"),
                new ConfigNode("dataBits", "8", "数据位"),
                new ConfigNode("stopBits", "1", "停止位[0,1,2,3]"),
                new ConfigNode("parity", "1", "奇偶校验[0,1,2,3,4]")
            };
            //4.保养开关
            ConfigNode node4 = new ConfigNode("operate", null, "==========保养操作==========");
            node4.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("dayMaintenanceCheckFlag", "N", "日保养检查开关（Y:开启，N:关闭）"),
                new ConfigNode("weekMaintenanceCheckFlag","N", "周保养检查开关（Y:开启，N:关闭）"),
                new ConfigNode("monthMaintenanceCheckFlag", "N", "月保养检查开关（Y:开启，N:关闭）"),
            };
            //5.通信编码
            ConfigNode node5 = new ConfigNode("codeRule", null, "==========通信编码==========");
            node5.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("prefixHexCode", "", "前缀16进制编码字符"),
                new ConfigNode( "suffixHexCode", "", "后缀16进制编码字符"),
                new ConfigNode( "hexCodeByteSize", "10", "总编码字节长度"),
            };
            ConfigNodeTree.SubNodes = new List<ConfigNode>
            {
                node1,
                node2,
                node3,
                node4,
                node5
            };
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
                if (!CheckConfigNode(doc))
                {
                    doc = CorrectionConfigFile(doc);
                }
                XmlNode root = doc.SelectSingleNode("configuration");
                //数据库连接
                XmlNode dbNode = root.SelectSingleNode("database");
                DBUtil.ConnectionStr = DBUtil.GetDeConnectionStr(dbNode.SelectSingleNode("connectionString").InnerText);
                //TCP设置 
                XmlNode tcpNode = root.SelectSingleNode("tcpListen");
                BaseInfo.TCPListenFlag = tcpNode.SelectSingleNode("tcpListenFlag").InnerText == "Y" ? true : false;
                BaseInfo.ListenIP = System.Net.IPAddress.Parse(tcpNode.SelectSingleNode("listenIP").InnerText);
                BaseInfo.Port = Convert.ToInt32(tcpNode.SelectSingleNode("listenPort").InnerText);
                BaseInfo.ReceiveTimeout = Convert.ToInt32(tcpNode.SelectSingleNode("receiveTimeout").InnerText);
                //串口监听设置
                XmlNode serialNode = root.SelectSingleNode("serialListen");
                BaseInfo.SerialListenFlag = serialNode.SelectSingleNode("serialListenFlag").InnerText == "Y" ? true : false;
                BaseInfo.PortName = serialNode.SelectSingleNode("listenPortName").InnerText;
                BaseInfo.BaudRate = Convert.ToInt32(serialNode.SelectSingleNode("baudRate").InnerText);
                BaseInfo.DataBits = Convert.ToInt32(serialNode.SelectSingleNode("dataBits").InnerText);
                BaseInfo.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), serialNode.SelectSingleNode("stopBits").InnerText);
                BaseInfo.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), serialNode.SelectSingleNode("parity").InnerText);
                //操作
                XmlNode operateNode = root.SelectSingleNode("operate");
                BaseInfo.DayMaintenanceCheckFlag = operateNode.SelectSingleNode("dayMaintenanceCheckFlag").InnerText == "Y" ? true : false;
                BaseInfo.WeekMaintenanceCheckFlag = operateNode.SelectSingleNode("weekMaintenanceCheckFlag").InnerText == "Y" ? true : false;
                BaseInfo.MonthMaintenanceCheckFlag = operateNode.SelectSingleNode("monthMaintenanceCheckFlag").InnerText == "Y" ? true : false;
                //通信编码
                XmlNode codeNode = root.SelectSingleNode("codeRule");
                BaseInfo.PrefixHexCode = codeNode.SelectSingleNode("prefixHexCode").InnerText;
                BaseInfo.SuffixHexCode = codeNode.SelectSingleNode("suffixHexCode").InnerText;
                BaseInfo.HexCodeByteSize = Convert.ToInt32(codeNode.SelectSingleNode("hexCodeByteSize").InnerText);
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(ConfigUtil), "加载配置文件发生异常", ex);
                return;
            }
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
        /// 读取配置文件(不存在创建默认配置文件)
        /// </summary>
        /// <returns></returns>
        private static XmlDocument ReadConfigFile()
        {
            if (!File.Exists(FilePath))
            {
                CreateDefaultXmlConfig();
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(FilePath);
            return doc;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="doc"></param>
        private static void SaveConfigFile(XmlDocument doc)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            doc.Save(filePath);
        }


        /// <summary>
        /// 检查配置节点是否正确
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static bool CheckConfigNode(XmlDocument doc)
        {
            return CheckConfigNodes(ConfigNodeTree, doc.SelectSingleNode(ConfigNodeTree.Name));
        }

        /// <summary>
        /// 递归检查配置节点结构
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private static bool CheckConfigNodes(ConfigNode node, XmlNode xmlNode)
        {
            if (node.Name != xmlNode.Name)
            {
                return false;
            }
            if (node.SubNodes != null && node.SubNodes.Count > 0)
            {
                foreach (ConfigNode subNode in node.SubNodes)
                {
                    XmlNode subXmlNode = xmlNode.SelectSingleNode(subNode.Name);
                    if (subXmlNode == null)
                        return false;
                    if (!CheckConfigNodes(subNode, subXmlNode))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 创建默认配置文件
        /// </summary>
        private static void CreateDefaultXmlConfig()
        {
            string dirPath = System.AppDomain.CurrentDomain.BaseDirectory;
            XmlDocument doc = GenerateConfigStructure();
            //生成并保存文件
            if (!Directory.Exists(dirPath))
            {
                /* 如果待保存的文件夹目录不存在就创建文件夹目录 */
                Directory.CreateDirectory(dirPath);
            }
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
            SaveConfigFile(doc);
        }

        /// <summary>
        /// 修正配置文件结构，原有配置转移到新配置结构文件中
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static XmlDocument CorrectionConfigFile(XmlDocument doc)
        {
            MapperConfigNodeValue(ConfigNodeTree, doc.SelectSingleNode(ConfigNodeTree.Name));
            XmlDocument newDoc = GenerateConfigStructure();
            SaveConfigFile(newDoc);
            return newDoc;
        }

        /// <summary>
        /// 递归映射原有配置节点的值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xmlNode"></param>
        private static void MapperConfigNodeValue(ConfigNode node, XmlNode xmlNode)
        {
            if (node != null && xmlNode != null && node.Name == xmlNode.Name && node.SubNodes == null)
            {
                node.Value = xmlNode.InnerText;
            }
            if (node.SubNodes != null && node.SubNodes.Count > 0)
            {
                foreach (ConfigNode subNode in node.SubNodes)
                {
                    XmlNode subXmlNode = xmlNode.SelectSingleNode(subNode.Name);
                    if (subXmlNode != null)
                        MapperConfigNodeValue(subNode, subXmlNode);
                }
            }
        }

        /// <summary>
        /// 生成全新的配置文件
        /// </summary>
        private static XmlDocument GenerateConfigStructure()
        {
            //首先创建XmlDocument xml文档
            XmlDocument doc = new XmlDocument();
            //<?xml version="1.0" encoding="UTF-8">
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));
            GenerateChindNode(ConfigNodeTree, doc, null);
            return doc;
        }

        /// <summary>
        /// 递归创建默认配置结点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="doc"></param>
        /// <param name="parentEle"></param>
        private static void GenerateChindNode(ConfigNode node, XmlDocument doc, XmlElement parentEle)
        {
            if (node != null)
            {
                XmlElement ele = null;
                //判断是否有父节点，如果没有，表示没有根节点，追加一个根结点
                if (parentEle == null)
                {
                    ele = doc.CreateElement(node.Name);
                    doc.AppendChild(ele);
                }
                else
                {
                    //添加子节点
                    ele = AddChildNode(doc, parentEle, node.Name, node.Value, node.Desc);
                }
                //判断是否有字节点，递归
                if (node.SubNodes != null && node.SubNodes.Count > 0)
                {
                    foreach (ConfigNode subNode in node.SubNodes)
                    {
                        GenerateChindNode(subNode, doc, ele);
                    }
                }
            }
        }


        /// <summary>
        /// 修改指定配置
        /// </summary>
        /// <param name="eleName"></param>
        /// <param name="innerText"></param>
        private static void ModifyXmlConfig(string eleName, string innerText)
        {
            XmlDocument doc = ReadConfigFile();
            XmlNodeList nodeList = doc.GetElementsByTagName(eleName);
            if (nodeList != null && nodeList.Count > 0)
                nodeList.Item(0).InnerText = innerText;
            SaveConfigFile(doc);
            LoadXmlConfig();
        }


        #region XML操作
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
        #endregion

        #region 节点类
        /// <summary>
        /// 配置节点结构类（内部类）
        /// </summary>
        private class ConfigNode
        {
            public ConfigNode(string name)
            {
                this.Name = name;
            }
            public ConfigNode(string name, string value, string desc)
            {
                this.Name = name;
                this.Value = value;
                this.Desc = desc;
            }
            public string Name { get; set; }
            public string Value { get; set; }

            public string Desc { get; set; }

            public List<ConfigNode> SubNodes { get; set; }
        }
        #endregion
    }
}
