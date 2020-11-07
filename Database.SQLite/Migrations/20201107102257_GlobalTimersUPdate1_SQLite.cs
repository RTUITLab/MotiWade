using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.SQLite.Migrations
{
    public partial class GlobalTimersUPdate1_SQLite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "expired",
                table: "GlobalTimers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expired",
                table: "GlobalTimers");
        }
    }
}
