using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Postgres.Migrations
{
    public partial class AddFinishAndEmotion_Postgres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Emotion",
                table: "UserToExercises",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "FinishTime",
                table: "UserToExercises",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emotion",
                table: "UserToExercises");

            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "UserToExercises");
        }
    }
}
