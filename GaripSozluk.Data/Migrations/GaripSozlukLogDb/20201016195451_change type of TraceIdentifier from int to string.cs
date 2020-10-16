using Microsoft.EntityFrameworkCore.Migrations;

namespace GaripSozluk.Data.Migrations.GaripSozlukLogDb
{
    public partial class changetypeofTraceIdentifierfrominttostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TraceIdentifier",
                table: "Logs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TraceIdentifier",
                table: "Logs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
