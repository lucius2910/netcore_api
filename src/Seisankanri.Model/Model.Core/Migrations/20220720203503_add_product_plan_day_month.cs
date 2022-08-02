using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class add_product_plan_day_month : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "item_name2",
                table: "sale_plan",
                type: "character varying(160)",
                maxLength: 160,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(160)",
                oldMaxLength: 160);

            migrationBuilder.CreateTable(
                name: "product_plan_day",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    plan_day = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    item_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_name1 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    item_name2 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    to_be_sold = table.Column<int>(type: "integer", nullable: true),
                    machine_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    production_schedule = table.Column<int>(type: "integer", nullable: true),
                    production_results = table.Column<int>(type: "integer", nullable: true),
                    shipment_results = table.Column<int>(type: "integer", nullable: true),
                    estimate_shipment_results = table.Column<int>(type: "integer", nullable: true),
                    estimate_inventory = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_plan_day", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_plan_day_item_item_cd",
                        column: x => x.item_cd,
                        principalTable: "item",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_plan_day_machine_machine_cd",
                        column: x => x.machine_cd,
                        principalTable: "machine",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_plan_month",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    plan_month = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    item_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_div = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_name1 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    item_name2 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    machine = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    pre_month_inventory = table.Column<int>(type: "integer", nullable: true),
                    to_be_sold = table.Column<int>(type: "integer", nullable: true),
                    production_schedule = table.Column<int>(type: "integer", nullable: true),
                    production_results = table.Column<int>(type: "integer", nullable: true),
                    shipment_results = table.Column<int>(type: "integer", nullable: true),
                    estimate_inventory = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_plan_month", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_plan_month_item_item_cd",
                        column: x => x.item_cd,
                        principalTable: "item",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_plan_day_item_cd",
                table: "product_plan_day",
                column: "item_cd");

            migrationBuilder.CreateIndex(
                name: "IX_product_plan_day_machine_cd",
                table: "product_plan_day",
                column: "machine_cd");

            migrationBuilder.CreateIndex(
                name: "IX_product_plan_month_item_cd",
                table: "product_plan_month",
                column: "item_cd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_plan_day");

            migrationBuilder.DropTable(
                name: "product_plan_month");

            migrationBuilder.AlterColumn<string>(
                name: "item_name2",
                table: "sale_plan",
                type: "character varying(160)",
                maxLength: 160,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(160)",
                oldMaxLength: 160,
                oldNullable: true);
        }
    }
}
