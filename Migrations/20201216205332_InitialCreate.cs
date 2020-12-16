using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Ticaret.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(maxLength: 100, nullable: true),
                    Resim = table.Column<string>(maxLength: 250, nullable: true),
                    Fiyat = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrunKategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunId = table.Column<int>(nullable: false),
                    KategoriId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunKategoriler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrunKategoriler_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrunKategoriler_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrunKategoriler_UrunId",
                table: "UrunKategoriler",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunKategoriler_KategoriId_UrunId",
                table: "UrunKategoriler",
                columns: new[] { "KategoriId", "UrunId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrunKategoriler");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "Urunler");
        }
    }
}
