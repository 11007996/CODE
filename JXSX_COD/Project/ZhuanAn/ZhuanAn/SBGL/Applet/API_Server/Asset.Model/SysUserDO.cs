using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asset.Model
{
    public class SysUserDO
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Dept { get; set; }
        public string UserRight { get; set; }
    }
}