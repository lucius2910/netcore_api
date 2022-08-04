using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class udpate_fk_calendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_calendars_company_companyid",
                table: "calendars");

            migrationBuilder.DropIndex(
                name: "IX_calendars_companyid",
                table: "calendars");

            migrationBuilder.DropColumn(
                name: "companyid",
                table: "calendars");

            migrationBuilder.CreateIndex(
                name: "IX_calendars_company_cd",
                table: "calendars",
                column: "company_cd");

            migrationBuilder.AddForeignKey(
                name: "FK_calendars_company_company_cd",
                table: "calendars",
                column: "company_cd",
                principalTable: "company",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_calendars_company_company_cd",
                table: "calendars");

            migrationBuilder.DropIndex(
                name: "IX_calendars_company_cd",
                table: "calendars");

            migrationBuilder.AddColumn<Guid>(
                name: "companyid",
                table: "calendars",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_calendars_companyid",
                table: "calendars",
                column: "companyid");

            migrationBuilder.AddForeignKey(
                name: "FK_calendars_company_companyid",
                table: "calendars",
                column: "companyid",
                principalTable: "company",
                principalColumn: "id");
        }
    }
}
