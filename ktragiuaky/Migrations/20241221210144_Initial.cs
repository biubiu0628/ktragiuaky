using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ktragiuaky.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HocPhans",
                columns: table => new
                {
                    MaHp = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenHp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhans", x => x.MaHp);
                });

            migrationBuilder.CreateTable(
                name: "NganhHocs",
                columns: table => new
                {
                    MaNganh = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenNganh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NganhHocs", x => x.MaNganh);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    MaSv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: true),
                    Hinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNganhNavigationMaNganh = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.MaSv);
                    table.ForeignKey(
                        name: "FK_SinhViens_NganhHocs_MaNganhNavigationMaNganh",
                        column: x => x.MaNganhNavigationMaNganh,
                        principalTable: "NganhHocs",
                        principalColumn: "MaNganh");
                });

            migrationBuilder.CreateTable(
                name: "DangKies",
                columns: table => new
                {
                    MaDk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayDk = table.Column<DateOnly>(type: "date", nullable: true),
                    MaSv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSvNavigationMaSv = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKies", x => x.MaDk);
                    table.ForeignKey(
                        name: "FK_DangKies_SinhViens_MaSvNavigationMaSv",
                        column: x => x.MaSvNavigationMaSv,
                        principalTable: "SinhViens",
                        principalColumn: "MaSv");
                });

            migrationBuilder.CreateTable(
                name: "DangKyHocPhan",
                columns: table => new
                {
                    MaDksMaDk = table.Column<int>(type: "int", nullable: false),
                    MaHpsMaHp = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKyHocPhan", x => new { x.MaDksMaDk, x.MaHpsMaHp });
                    table.ForeignKey(
                        name: "FK_DangKyHocPhan_DangKies_MaDksMaDk",
                        column: x => x.MaDksMaDk,
                        principalTable: "DangKies",
                        principalColumn: "MaDk",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DangKyHocPhan_HocPhans_MaHpsMaHp",
                        column: x => x.MaHpsMaHp,
                        principalTable: "HocPhans",
                        principalColumn: "MaHp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DangKies_MaSvNavigationMaSv",
                table: "DangKies",
                column: "MaSvNavigationMaSv");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyHocPhan_MaHpsMaHp",
                table: "DangKyHocPhan",
                column: "MaHpsMaHp");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_MaNganhNavigationMaNganh",
                table: "SinhViens",
                column: "MaNganhNavigationMaNganh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DangKyHocPhan");

            migrationBuilder.DropTable(
                name: "DangKies");

            migrationBuilder.DropTable(
                name: "HocPhans");

            migrationBuilder.DropTable(
                name: "SinhViens");

            migrationBuilder.DropTable(
                name: "NganhHocs");
        }
    }
}
