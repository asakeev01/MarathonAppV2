using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class IndexForMarathonTranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MarathonTranslations_MarathonId",
                table: "MarathonTranslations");

            migrationBuilder.CreateIndex(
                name: "IX_MarathonTranslations_MarathonId_LanguageId",
                table: "MarathonTranslations",
                columns: new[] { "MarathonId", "LanguageId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MarathonTranslations_MarathonId_LanguageId",
                table: "MarathonTranslations");

            migrationBuilder.CreateIndex(
                name: "IX_MarathonTranslations_MarathonId",
                table: "MarathonTranslations",
                column: "MarathonId");
        }
    }
}
