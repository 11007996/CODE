using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DelegateDemo
{
    //在使用EventSet类时，作为Key使用。
    public sealed class EventKey { }

    public sealed class EventSet {
        //该字典用户维护 EventKey -> Delegate 的映射
        private readonly Dictionary<EventKey, Delegate> m_events = new Dictionary<EventKey, Delegate>();

        //添加 EventKey -> Delegate 的映射(如果不存在)
        //将新委托合并到旧委托中去(如果已经存在该EventKey的映射)
        public void Add(EventKey eventKey, Delegate handler) {
            Monitor.Enter(m_events);
            Delegate d;
            m_events.TryGetValue(eventKey,out d);
            m_events[eventKey] = Delegate.Combine(d,handler);
            Monitor.Exit(m_events);
        }

        //从eventKey映射的Delegate中删除hanlder委托
        //在删除最后一个委托后，同时删除 eventKey -> Delegate的映射
        public void remove(EventKey eventKey, Delegate handler) {
            Monitor.Enter(m_events);
            Delegate d;
            if (m_events.TryGetValue(eventKey, out d)) {
                d = Delegate.Remove(d,handler);

                if (d == null) { //没有委托了
                    m_events.Remove(eventKey);
                }
            }
            Monitor.Exit(m_events);
        }

        //为指定eventKey映射的委托触发
        public void Raise(EventKey eventKey,Object sender,EventArgs e) {
            Monitor.Enter(m_events);
            Delegate d;
            m_events.TryGetValue(eventKey,out d);
            Monitor.Exit(m_events);
            if (d != null) {
                //以对象数组的形式传递参数，如果参数不匹配DynamicInvoke会抛出异常。
                d.DynamicInvoke(sender,e);
            }
        }
    }
	
	
	class FooEventArgs : EventArgs {
        
    }
    class TypeWithLotsOfEvents {

        private readonly EventSet m_eventSet = new EventSet();
        protected static readonly EventKey s_fooEventKey = new EventKey();


        //使派生类也能够访问
        protected EventSet EventSet
        {
            get{return m_eventSet;}
        }


        //定义事件访问器
        public event EventHandler<FooEventArgs> Foo {
            add { m_eventSet.Add(s_fooEventKey,value); }
            remove { m_eventSet.remove(s_fooEventKey, value); }
        }

        //定义触发事件的受保护的虚方法
        protected virtual void OnFoo(FooEventArgs e) {
            m_eventSet.Raise(s_fooEventKey,this,e);
        }

        //定义将输入转化为这个事件的方法
        public void SimulateFoo() {
            OnFoo(new FooEventArgs());
        }
    }
	
	
	public sealed class Program {
        static void Main(String[] args) {
            TypeWithLotsOfEvents typeWithLotsOfEvents = new TypeWithLotsOfEvents();
            typeWithLotsOfEvents.Foo += HandlerFooEvent;

            typeWithLotsOfEvents.SimulateFoo();
        }
       static  void HandlerFooEvent(Object obj, FooEventArgs e) {
           Console.WriteLine("here arrived ...");
        }
    }
}




