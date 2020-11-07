using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.SQLite.Migrations
{
    public partial class ExerciseIterations_SQLite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interations",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "Iterations",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iterations",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "Interations",
                table: "Exercises",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
