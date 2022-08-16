using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class update_functtion_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_function_function_parentid",
                schema: "public",
                table: "function");

            migrationBuilder.DropIndex(
                name: "IX_function_parentid",
                schema: "public",
                table: "function");

            migrationBuilder.DropColumn(
                name: "parentid",
                schema: "public",
                table: "function");

            migrationBuilder.AlterColumn<string>(
                name: "parent_cd",
                schema: "public",
                table: "function",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_function_parent_cd",
                schema: "public",
                table: "function",
                column: "parent_cd");

            migrationBuilder.AddForeignKey(
                name: "FK_function_function_code",
                schema: "public",
                table: "function",
                column: "code",
                principalSchema: "public",
                principalTable: "function",
                principalColumn: "parent_cd",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_function_function_code",
                schema: "public",
                table: "function");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_function_parent_cd",
                schema: "public",
                table: "function");

            migrationBuilder.AlterColumn<string>(
                name: "parent_cd",
                schema: "public",
                table: "function",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "parentid",
                schema: "public",
                table: "function",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_function_parentid",
                schema: "public",
                table: "function",
                column: "parentid");

            migrationBuilder.AddForeignKey(
                name: "FK_function_function_parentid",
                schema: "public",
                table: "function",
                column: "parentid",
                principalSchema: "public",
                principalTable: "function",
                principalColumn: "id");
        }
    }
}
