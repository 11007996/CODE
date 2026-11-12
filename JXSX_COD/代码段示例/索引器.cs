using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Net.Sockets;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using System.Timers;

namespace aa
{
    public class Pubc
    {
        static void Main()
        {
            indexC inc = new indexC();
            inc[0] = "aaaa";
            inc[1] = "bbbb";
            Console.WriteLine(inc[1]);
            Console.ReadKey();
        }
    }


    public class indexC
    {
        public int listLength = 10;
        // public string[] list = new string[10];
        private readonly List<string> list =new List<string>();   //设置只读，防止￥外部修改，readonly是不可修改地址，因此对于引用类型，可以进行赋值修改访问，只要不修改其指针地址即可
        public indexC()
        {
            for(int i =0;i<listLength;i++)
            {
                // list[i] = "NA";
                list.Add("NA");
            }
        }

        // 索引器方法
        public string this[int index]
        {
            get{
                string tmp;
                if(index>=0 && index <listLength)
                {
                    tmp=list[index];
                }
                else{
                    tmp="无此项";
                }
                return tmp;
            }
            set{
                if(index>=0 && index <listLength)
                {
                    list[index] = value;
                }
            }
        }
    }
}
