using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LanguageBuilder.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Subscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    SyntaxType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Word", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Word_tbl_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "tbl_Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WordList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WordList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_WordList_tbl_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "tbl_Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IdentityRoleClaim`1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IdentityRoleClaim`1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_IdentityRoleClaim`1_tbl_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_User_tbl_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "tbl_Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Example",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    WordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Example", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Example_tbl_Word_WordId",
                        column: x => x.WordId,
                        principalTable: "tbl_Word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Translation",
                columns: table => new
                {
                    SourceWordId = table.Column<int>(type: "int", nullable: false),
                    TargetWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Translation", x => new { x.SourceWordId, x.TargetWordId });
                    table.ForeignKey(
                        name: "FK_tbl_Translation_tbl_Word_SourceWordId",
                        column: x => x.SourceWordId,
                        principalTable: "tbl_Word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Translation_tbl_Word_TargetWordId",
                        column: x => x.TargetWordId,
                        principalTable: "tbl_Word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IdentityUserClaim`1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IdentityUserClaim`1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserClaim`1_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IdentityUserLogin`1",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IdentityUserLogin`1", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserLogin`1_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IdentityUserRole`1",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IdentityUserRole`1", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserRole`1_tbl_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserRole`1_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IdentityUserToken`1",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IdentityUserToken`1", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserToken`1_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_UserLanguage_tbl_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "tbl_Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_UserLanguage_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserWord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTargeted = table.Column<bool>(type: "bit", nullable: false),
                    MatchLevel = table.Column<int>(type: "int", nullable: false),
                    NextReview = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ReproduceLevel = table.Column<int>(type: "int", nullable: false),
                    StudyLevel = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    WordListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_UserWord_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_UserWord_tbl_Word_WordId",
                        column: x => x.WordId,
                        principalTable: "tbl_Word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_UserWord_tbl_WordList_WordListId",
                        column: x => x.WordListId,
                        principalTable: "tbl_WordList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserWordExample",
                columns: table => new
                {
                    UserWordId = table.Column<int>(type: "int", nullable: false),
                    ExampleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserWordExample", x => new { x.UserWordId, x.ExampleId });
                    table.ForeignKey(
                        name: "FK_tbl_UserWordExample_tbl_Example_ExampleId",
                        column: x => x.ExampleId,
                        principalTable: "tbl_Example",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_UserWordExample_tbl_UserWord_UserWordId",
                        column: x => x.UserWordId,
                        principalTable: "tbl_UserWord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Example_WordId",
                table: "tbl_Example",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IdentityRoleClaim`1_RoleId",
                table: "tbl_IdentityRoleClaim`1",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IdentityUserClaim`1_UserId",
                table: "tbl_IdentityUserClaim`1",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IdentityUserLogin`1_UserId",
                table: "tbl_IdentityUserLogin`1",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IdentityUserRole`1_RoleId",
                table: "tbl_IdentityUserRole`1",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "tbl_Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Translation_TargetWordId",
                table: "tbl_Translation",
                column: "TargetWordId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "tbl_User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "tbl_User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_User_SubscriptionId",
                table: "tbl_User",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserLanguage_LanguageId",
                table: "tbl_UserLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserLanguage_UserId",
                table: "tbl_UserLanguage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserWord_UserId",
                table: "tbl_UserWord",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserWord_WordId",
                table: "tbl_UserWord",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserWord_WordListId",
                table: "tbl_UserWord",
                column: "WordListId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserWordExample_ExampleId",
                table: "tbl_UserWordExample",
                column: "ExampleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Word_LanguageId",
                table: "tbl_Word",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WordList_LanguageId",
                table: "tbl_WordList",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_IdentityRoleClaim`1");

            migrationBuilder.DropTable(
                name: "tbl_IdentityUserClaim`1");

            migrationBuilder.DropTable(
                name: "tbl_IdentityUserLogin`1");

            migrationBuilder.DropTable(
                name: "tbl_IdentityUserRole`1");

            migrationBuilder.DropTable(
                name: "tbl_IdentityUserToken`1");

            migrationBuilder.DropTable(
                name: "tbl_Translation");

            migrationBuilder.DropTable(
                name: "tbl_UserLanguage");

            migrationBuilder.DropTable(
                name: "tbl_UserWordExample");

            migrationBuilder.DropTable(
                name: "tbl_Role");

            migrationBuilder.DropTable(
                name: "tbl_Example");

            migrationBuilder.DropTable(
                name: "tbl_UserWord");

            migrationBuilder.DropTable(
                name: "tbl_User");

            migrationBuilder.DropTable(
                name: "tbl_Word");

            migrationBuilder.DropTable(
                name: "tbl_WordList");

            migrationBuilder.DropTable(
                name: "tbl_Subscription");

            migrationBuilder.DropTable(
                name: "tbl_Language");
        }
    }
}
