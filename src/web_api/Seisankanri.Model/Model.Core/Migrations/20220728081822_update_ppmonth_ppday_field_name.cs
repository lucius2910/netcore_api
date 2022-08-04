using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class update_ppmonth_ppday_field_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "machine",
                schema: "public",
                table: "product_plan_month",
                newName: "machine_names");

            migrationBuilder.RenameColumn(
                name: "item_div",
                schema: "public",
                table: "product_plan_month",
                newName: "item_type");

            migrationBuilder.RenameColumn(
                name: "item_div",
                schema: "public",
                table: "product_plan_day",
                newName: "item_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "machine_names",
                schema: "public",
                table: "product_plan_month",
                newName: "machine");

            migrationBuilder.RenameColumn(
                name: "item_type",
                schema: "public",
                table: "product_plan_month",
                newName: "item_div");

            migrationBuilder.RenameColumn(
                name: "item_type",
                schema: "public",
                table: "product_plan_day",
                newName: "item_div");
        }
    }
}
