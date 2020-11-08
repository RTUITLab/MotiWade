using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Postgres.Migrations
{
    public partial class AddTime_Postgres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreeTime",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkTime",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeTime",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "WorkTime",
                table: "Exercises");
        }
    }
}
