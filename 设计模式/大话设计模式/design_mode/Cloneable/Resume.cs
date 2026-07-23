using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloneable
{
    class Resume:ICloneable   //克隆接口
    {
        private string name;
        private string sex;
        private string age;
        private WorkExperience work;

        public Resume(string name)
        {
            this.name = name;
            work = new WorkExperience();
        }
        private Resume(WorkExperience work)
        {
            this.work = (WorkExperience)work.Clone();   //通过多态原型复制
        }

        public void SetPersonalInfo(string sex,string age)
        {
            this.sex = sex;
            this.age = age;
        }

        public void SetWorkExperience(string workDate,string company)
        {
            work.WorkDate = workDate;
            work.Company = company;
        }
        public void Display()
        {
            Console.WriteLine("{0} {1} {2}",name,sex,age);
            Console.WriteLine("工作经历：{0} {1}", work.WorkDate,work.Company);
        }

        public Object Clone()   //实现接口
        {
            Resume obj = new Resume(this.work);   //构造函数复制原型
            obj.name = this.name;    //深度复制引用类型
            obj.sex = this.sex;
            obj.age = this.age;
            return obj;    //object关箱
        }
    }
}
