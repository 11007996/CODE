using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Cons
{
    class Program
    {
        //private delegate int actDele(int a, int c);
        static void Main(string[] args)
        {
            /*
            Func<int, int,int> act = add;
            act += jian;
            act += chen;
            int aa =act(2, 1);
            Console.WriteLine(aa);
            
            foreach (var fun in act.GetInvocationList())
            {
                Console.WriteLine(fun.Method);
            }
            Console.ReadKey();
            */


            /*
            actDele act = new actDele(add);
            act += jian;
            act += chen;
            int aa = act(2, 1);
            Console.WriteLine(aa);

            foreach (var fun in act.GetInvocationList())
            {
                Console.WriteLine(fun.Method);
            }
             * */

            /*
            Timer t = new Timer();
            t.Interval = 2000;
            t.Elapsed += showtime;
            t.Start();
             * */




            publish p = new publish();
            dyC d = new dyC();
            p.handler += d.showdata;
            p.pubShow();
            Console.ReadKey();

        }


        static void showtime(object obj,ElapsedEventArgs e)
        {
            Console.WriteLine(DateTime.Now);
        }
        static int add(int a,int b)
        {
            Console.WriteLine("a + b=" + (a + b));
            return a + b;
        }
        static int jian(int a,int b)
        {
            Console.WriteLine("a - b=" + (a - b));
            return a - b;
        }
        static int chen (int a,int b)
        {
            Console.WriteLine("a * b=" + (a * b));
            return a * b;
        }
    }

    class myEventArgs : EventArgs    //EventArgs  预定义的参数类，用于自定义参数，事件可携带的参数
    {
        public string name{get;set;}
    }

    class publish
    {
        public EventHandler<myEventArgs> handler;    //EventHandler  预定义的无返回值的泛型委托，委托带两个参数，一个是触发事件的类，也是发布类，一个是EventArgs参数类，其本身不带参数，需以它为基类自定义参数类
        public void pubShow()
        {
            myEventArgs args = new myEventArgs();
            args.name = "zhou";
            EventHandler<myEventArgs> h = handler;
            if (h != null)
                h.Invoke(this, args);
        }
    }

    class dyC
    {
        public void showdata(object obj,myEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(e.name);
        }
    }
}
