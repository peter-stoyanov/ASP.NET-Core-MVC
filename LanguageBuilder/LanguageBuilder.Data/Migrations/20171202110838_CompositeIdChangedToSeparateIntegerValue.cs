using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LanguageBuilder.Data.Migrations
{
    public partial class CompositeIdChangedToSeparateIntegerValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_UserWordExample",
                table: "tbl_UserWordExample");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_Translation",
                table: "tbl_Translation");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "tbl_UserWordExample",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "tbl_User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "tbl_Translation",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_UserWordExample",
                table: "tbl_UserWordExample",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_Translation",
                table: "tbl_Translation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserWordExample_UserWordId",
                table: "tbl_UserWordExample",
                column: "UserWordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Translation_SourceWordId",
                table: "tbl_Translation",
                column: "SourceWordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_UserWordExample",
                table: "tbl_UserWordExample");

            migrationBuilder.DropIndex(
                name: "IX_tbl_UserWordExample_UserWordId",
                table: "tbl_UserWordExample");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_Translation",
                table: "tbl_Translation");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Translation_SourceWordId",
                table: "tbl_Translation");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tbl_UserWordExample");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tbl_Translation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_UserWordExample",
                table: "tbl_UserWordExample",
                columns: new[] { "UserWordId", "ExampleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_Translation",
                table: "tbl_Translation",
                columns: new[] { "SourceWordId", "TargetWordId" });
        }
    }
}
