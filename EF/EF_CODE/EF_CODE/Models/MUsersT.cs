using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EF_CODE.Models
{
    public partial class MUsersT
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string PwdofRePrint { get; set; }
        public string Descript { get; set; }
        public string Dept { get; set; }
        public string Team { get; set; }
        public string Jobs { get; set; }
        public string GroupId { get; set; }
        public string UserGrade { get; set; }
        public string FactoryId { get; set; }
        public string Usey { get; set; }
        public string AutoId { get; set; }
        public string Omd { get; set; }
        public string Creater { get; set; }
        public DateTime Intime { get; set; }
        public string Lineid { get; set; }
        public int? FingerMask { get; set; }
        public int? Verifytype { get; set; }
        public int? ShowAdv { get; set; }
        public int? AdvId { get; set; }
        public DateTime? Advtime { get; set; }
        public string Email { get; set; }
        public int? StyleId { get; set; }
        public string IsOutUser { get; set; }
    }
}
