using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangedDistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distances_Marathons_MarathonsId",
                table: "Distances");

            migrationBuilder.DropColumn(
                name: "DistanceId",
                table: "Distances");

            migrationBuilder.RenameColumn(
                name: "MarathonsId",
                table: "Distances",
                newName: "MarathonId");

            migrationBuilder.RenameIndex(
                name: "IX_Distances_MarathonsId",
                table: "Distances",
                newName: "IX_Distances_MarathonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Distances_Marathons_MarathonId",
                table: "Distances",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distances_Marathons_MarathonId",
                table: "Distances");

            migrationBuilder.RenameColumn(
                name: "MarathonId",
                table: "Distances",
                newName: "MarathonsId");

            migrationBuilder.RenameIndex(
                name: "IX_Distances_MarathonId",
                table: "Distances",
                newName: "IX_Distances_MarathonsId");

            migrationBuilder.AddColumn<int>(
                name: "DistanceId",
                table: "Distances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Distances_Marathons_MarathonsId",
                table: "Distances",
                column: "MarathonsId",
                principalTable: "Marathons",
                principalColumn: "Id");
        }
    }
}
