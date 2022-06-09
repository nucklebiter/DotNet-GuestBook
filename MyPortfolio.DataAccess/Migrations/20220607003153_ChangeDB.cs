using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.DataAccess.Migrations
{
    public partial class ChangeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlienTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlienTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlienTypeCreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlienTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestTypeDisplayOrder = table.Column<int>(type: "int", nullable: false),
                    GuestTypeCreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestBookFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestBookLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestBookEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestBookDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestBookCreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuestTypeId = table.Column<int>(type: "int", nullable: false),
                    AlienTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestBooks_AlienTypes_AlienTypeId",
                        column: x => x.AlienTypeId,
                        principalTable: "AlienTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestBooks_GuestTypes_GuestTypeId",
                        column: x => x.GuestTypeId,
                        principalTable: "GuestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestBooks_AlienTypeId",
                table: "GuestBooks",
                column: "AlienTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestBooks_GuestTypeId",
                table: "GuestBooks",
                column: "GuestTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestBooks");

            migrationBuilder.DropTable(
                name: "AlienTypes");

            migrationBuilder.DropTable(
                name: "GuestTypes");
        }
    }
}
