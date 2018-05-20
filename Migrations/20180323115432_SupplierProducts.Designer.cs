﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApiJwt.Entities;

namespace WebApiJwt.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180323115432_SupplierProducts")]
    partial class SupplierProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApiJwt.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebApiJwt.Entities.Codes", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AltCode");

                    b.Property<string>("CodeDescription");

                    b.Property<string>("Type");

                    b.Property<string>("UserID");

                    b.HasKey("Code");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("WebApiJwt.Entities.MainMenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Data");

                    b.Property<string>("Fragment");

                    b.Property<bool?>("Group");

                    b.Property<bool?>("Hidden");

                    b.Property<bool?>("Home");

                    b.Property<string>("Icon");

                    b.Property<string>("Link");

                    b.Property<string>("PathMatch");

                    b.Property<bool?>("Selected");

                    b.Property<int?>("SubMenuHeight");

                    b.Property<string>("Target");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.Property<bool?>("expanded");

                    b.HasKey("Id");

                    b.ToTable("MenuItemMain");
                });

            modelBuilder.Entity("WebApiJwt.Entities.SchoolContacts", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cell");

                    b.Property<string>("LandLine");

                    b.Property<string>("Name");

                    b.Property<string>("PositionCode");

                    b.Property<int>("SchoolId");

                    b.Property<string>("Type005");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("SchoolContacts");
                });

            modelBuilder.Entity("WebApiJwt.Entities.SchoolGradeTotals", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NoOffClasses");

                    b.Property<int>("NoOffLearners");

                    b.Property<int>("NoOffParticipation");

                    b.Property<int>("PeriodId");

                    b.Property<int>("SchoolGradeId");

                    b.Property<int>("SchoolId");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("SchoolGradeTotals");
                });

            modelBuilder.Entity("WebApiJwt.Entities.Schools", b =>
                {
                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Butterfly");

                    b.Property<string>("Category")
                        .HasMaxLength(10);

                    b.Property<string>("Cover_letter");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Forum_area");

                    b.Property<string>("Language");

                    b.Property<string>("Letter_file");

                    b.Property<string>("Logo");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PeriodId");

                    b.Property<string>("Principal");

                    b.Property<string>("Principal_secretary");

                    b.Property<string>("Reg_number");

                    b.Property<string>("Representative");

                    b.Property<string>("Signature");

                    b.Property<string>("Store_learners");

                    b.Property<string>("Store_school");

                    b.Property<string>("Type")
                        .HasMaxLength(10);

                    b.Property<string>("Type002")
                        .HasMaxLength(3);

                    b.Property<string>("Type003")
                        .HasMaxLength(3);

                    b.Property<string>("Type004")
                        .HasMaxLength(3);

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.HasKey("SchoolId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("WebApiJwt.Entities.SchoolsAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("Physical_AddressLine1");

                    b.Property<string>("Physical_AddressLine2");

                    b.Property<string>("Physical_AddressLine3");

                    b.Property<int>("Physical_PostalCode");

                    b.Property<string>("Physical_Province");

                    b.Property<string>("Posta_AddressLine1");

                    b.Property<string>("Posta_AddressLine3");

                    b.Property<string>("Posta_AddressLine4");

                    b.Property<int>("Posta_PostalCode");

                    b.Property<string>("Posta_Province");

                    b.Property<int>("SchoolId");

                    b.Property<string>("Telephone");

                    b.Property<string>("UserID");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.ToTable("SchoolsAddress");
                });

            modelBuilder.Entity("WebApiJwt.Entities.SchoolsPeriods", b =>
                {
                    b.Property<int>("PeriodId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CloseDate");

                    b.Property<int>("PeriodYear");

                    b.Property<string>("UserID");

                    b.HasKey("PeriodId");

                    b.ToTable("SchoolsPeriods");
                });

            modelBuilder.Entity("WebApiJwt.Entities.SchoolsStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Deadline");

                    b.Property<int>("PeriodId");

                    b.Property<int>("PeriodYear");

                    b.Property<int>("SchoolId");

                    b.Property<string>("StatusCode");

                    b.Property<string>("Type001");

                    b.Property<string>("UserID");

                    b.HasKey("Id");

                    b.ToTable("SchoolsStatus");
                });

            modelBuilder.Entity("WebApiJwt.Entities.ScoolGrades", b =>
                {
                    b.Property<int>("SchoolGradeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GrageCode");

                    b.Property<int>("SchoolId");

                    b.Property<string>("Type006");

                    b.Property<string>("UserID");

                    b.HasKey("SchoolGradeId");

                    b.ToTable("ScoolGrades");
                });

            modelBuilder.Entity("WebApiJwt.Entities.SubMenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Data");

                    b.Property<string>("Fragment");

                    b.Property<bool?>("Group");

                    b.Property<bool?>("Hidden");

                    b.Property<bool?>("Home");

                    b.Property<string>("Icon");

                    b.Property<string>("Link");

                    b.Property<int>("MainMenuItemId");

                    b.Property<string>("PathMatch");

                    b.Property<bool?>("Selected");

                    b.Property<int?>("SubMenuHeight");

                    b.Property<string>("Target");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.Property<bool?>("expanded");

                    b.HasKey("Id");

                    b.HasIndex("MainMenuItemId");

                    b.ToTable("MenuItemSub");
                });

            modelBuilder.Entity("WebApiJwt.Entities.SupplierProducts", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("ProductCode");

                    b.Property<decimal>("RetailPrice");

                    b.Property<decimal>("SupplierPrice");

                    b.Property<string>("SuppliersId");

                    b.HasKey("ProductId");

                    b.ToTable("SupplierProducts");
                });

            modelBuilder.Entity("WebApiJwt.Entities.Suppliers", b =>
                {
                    b.Property<string>("SuppliersId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactNumber");

                    b.Property<string>("Email");

                    b.Property<string>("FaxNumber");

                    b.Property<string>("Name");

                    b.Property<string>("Website");

                    b.HasKey("SuppliersId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("WebApiJwt.Entities.SuppliersAddress", b =>
                {
                    b.Property<string>("SuppliersId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AddressLine3");

                    b.Property<int>("PostalCode");

                    b.Property<string>("Province");

                    b.HasKey("SuppliersId");

                    b.ToTable("SuppliersAddress");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApiJwt.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApiJwt.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApiJwt.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebApiJwt.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApiJwt.Entities.SubMenuItem", b =>
                {
                    b.HasOne("WebApiJwt.Entities.MainMenuItem")
                        .WithMany("Children")
                        .HasForeignKey("MainMenuItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
