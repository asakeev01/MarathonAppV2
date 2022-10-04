using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Marathons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SavedFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marathons_LogoId",
                table: "Marathons",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marathons_SavedFile_LogoId",
                table: "Marathons",
                column: "LogoId",
                principalTable: "SavedFile",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marathons_SavedFile_LogoId",
                table: "Marathons");

            migrationBuilder.DropTable(
                name: "SavedFile");

            migrationBuilder.DropIndex(
                name: "IX_Marathons_LogoId",
                table: "Marathons");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Marathons");
        }
    }
}
