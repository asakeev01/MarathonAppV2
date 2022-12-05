using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddedCompanyToPartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedFile_Partner_PartnerId",
                table: "SavedFile");

            migrationBuilder.DropIndex(
                name: "IX_SavedFile_PartnerId",
                table: "SavedFile");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "SavedFile");

            migrationBuilder.CreateTable(
                name: "PartnerCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerCompanies_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PartnerCompanies_SavedFile_LogoId",
                        column: x => x.LogoId,
                        principalTable: "SavedFile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerCompanies_LogoId",
                table: "PartnerCompanies",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerCompanies_PartnerId",
                table: "PartnerCompanies",
                column: "PartnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerCompanies");

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "SavedFile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedFile_PartnerId",
                table: "SavedFile",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedFile_Partner_PartnerId",
                table: "SavedFile",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id");
        }
    }
}
