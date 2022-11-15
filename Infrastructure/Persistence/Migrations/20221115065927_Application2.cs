using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class Application2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    StarterKit = table.Column<bool>(type: "bit", nullable: false),
                    Payment = table.Column<int>(type: "int", nullable: false),
                    DistanceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    MarathonId = table.Column<int>(type: "int", nullable: false),
                    PromocodeId = table.Column<int>(type: "int", nullable: true),
                    DistanceAgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Applications_DistanceAges_DistanceAgeId",
                        column: x => x.DistanceAgeId,
                        principalTable: "DistanceAges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Applications_Distances_DistanceId",
                        column: x => x.DistanceId,
                        principalTable: "Distances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Applications_Marathons_MarathonId",
                        column: x => x.MarathonId,
                        principalTable: "Marathons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Applications_Promocodes_PromocodeId",
                        column: x => x.PromocodeId,
                        principalTable: "Promocodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_DistanceAgeId",
                table: "Applications",
                column: "DistanceAgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_DistanceId",
                table: "Applications",
                column: "DistanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_MarathonId",
                table: "Applications",
                column: "MarathonId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PromocodeId",
                table: "Applications",
                column: "PromocodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
