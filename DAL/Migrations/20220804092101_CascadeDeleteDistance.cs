using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class CascadeDeleteDistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistanceAge_Distance_DistanceId",
                table: "DistanceAge");

            migrationBuilder.DropForeignKey(
                name: "FK_DistancePrice_Distance_DistanceId",
                table: "DistancePrice");

            migrationBuilder.AddForeignKey(
                name: "FK_DistanceAge_Distance_DistanceId",
                table: "DistanceAge",
                column: "DistanceId",
                principalTable: "Distance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DistancePrice_Distance_DistanceId",
                table: "DistancePrice",
                column: "DistanceId",
                principalTable: "Distance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistanceAge_Distance_DistanceId",
                table: "DistanceAge");

            migrationBuilder.DropForeignKey(
                name: "FK_DistancePrice_Distance_DistanceId",
                table: "DistancePrice");

            migrationBuilder.AddForeignKey(
                name: "FK_DistanceAge_Distance_DistanceId",
                table: "DistanceAge",
                column: "DistanceId",
                principalTable: "Distance",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DistancePrice_Distance_DistanceId",
                table: "DistancePrice",
                column: "DistanceId",
                principalTable: "Distance",
                principalColumn: "Id");
        }
    }
}
