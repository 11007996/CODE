using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EF_CODE.Models
{
    public partial class MESDBContext : DbContext
    {
        public MESDBContext()
        {
        }

        public MESDBContext(DbContextOptions<MESDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MAssetT> MAssetT { get; set; }
        public virtual DbSet<MUsersT> MUsersT { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=172.18.32.205;Initial Catalog=MESDB;User ID=sfcuser;Password=sfcuser!@#'songyy");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MAssetT>(entity =>
            {
                entity.ToTable("m_Asset_t");

                entity.HasComment("资产表");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("ID");

                entity.Property(e => e.Affiliation).HasMaxLength(50);

                entity.Property(e => e.AssetName)
                    .HasMaxLength(100)
                    .HasComment("资产名称");

                entity.Property(e => e.AssetNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("资产编码");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CreateUserID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("创建人");

                entity.Property(e => e.Cstatus)
                    .HasColumnName("CStatus")
                    .HasMaxLength(50);

                entity.Property(e => e.FactoryName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("厂区");

                entity.Property(e => e.KeeperId)
                    .HasColumnName("KeeperID")
                    .HasMaxLength(50)
                    .HasComment("保管人");

                entity.Property(e => e.KeeperName)
                    .HasMaxLength(50)
                    .HasComment("保管姓名");

                entity.Property(e => e.Model)
                    .HasMaxLength(500)
                    .HasComment("规格");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId)
                    .HasColumnName("ModifyUserID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("修改人");

                entity.Property(e => e.Profitcenter)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("利润中心");

                entity.Property(e => e.Qty).HasComment("数量");

                entity.Property(e => e.Status).HasComment("状态");

                entity.Property(e => e.Storage)
                    .HasMaxLength(500)
                    .HasComment("存储位置");

                entity.Property(e => e.TempLocation)
                    .HasMaxLength(200)
                    .HasComment("临时位置");

                entity.Property(e => e.UseY)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("是否生效");
            });

            modelBuilder.Entity<MUsersT>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("m_Users_t");

                entity.HasComment("用户表");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("用户工号");

                entity.Property(e => e.AdvId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Advtime)
                    .HasColumnName("advtime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AutoId)
                    .IsRequired()
                    .HasColumnName("AutoID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("自动编号");

                entity.Property(e => e.Creater)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("创建人");

                entity.Property(e => e.Dept)
                    .HasMaxLength(50)
                    .HasComment("部门");

                entity.Property(e => e.Descript)
                    .HasMaxLength(150)
                    .HasComment("备注");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FactoryId)
                    .IsRequired()
                    .HasColumnName("FactoryID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("营运中心");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasMaxLength(50)
                    .HasComment("用户群组");

                entity.Property(e => e.Intime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.IsOutUser)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.Jobs)
                    .HasMaxLength(50)
                    .HasComment("用户职称");

                entity.Property(e => e.Lineid)
                    .HasColumnName("lineid")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("线别");

                entity.Property(e => e.Omd)
                    .HasColumnName("OMd")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("用户密码");

                entity.Property(e => e.PwdofRePrint)
                    .HasColumnName("PWDOfRePrint")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((111111))");

                entity.Property(e => e.ShowAdv).HasDefaultValueSql("((0))");

                entity.Property(e => e.StyleId).HasColumnName("StyleID");

                entity.Property(e => e.Team)
                    .HasMaxLength(50)
                    .HasComment("部门组别");

                entity.Property(e => e.UserGrade)
                    .HasMaxLength(50)
                    .HasComment("用户级别");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasComment("用户姓名");

                entity.Property(e => e.Usey)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasComment("是否有效");

                entity.Property(e => e.Verifytype).HasComment("验证方式");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
