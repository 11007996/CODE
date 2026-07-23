using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common.Util
{
    public class ConfigUtil
    {
        private static readonly string FilePath = System.AppDomain.CurrentDomain.BaseDirectory + "Config.xml";

        private static ConfigNode ConfigNodeTree = null;

        /// <summary>
        /// 静态初始配置类结构
        ///     类加载时执行一次
        /// </summary>
        static ConfigUtil()
        {
            ConfigNodeTree = new ConfigNode("configuration");

            //1.数据库连接
            ConfigNode node1 = new ConfigNode("database", null, "==========数据库设置==========");
            node1.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("connectionString", "server=127.0.0.1;database=HJDB;uid=cs;pwd=iMvyh5JHLzv3uy+vYsrOkw==", "数据库连接")
            };
            //2.系统设置
            ConfigNode node2 = new ConfigNode("systemInfo", null, "==========系统设置==========");
            node2.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("picCachePath", System.AppDomain.CurrentDomain.BaseDirectory + "Temp\\UserPicture", "头像文件缓存目录"),
                new ConfigNode("autoUpdate", "Y", "是否自动更新(Y:是，N:否)")
            };
            //3.基本信息设置
            ConfigNode node3 = new ConfigNode("baseInfo", null, "==========基本信息设置==========");
            node3.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("factoryName", "", "工厂名称"),
                new ConfigNode("areaName", "", "厂区名称"),
                new ConfigNode("lineName", "", "线别名称"),
                new ConfigNode("machineName", "", "机台名称"),
                new ConfigNode("callLimit", "N", "呼叫数量限制(Y:限制，N:不限制)")
            };
            //4.看板信息设置
            ConfigNode node4 = new ConfigNode("screenPanel", null, "==========看板相关设置==========");
            node4.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("kanbanShow", "呼叫看板", "看板显示:[呼叫看板,设备看板,兼容切换]"),
                new ConfigNode("deptName", "生技", "看板显示的人员所属部门"),
                new ConfigNode("machineNum", "20", "看板统计图表显示的机台数(1~30,默认20)"),
                new ConfigNode( "preWeekNum", "2", "看板统计图表显示前几周数据（0当前周，1前一周，以此类推，最大5）"),
                new ConfigNode( "callHandlerShowFlag", "N", "是否开启呼叫人员故障显示(Y:开启，N:不开启)")
            };

            //5：呼叫设置
            ConfigNode node5 = new ConfigNode("speechVoice", null, "==========广播相关设置==========");
            node5.SubNodes = new List<ConfigNode>
            {
                new ConfigNode("speechRate", "0", "广播语速[-5,5]"),
                new ConfigNode("speechSpanMinute", "1", "广播频率（最小值1），单位:分钟"),
                new ConfigNode("callHandlerSpeechFlag", "N", "是否开启呼叫人员广播(Y:开启，N:不开启)")
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
                //系统设置
                XmlNode sysNode = root.SelectSingleNode("systemInfo");
                BaseInfo.PicCachePath = sysNode.SelectSingleNode("picCachePath").InnerText;
                BaseInfo.AutoUpdate = sysNode.SelectSingleNode("autoUpdate").InnerText == "Y" ? true : false;
                //基本信息
                XmlNode baseNode = root.SelectSingleNode("baseInfo");
                BaseInfo.Factory = baseNode.SelectSingleNode("factoryName").InnerText;
                BaseInfo.Area = baseNode.SelectSingleNode("areaName").InnerText;
                BaseInfo.Line = baseNode.SelectSingleNode("lineName").InnerText;
                BaseInfo.Machine = baseNode.SelectSingleNode("machineName").InnerText;
                BaseInfo.CallLimit = baseNode.SelectSingleNode("callLimit").InnerText == "Y" ? true : false;
                //看板信息
                XmlNode kbNode = root.SelectSingleNode("screenPanel");
                BaseInfo.KanBanShow = kbNode.SelectSingleNode("kanbanShow").InnerText;
                BaseInfo.Dept = kbNode.SelectSingleNode("deptName").InnerText;
                BaseInfo.MachineNum = int.Parse(kbNode.SelectSingleNode("machineNum").InnerText);
                BaseInfo.PreWeekNum = int.Parse(kbNode.SelectSingleNode("preWeekNum").InnerText);
                BaseInfo.CallHandlerShowFlag = kbNode.SelectSingleNode("callHandlerShowFlag").InnerText == "Y" ? true : false;
                //广播设置
                XmlNode speechNode = root.SelectSingleNode("speechVoice");
                BaseInfo.SpeechRate = int.Parse(speechNode.SelectSingleNode("speechRate").InnerText);
                BaseInfo.SpeechSpanMinute = int.Parse(speechNode.SelectSingleNode("speechSpanMinute").InnerText);
                BaseInfo.CallHandlerSpeechFlag = speechNode.SelectSingleNode("callHandlerSpeechFlag").InnerText == "Y" ? true : false;
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
            doc.Save(FilePath);
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
