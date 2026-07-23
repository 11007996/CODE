using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData.Models
{
    public class CallStatVo
    {

        public ChartXY<string,int> month { get; set; }
        public ChartXY<string, int> week { get; set; }
        
    }

    public class ChartXY<TX,TY> {
        public IList<TX> XData { get; set; }
        public IList<TY> YData { get; set; }

    }
}
