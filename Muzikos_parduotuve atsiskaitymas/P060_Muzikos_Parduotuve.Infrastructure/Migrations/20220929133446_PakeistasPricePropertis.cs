using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muzikos_Parduotuve.Infrastructure.Migrations
{
    public partial class PakeistasPricePropertis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "tracks",
                type: "NUMERIC(10,2)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "NUMERIC(10,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "UnitPrice",
                table: "tracks",
                type: "NUMERIC(10,2)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(double),
                oldType: "NUMERIC(10,2)",
                oldNullable: true);
        }
    }
}
