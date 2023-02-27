using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muzikos_Parduotuve.Infrastructure.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "tracks",
                type: "TEXT",
                nullable: false,
                defaultValue: "Active");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tracks");
        }
    }
}
