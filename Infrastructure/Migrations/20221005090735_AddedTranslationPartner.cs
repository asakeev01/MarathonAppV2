using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedTranslationPartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Partner");

            migrationBuilder.CreateTable(
                name: "PartnerTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerTranslation_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PartnerTranslation_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerTranslation_LanguageId",
                table: "PartnerTranslation",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerTranslation_PartnerId_LanguageId",
                table: "PartnerTranslation",
                columns: new[] { "PartnerId", "LanguageId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerTranslation");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Partner",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
