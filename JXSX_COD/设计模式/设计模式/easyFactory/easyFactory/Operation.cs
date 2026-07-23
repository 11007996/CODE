using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyFactory
{
    class Operation    //父类
    {
        private double _numberA = 0;
        private double _numberB = 0;   //私有字段，公共属性
        public double NumberA
        {
            get { return _numberA; }
            set {
                if (value < 0)
                {
                    Console.WriteLine("不能小于0,默认赋值为0");
                    //throw ("不能小于0");
                    _numberA = 0;
                } 
                else
                _numberA = value; }
            
        }

        public double NumberB
        {
            get { return _numberB; }
            set { _numberB = value; }
        }

        public virtual double GetResult()   //虚方法，必须有实现体，也就是必须带大括号，不一定要在子类中重写
        {
            double result = 0;
            return result;
        }
    }
}
