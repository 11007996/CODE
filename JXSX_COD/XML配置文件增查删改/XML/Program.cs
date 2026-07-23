using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace XML
{
    class Program
    {
        
            private static void addNode()
            {
                try{
                    // string XPath = @"C:\Users\mh.guo\Desktop\XML\XML\App.config";
                    string XPath = @"D:\zzzzzzzz\代码块\XML配置文件增查删改\XML\App.config";
                    Console.WriteLine("请输入节点名");
                    string str = Console.ReadLine();
                    Console.WriteLine("请输入节点内容");
                    string str1 = Console.ReadLine();
                    XmlDocument doc = new XmlDocument();
                    doc.Load(XPath);
                    XmlNode Xdn = doc.SelectSingleNode("configuration/NewNode");
                    //XmlElement Nele = doc.CreateElement("NewNode");
                    //Xdn.AppendChild(Nele);
                    XmlElement ChNele = doc.CreateElement(str);
                    ChNele.InnerText = str1.Trim();
                    Xdn.AppendChild(ChNele);
                    doc.Save(XPath);
                    Console.WriteLine("增加完成");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        private static void updataNode()
        {
            try
            {
            string XPath = @"D:\zzzzzzzz\代码块\XML配置文件增查删改\XML\App.config";
            XmlDocument doc = new XmlDocument();
            doc.Load(XPath);
            Console.WriteLine("请输入要修改的节点名");
            string str = Console.ReadLine();
            Console.WriteLine("请输入要修改的节点内容");
            string str1 = Console.ReadLine();
            XmlNode updateN = doc.SelectSingleNode("configuration/NewNode/" + str);
            XmlNodeList updateNs = updateN.ChildNodes;
            foreach (XmlNode Nodea in updateNs)
            {
                Nodea.InnerText = str1;
            }
            doc.Save(XPath);
            Console.WriteLine("修改完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void RemoveNode()
        {
            try
            {
            string XPath = @"D:\zzzzzzzz\代码块\XML配置文件增查删改\XML\App.config";
            //XmlDocument doc = new XmlDocument();
            //doc.Load(XPath);
            Console.WriteLine("请输入要修改的节点名");
            string str = Console.ReadLine();
            //XmlNode updateN = doc.SelectSingleNode("configuration/NewNode");
            //XmlNode updateN1 = doc.SelectSingleNode(string.Format("configuration/NewNode/{0}",str));
            //foreach (XmlNode reNode in updateN)
            //{
            //updateN.RemoveChild(updateN1);
            //}
            //doc.Save(XPath);
            XDocument Xdoc = XDocument.Load(XPath);
            
            XElement Xele = Xdoc.Element("configuration").Element("NewNode").Element(str);
            Xele.Remove();
            Xdoc.Save(XPath);
            Console.WriteLine("删除完成");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Read()
        {
            try 
            {
            string XPath = @"D:\zzzzzzzz\代码块\XML配置文件增查删改\XML\App.config";
            byte[] buffer = File.ReadAllBytes(XPath);
            string str = System.Text.Encoding.UTF8.GetString(buffer);
            Console.WriteLine(str);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("a:增加；b.修改；c.删除；d.查看");
            string str = Console.ReadLine();
            switch (str)
            {
                case "a":
                    addNode();
                    break;
                case "b":
                    updataNode();
                    break;
                case "c":
                    RemoveNode();
                    break;
                case "d":
                    Read();
                    break;
            }
            Console.ReadKey();
        }
    }
}
