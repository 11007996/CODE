using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.AssetSystem.Base
{
    internal class ApiResultMsg
    {
        public string msgCode { get; set; }
        public string msgInfo { get; set; }
        public object data { get; set; }
    }
}
