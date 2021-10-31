using Microsoft.EntityFrameworkCore.Migrations;

namespace WearHouse_WebApp.Data.Migrations
{
    public partial class AddingusernameFKtowearable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Wearables",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Wearables");
        }
    }
}
