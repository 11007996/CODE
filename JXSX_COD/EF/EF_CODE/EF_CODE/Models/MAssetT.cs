using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EF_CODE.Models
{
    public partial class MAssetT
    {
        public int Id { get; set; }
        public string AssetNo { get; set; }
        public string AssetName { get; set; }
        public string Model { get; set; }
        public int? Status { get; set; }
        public string Cstatus { get; set; }
        public int? Qty { get; set; }
        public string KeeperId { get; set; }
        public string KeeperName { get; set; }
        public string Storage { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUserId { get; set; }
        public string FactoryName { get; set; }
        public string Profitcenter { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string TempLocation { get; set; }
        public string UseY { get; set; }
        public string Affiliation { get; set; }
    }
}
