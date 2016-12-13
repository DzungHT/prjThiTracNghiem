namespace prjThiTracNghiem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        public virtual DbSet<BaiThi> BaiThis { get; set; }
        public virtual DbSet<CauHoi> CauHois { get; set; }
        public virtual DbSet<CauHoiBaiThi> CauHoiBaiThis { get; set; }
        public virtual DbSet<Chuong> Chuongs { get; set; }
        public virtual DbSet<DapAnCauHoi> DapAnCauHois { get; set; }
        public virtual DbSet<DeThi> DeThis { get; set; }
        public virtual DbSet<DoKho> DoKhoes { get; set; }
        public virtual DbSet<DotThi> DotThis { get; set; }
        public virtual DbSet<GiaoVien> GiaoViens { get; set; }
        public virtual DbSet<HocPhan> HocPhans { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiThi>()
                .Property(e => e.NgayLamBai)
                .IsUnicode(false);

            modelBuilder.Entity<BaiThi>()
                .HasMany(e => e.CauHoiBaiThis)
                .WithRequired(e => e.BaiThi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BaiThi>()
                .HasMany(e => e.DapAnCauHois)
                .WithMany(e => e.BaiThis)
                .Map(m => m.ToTable("TraLoiBaiThi").MapLeftKey("BaiThiID").MapRightKey("DapAnCauHoiID"));

            modelBuilder.Entity<CauHoi>()
                .HasMany(e => e.CauHoiBaiThis)
                .WithRequired(e => e.CauHoi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CauHoi>()
                .HasMany(e => e.DeThis)
                .WithMany(e => e.CauHois)
                .Map(m => m.ToTable("CauHoiDeThi").MapLeftKey("CauhoiID").MapRightKey("DeThiID"));

            modelBuilder.Entity<Chuong>()
                .Property(e => e.TenChuong)
                .IsUnicode(false);

            modelBuilder.Entity<DeThi>()
                .Property(e => e.MaDeThi)
                .IsFixedLength();

            modelBuilder.Entity<GiaoVien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<HocPhan>()
                .Property(e => e.TenHocPhan)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.NgaySinh)
                .IsFixedLength();

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.DiaChi)
                .IsFixedLength();

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.KhoaHoc)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.GiaoViens)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.SinhViens)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);
        }
    }
}
