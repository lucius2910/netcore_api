using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class udpate_item_edi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "item_edi",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_type = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    item_name1 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    item_name2 = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    edi_cd = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    export_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    del_flg = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_edi", x => x.id);
                    table.ForeignKey(
                        name: "FK_item_edi_company_company_cd",
                        column: x => x.company_cd,
                        principalTable: "company",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_item_edi_item_item_cd",
                        column: x => x.item_cd,
                        principalTable: "item",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_item_edi_company_cd",
                table: "item_edi",
                column: "company_cd");

            migrationBuilder.CreateIndex(
                name: "IX_item_edi_item_cd",
                table: "item_edi",
                column: "item_cd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item_edi");
        }
    }
}
