using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muzikos_Parduotuve.Infrastructure.Migrations
{
    public partial class IstaisytasduomenuBazesPakeitimasInvoiceItemClaseje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "invoice_items",
                type: "NUMERIC(10,2)",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "NUMERIC(10,2)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "invoice_items",
                type: "NUMERIC(10,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "NUMERIC(10,2)");
        }
    }
}
