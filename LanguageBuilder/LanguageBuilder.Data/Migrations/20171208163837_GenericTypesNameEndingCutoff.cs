using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LanguageBuilder.Data.Migrations
{
    public partial class GenericTypesNameEndingCutoff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityRoleClaim`1_tbl_Role_RoleId",
                table: "tbl_IdentityRoleClaim`1");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserClaim`1_tbl_User_UserId",
                table: "tbl_IdentityUserClaim`1");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserLogin`1_tbl_User_UserId",
                table: "tbl_IdentityUserLogin`1");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserRole`1_tbl_Role_RoleId",
                table: "tbl_IdentityUserRole`1");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserRole`1_tbl_User_UserId",
                table: "tbl_IdentityUserRole`1");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserToken`1_tbl_User_UserId",
                table: "tbl_IdentityUserToken`1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityUserToken`1",
                table: "tbl_IdentityUserToken`1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityUserRole`1",
                table: "tbl_IdentityUserRole`1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityUserLogin`1",
                table: "tbl_IdentityUserLogin`1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityUserClaim`1",
                table: "tbl_IdentityUserClaim`1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityRoleClaim`1",
                table: "tbl_IdentityRoleClaim`1");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityUserToken`1",
                newName: "tbl_IdentityUserToken");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityUserRole`1",
                newName: "tbl_IdentityUserRole");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityUserLogin`1",
                newName: "tbl_IdentityUserLogin");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityUserClaim`1",
                newName: "tbl_IdentityUserClaim");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityRoleClaim`1",
                newName: "tbl_IdentityRoleClaim");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_IdentityUserRole`1_RoleId",
                table: "tbl_IdentityUserRole",
                newName: "IX_tbl_IdentityUserRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_IdentityUserLogin`1_UserId",
                table: "tbl_IdentityUserLogin",
                newName: "IX_tbl_IdentityUserLogin_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_IdentityUserClaim`1_UserId",
                table: "tbl_IdentityUserClaim",
                newName: "IX_tbl_IdentityUserClaim_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_IdentityRoleClaim`1_RoleId",
                table: "tbl_IdentityRoleClaim",
                newName: "IX_tbl_IdentityRoleClaim_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityUserToken",
                table: "tbl_IdentityUserToken",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityUserRole",
                table: "tbl_IdentityUserRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityUserLogin",
                table: "tbl_IdentityUserLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityUserClaim",
                table: "tbl_IdentityUserClaim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityRoleClaim",
                table: "tbl_IdentityRoleClaim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityRoleClaim_tbl_Role_RoleId",
                table: "tbl_IdentityRoleClaim",
                column: "RoleId",
                principalTable: "tbl_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserClaim_tbl_User_UserId",
                table: "tbl_IdentityUserClaim",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserLogin_tbl_User_UserId",
                table: "tbl_IdentityUserLogin",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserRole_tbl_Role_RoleId",
                table: "tbl_IdentityUserRole",
                column: "RoleId",
                principalTable: "tbl_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserRole_tbl_User_UserId",
                table: "tbl_IdentityUserRole",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserToken_tbl_User_UserId",
                table: "tbl_IdentityUserToken",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityRoleClaim_tbl_Role_RoleId",
                table: "tbl_IdentityRoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserClaim_tbl_User_UserId",
                table: "tbl_IdentityUserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserLogin_tbl_User_UserId",
                table: "tbl_IdentityUserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserRole_tbl_Role_RoleId",
                table: "tbl_IdentityUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserRole_tbl_User_UserId",
                table: "tbl_IdentityUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_IdentityUserToken_tbl_User_UserId",
                table: "tbl_IdentityUserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityUserToken",
                table: "tbl_IdentityUserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityUserRole",
                table: "tbl_IdentityUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityUserLogin",
                table: "tbl_IdentityUserLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityUserClaim",
                table: "tbl_IdentityUserClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_IdentityRoleClaim",
                table: "tbl_IdentityRoleClaim");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityUserToken",
                newName: "tbl_IdentityUserToken`1");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityUserRole",
                newName: "tbl_IdentityUserRole`1");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityUserLogin",
                newName: "tbl_IdentityUserLogin`1");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityUserClaim",
                newName: "tbl_IdentityUserClaim`1");

            migrationBuilder.RenameTable(
                name: "tbl_IdentityRoleClaim",
                newName: "tbl_IdentityRoleClaim`1");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_IdentityUserRole_RoleId",
                table: "tbl_IdentityUserRole`1",
                newName: "IX_tbl_IdentityUserRole`1_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_IdentityUserLogin_UserId",
                table: "tbl_IdentityUserLogin`1",
                newName: "IX_tbl_IdentityUserLogin`1_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_IdentityUserClaim_UserId",
                table: "tbl_IdentityUserClaim`1",
                newName: "IX_tbl_IdentityUserClaim`1_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_IdentityRoleClaim_RoleId",
                table: "tbl_IdentityRoleClaim`1",
                newName: "IX_tbl_IdentityRoleClaim`1_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityUserToken`1",
                table: "tbl_IdentityUserToken`1",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityUserRole`1",
                table: "tbl_IdentityUserRole`1",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityUserLogin`1",
                table: "tbl_IdentityUserLogin`1",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityUserClaim`1",
                table: "tbl_IdentityUserClaim`1",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_IdentityRoleClaim`1",
                table: "tbl_IdentityRoleClaim`1",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityRoleClaim`1_tbl_Role_RoleId",
                table: "tbl_IdentityRoleClaim`1",
                column: "RoleId",
                principalTable: "tbl_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserClaim`1_tbl_User_UserId",
                table: "tbl_IdentityUserClaim`1",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserLogin`1_tbl_User_UserId",
                table: "tbl_IdentityUserLogin`1",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserRole`1_tbl_Role_RoleId",
                table: "tbl_IdentityUserRole`1",
                column: "RoleId",
                principalTable: "tbl_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserRole`1_tbl_User_UserId",
                table: "tbl_IdentityUserRole`1",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_IdentityUserToken`1_tbl_User_UserId",
                table: "tbl_IdentityUserToken`1",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
