using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class IndexForMarathonTranslation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MarathonTranslations_MarathonId_LanguageId",
                table: "MarathonTranslations");

            migrationBuilder.CreateIndex(
                name: "IX_MarathonTranslations_MarathonId_LanguageId",
                table: "MarathonTranslations",
                columns: new[] { "MarathonId", "LanguageId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MarathonTranslations_MarathonId_LanguageId",
                table: "MarathonTranslations");

            migrationBuilder.CreateIndex(
                name: "IX_MarathonTranslations_MarathonId_LanguageId",
                table: "MarathonTranslations",
                columns: new[] { "MarathonId", "LanguageId" });
        }
    }
}
