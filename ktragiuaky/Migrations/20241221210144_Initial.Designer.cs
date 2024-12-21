﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ktragiuaky.Models;

#nullable disable

namespace ktragiuaky.Migrations
{
    [DbContext(typeof(QlsvContext))]
    [Migration("20241221210144_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DangKyHocPhan", b =>
                {
                    b.Property<int>("MaDksMaDk")
                        .HasColumnType("int");

                    b.Property<string>("MaHpsMaHp")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MaDksMaDk", "MaHpsMaHp");

                    b.HasIndex("MaHpsMaHp");

                    b.ToTable("DangKyHocPhan");
                });

            modelBuilder.Entity("ktragiuaky.Models.DangKy", b =>
                {
                    b.Property<int>("MaDk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDk"));

                    b.Property<string>("MaSv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaSvNavigationMaSv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("NgayDk")
                        .HasColumnType("date");

                    b.HasKey("MaDk");

                    b.HasIndex("MaSvNavigationMaSv");

                    b.ToTable("DangKies");
                });

            modelBuilder.Entity("ktragiuaky.Models.HocPhan", b =>
                {
                    b.Property<string>("MaHp")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("SoTinChi")
                        .HasColumnType("int");

                    b.Property<string>("TenHp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaHp");

                    b.ToTable("HocPhans");
                });

            modelBuilder.Entity("ktragiuaky.Models.NganhHoc", b =>
                {
                    b.Property<string>("MaNganh")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenNganh")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaNganh");

                    b.ToTable("NganhHocs");
                });

            modelBuilder.Entity("ktragiuaky.Models.SinhVien", b =>
                {
                    b.Property<string>("MaSv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GioiTinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaNganh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaNganhNavigationMaNganh")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("NgaySinh")
                        .HasColumnType("date");

                    b.HasKey("MaSv");

                    b.HasIndex("MaNganhNavigationMaNganh");

                    b.ToTable("SinhViens");
                });

            modelBuilder.Entity("DangKyHocPhan", b =>
                {
                    b.HasOne("ktragiuaky.Models.DangKy", null)
                        .WithMany()
                        .HasForeignKey("MaDksMaDk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ktragiuaky.Models.HocPhan", null)
                        .WithMany()
                        .HasForeignKey("MaHpsMaHp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ktragiuaky.Models.DangKy", b =>
                {
                    b.HasOne("ktragiuaky.Models.SinhVien", "MaSvNavigation")
                        .WithMany("DangKies")
                        .HasForeignKey("MaSvNavigationMaSv");

                    b.Navigation("MaSvNavigation");
                });

            modelBuilder.Entity("ktragiuaky.Models.SinhVien", b =>
                {
                    b.HasOne("ktragiuaky.Models.NganhHoc", "MaNganhNavigation")
                        .WithMany("SinhViens")
                        .HasForeignKey("MaNganhNavigationMaNganh");

                    b.Navigation("MaNganhNavigation");
                });

            modelBuilder.Entity("ktragiuaky.Models.NganhHoc", b =>
                {
                    b.Navigation("SinhViens");
                });

            modelBuilder.Entity("ktragiuaky.Models.SinhVien", b =>
                {
                    b.Navigation("DangKies");
                });
#pragma warning restore 612, 618
        }
    }
}