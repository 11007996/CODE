using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton_1
{
    class Singleton
    {
        //懒汉模式要在第一次被引用时，才会将自己实例化
        private static Singleton instance;
        private static readonly object syncRoot = new object();
        private Singleton()
        {

        }
        public static Singleton GetInstance()
        {
            if (instance == null)
            {
                lock(syncRoot)
                {
                    if(instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
}
