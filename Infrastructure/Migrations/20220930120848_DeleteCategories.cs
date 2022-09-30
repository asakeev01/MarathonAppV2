using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class DeleteCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distances_DistanceCategories_DistanceCategoryId",
                table: "Distances");

            migrationBuilder.DropTable(
                name: "DistanceCategoryTranslations");

            migrationBuilder.DropTable(
                name: "DistanceCategories");

            migrationBuilder.DropIndex(
                name: "IX_Distances_DistanceCategoryId",
                table: "Distances");

            migrationBuilder.DropColumn(
                name: "DistanceCategoryId",
                table: "Distances");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Distances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Distances");

            migrationBuilder.AddColumn<int>(
                name: "DistanceCategoryId",
                table: "Distances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DistanceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistanceCategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistanceCategoryId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceCategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistanceCategoryTranslations_DistanceCategories_DistanceCategoryId",
                        column: x => x.DistanceCategoryId,
                        principalTable: "DistanceCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DistanceCategoryTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Distances_DistanceCategoryId",
                table: "Distances",
                column: "DistanceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DistanceCategoryTranslations_DistanceCategoryId",
                table: "DistanceCategoryTranslations",
                column: "DistanceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DistanceCategoryTranslations_LanguageId",
                table: "DistanceCategoryTranslations",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Distances_DistanceCategories_DistanceCategoryId",
                table: "Distances",
                column: "DistanceCategoryId",
                principalTable: "DistanceCategories",
                principalColumn: "Id");
        }
    }
}
