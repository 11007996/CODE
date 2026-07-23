using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Model
{
    public class AssetInfoDO
    {
        public string AssetNo { get; set; }
        public string AssetName { get; set; }
        public string AssetClass { get; set; }
        public string Model { get; set; }
        public DateTime EntryDate { get; set; }
        public string CostCenter { get; set; }
        public int DurableYear { get; set; }
        public int DurableMonth { get; set; }
        public string MadeFactory { get; set; }
        public List<FileInfoDO> FileInfo { get; set;}
    }
}
