using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesfo.Data.Migrations
{
    public partial class CriateLable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Articleid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleLabels_Articles_Articleid",
                        column: x => x.Articleid,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLabel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLabel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLabel_Products_productid",
                        column: x => x.productid,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLabels_Articleid",
                table: "ArticleLabels",
                column: "Articleid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLabel_productid",
                table: "ProductLabel",
                column: "productid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleLabels");

            migrationBuilder.DropTable(
                name: "ProductLabel");
        }
    }
}
