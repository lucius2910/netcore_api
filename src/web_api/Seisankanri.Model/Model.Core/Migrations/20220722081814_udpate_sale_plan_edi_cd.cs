using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class udpate_sale_plan_edi_cd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_item_edi_edi_cd",
                table: "item_edi",
                column: "edi_cd");

            migrationBuilder.CreateIndex(
                name: "IX_sale_plan_item_edi_cd",
                table: "sale_plan",
                column: "item_edi_cd");

            migrationBuilder.AddForeignKey(
                name: "FK_sale_plan_item_edi_item_edi_cd",
                table: "sale_plan",
                column: "item_edi_cd",
                principalTable: "item_edi",
                principalColumn: "edi_cd",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sale_plan_item_edi_item_edi_cd",
                table: "sale_plan");

            migrationBuilder.DropIndex(
                name: "IX_sale_plan_item_edi_cd",
                table: "sale_plan");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_item_edi_edi_cd",
                table: "item_edi");
        }
    }
}
