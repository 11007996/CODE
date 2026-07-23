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
    public class sss
    {
        static void Main()
        {
            // Int32 a =Convert.ToInt32(DateTime.Now.AddDays(-1).ToString("MMdd") + "2230");
            // Int32 b =Convert.ToInt32(DateTime.Now.ToString("MMdd") + "0130");
            // Int32 c =Convert.ToInt32(DateTime.Now.ToString("MMdd") + "0030");
            // Console.WriteLine(a);
            // Console.WriteLine(b);
            // Console.WriteLine(c);

            // string a = DateTime.Now.ToString("yyMMdd");
            // int a = -1;
            // Console.WriteLine(a);

            // string a = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");

            // string station_id="D3-2FT-01_1_FG_APP";
            // string test_station_name = station_id.Split('_')[station_id.Split('_').Length - 1];

            // string sn =transSN("fsgrhsghsrhtrhtr-123-456");

            // string a = "111222333444-1234-1234";
            // string a = "FC9111ASDFDF";
            string a = "1406-00400-0001H260602000111";
            a = a.Substring(0, 16);
            Console.WriteLine(a);
            Console.Read();


            /*string a = "包装卡控纸带物料，F有卡针无印字";
            string zdStatus = "";
            if(!string.IsNullOrEmpty(a))
            {
                // zdStatus = dt.Rows[0][0].ToString();
                zdStatus = a.Split('，')[1];
            }
            else if(a=="H")
            {
                zdStatus= "其他";
            }
            Console.WriteLine(zdStatus);*/
        }
    }
}

            // Console.WriteLine(Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(DateTime.Now.Hour));
                /*char[]  a = {'a','b','c','d'};
                  Console.WriteLine(a[0]);*/

                /*DateTime a =Convert.ToDateTime("2025-8-20 19:55:30");
                DateTime b = Convert.ToDateTime("2025-8-20 19:54:30");
                TimeSpan ts = a-b;
                Console.WriteLine(ts.TotalMilliseconds);
                Console.WriteLine(ts);*/

                /*if(Convert.ToDateTime("2025-08-25 10:00:00").ToShortDateString()!=DateTime.Now.ToShortDateString())
                {
                    Console.WriteLine("非当天抽样计划");
                }
                else{
                    Console.WriteLine("OK");
                    }*/

                // Console.WriteLine(ts.Seconds);
                /*int[] arr1 = { 1, 2, 3 };
                int[] arr2 = { 3,4, 5, 6 };
                int[] arr3 = new int[arr1.Length + arr2.Length];

                Array.Copy(arr1, arr3, arr1.Length);
                Array.Copy(arr2, 0, arr3, arr1.Length, arr2.Length);

                foreach (var e in arr3)
                {
                    Console.WriteLine(e);
                }
               */

                /*var rnd   = new Random();
                double d = rnd.Next(5);
                Console.WriteLine(d);*/


                // Random rd = new Random();
                // int a =rd.Next(1, 5);

                // string a = DateTime.Now.ToString("yyMMddHHmmssfff");
                // string a = DateTime.Now.AddDays(4).DayOfWeek.ToString();
                // string a = DateTime.Now.ToString("yyyyMMdd");
                // DateTime d=Convert.ToDateTime(DateTime.Now.ToShortDateString());
                // string a =DateTime.Now.ToString("W");

                /*string a = "FC93314000W26GVB7";
                a = a.Substring(a.Length-3,3);*/
                // Console.WriteLine(a);

                /*string a = "111-dfje";
                a =a.Substring(a.IndexOf('-')+1);
                Console.WriteLine(a);*/
                /*DateTime t = Convert.ToDateTime("2025/06/10 15:31:32");
                string str = t.ToString("yyMMddHH");
                Console.WriteLine(str);*/
                /*string a = "EC04C_AOI_FG_AOI5";
                a = a.Substring(a.LastIndexOf("_")+1);
                Console.WriteLine(a);*/

                /*StringBuilder sb = new StringBuilder();
                sb.Append("qqqqq;");
                sb.Remove(sb.Length-1,1);
                Console.WriteLine(sb.ToString());*/


                /*int QTY=0;
                bool b = int.TryParse("", out QTY);
                if(b)
                {
                    Console.WriteLine(QTY);
                }
                Console.WriteLine("NG");*/

                /*string a = DateTime.Now.ToString();
                a = Convert.ToDateTime(a).ToShortDateString();
                Console.WriteLine(a);*/

                /*string a = "q|b|c";
                a = a.Replace("|","");
                Console.WriteLine(a);*/

                /*string a = "abcdefgvn";
                a = a.Substring(0,a.Length-2);
                Console.WriteLine(a);*/


                /*string Reel_no = "13101-S0D0075H-0020250221F210004";
                string datecode = "20250221";

                 Reel_no = "13101-S0D0075H-00" + datecode.Substring(2) +"1"+ Reel_no.Substring(Reel_no.Length - 5);
                 Console.WriteLine(Reel_no);*/
                 /*string datecode = "20250221";
                 string a  = "13101-S0D0075H-00" + datecode.Substring(2);*/
                 /*string a = "253-800000-A03H20250226050";
                 string dateStr = a.Substring(a.Length-11,8);*/
                 //string aa =dateStr.Substring(0,4)+"/"+dateStr.Substring(4,2)+"/"+dateStr.Substring(6,2);
                 //Console.WriteLine(aa);
                 // DateTime dd = Convert.ToDateTime(dateStr.Substring(0,4)+"/"+dateStr.Substring(4,2)+"/"+dateStr.Substring(6,2));
                 // Console.WriteLine(dd);
                /*string s= System.Windows.Forms.Application.StartupPath;
                 Console.WriteLine(s);*/

            /*long a=System.DateTime.Now.Ticks;
                 Console.WriteLine(a);*/
            /*
            string str = "+001.515kg+001.525kg";
            //bool b = Regex.IsMatch(str, @"[1-9][0-9]*\.[0-9]*(?=kg)$");
            StringBuilder strb = new StringBuilder();
            if (!Regex.IsMatch(str, @"[1-9][0-9]*\.[0-9]*(?=kg)"))
                {
                    Console.WriteLine("重量获取失败，请称重");
                }
                else
                {
                    MatchCollection matches = Regex.Matches(str, @"[1-9][0-9]*\.[0-9]*(?=kg)");
                    
                    foreach (Match match in matches)
                    {
                        strb.Length =0;
                        strb.Append(match.Value);
                    }
                }
            Console.WriteLine(strb.ToString());
           */
            // MatchCollection matches = Regex.Matches(str, @"[1-9][0-9]*\.[0-9]*(?=kg)");
            // foreach (Match match in matches)
            // {
            //     Console.WriteLine(match.Value);
            //     }


            /*try
            {
            string a = "\"QTY\":\"6720\"";
            Regex rx = new Regex(@"\d+",RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(a);
            Console.WriteLine(matches[0]);
            Console.Read();
            Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
                }*/


            /*string louzhan= "地 EC04C_F_QT1 未测试";
            string c = louzhan.Substring(louzhan.IndexOf("EC04C"),11);
            Console.WriteLine(c);
            Console.ReadKey();*/

            /*Text a = new Text();
            a.txt = "aaaaa";
            Text b = a;  //类为引用类型此外为浅复制
            Console.WriteLine(b.txt);
            a.txt = "bbbbbbb";
            Console.WriteLine(b.txt);
            Console.ReadKey();*/

            /*StringBuilder sb = new StringBuilder();
            sb.AppendLine("aaaaaa" + "\t" + "bbbbb");
            sb.AppendLine("aaaaaa" + "\t" + "bbbbb");
            sb.Append(Environment.NewLine);
            sb.Remove(sb.Length-4,4);
            Console.WriteLine(sb.ToString());
            Console.ReadKey();*/
            
            /*string tsid = "ITJI_D01-3FT-07_3_QT1";
            bool bo ="SN Already Packed" == "SN Already Packed" && (tsid.Contains("QT1") || tsid.Contains("APP"));
            Console.WriteLine(bo);
            Console.ReadKey();*/


            /*DateTime timeNow = DateTime.Now;
            DateTime time1, time2;
            int timeHour = timeNow.Hour;
            if(timeHour>=8 && timeHour<=19)
            {
                time1 =Convert.ToDateTime(timeNow.ToShortDateString() + " 08:00:00");
                time2 = Convert.ToDateTime(timeNow.ToShortDateString() + " 19:59:59");
            }
            else
            {
                time1 = Convert.ToDateTime(timeNow.ToShortDateString() + " 20:00:00");
                time2 = Convert.ToDateTime(timeNow.AddDays(1).ToShortDateString() + " 07:59:59");
            }
            Console.WriteLine(time1);
            Console.WriteLine(time2);
            Console.ReadKey();*/

            /*string aa = "qwer;sdfghj;";
            string[] kpsns = aa.Split(';');
            Console.WriteLine(kpsns.Length);
            Console.ReadKey();*/

            /*string aa = DateTime.Now.ToString("yyyyMMdd");
            Console.WriteLine(aa);
            Console.ReadKey();*/

           /* //十六进制字符串转换为字符类型
            List<char> hexCharList = new List<char>
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F'
            };
            string hex="AB 2B";
            byte b;
            for (int i = 0; i < hex.Length; i++)
            {
                if (i + 1 < hex.Length && hexCharList.Contains(hex[i]) && hexCharList.Contains(hex[i + 1]))
                {
                    b =(byte)(hexCharList.IndexOf(hex[i]) * 16 + hexCharList.IndexOf(hex[i + 1]));
                    i++;
                    Console.WriteLine(b);
                }
            }
            Console.ReadKey();*/



            /*
            string station_id = "D01-3FT-08_1_WirecombMolding";

            string Romc= station_id.Substring(station_id.IndexOf('_') + 1, station_id.LastIndexOf('_') - station_id.IndexOf('_') - 1);
            Console.WriteLine(Romc);
            Console.ReadKey();



            Random r = new Random();
            int a = r.Next(10,16);
            string aaa = a.ToString().PadLeft(3,'0');  //补零
            string bbb = a.ToString("F3");   //三位小数
            string ccc = a.ToString("D3");   //补零
            string ddd = a.ToString("C");    //货币
            string hhh = a.ToString("X2");   //大写的十六进制
            string iii = a.ToString("x2");   //小写的十六进制
            string eee = DateTime.Now.ToString("ddd");   //星期
            string fff = DateTime.Now.ToString("%d");    //每月几号
            string ggg = DateTime.Now.ToString("d");     //年月日
            Console.WriteLine("aaa " + aaa);
            Console.WriteLine("bbb " + bbb);
            Console.WriteLine("ccc " + ccc);
            Console.WriteLine("ddd " + ddd);
            Console.WriteLine("eee " + eee);
            Console.WriteLine("fff " + fff);
            Console.WriteLine("ggg " + ggg);
            Console.WriteLine("hhh " + hhh);
            Console.WriteLine("iii " + iii);


            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
        　　int weekOfYear = gc.GetWeekOfYear(DateTime.Now,System.Globalization.CalendarWeekRule.FirstDay,DayOfWeek.Monday);
            Console.WriteLine("weekOfYear " + weekOfYear);   //一年中的第几周
            Console.ReadKey();

            string dcno = "20230810";
            string str_Lot_time_Y = dcno.Substring(2, 2);
            string str_Lot_time_M = dcno.Substring(4, 2);
            string str_Lot_time_D = dcno.Substring(6, 2);
            Console.WriteLine("{0}{1}{2}",str_Lot_time_Y,str_Lot_time_M,str_Lot_time_D);
            Console.ReadKey();

            string timeAA = DateTime.Now.ToString("f");
            Console.WriteLine(timeAA);
            Console.ReadKey();

            string a = (170 & 0xf0).ToString();
            Console.WriteLine(a);
            Console.ReadKey();
            */

            /*DateTime dt = DateTime.Now;
            string dd = Convert.ToString(dt);
            // string dd = dt.ToString();
            Console.WriteLine(dd);
            Console.ReadKey();*/

            
            /*
            string path = "C:/Users/mh.guo/Desktop/d.txt";
            FileStream fs = new FileStream(path,FileMode.Append,FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("11223344556");
            sw.Close();

            StreamReader sr = File.OpenText("C:/Users/mh.guo/Desktop/d.txt");
            Console.Write(sr.ReadToEnd());
            sr.Close();
            Console.ReadKey();
            */

            /*string a = "FC934431US526GVBG;FC934441DX026GVBL";
            string[] StrNew = a.Split(new char[] { ';' });
            //string b =a.Substring(0,17);
            //string c=a.Substring(19,a.Length);

            //Console.WriteLine(a);
            Console.WriteLine(StrNew[0]);
            Console.WriteLine(StrNew[1]);
            Console.ReadKey();*/



            /*string portString = "127,18,30,86,1080,20";
            String[] tmp = portString.Split(',');
            String ipString = "" + tmp[0] + "." + tmp[1] + "." + tmp[2] + "." + tmp[3];
            int portNum = (int.Parse(tmp[4]) << 8) | int.Parse(tmp[5]);
            foreach(string i in tmp)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine(ipString);
            Console.WriteLine(portNum);
            Console.ReadKey();*/


            /*string uri="https://cn.bing.com/search?q=%e5%93%88%e5%b7%a5%e5%a4%a7%e5%8e%bb%e4%b8%8a%e8%af%be%e5%83%8f%e7%99%bb%e6%9c%ba&efirst=0&ecount=50&filters=tnTID%3a%22DSBOS_F165A309AEC34480925FB7937419ADDA%22+tnVersion%3a%22e72716ed340f45c8a8996e35666ed951%22+Segment%3a%22popularnow.carousel%22+tnCol%3a%220%22+tnOrder%3a%22698a4b86-22b5-4930-8ae8-3f921df3080c%22&form=HPNN01";
            //URI解码
            string data = System.Web.HttpUtility.UrlDecode(uri, System.Text.Encoding.UTF8);
            //URI编码
            string data = System.Web.HttpUtility.UrlEncode(uri, System.Text.Encoding.UTF8);
            Console.WriteLine(data);
            Console.ReadKey();*/

            /*string a = DateTime.Now.ToString("yyMMddhhmmss");
            Console.WriteLine(a);
            Console.ReadKey();*/

            /*string a = "tcp://172.18.32.153:8085";
            string bb = a.Split(new char[] { ':' })[1].Substring(2);
            Console.WriteLine(bb);
            Console.ReadKey();*/


            /*try
            {
                Uri uri = new Uri("http://localhost:64778/sfc_response.aspx");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/x-www-form-urlencoded";
                string body ="c=QUERY_HISTORY&p=Auto_SN&tsid=EC04C_F_01_Cable_Input&sn=EC04CTEST001";
                byte[] bt = Encoding.UTF8.GetBytes(body);
                request.ContentLength = bt.Length;
                Stream str = request.GetRequestStream();
                str.Write(bt, 0, bt.Length);
                str.Close();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream s = response.GetResponseStream();
                    StreamReader sr = new StreamReader(s, Encoding.UTF8);
                    string res = sr.ReadToEnd();
                    s.Close();
                    Console.WriteLine(res);
                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                }*/

                /*s/*tring bb = "0";
                string LotSum;
                bool aa = (int.Parse(bb == "" ? "0" : bb) > 0);
                if(bb !="")
                {
                    LotSum = bb;
                }
                else
                {
                    LotSum = "0";
                }
                Console.WriteLine(aa);*/

                /*try
                {
                    IPAddress ip = IPAddress.Parse("172.30.208.1");
                    IPEndPoint iep = new IPEndPoint(ip,55547);
                    TcpClient client = new TcpClient(iep);
                    Console.ReadKey();
                    client.Close();

                    Console.Write("虎莆胶在有在");
                    Console.ReadKey();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    }*/




            /*
            static void Main(string[] args)
            {
                var t1 = new Task(() => TaskMethod("Task 1"));
                var t2 = new Task(() => TaskMethod("Task 2"));
                t2.Start();
                t1.Start();
                Task.WaitAll(t1, t2);
                Task.Run(() => TaskMethod("Task 3"));
                Task.Factory.StartNew(() => TaskMethod("Task 4"));
    //标记为长时间运行任务,则任务不会使用线程池,而在单独的线程中运行。
                Task.Factory.StartNew(() => TaskMethod("Task 5"), TaskCreationOptions.LongRunning);

    #region 常规的使用方式
                Console.WriteLine("主线程执行业务处理.");
    //创建任务
                Task task = new Task(() =>
                {
                 Console.WriteLine("使用`System.Threading.Tasks.Task`执行异步操作.");
                 for (int i = 0; i < 10; i++)
                 {
                     Console.WriteLine(i);
                 }
                 });
    //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
                task.Start();
                Console.WriteLine("主线程执行其他处理");
                task.Wait();
    #endregion

                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.ReadLine();
            }

            static void TaskMethod(string name)
            {
                Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                  name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
                  }

                  }
}
}*/

            /*int a = 16/5;
            Console.WriteLine(a);
            Console.ReadKey();*/
            // StringBuilder res = new StringBuilder();
            // res.Append("\"abc\"");
            // string a = "\"abc\"";
            // Console.WriteLine(res.ToString());
            // Console.ReadKey();



            // Console.WriteLine("请输入{0}",argv[0]);
            /*Program g = new Program();
            g.show("a");
            Console.WriteLine("常量：{0}",X.PI);    //不用实例化也可以调用类的字段
            C2 cc = new C2();
            cc[0] = "aaa";
            cc[1] = "bbb";
            cc[2] = "ccc";
            Console.WriteLine("索引器：{0}",cc[1]);    //不用实例化也可以调用类的字段
            Console.ReadKey();*/
            /*C1 c = new C1();
            Console.WriteLine("theRealValue:{0}",c.theReal);
            c.myValue = 109;
            Console.WriteLine("myValue:{0}",c.myValue);
            c.aa = 12;
            Console.WriteLine("myValue:{0}",c.aa);
            Console.ReadKey();*/

            /*cal c = new cal();
            c.name = "aa";
            c.sex = "bb";
            Console.WriteLine("name:{0},sex:{1}",c.name,c.sex);
            Console.ReadKey();*/

            /*MyDerivedClass derived = new MyDerivedClass();   //调用派生类的方法
            MyBaseClass mybc = (MyBaseClass)derived;    //强制转换为基类，虚方法调用的只往上追溯一级调用定义的方法
            MyBaseClass bac = new MyBaseClass();    //调用基类的方法
            derived.Print();
            mybc.Print();
            bac.Print();
            Console.ReadKey();*/

            
            // var groupA = new[] {3,4,5,6};
            // var groupB = new[] {9,4,5,6,7};
            /*var someInts = from int a in groupA
                           from int b in groupB
                           let sum = a+b
                           where sum >=11 where a == 5
                           select new {a,b,sum};
            var someInts = from int a in groupB
                           orderby a descending
                           select a;*/

            /*var someInts = from a in groupA
                           join b in groupB on a equals b
                           into groupAandB
                           from c in groupAandB
                           select c;*/
            // int a = groupB.Last();
            // int a = groupB.Count(x => x > 5);
            // foreach(var a in someInts)
                // Console.WriteLine(a);

            // Console.ReadKey();


        /*void show(string a)
        {
            Console.WriteLine("请输入aaa");
        }
        void show(int a)
        {
            Console.WriteLine("请输入111");
            }*/
        // }

    /*class X
    {
        public const double PI = 3.1416;   //公共常量，表现是像静态值，在编译时替换，存储方式类似C语言的#define值
        
        public int aa
        {
            set {aa = value;}
            get{return aa;}
        }

        int bb{set;get;}
    }
    class C1
    {
        private int theRealValue = 10;
        private int MyValue;
        public int theReal
        {
            set {theRealValue = value;}
            get {return theRealValue;}
        }
        public int myValue
        {
            set {MyValue = value > 100 ? 100 : value;}
            get {return MyValue;}
        }
    }
    class C2
    {
        public string name;
        public string sex;
        public string firstName;
        public string this[int index]
        {
            set
            {
                switch(index)
                {
                    case 0: name = value; break;
                    case 1: sex = value; break;
                    case 2: firstName = value; break;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
            get
            {
                switch(index)
                {
                    case 0: return name;
                    case 1: return sex;
                    case 2: return firstName;
                    default: throw new ArgumentOutOfRangeException("index");
                }
            }
        }
        }*/

    /*partial class cal  //分部类
    {
        public string name;
    }
    partial class cal
    {
        public string sex;
        }*/

    /*class MyBaseClass
    {
        virtual public void Print()
        {
            Console.WriteLine("This is the base class.");
        }
    }
    class MyDerivedClass:MyBaseClass
    {
        override public void Print()
        {
            Console.WriteLine("This is the derived class.");
        }
        }*/


    /*var query = from s in students
                join t in student on s.stid equals t.stid
                where c.stdName == "张三" && t.age > 12
                select new {s.age,t.stdid};*/
//     }
// }