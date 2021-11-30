using Microsoft.EntityFrameworkCore.Migrations;

namespace WearHouse_WebApp.Migrations
{
    public partial class BuilderUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbComments_dbWearables_dbWearableWearableId",
                table: "dbComments");

            migrationBuilder.DropIndex(
                name: "IX_dbComments_dbWearableWearableId",
                table: "dbComments");

            migrationBuilder.DropColumn(
                name: "dbWearableWearableId",
                table: "dbComments");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "dbComments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_dbComments_userId",
                table: "dbComments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_dbComments_WearableId",
                table: "dbComments",
                column: "WearableId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbComments_AspNetUsers_userId",
                table: "dbComments",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbComments_dbWearables_WearableId",
                table: "dbComments",
                column: "WearableId",
                principalTable: "dbWearables",
                principalColumn: "WearableId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbComments_AspNetUsers_userId",
                table: "dbComments");

            migrationBuilder.DropForeignKey(
                name: "FK_dbComments_dbWearables_WearableId",
                table: "dbComments");

            migrationBuilder.DropIndex(
                name: "IX_dbComments_userId",
                table: "dbComments");

            migrationBuilder.DropIndex(
                name: "IX_dbComments_WearableId",
                table: "dbComments");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "dbComments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dbWearableWearableId",
                table: "dbComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_dbComments_dbWearableWearableId",
                table: "dbComments",
                column: "dbWearableWearableId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbComments_dbWearables_dbWearableWearableId",
                table: "dbComments",
                column: "dbWearableWearableId",
                principalTable: "dbWearables",
                principalColumn: "WearableId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
