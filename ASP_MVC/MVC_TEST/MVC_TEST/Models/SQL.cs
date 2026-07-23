namespace MVC_TEST.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SQL : DbContext
    {
        public SQL()
            : base("name=SQL")
        {
        }

        public virtual DbSet<tb_User> tb_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tb_User>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<tb_User>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<tb_User>()
                .Property(e => e.Usey)
                .IsUnicode(false);

            modelBuilder.Entity<tb_User>()
                .Property(e => e.Intime)
                .IsUnicode(false);
        }
    }
}
