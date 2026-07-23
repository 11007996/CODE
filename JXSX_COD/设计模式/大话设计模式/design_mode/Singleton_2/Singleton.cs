using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton_2
{
    class Singleton
    {
        //静态初始化的方式是在自己被加载时就将自己实例化，所以被形象地称为饿汉单例类
        private static readonly Singleton instance = new Singleton();
        private Singleton() { }
        public static Singleton  GetInstance()
        {
            return instance;
        }
    }
}
