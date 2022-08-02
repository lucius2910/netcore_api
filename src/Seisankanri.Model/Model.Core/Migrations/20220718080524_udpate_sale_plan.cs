using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class udpate_sale_plan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_sale_plan_company_cd",
                table: "sale_plan",
                column: "company_cd");

            migrationBuilder.CreateIndex(
                name: "IX_sale_plan_item_cd",
                table: "sale_plan",
                column: "item_cd");

            migrationBuilder.AddForeignKey(
                name: "FK_sale_plan_company_company_cd",
                table: "sale_plan",
                column: "company_cd",
                principalTable: "company",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sale_plan_item_item_cd",
                table: "sale_plan",
                column: "item_cd",
                principalTable: "item",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sale_plan_company_company_cd",
                table: "sale_plan");

            migrationBuilder.DropForeignKey(
                name: "FK_sale_plan_item_item_cd",
                table: "sale_plan");

            migrationBuilder.DropIndex(
                name: "IX_sale_plan_company_cd",
                table: "sale_plan");

            migrationBuilder.DropIndex(
                name: "IX_sale_plan_item_cd",
                table: "sale_plan");
        }
    }
}
