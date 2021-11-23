using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WearHouse_WebApp.Migrations
{
    public partial class createdComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dbComments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WearableId = table.Column<int>(type: "int", nullable: false),
                    dbWearableWearableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbComments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_dbComments_dbWearables_dbWearableWearableId",
                        column: x => x.dbWearableWearableId,
                        principalTable: "dbWearables",
                        principalColumn: "WearableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dbComments_dbWearableWearableId",
                table: "dbComments",
                column: "dbWearableWearableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbComments");
        }
    }
}
