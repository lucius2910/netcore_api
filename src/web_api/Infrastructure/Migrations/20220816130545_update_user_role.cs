using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class update_user_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role_role_cd",
                schema: "public",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_role_cd",
                schema: "public",
                table: "user");

            migrationBuilder.DropColumn(
                name: "role_cd",
                schema: "public",
                table: "user");

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    role_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_role_role_cd",
                        column: x => x.role_cd,
                        principalSchema: "public",
                        principalTable: "role",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_role_user_user_cd",
                        column: x => x.user_cd,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_role_role_cd",
                schema: "public",
                table: "user_role",
                column: "role_cd");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_user_cd",
                schema: "public",
                table: "user_role",
                column: "user_cd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_role",
                schema: "public");

            migrationBuilder.AddColumn<string>(
                name: "role_cd",
                schema: "public",
                table: "user",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_cd",
                schema: "public",
                table: "user",
                column: "role_cd");

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_role_cd",
                schema: "public",
                table: "user",
                column: "role_cd",
                principalSchema: "public",
                principalTable: "role",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
