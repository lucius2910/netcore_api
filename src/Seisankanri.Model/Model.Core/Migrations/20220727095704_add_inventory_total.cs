using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class add_inventory_total : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventory_total",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    branch_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    inventory_k = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    inventory_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_div = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    inspection_k = table.Column<bool>(type: "boolean", maxLength: 2, nullable: false),
                    location = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    lot_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    whin_dt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    stock_qty = table.Column<int>(type: "integer", nullable: true),
                    stock_amt = table.Column<int>(type: "integer", nullable: true),
                    silo_k = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    remark = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    curmth_whin_qty1 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whin_qty2 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whin_qty3 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whin_qty4 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whin_qty5 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whin_qty6 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whin_qty7 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whin_qty8 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whin_qty9 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whin_qty10 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty1 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty2 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty3 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty4 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty5 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty6 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty7 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty8 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty9 = table.Column<int>(type: "integer", nullable: true),
                    curmth_whout_qty10 = table.Column<int>(type: "integer", nullable: true),
                    nextmth_whin_qty = table.Column<int>(type: "integer", nullable: true),
                    nextmth_whout_qty = table.Column<int>(type: "integer", nullable: true),
                    whin_qty_plan_pp = table.Column<int>(type: "integer", nullable: true),
                    whin_qty_plan_po = table.Column<int>(type: "integer", nullable: true),
                    last_whin_date = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    last_whout_date = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    last_invt_date = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    stock_cutoff_date = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_total", x => x.id);
                    table.ForeignKey(
                        name: "FK_inventory_total_item_item_cd",
                        column: x => x.item_cd,
                        principalSchema: "public",
                        principalTable: "item",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inventory_total_item_cd",
                schema: "public",
                table: "inventory_total",
                column: "item_cd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventory_total",
                schema: "public");
        }
    }
}
