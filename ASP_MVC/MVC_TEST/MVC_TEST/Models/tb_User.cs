namespace MVC_TEST.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_User
    {
        [Key]
        [StringLength(50)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        [StringLength(50)]
        public string Usey { get; set; }

        [StringLength(50)]
        public string Intime { get; set; }

        [StringLength(50)]
        public string job { get; set; }

        [StringLength(50)]
        public string depID { get; set; }
    }
}
