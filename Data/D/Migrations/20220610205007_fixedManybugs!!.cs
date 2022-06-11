using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesfo.Data.Migrations
{
    public partial class fixedManybugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSend",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSend",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsSend",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsSend",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
