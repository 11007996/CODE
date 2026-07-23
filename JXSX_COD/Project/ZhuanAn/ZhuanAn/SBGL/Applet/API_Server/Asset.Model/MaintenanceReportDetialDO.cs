using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Model
{
   public  class MaintenanceReportDetialDO
    {
       public int? Id { get; set; }

       public string AssetNo { get; set; }

       public int Year { get; set; }

       public string TimeMark { get; set; }

       //相对年标记
       public int TimeMarkStamp { get; set; }

       /// <summary>
       /// 保养的项目与值
       /// </summary>
       public List<MaintenanceItemDO> ItemValueDic { get; set; }

    }
}
