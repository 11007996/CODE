using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Linq;
using System.Xml;
using System.Linq;
using System.Text;
using System.IO;


#region        新建XML
//Linq to XML
/*namespace MyGeneric
{
	class Program
	{
		static void Main()      
		{
			XDocument employees1 = XDocument.Load(@"C:\Users\mh.guo\Desktop\test.xml");
			XElement root = employees1.Element("Employees");          //获取根节点
			employees1.Declaration = new XDeclaration("1.0","utf-8","yes");     //插入XML声明
			root.Add(new XComment("这是个注释"));           //在root节点后添加注释
			
			// XElement root1 = employees1.Element("Employee");   //获取根节点下的子节点
			// XElement root2 = root1.ElementText("Name");       //获取子节点下的子节点
			// root2.Remove();                  //删除子节点
		    root.AddFirst(new XElement("Employee",       
			             new XAttribute("Color","Red"),            //添加节点属性
			             new XElement("Name","guo",new XAttribute("ming","minghui")),          //添加子节点
						 new XElement("PhontNumber","123456")));
			// root1.Attribute("ming").Remove();		 //删除节点属性
						 
			employees1.Save(@"C:\Users\mh.guo\Desktop\test.xml");           //保存
			Console.WriteLine(employees1);
		}
	}
}*/

#endregion


#region        新建XML

namespace MyGeneric         //c#硬编码
{
	class Program
	{
		static void Main()      
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(doc.CreateXmlDeclaration("1.0","utf-8","yes"));     //插入XML声明
			
			XmlElement ele = doc.CreateElement("root");             //新建根节点
			doc.AppendChild(ele);                                   //将根节点放入文档树
			
			XmlElement eleBook = doc.CreateElement("book");         //新建节点book
			eleBook.SetAttribute("orderId","序号");                 //新建book节点属性
			eleBook.SetAttribute("bookName","书名");
			ele.AppendChild(eleBook);                               //book节点插入root根节点
			
			XmlElement book1 = doc.CreateElement("book1");          //新建子节点book1
			book1.SetAttribute("a","西游记");                       //新建book1节点属性
			XmlElement book2 = doc.CreateElement("book2");
			book2.SetAttribute("b","水浒");
			XmlElement book3 = doc.CreateElement("book3");
			book3.SetAttribute("c","红楼梦");
			XmlElement book4 = doc.CreateElement("book4");
			book4.SetAttribute("d","三国"); 
			eleBook.AppendChild(book1);                            //book1节点插入book节点
			eleBook.AppendChild(book2);
			eleBook.AppendChild(book3);
			eleBook.AppendChild(book4);
			
			XmlElement book1_sac = doc.CreateElement("saction");   //新建子节点saction 
			book1_sac.InnerText = "唐僧";                          //向节点saction写入文本
			book1.AppendChild(book1_sac);                          //saction节点插入book1节点
			
			XmlElement book2_sac = doc.CreateElement("saction");
			book2_sac.InnerText = "宋江";
			book2.AppendChild(book2_sac);
			
			XmlElement book3_sac = doc.CreateElement("saction");
			book3_sac.InnerText = "贾宝玉";
			book3.AppendChild(book3_sac);
			
			XmlElement book4_sac = doc.CreateElement("saction");
			book4_sac.InnerText = "赵子龙";
			book4.AppendChild(book4_sac);
			
			doc.Save(@"C:\Users\mh.guo\Desktop\test.xml");         //XML文档，若没有则新建
			Console.WriteLine("编写完成");
			
			
			
		}
	}
}

#endregion



#region   修改XML 
/*namespace MyGeneric         //c#硬编码
{
	class Program
	{
		static void Main()      
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"C:\Users\mh.guo\Desktop\test.xml");
			XmlNode Xdn = doc.SelectSingleNode("root/book/book1/saction");
			XmlNodeList XdL = Xdn.ChildNodes;
			// XmlNode XdL = Xdn.ChildNodes[0];
			// XdL.InnerText = "11111";
			foreach( XmlNode XdNo in XdL)
			{
			XdNo.InnerText = "孙悟空";
			}
			doc.Save(@"C:\Users\mh.guo\Desktop\test.xml");
			Console.WriteLine("修改完成");
		}
	}
}*/
#endregion



#region   添加子节点

/*namespace MyGeneric         //c#硬编码
{
	class Program
	{
		static void Main()      
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"C:\Users\mh.guo\Desktop\test.xml");
			XmlNode Xdn = doc.SelectSingleNode("root/book");
			XmlElement Ele = doc.CreateElement("booke");                //添加子节点
			Ele.SetAttribute("e","三体");
			Xdn.AppendChild(Ele);
			XmlElement Elf = doc.CreateElement("saction");
			Elf.InnerText = "罗锋";
			Ele.AppendChild(Elf);
			
			doc.Save(@"C:\Users\mh.guo\Desktop\test.xml");
			Console.WriteLine("修改完成");
		}
	}
}*/

#endregion