using System;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
namespace asynctb
{
    class Class1
    {
        public static void Main(string[] args)
        {
            string filepath = @"C:\Users\mh.guo\Desktop\3.txt";
            byte[] Bytes = new byte[1024];
            FileStream fs = new FileStream(filepath,FileMode.Append,FileAccess.Write);
            string s = "\r\nfd fghfjukmudf";
            Bytes = Encoding.UTF8.GetBytes(s);
            fs.Write(Bytes,0,Bytes.Length);
            fs.Close();
            Console.ReadKey();	


            /*string filepath = @"C:\Users\mh.guo\Desktop\3.txt";
            StreamWriter sw = new StreamWriter(filepath);
            string s = "\r\nfd fghfjukmudf";
            sw.WriteLine(s,0,s.Length);
            sw.Close();
            Console.ReadKey(); */ 


            /*StreamReader sr = new StreamReader(filepath);
            string srr = sr.ReadToEnd();
            sr.Close();
            Console.WriteLine(srr);
            Console.ReadKey();*/
        }
    }
}



/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filepath = { "C:\\Users\\mh.guo\\Desktop\\12.txt", "C:\\Users\\mh.guo\\Desktop\\13.txt", "C:\\Users\\mh.guo\\Desktop\\15.txt" };
            string filepath4 = "C:\\Users\\mh.guo\\Desktop\\14.txt";

            //FileMode.Append在文件末尾写入内容;FileMode.Open打开文件,FileAccess.Write文件可写，FileAccess.Read文件可读,FileAccess.ReadWrite文件可读可写
             FileStream ReadFile3 = new FileStream(filepath4, FileMode.Append, FileAccess.Write);
             fileName[] d = new fileName[3];
             byte[][] a = new byte[3][];
            try
            {
                
                for (int i = 0; i < 3; i++)
                {
                    d[i] = new fileName();
                    //将三个文件内容存入数组
                    a[i] = d[i].ReadFileName(filepath[i]);
                    d[i].ReadFile.Close();
                }
                for (int i = 0; i < 3; i++)
                {
                    //依次将三个文件的内容都写入filepath4中合并
                    ReadFile3.Write(a[i], 0, a[i].Length);
                }
                Console.WriteLine("合并文件成功");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            finally
            {
                //关闭文件流
                ReadFile3.Close();
            }
        }

        private class fileName
        {
            private string filepath;
            private byte[] bts;
            public FileStream ReadFile;
            public byte[] ReadFileName(string filepath)
            {
                this.filepath = filepath;
                //文件可读可写
                ReadFile = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
                //获取文件内容的长度
                bts = new byte[ReadFile.Length];
                //读取文件内容存入bts
                ReadFile.Read(bts, 0, bts.Length);
                return bts;
            }

        }
    }
}
*/