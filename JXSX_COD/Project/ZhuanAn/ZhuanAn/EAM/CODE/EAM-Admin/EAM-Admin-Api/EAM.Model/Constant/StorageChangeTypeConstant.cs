using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAM.Model.Constant
{
    /// <summary>
    /// 库存变动类型常量
    /// </summary>
    public class StorageChangeTypeConstant
    {
        public const string 入库 = "In";
        public const string 出库 = "Out";
        public const string 领用 = "Receive";
        public const string 归还 = "Back";
        public const string 报废 = "Scrapped";
        public const string 转移 = "Transfer";
    }
}
