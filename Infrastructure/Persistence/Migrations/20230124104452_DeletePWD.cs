using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class DeletePWD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_DistanceForPWD_DistanceForPWDId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "DistanceForPWD");

            migrationBuilder.DropIndex(
                name: "IX_Applications_DistanceForPWDId",
                table: "Applications");

            migrationBuilder.AddColumn<bool>(
                name: "IsPWD",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPWD",
                table: "Applications");

            migrationBuilder.CreateTable(
                name: "DistanceForPWD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarathonId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredParticipants = table.Column<int>(type: "int", nullable: false),
                    StartNumbersFrom = table.Column<int>(type: "int", nullable: false),
                    StartNumbersTo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceForPWD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistanceForPWD_Marathons_MarathonId",
                        column: x => x.MarathonId,
                        principalTable: "Marathons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_DistanceForPWDId",
                table: "Applications",
                column: "DistanceForPWDId");

            migrationBuilder.CreateIndex(
                name: "IX_DistanceForPWD_MarathonId",
                table: "DistanceForPWD",
                column: "MarathonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_DistanceForPWD_DistanceForPWDId",
                table: "Applications",
                column: "DistanceForPWDId",
                principalTable: "DistanceForPWD",
                principalColumn: "Id");
        }
    }
}
