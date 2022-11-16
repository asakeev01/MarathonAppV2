using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class ApplicationForPWD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DistanceId",
                table: "Applications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DistanceAgeId",
                table: "Applications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DistanceForPWDId",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_DistanceForPWDId",
                table: "Applications",
                column: "DistanceForPWDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_DistanceForPWD_DistanceForPWDId",
                table: "Applications",
                column: "DistanceForPWDId",
                principalTable: "DistanceForPWD",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_DistanceForPWD_DistanceForPWDId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_DistanceForPWDId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "DistanceForPWDId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "DistanceId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistanceAgeId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
