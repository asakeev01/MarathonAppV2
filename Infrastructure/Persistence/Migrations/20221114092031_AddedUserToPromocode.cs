using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddedUserToPromocode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Promocodes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promocodes_UserId",
                table: "Promocodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promocodes_AspNetUsers_UserId",
                table: "Promocodes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promocodes_AspNetUsers_UserId",
                table: "Promocodes");

            migrationBuilder.DropIndex(
                name: "IX_Promocodes_UserId",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Promocodes");
        }
    }
}
