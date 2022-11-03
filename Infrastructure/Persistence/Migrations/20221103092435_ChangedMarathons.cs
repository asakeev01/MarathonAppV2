using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class ChangedMarathons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marathons_SavedFile_LogoId",
                table: "Marathons");

            migrationBuilder.DropIndex(
                name: "IX_Marathons_LogoId",
                table: "Marathons");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Marathons");

            migrationBuilder.DropColumn(
                name: "MedicalCertificate",
                table: "Distances");

            migrationBuilder.DropColumn(
                name: "PassingLimit",
                table: "Distances");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Distances");

            migrationBuilder.RenameColumn(
                name: "RegistredParticipants",
                table: "Distances",
                newName: "StartNumbersTo");

            migrationBuilder.RenameColumn(
                name: "NumberOfParticipants",
                table: "Distances",
                newName: "StartNumbersFrom");

            migrationBuilder.RenameColumn(
                name: "AgeFrom",
                table: "Distances",
                newName: "RegisteredParticipants");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "SavedFile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SavedFile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "MarathonTranslations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "DistanceAges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DistanceForPWD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartNumbersFrom = table.Column<int>(type: "int", nullable: false),
                    StartNumbersTo = table.Column<int>(type: "int", nullable: false),
                    RegisteredParticipants = table.Column<int>(type: "int", nullable: false),
                    MarathonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceForPWD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistanceForPWD_Marathons_MarathonId",
                        column: x => x.MarathonId,
                        principalTable: "Marathons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarathonTranslations_LogoId",
                table: "MarathonTranslations",
                column: "LogoId",
                unique: true,
                filter: "[LogoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DistanceForPWD_MarathonId",
                table: "DistanceForPWD",
                column: "MarathonId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarathonTranslations_SavedFile_LogoId",
                table: "MarathonTranslations",
                column: "LogoId",
                principalTable: "SavedFile",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarathonTranslations_SavedFile_LogoId",
                table: "MarathonTranslations");

            migrationBuilder.DropTable(
                name: "DistanceForPWD");

            migrationBuilder.DropIndex(
                name: "IX_MarathonTranslations_LogoId",
                table: "MarathonTranslations");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "MarathonTranslations");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "DistanceAges");

            migrationBuilder.RenameColumn(
                name: "StartNumbersTo",
                table: "Distances",
                newName: "RegistredParticipants");

            migrationBuilder.RenameColumn(
                name: "StartNumbersFrom",
                table: "Distances",
                newName: "NumberOfParticipants");

            migrationBuilder.RenameColumn(
                name: "RegisteredParticipants",
                table: "Distances",
                newName: "AgeFrom");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "SavedFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SavedFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Marathons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MedicalCertificate",
                table: "Distances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PassingLimit",
                table: "Distances",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Distances",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Marathons_LogoId",
                table: "Marathons",
                column: "LogoId",
                unique: true,
                filter: "[LogoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Marathons_SavedFile_LogoId",
                table: "Marathons",
                column: "LogoId",
                principalTable: "SavedFile",
                principalColumn: "Id");
        }
    }
}
