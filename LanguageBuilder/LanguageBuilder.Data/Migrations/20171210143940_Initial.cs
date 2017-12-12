using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LanguageBuilder.Data.Migrations
{
    public partial class Initial : Migration
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
                name: "tbl_SyntaxType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SyntaxType", x => x.Id);
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
                name: "tbl_IdentityRoleClaim",
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
                    table.PrimaryKey("PK_tbl_IdentityRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_IdentityRoleClaim_tbl_Role_RoleId",
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
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "tbl_Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    SyntaxTypeId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_tbl_Word_tbl_SyntaxType_SyntaxTypeId",
                        column: x => x.SyntaxTypeId,
                        principalTable: "tbl_SyntaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Article_tbl_User_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IdentityUserClaim",
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
                    table.PrimaryKey("PK_tbl_IdentityUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserClaim_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IdentityUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IdentityUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserLogin_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IdentityUserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IdentityUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserRole_tbl_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserRole_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_IdentityUserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IdentityUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_tbl_IdentityUserToken_tbl_User_UserId",
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SourceWordId = table.Column<int>(type: "int", nullable: false),
                    TargetWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Translation", x => x.Id);
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExampleId = table.Column<int>(type: "int", nullable: false),
                    UserWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserWordExample", x => x.Id);
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
                name: "IX_tbl_Article_AuthorId",
                table: "tbl_Article",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Example_WordId",
                table: "tbl_Example",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IdentityRoleClaim_RoleId",
                table: "tbl_IdentityRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IdentityUserClaim_UserId",
                table: "tbl_IdentityUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IdentityUserLogin_UserId",
                table: "tbl_IdentityUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IdentityUserRole_RoleId",
                table: "tbl_IdentityUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "tbl_Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Translation_SourceWordId",
                table: "tbl_Translation",
                column: "SourceWordId");

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
                name: "IX_tbl_UserWordExample_UserWordId",
                table: "tbl_UserWordExample",
                column: "UserWordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Word_LanguageId",
                table: "tbl_Word",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Word_SyntaxTypeId",
                table: "tbl_Word",
                column: "SyntaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WordList_LanguageId",
                table: "tbl_WordList",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Article");

            migrationBuilder.DropTable(
                name: "tbl_IdentityRoleClaim");

            migrationBuilder.DropTable(
                name: "tbl_IdentityUserClaim");

            migrationBuilder.DropTable(
                name: "tbl_IdentityUserLogin");

            migrationBuilder.DropTable(
                name: "tbl_IdentityUserRole");

            migrationBuilder.DropTable(
                name: "tbl_IdentityUserToken");

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
                name: "tbl_SyntaxType");

            migrationBuilder.DropTable(
                name: "tbl_Language");
        }
    }
}
