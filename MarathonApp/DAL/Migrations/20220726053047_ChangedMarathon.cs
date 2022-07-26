using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarathonApp.Migrations
{
    public partial class ChangedMarathon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Awards",
                table: "Marathons");

            migrationBuilder.DropColumn(
                name: "FinishPlace",
                table: "Marathons");

            migrationBuilder.DropColumn(
                name: "Rules",
                table: "Marathons");

            migrationBuilder.RenameColumn(
                name: "StartPlace",
                table: "Marathons",
                newName: "Text");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfParticipants",
                table: "Distance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegistredParticipants",
                table: "Distance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfParticipants",
                table: "Distance");

            migrationBuilder.DropColumn(
                name: "RegistredParticipants",
                table: "Distance");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Marathons",
                newName: "StartPlace");

            migrationBuilder.AddColumn<string>(
                name: "Awards",
                table: "Marathons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FinishPlace",
                table: "Marathons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rules",
                table: "Marathons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
