﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221115065639_Application1")]
    partial class Application1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Distances.Distance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActivatedReservedPlaces")
                        .HasColumnType("int");

                    b.Property<int>("MarathonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegisteredParticipants")
                        .HasColumnType("int");

                    b.Property<int>("ReservedPlaces")
                        .HasColumnType("int");

                    b.Property<int>("StartNumbersFrom")
                        .HasColumnType("int");

                    b.Property<int>("StartNumbersTo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarathonId");

                    b.ToTable("Distances");
                });

            modelBuilder.Entity("Domain.Entities.Distances.DistanceAge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AgeFrom")
                        .HasColumnType("int");

                    b.Property<int?>("AgeTo")
                        .HasColumnType("int");

                    b.Property<int>("DistanceId")
                        .HasColumnType("int");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DistanceId");

                    b.ToTable("DistanceAges");
                });

            modelBuilder.Entity("Domain.Entities.Distances.DistanceForPWD", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MarathonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegisteredParticipants")
                        .HasColumnType("int");

                    b.Property<int>("StartNumbersFrom")
                        .HasColumnType("int");

                    b.Property<int>("StartNumbersTo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarathonId");

                    b.ToTable("DistanceForPWD");
                });

            modelBuilder.Entity("Domain.Entities.Distances.DistancePrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("DistanceId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DistanceId");

                    b.ToTable("DistancePrices");
                });

            modelBuilder.Entity("Domain.Entities.Documents.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("DisabilityPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FrontPassportPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsurancePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Domain.Entities.Languages.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Domain.Entities.Marathons.Marathon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDateAcceptingApplications")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDateAcceptingApplications")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Marathons");
                });

            modelBuilder.Entity("Domain.Entities.Marathons.MarathonTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<int?>("LogoId")
                        .HasColumnType("int");

                    b.Property<int>("MarathonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LogoId")
                        .IsUnique()
                        .HasFilter("[LogoId] IS NOT NULL");

                    b.HasIndex("MarathonId", "LanguageId")
                        .IsUnique();

                    b.ToTable("MarathonTranslations");
                });

            modelBuilder.Entity("Domain.Entities.Marathons.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MarathonId")
                        .HasColumnType("int");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarathonId");

                    b.ToTable("Partner");
                });

            modelBuilder.Entity("Domain.Entities.Marathons.PartnerTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PartnerId", "LanguageId")
                        .IsUnique();

                    b.ToTable("PartnerTranslation");
                });

            modelBuilder.Entity("Domain.Entities.SavedFiles.SavedFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("MarathonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PartnerId")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MarathonId");

                    b.HasIndex("PartnerId");

                    b.ToTable("SavedFile");
                });

            modelBuilder.Entity("Domain.Entities.Users.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Users.Status", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("Comment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("CurrentStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Domain.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Country")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfConfirmation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ExtraPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsDisable")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Tshirt")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Users.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Vouchers.Promocode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DistanceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("VoucherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DistanceId");

                    b.HasIndex("UserId");

                    b.HasIndex("VoucherId");

                    b.ToTable("Promocodes");
                });

            modelBuilder.Entity("Domain.Entities.Vouchers.Voucher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MarathonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MarathonId");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserToken<long>");
                });

            modelBuilder.Entity("Domain.Entities.Users.RefreshToken", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserToken<long>");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("RefreshToken");
                });

            modelBuilder.Entity("Domain.Entities.Distances.Distance", b =>
                {
                    b.HasOne("Domain.Entities.Marathons.Marathon", "Marathon")
                        .WithMany("Distances")
                        .HasForeignKey("MarathonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Marathon");
                });

            modelBuilder.Entity("Domain.Entities.Distances.DistanceAge", b =>
                {
                    b.HasOne("Domain.Entities.Distances.Distance", "Distance")
                        .WithMany("DistanceAges")
                        .HasForeignKey("DistanceId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Distance");
                });

            modelBuilder.Entity("Domain.Entities.Distances.DistanceForPWD", b =>
                {
                    b.HasOne("Domain.Entities.Marathons.Marathon", "Marathon")
                        .WithMany("DistancesForPWD")
                        .HasForeignKey("MarathonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Marathon");
                });

            modelBuilder.Entity("Domain.Entities.Distances.DistancePrice", b =>
                {
                    b.HasOne("Domain.Entities.Distances.Distance", "Distance")
                        .WithMany("DistancePrices")
                        .HasForeignKey("DistanceId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Distance");
                });

            modelBuilder.Entity("Domain.Entities.Documents.Document", b =>
                {
                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithOne("Document")
                        .HasForeignKey("Domain.Entities.Documents.Document", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Marathons.MarathonTranslation", b =>
                {
                    b.HasOne("Domain.Entities.Languages.Language", "Language")
                        .WithMany("MarathonTranslations")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.SavedFiles.SavedFile", "Logo")
                        .WithOne("MarathonLogo")
                        .HasForeignKey("Domain.Entities.Marathons.MarathonTranslation", "LogoId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Domain.Entities.Marathons.Marathon", "Marathon")
                        .WithMany("MarathonTranslations")
                        .HasForeignKey("MarathonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Logo");

                    b.Navigation("Marathon");
                });

            modelBuilder.Entity("Domain.Entities.Marathons.Partner", b =>
                {
                    b.HasOne("Domain.Entities.Marathons.Marathon", "Marathon")
                        .WithMany("Partners")
                        .HasForeignKey("MarathonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Marathon");
                });

            modelBuilder.Entity("Domain.Entities.Marathons.PartnerTranslation", b =>
                {
                    b.HasOne("Domain.Entities.Languages.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Marathons.Partner", "Partner")
                        .WithMany("Translations")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Domain.Entities.SavedFiles.SavedFile", b =>
                {
                    b.HasOne("Domain.Entities.Marathons.Marathon", "Marathon")
                        .WithMany("Documents")
                        .HasForeignKey("MarathonId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Domain.Entities.Marathons.Partner", "Partner")
                        .WithMany("Logos")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Marathon");

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Domain.Entities.Users.Status", b =>
                {
                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithOne("Status")
                        .HasForeignKey("Domain.Entities.Users.Status", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Users.UserRole", b =>
                {
                    b.HasOne("Domain.Entities.Users.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Vouchers.Promocode", b =>
                {
                    b.HasOne("Domain.Entities.Distances.Distance", "Distance")
                        .WithMany("Promocodes")
                        .HasForeignKey("DistanceId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Domain.Entities.Vouchers.Voucher", "Voucher")
                        .WithMany("Promocodes")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Distance");

                    b.Navigation("User");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Domain.Entities.Vouchers.Voucher", b =>
                {
                    b.HasOne("Domain.Entities.Marathons.Marathon", "Marathon")
                        .WithMany("Vouchers")
                        .HasForeignKey("MarathonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Marathon");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Domain.Entities.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Distances.Distance", b =>
                {
                    b.Navigation("DistanceAges");

                    b.Navigation("DistancePrices");

                    b.Navigation("Promocodes");
                });

            modelBuilder.Entity("Domain.Entities.Languages.Language", b =>
                {
                    b.Navigation("MarathonTranslations");
                });

            modelBuilder.Entity("Domain.Entities.Marathons.Marathon", b =>
                {
                    b.Navigation("Distances");

                    b.Navigation("DistancesForPWD");

                    b.Navigation("Documents");

                    b.Navigation("MarathonTranslations");

                    b.Navigation("Partners");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("Domain.Entities.Marathons.Partner", b =>
                {
                    b.Navigation("Logos");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Domain.Entities.SavedFiles.SavedFile", b =>
                {
                    b.Navigation("MarathonLogo");
                });

            modelBuilder.Entity("Domain.Entities.Users.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.Users.User", b =>
                {
                    b.Navigation("Document")
                        .IsRequired();

                    b.Navigation("Status")
                        .IsRequired();

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.Vouchers.Voucher", b =>
                {
                    b.Navigation("Promocodes");
                });
#pragma warning restore 612, 618
        }
    }
}
