using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarathonApp.Migrations
{
    public partial class Marathons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marathons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinishPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rules = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Awards = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marathons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Distance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    PassingLimit = table.Column<TimeSpan>(type: "time", nullable: false),
                    AgeFrom = table.Column<int>(type: "int", nullable: false),
                    MarathonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distance_Marathons_MarathonId",
                        column: x => x.MarathonId,
                        principalTable: "Marathons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MarathonPartner",
                columns: table => new
                {
                    MarathonsId = table.Column<int>(type: "int", nullable: false),
                    PartnersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarathonPartner", x => new { x.MarathonsId, x.PartnersId });
                    table.ForeignKey(
                        name: "FK_MarathonPartner_Marathons_MarathonsId",
                        column: x => x.MarathonsId,
                        principalTable: "Marathons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarathonPartner_Partners_PartnersId",
                        column: x => x.PartnersId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistanceAge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeFrom = table.Column<int>(type: "int", nullable: true),
                    AgeTo = table.Column<int>(type: "int", nullable: true),
                    DistanceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceAge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistanceAge_Distance_DistanceId",
                        column: x => x.DistanceId,
                        principalTable: "Distance",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DistancePrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DistanceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistancePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistancePrice_Distance_DistanceId",
                        column: x => x.DistanceId,
                        principalTable: "Distance",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Distance_MarathonId",
                table: "Distance",
                column: "MarathonId");

            migrationBuilder.CreateIndex(
                name: "IX_DistanceAge_DistanceId",
                table: "DistanceAge",
                column: "DistanceId");

            migrationBuilder.CreateIndex(
                name: "IX_DistancePrice_DistanceId",
                table: "DistancePrice",
                column: "DistanceId");

            migrationBuilder.CreateIndex(
                name: "IX_MarathonPartner_PartnersId",
                table: "MarathonPartner",
                column: "PartnersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistanceAge");

            migrationBuilder.DropTable(
                name: "DistancePrice");

            migrationBuilder.DropTable(
                name: "MarathonPartner");

            migrationBuilder.DropTable(
                name: "Distance");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Marathons");
        }
    }
}
