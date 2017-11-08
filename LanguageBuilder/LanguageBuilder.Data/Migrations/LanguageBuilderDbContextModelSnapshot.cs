﻿// <auto-generated />
using LanguageBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LanguageBuilder.Data.Migrations
{
    [DbContext(typeof(LanguageBuilderDbContext))]
    partial class LanguageBuilderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LanguageBuilder.Data.Models.Example", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<bool>("IsActive");

                    b.Property<int>("WordId");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("tbl_Example");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsUsed");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("tbl_Language");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.Role", b =>
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

                    b.ToTable("tbl_Role");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("tbl_Subscription");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.Translation", b =>
                {
                    b.Property<int>("SourceWordId");

                    b.Property<int>("TargetWordId");

                    b.HasKey("SourceWordId", "TargetWordId");

                    b.HasIndex("TargetWordId");

                    b.ToTable("tbl_Translation");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsActive");

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

                    b.Property<int>("SubscriptionId");

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

                    b.HasIndex("SubscriptionId");

                    b.ToTable("tbl_User");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.UserLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LanguageId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_UserLanguage");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.UserWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsTargeted");

                    b.Property<int>("MatchLevel");

                    b.Property<DateTime>("NextReview");

                    b.Property<string>("Notes")
                        .HasMaxLength(300);

                    b.Property<int>("ReproduceLevel");

                    b.Property<int>("StudyLevel");

                    b.Property<string>("UserId");

                    b.Property<int>("WordId");

                    b.Property<int?>("WordListId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WordId");

                    b.HasIndex("WordListId");

                    b.ToTable("tbl_UserWord");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.UserWordExample", b =>
                {
                    b.Property<int>("UserWordId");

                    b.Property<int>("ExampleId");

                    b.HasKey("UserWordId", "ExampleId");

                    b.HasIndex("ExampleId");

                    b.ToTable("tbl_UserWordExample");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Gender")
                        .HasMaxLength(15);

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LanguageId");

                    b.Property<string>("SyntaxType")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("tbl_Word");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.WordList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsActive");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Notes")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("tbl_WordList");
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

                    b.ToTable("tbl_IdentityRoleClaim`1");
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

                    b.ToTable("tbl_IdentityUserClaim`1");
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

                    b.ToTable("tbl_IdentityUserLogin`1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("tbl_IdentityUserRole`1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("tbl_IdentityUserToken`1");
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.Example", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.Word", "Word")
                        .WithMany("Examples")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.Translation", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.Word", "SourceWord")
                        .WithMany("SourceTranslations")
                        .HasForeignKey("SourceWordId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LanguageBuilder.Data.Models.Word", "TargetWord")
                        .WithMany("TargetTranslations")
                        .HasForeignKey("TargetWordId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.User", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.UserLanguage", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.Language", "Language")
                        .WithMany("Users")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LanguageBuilder.Data.Models.User", "User")
                        .WithMany("Languages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.UserWord", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.User", "User")
                        .WithMany("Words")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LanguageBuilder.Data.Models.Word", "Word")
                        .WithMany("Users")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LanguageBuilder.Data.Models.WordList", "WordList")
                        .WithMany("Words")
                        .HasForeignKey("WordListId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.UserWordExample", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.Example", "Example")
                        .WithMany("UserWords")
                        .HasForeignKey("ExampleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LanguageBuilder.Data.Models.UserWord", "UserWord")
                        .WithMany("Examples")
                        .HasForeignKey("UserWordId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.Word", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.Language", "Language")
                        .WithMany("Words")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LanguageBuilder.Data.Models.WordList", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LanguageBuilder.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LanguageBuilder.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
