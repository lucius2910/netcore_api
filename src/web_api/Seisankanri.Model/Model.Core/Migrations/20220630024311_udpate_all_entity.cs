using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class udpate_all_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_machine_user_adminid",
                table: "machine");

            migrationBuilder.DropIndex(
                name: "IX_machine_adminid",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "admin_cd",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "adminid",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "country",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "description",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "location",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "producer",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "serial",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "status",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "code",
                table: "item_price");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "warehouses",
                type: "character varying(160)",
                maxLength: 160,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<Guid>(
                name: "admin_id",
                table: "machine",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "min_qty",
                table: "item_buy_prices",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "classifications",
                type: "character varying(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(240)",
                oldMaxLength: 240);

            migrationBuilder.AlterColumn<string>(
                name: "name2",
                table: "classifications",
                type: "character varying(160)",
                maxLength: 160,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(160)",
                oldMaxLength: 160);

            migrationBuilder.AddColumn<string>(
                name: "value1",
                table: "classifications",
                type: "character varying(160)",
                maxLength: 160,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value2",
                table: "classifications",
                type: "character varying(160)",
                maxLength: 160,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "calendar_date",
                table: "calendars",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_machine_admin_id",
                table: "machine",
                column: "admin_id");

            migrationBuilder.AddForeignKey(
                name: "FK_machine_user_admin_id",
                table: "machine",
                column: "admin_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_machine_user_admin_id",
                table: "machine");

            migrationBuilder.DropIndex(
                name: "IX_machine_admin_id",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "admin_id",
                table: "machine");

            migrationBuilder.DropColumn(
                name: "value1",
                table: "classifications");

            migrationBuilder.DropColumn(
                name: "value2",
                table: "classifications");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "warehouses",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(160)",
                oldMaxLength: 160);

            migrationBuilder.AddColumn<string>(
                name: "admin_cd",
                table: "machine",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "adminid",
                table: "machine",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "machine",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "machine",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "machine",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "producer",
                table: "machine",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "serial",
                table: "machine",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "machine",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "item_price",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "min_qty",
                table: "item_buy_prices",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "remarks",
                table: "classifications",
                type: "character varying(240)",
                maxLength: 240,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(240)",
                oldMaxLength: 240,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name2",
                table: "classifications",
                type: "character varying(160)",
                maxLength: 160,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(160)",
                oldMaxLength: 160,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "calendar_date",
                table: "calendars",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_machine_adminid",
                table: "machine",
                column: "adminid");

            migrationBuilder.AddForeignKey(
                name: "FK_machine_user_adminid",
                table: "machine",
                column: "adminid",
                principalTable: "user",
                principalColumn: "id");
        }
    }
}
