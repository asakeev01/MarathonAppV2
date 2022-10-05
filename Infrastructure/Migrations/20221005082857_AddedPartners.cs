using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedPartners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "SavedFile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarathonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partner_Marathons_MarathonId",
                        column: x => x.MarathonId,
                        principalTable: "Marathons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedFile_PartnerId",
                table: "SavedFile",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_MarathonId",
                table: "Partner",
                column: "MarathonId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedFile_Partner_PartnerId",
                table: "SavedFile",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedFile_Partner_PartnerId",
                table: "SavedFile");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropIndex(
                name: "IX_SavedFile_PartnerId",
                table: "SavedFile");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "SavedFile");
        }
    }
}
