using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class udpate_all_entity_sq1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permission_function_functionid",
                table: "permission");

            migrationBuilder.DropForeignKey(
                name: "FK_permission_role_roleid",
                table: "permission");

            migrationBuilder.DropIndex(
                name: "IX_permission_functionid",
                table: "permission");

            migrationBuilder.DropIndex(
                name: "IX_permission_roleid",
                table: "permission");

            migrationBuilder.DropColumn(
                name: "functionid",
                table: "permission");

            migrationBuilder.DropColumn(
                name: "roleid",
                table: "permission");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_function_code",
                table: "function",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_permission_function_cd",
                table: "permission",
                column: "function_cd");

            migrationBuilder.CreateIndex(
                name: "IX_permission_role_cd",
                table: "permission",
                column: "role_cd");

            migrationBuilder.AddForeignKey(
                name: "FK_permission_function_function_cd",
                table: "permission",
                column: "function_cd",
                principalTable: "function",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_permission_role_role_cd",
                table: "permission",
                column: "role_cd",
                principalTable: "role",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permission_function_function_cd",
                table: "permission");

            migrationBuilder.DropForeignKey(
                name: "FK_permission_role_role_cd",
                table: "permission");

            migrationBuilder.DropIndex(
                name: "IX_permission_function_cd",
                table: "permission");

            migrationBuilder.DropIndex(
                name: "IX_permission_role_cd",
                table: "permission");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_function_code",
                table: "function");

            migrationBuilder.AddColumn<Guid>(
                name: "functionid",
                table: "permission",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "roleid",
                table: "permission",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_permission_functionid",
                table: "permission",
                column: "functionid");

            migrationBuilder.CreateIndex(
                name: "IX_permission_roleid",
                table: "permission",
                column: "roleid");

            migrationBuilder.AddForeignKey(
                name: "FK_permission_function_functionid",
                table: "permission",
                column: "functionid",
                principalTable: "function",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_permission_role_roleid",
                table: "permission",
                column: "roleid",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
