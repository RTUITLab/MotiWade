using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Postgres.Migrations
{
    public partial class ExerciseIterations_Postgres : Migration
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
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
