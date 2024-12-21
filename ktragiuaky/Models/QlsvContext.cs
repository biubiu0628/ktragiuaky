using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ktragiuaky.Models;

public partial class QlsvContext : DbContext
{
    public QlsvContext()
    {
    }

    public QlsvContext(DbContextOptions<QlsvContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DangKy> DangKy { get; set; }

    public virtual DbSet<HocPhan> HocPhan { get; set; }

    public virtual DbSet<NganhHoc> NganhHoc { get; set; }

    public virtual DbSet<SinhVien> SinhVien { get; set; }

    public virtual DbSet<ChiTietDangKy> ChiTietDangKy { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DangKy>(entity =>
        {
            entity.HasKey(e => e.MaDk).HasName("PK__DangKy__2725866CA48B7CB3");

            entity.ToTable("DangKy");

            entity.Property(e => e.MaDk).HasColumnName("MaDK");
            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSV");
            entity.Property(e => e.NgayDk).HasColumnName("NgayDK");

            entity.HasOne(d => d.MaSvNavigation)
                .WithMany(p => p.DangKies)
                .HasForeignKey(d => d.MaSv)
                .HasConstraintName("FK__DangKy__MaSV__2B3F6F97");
        });

        modelBuilder.Entity<ChiTietDangKy>().HasKey(cd => new { cd.MaDk, cd.MaHp });

        modelBuilder.Entity<ChiTietDangKy>()
            .HasOne(cd => cd.DangKy) 
            .WithMany(d => d.ChiTietDangKies)
            .HasForeignKey(cd => cd.MaDk);

        modelBuilder.Entity<ChiTietDangKy>()
            .HasOne(cd => cd.HocPhan)
            .WithMany(h => h.ChiTietDangKies)
            .HasForeignKey(cd => cd.MaHp);

        modelBuilder.Entity<HocPhan>(entity =>
        {
            entity.HasKey(e => e.MaHp).HasName("PK__HocPhan__2725A6ECFFD62344");

            entity.ToTable("HocPhan");

            entity.Property(e => e.MaHp)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHP");
            entity.Property(e => e.TenHp)
                .HasMaxLength(30)
                .HasColumnName("TenHP");
        });

        modelBuilder.Entity<NganhHoc>(entity =>
        {
            entity.HasKey(e => e.MaNganh).HasName("PK__NganhHoc__A2CEF50D45F53F09");

            entity.ToTable("NganhHoc");

            entity.Property(e => e.MaNganh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNganh).HasMaxLength(30);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.MaSv).HasName("PK__SinhVien__2725081A2DADFD80");

            entity.ToTable("SinhVien");

            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSV");

            entity.Property(e => e.GioiTinh).HasMaxLength(5);

            entity.Property(e => e.Hinh)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.HoTen).HasMaxLength(50);

            entity.Property(e => e.MaNganh)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaNganhNavigation)
                .WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.MaNganh)
                .HasConstraintName("FK__SinhVien__MaNgan__267ABA7A");
        });
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
