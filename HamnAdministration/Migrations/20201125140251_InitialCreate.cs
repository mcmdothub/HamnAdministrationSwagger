using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HamnAdministration.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BestallStore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestallStore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parkering",
                columns: table => new
                {
                    Namn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsOpened = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkering", x => x.Namn);
                });

            migrationBuilder.CreateTable(
                name: "ParkeringsPlatser",
                columns: table => new
                {
                    ParkeringsNamn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Siffra = table.Column<int>(type: "int", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkeringsPlatser", x => new { x.ParkeringsNamn, x.Siffra });
                    table.ForeignKey(
                        name: "FK_ParkeringsPlatser_Parkering_ParkeringsNamn",
                        column: x => x.ParkeringsNamn,
                        principalTable: "Parkering",
                        principalColumn: "Namn",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestallStore");

            migrationBuilder.DropTable(
                name: "ParkeringsPlatser");

            migrationBuilder.DropTable(
                name: "Parkering");
        }
    }
}
