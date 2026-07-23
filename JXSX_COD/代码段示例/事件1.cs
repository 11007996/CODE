using System;
namespace SimpleEvent
{
  // using System;
  /***********发布器类***********/
  public class EventTest
  {
    private int value;

    public delegate void NumManipulationHandler();        //声明一个委托
    public event NumManipulationHandler ChangeNum;        //声明一个事件
    
    protected virtual void OnNumChanged()
    {
      if ( ChangeNum != null )          //检测事件是否被订阅
      {
        ChangeNum(); /* 事件被触发 */
      }else {
        Console.WriteLine( "event not fire" );
        Console.ReadKey(); /* 回车继续 */
      }
    }


    public EventTest()
    {
      int n = 5;
      SetValue( n );
    }


    public void SetValue( int n )
    {
      if ( value != n )
      {
        value = n;
        OnNumChanged();
      }
    }
  }


  /***********订阅器类***********/

  public class subscribEvent
  {
    public void printf()
    {
      Console.WriteLine( "event fire" );
      Console.ReadKey(); /* 回车继续 */
    }
  }

  /***********触发***********/
  public class MainClass
  {
    public static void Main()
    {
      EventTest e = new EventTest(); /* 实例化对象,第一次没有触发事件 */
      subscribEvent v = new subscribEvent(); /* 实例化对象 */
      e.ChangeNum += new EventTest.NumManipulationHandler( v.printf ); /* 注册事件发生程序 */
      e.SetValue( 13 );
      e.SetValue( 11 );
    }
  }
}





/*using System;
namespace pppp
{
  class Program
  {
    public delegate void deaaa(int a,int b);
    public event deaaa Eaaa;
    public void onbb(int a,int b)
    {
      if(Eaaa != null)
    {
      Eaaa(a,b);

    }
    else
    {
      Console.WriteLine("错误");

    }
    }
    
    // public void bb(int a,int b)
    // {
    //   onbb(a,b);
    // }

    public void aad(int a,int b)
    {
      int c = a*b;
      Console.WriteLine(c);
    }

  }
  class pro
  {
    public void sum(int a,int b)
    {
      int c = a + b;
      Console.WriteLine(c);
    }
  }

  class pp
  {
    static void Main()
    {
      Program p = new Program();
      pro pr = new pro();
      p.Eaaa += new Program.deaaa(pr.sum);
      p.onbb(5,2);
      Console.ReadKey();

    }
  }
}*/