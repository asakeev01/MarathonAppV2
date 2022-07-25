using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarathonApp.Migrations
{
    public partial class AddedNewUserBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contries",
                table: "AspNetUsers",
                newName: "Country");

            migrationBuilder.AddColumn<bool>(
                name: "NewUser",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewUser",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "AspNetUsers",
                newName: "Contries");
        }
    }
}
