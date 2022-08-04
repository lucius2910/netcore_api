using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Core.Migrations
{
    public partial class update_received_order_sq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "sq_receive_order",
                minValue: 1L);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "receive_order",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValueSql: "nextval('sq_receive_order')",
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "sq_receive_order");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "receive_order",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15,
                oldDefaultValueSql: "nextval('sq_receive_order')");
        }
    }
}
