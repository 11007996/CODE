using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneDepth
{
    class WorkExperience:ICloneable    //类作为属性值继承克隆接口，
    {
        private string workDate;
        public string WorkDate
        {
            get { return workDate; }
            set { workDate = value; }
        }
        private string company;
        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        public Object Clone()
        {
            return (Object)this.MemberwiseClone();    //值类型克隆可以进行深复制
        }
    }
}
