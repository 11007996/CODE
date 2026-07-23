using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTools.Util
{
    //线程进度消息类
    public class ProgressMsg
    {
        public int WriteSpeed { get; set; }
        public int ReadSpeed { get; set; }
        public string Text { get; set; }
        public bool Completed { get; set; }
    }
}
