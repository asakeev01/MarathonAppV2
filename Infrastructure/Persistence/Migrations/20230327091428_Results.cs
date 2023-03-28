using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class Results : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Distances_Marathons_MarathonId",
                table: "Distances");

            migrationBuilder.DropForeignKey(
                name: "FK_MarathonTranslations_Marathons_MarathonId",
                table: "MarathonTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Partner_Marathons_MarathonId",
                table: "Partner");

            migrationBuilder.DropIndex(
                name: "IX_Applications_PromocodeId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "PartnerCompanies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "DistancePrices",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Applications",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Paid",
                table: "Applications",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryPlace = table.Column<int>(type: "int", nullable: false),
                    GeneralPlace = table.Column<int>(type: "int", nullable: false),
                    GunTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChipTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Result_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PromocodeId",
                table: "Applications",
                column: "PromocodeId",
                unique: true,
                filter: "[PromocodeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Result_ApplicationId",
                table: "Result",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Distances_Marathons_MarathonId",
                table: "Distances",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarathonTranslations_Marathons_MarathonId",
                table: "MarathonTranslations",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partner_Marathons_MarathonId",
                table: "Partner",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Distances_Marathons_MarathonId",
                table: "Distances");

            migrationBuilder.DropForeignKey(
                name: "FK_MarathonTranslations_Marathons_MarathonId",
                table: "MarathonTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Partner_Marathons_MarathonId",
                table: "Partner");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropIndex(
                name: "IX_Applications_PromocodeId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "PartnerCompanies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "DistancePrices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Applications",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Paid",
                table: "Applications",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PromocodeId",
                table: "Applications",
                column: "PromocodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Distances_Marathons_MarathonId",
                table: "Distances",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarathonTranslations_Marathons_MarathonId",
                table: "MarathonTranslations",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partner_Marathons_MarathonId",
                table: "Partner",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
