using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class udpate_config_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_machine_user_admin_id",
            //    table: "machine");

            //migrationBuilder.DropIndex(
            //    name: "IX_machine_admin_id",
            //    table: "machine");

            //migrationBuilder.DropColumn(
            //    name: "admin_id",
            //    table: "machine");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "warehouses",
                newName: "warehouses",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "user",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "system_settings",
                newName: "system_settings",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "seq",
                newName: "seq",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "sale_plan",
                newName: "sale_plan",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "role",
                newName: "role",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "resource",
                newName: "resource",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "receive_order_dt",
                newName: "receive_order_dt",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "receive_order",
                newName: "receive_order",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "product_plan_month",
                newName: "product_plan_month",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "product_plan_day",
                newName: "product_plan_day",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "permission",
                newName: "permission",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "master_code",
                newName: "master_code",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "manufactory",
                newName: "manufactory",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "log_exception",
                newName: "log_exception",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "log_action",
                newName: "log_action",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "item_sale_prices",
                newName: "item_sale_prices",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "item_price",
                newName: "item_price",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "item_edi",
                newName: "item_edi",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "item_buy_prices",
                newName: "item_buy_prices",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "item",
                newName: "item",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "inventory",
                newName: "inventory",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "function",
                newName: "function",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "company",
                newName: "company",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "classifications",
                newName: "classifications",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "category",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "calendars",
                newName: "calendars",
                newSchema: "public");

            //migrationBuilder.AddColumn<string>(
            //    name: "admin_cd",
            //    table: "machine",
            //    type: "character varying(15)",
            //    maxLength: 15,
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "item_name2",
                schema: "public",
                table: "item_edi",
                type: "character varying(160)",
                maxLength: 160,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(160)",
                oldMaxLength: 160);

            //migrationBuilder.CreateIndex(
            //    name: "IX_machine_admin_cd",
            //    table: "machine",
            //    column: "admin_cd");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_machine_user_admin_cd",
            //    table: "machine",
            //    column: "admin_cd",
            //    principalSchema: "public",
            //    principalTable: "user",
            //    principalColumn: "code",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_machine_user_admin_cd",
            //    table: "machine");

            //migrationBuilder.DropIndex(
            //    name: "IX_machine_admin_cd",
            //    table: "machine");

            //migrationBuilder.DropColumn(
            //    name: "admin_cd",
            //    table: "machine");

            migrationBuilder.RenameTable(
                name: "warehouses",
                schema: "public",
                newName: "warehouses");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "public",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "system_settings",
                schema: "public",
                newName: "system_settings");

            migrationBuilder.RenameTable(
                name: "seq",
                schema: "public",
                newName: "seq");

            migrationBuilder.RenameTable(
                name: "sale_plan",
                schema: "public",
                newName: "sale_plan");

            migrationBuilder.RenameTable(
                name: "role",
                schema: "public",
                newName: "role");

            migrationBuilder.RenameTable(
                name: "resource",
                schema: "public",
                newName: "resource");

            migrationBuilder.RenameTable(
                name: "receive_order_dt",
                schema: "public",
                newName: "receive_order_dt");

            migrationBuilder.RenameTable(
                name: "receive_order",
                schema: "public",
                newName: "receive_order");

            migrationBuilder.RenameTable(
                name: "product_plan_month",
                schema: "public",
                newName: "product_plan_month");

            migrationBuilder.RenameTable(
                name: "product_plan_day",
                schema: "public",
                newName: "product_plan_day");

            migrationBuilder.RenameTable(
                name: "permission",
                schema: "public",
                newName: "permission");

            migrationBuilder.RenameTable(
                name: "master_code",
                schema: "public",
                newName: "master_code");

            migrationBuilder.RenameTable(
                name: "manufactory",
                schema: "public",
                newName: "manufactory");

            migrationBuilder.RenameTable(
                name: "log_exception",
                schema: "public",
                newName: "log_exception");

            migrationBuilder.RenameTable(
                name: "log_action",
                schema: "public",
                newName: "log_action");

            migrationBuilder.RenameTable(
                name: "item_sale_prices",
                schema: "public",
                newName: "item_sale_prices");

            migrationBuilder.RenameTable(
                name: "item_price",
                schema: "public",
                newName: "item_price");

            migrationBuilder.RenameTable(
                name: "item_edi",
                schema: "public",
                newName: "item_edi");

            migrationBuilder.RenameTable(
                name: "item_buy_prices",
                schema: "public",
                newName: "item_buy_prices");

            migrationBuilder.RenameTable(
                name: "item",
                schema: "public",
                newName: "item");

            migrationBuilder.RenameTable(
                name: "inventory",
                schema: "public",
                newName: "inventory");

            migrationBuilder.RenameTable(
                name: "function",
                schema: "public",
                newName: "function");

            migrationBuilder.RenameTable(
                name: "company",
                schema: "public",
                newName: "company");

            migrationBuilder.RenameTable(
                name: "classifications",
                schema: "public",
                newName: "classifications");

            migrationBuilder.RenameTable(
                name: "category",
                schema: "public",
                newName: "category");

            migrationBuilder.RenameTable(
                name: "calendars",
                schema: "public",
                newName: "calendars");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "admin_id",
            //    table: "machine",
            //    type: "uuid",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "item_name2",
                table: "item_edi",
                type: "character varying(160)",
                maxLength: 160,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(160)",
                oldMaxLength: 160,
                oldNullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_machine_admin_id",
            //    table: "machine",
            //    column: "admin_id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_machine_user_admin_id",
            //    table: "machine",
            //    column: "admin_id",
            //    principalTable: "user",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
