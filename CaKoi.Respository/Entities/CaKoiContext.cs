using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CaKoi.Respository.Entities;

public partial class CaKoiContext : DbContext
{
    public CaKoiContext()
    {
    }

    public CaKoiContext(DbContextOptions<CaKoiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CaCoi> CaCois { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DANGKHOA;Initial Catalog=CaKoi;Persist Security Info=True;User ID=sa;Password=Team@123;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CaCoi>(entity =>
        {
            entity.HasKey(e => e.IdcaKoi).HasName("PK__CaCoi__F6C3BA0D48087AFC");

            entity.ToTable("CaCoi");

            entity.Property(e => e.IdcaKoi)
                .ValueGeneratedNever()
                .HasColumnName("IDCaKoi");
            entity.Property(e => e.Gia).HasDefaultValue(1000);
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idloai).HasColumnName("IDLoai");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tuoi)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.Idcv).HasName("PK__ChucVu__B87D808B03084F47");

            entity.ToTable("ChucVu");

            entity.Property(e => e.Idcv)
                .ValueGeneratedNever()
                .HasColumnName("IDCV");
            entity.Property(e => e.TenChucVu)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.IddonHang);

            entity.ToTable("DonHang");

            entity.Property(e => e.IddonHang).HasColumnName("IDDonHang");
            entity.Property(e => e.ChoDuyet)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Idkh).HasColumnName("IDKH");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DonHangChiTiet>(entity =>
        {
            entity.HasKey(e => e.IddonHang);

            entity.ToTable("DonHangChiTiet");

            entity.Property(e => e.IddonHang).HasColumnName("IDDonHang");
            entity.Property(e => e.IdcaKoi).HasColumnName("IDCaKoi");
            entity.Property(e => e.Idkh).HasColumnName("IDKH");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Idkh).HasName("PK__KhachHan__B87DC1A7CE73D971");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.TenTaiKhoan, "UQ_KhachHang").IsUnique();

            entity.Property(e => e.Idkh).HasColumnName("IDKH");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Hoten)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HOTEN");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ngaysinh).HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.TenTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.Idnv).HasName("PK__NhanVien__B87DC9B2BB937E00");

            entity.ToTable("NhanVien");

            entity.Property(e => e.Idnv)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("IDNV");
            entity.Property(e => e.ChucVu)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Hoten)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HOTEN");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ngaysinh).HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.TenTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
