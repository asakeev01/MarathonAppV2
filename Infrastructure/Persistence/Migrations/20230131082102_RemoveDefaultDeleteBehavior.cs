using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class RemoveDefaultDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Marathons_MarathonId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Distances_Marathons_MarathonId",
                table: "Distances");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_UserId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_MarathonTranslations_Languages_LanguageId",
                table: "MarathonTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_MarathonTranslations_Marathons_MarathonId",
                table: "MarathonTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_MarathonTranslations_SavedFile_LogoId",
                table: "MarathonTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Partner_Marathons_MarathonId",
                table: "Partner");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerCompanies_SavedFile_LogoId",
                table: "PartnerCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerTranslation_Languages_LanguageId",
                table: "PartnerTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerTranslation_Partner_PartnerId",
                table: "PartnerTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_Promocodes_Vouchers_VoucherId",
                table: "Promocodes");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedFile_Marathons_MarathonId",
                table: "SavedFile");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusComments_Comments_CommentId",
                table: "StatusComments");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusComments_Statuses_StatusId",
                table: "StatusComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_AspNetUsers_UserId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Marathons_MarathonId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_PartnerCompanies_LogoId",
                table: "PartnerCompanies");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerCompanies_LogoId",
                table: "PartnerCompanies",
                column: "LogoId",
                unique: true,
                filter: "[LogoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Marathons_MarathonId",
                table: "Applications",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
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
                name: "FK_Documents_AspNetUsers_UserId",
                table: "Documents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarathonTranslations_Languages_LanguageId",
                table: "MarathonTranslations",
                column: "LanguageId",
                principalTable: "Languages",
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
                name: "FK_MarathonTranslations_SavedFile_LogoId",
                table: "MarathonTranslations",
                column: "LogoId",
                principalTable: "SavedFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partner_Marathons_MarathonId",
                table: "Partner",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerCompanies_SavedFile_LogoId",
                table: "PartnerCompanies",
                column: "LogoId",
                principalTable: "SavedFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerTranslation_Languages_LanguageId",
                table: "PartnerTranslation",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerTranslation_Partner_PartnerId",
                table: "PartnerTranslation",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Promocodes_Vouchers_VoucherId",
                table: "Promocodes",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedFile_Marathons_MarathonId",
                table: "SavedFile",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusComments_Comments_CommentId",
                table: "StatusComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusComments_Statuses_StatusId",
                table: "StatusComments",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_AspNetUsers_UserId",
                table: "Statuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Marathons_MarathonId",
                table: "Vouchers",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Marathons_MarathonId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Distances_Marathons_MarathonId",
                table: "Distances");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_UserId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_MarathonTranslations_Languages_LanguageId",
                table: "MarathonTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_MarathonTranslations_Marathons_MarathonId",
                table: "MarathonTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_MarathonTranslations_SavedFile_LogoId",
                table: "MarathonTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Partner_Marathons_MarathonId",
                table: "Partner");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerCompanies_SavedFile_LogoId",
                table: "PartnerCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerTranslation_Languages_LanguageId",
                table: "PartnerTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerTranslation_Partner_PartnerId",
                table: "PartnerTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_Promocodes_Vouchers_VoucherId",
                table: "Promocodes");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedFile_Marathons_MarathonId",
                table: "SavedFile");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusComments_Comments_CommentId",
                table: "StatusComments");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusComments_Statuses_StatusId",
                table: "StatusComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_AspNetUsers_UserId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Marathons_MarathonId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_PartnerCompanies_LogoId",
                table: "PartnerCompanies");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerCompanies_LogoId",
                table: "PartnerCompanies",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Marathons_MarathonId",
                table: "Applications",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Distances_Marathons_MarathonId",
                table: "Distances",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_UserId",
                table: "Documents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarathonTranslations_Languages_LanguageId",
                table: "MarathonTranslations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarathonTranslations_Marathons_MarathonId",
                table: "MarathonTranslations",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarathonTranslations_SavedFile_LogoId",
                table: "MarathonTranslations",
                column: "LogoId",
                principalTable: "SavedFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partner_Marathons_MarathonId",
                table: "Partner",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerCompanies_SavedFile_LogoId",
                table: "PartnerCompanies",
                column: "LogoId",
                principalTable: "SavedFile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerTranslation_Languages_LanguageId",
                table: "PartnerTranslation",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerTranslation_Partner_PartnerId",
                table: "PartnerTranslation",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Promocodes_Vouchers_VoucherId",
                table: "Promocodes",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedFile_Marathons_MarathonId",
                table: "SavedFile",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusComments_Comments_CommentId",
                table: "StatusComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusComments_Statuses_StatusId",
                table: "StatusComments",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_AspNetUsers_UserId",
                table: "Statuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Marathons_MarathonId",
                table: "Vouchers",
                column: "MarathonId",
                principalTable: "Marathons",
                principalColumn: "Id");
        }
    }
}
