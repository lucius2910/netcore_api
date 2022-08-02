using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class update_item_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "item_type",
                table: "item",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_machine_code",
                table: "machine",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_item_company_cd",
                table: "item",
                column: "company_cd");

            migrationBuilder.CreateIndex(
                name: "IX_item_item_type",
                table: "item",
                column: "item_type");

            migrationBuilder.CreateIndex(
                name: "IX_item_machine_cd",
                table: "item",
                column: "machine_cd");

            migrationBuilder.CreateIndex(
                name: "IX_item_place_cd",
                table: "item",
                column: "place_cd");

            migrationBuilder.AddForeignKey(
                name: "FK_item_classifications_item_type",
                table: "item",
                column: "item_type",
                principalTable: "classifications",
                principalColumn: "code2",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_item_company_company_cd",
                table: "item",
                column: "company_cd",
                principalTable: "company",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_item_company_place_cd",
                table: "item",
                column: "place_cd",
                principalTable: "company",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_item_machine_machine_cd",
                table: "item",
                column: "machine_cd",
                principalTable: "machine",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_item_classifications_item_type",
                table: "item");

            migrationBuilder.DropForeignKey(
                name: "FK_item_company_company_cd",
                table: "item");

            migrationBuilder.DropForeignKey(
                name: "FK_item_company_place_cd",
                table: "item");

            migrationBuilder.DropForeignKey(
                name: "FK_item_machine_machine_cd",
                table: "item");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_machine_code",
                table: "machine");

            migrationBuilder.DropIndex(
                name: "IX_item_company_cd",
                table: "item");

            migrationBuilder.DropIndex(
                name: "IX_item_item_type",
                table: "item");

            migrationBuilder.DropIndex(
                name: "IX_item_machine_cd",
                table: "item");

            migrationBuilder.DropIndex(
                name: "IX_item_place_cd",
                table: "item");

            migrationBuilder.AlterColumn<string>(
                name: "item_type",
                table: "item",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);
        }
    }
}
