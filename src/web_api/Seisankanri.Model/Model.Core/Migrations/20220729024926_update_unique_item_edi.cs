using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class update_unique_item_edi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_item_edi_company_cd",
                schema: "public",
                table: "item_edi");

            migrationBuilder.AlterColumn<int>(
                name: "order",
                schema: "public",
                table: "item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_item_edi_company_cd_item_cd_edi_cd",
                schema: "public",
                table: "item_edi",
                columns: new[] { "company_cd", "item_cd", "edi_cd" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_item_edi_company_cd_item_cd_edi_cd",
                schema: "public",
                table: "item_edi");

            migrationBuilder.AlterColumn<int>(
                name: "order",
                schema: "public",
                table: "item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_item_edi_company_cd",
                schema: "public",
                table: "item_edi",
                column: "company_cd");
        }
    }
}
