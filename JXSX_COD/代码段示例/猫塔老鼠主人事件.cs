using System;
using System.Text;
namespace DelegateDemo
{
    //定义猫叫委托
    public delegate void CatCallEventHandler();
    public class Cat
    {
        //定义猫叫事件
        public event CatCallEventHandler CatCall;

        public void OnCatCall()
        {
            Console.WriteLine("猫叫了一声");
            CatCall();
        }
    }
    public class Mouse
    {
        public event CatCallEventHandler mouse;
        //定义老鼠跑掉方法
        public void MouseRun()
        {
            Console.WriteLine("老鼠跑了");
            mouse();
        }
    }
    public class People
    {
        //定义主人醒来方法
        public void WakeUp()
        {
            Console.WriteLine("主人醒了");
        }
    }
	
	public class suxing
    {
        //定义主人醒来方法
        public void sux()
        {
            Console.WriteLine("属性测试");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Cat cat = new Cat();
            Mouse m = new Mouse();
            People p = new People();

            //关联绑定 
            cat.CatCall += new CatCallEventHandler(m.MouseRun);
            // cat.CatCall += new CatCallEventHandler(p.WakeUp);
            m.mouse += new CatCallEventHandler(p.WakeUp);
            cat.OnCatCall();

            Console.ReadKey();
        }
    }
}