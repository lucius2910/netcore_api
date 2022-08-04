using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class remove_ro_req : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "receive_order",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15,
                oldDefaultValueSql: "replace(concat(TO_CHAR(now(),'yyyyMMdd'), TO_CHAR(nextval('sq_receive_order'),'0000000')), ' ', '')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "receive_order",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValueSql: "replace(concat(TO_CHAR(now(),'yyyyMMdd'), TO_CHAR(nextval('sq_receive_order'),'0000000')), ' ', '')",
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);
        }
    }
}
