using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Database.Postgres.Migrations
{
    public partial class TomatoLink_Postgres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToExercises_GlobalTimers_TomatoTimerId",
                table: "UserToExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToExercises",
                table: "UserToExercises");

            migrationBuilder.DropIndex(
                name: "IX_UserToExercises_TomatoTimerId",
                table: "UserToExercises");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserToExercises",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserToExerciseId",
                table: "GlobalTimers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToExercises",
                table: "UserToExercises",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserToExercises_UserId_ExerciseId",
                table: "UserToExercises",
                columns: new[] { "UserId", "ExerciseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GlobalTimers_UserToExerciseId",
                table: "GlobalTimers",
                column: "UserToExerciseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalTimers_UserToExercises_UserToExerciseId",
                table: "GlobalTimers",
                column: "UserToExerciseId",
                principalTable: "UserToExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalTimers_UserToExercises_UserToExerciseId",
                table: "GlobalTimers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToExercises",
                table: "UserToExercises");

            migrationBuilder.DropIndex(
                name: "IX_UserToExercises_UserId_ExerciseId",
                table: "UserToExercises");

            migrationBuilder.DropIndex(
                name: "IX_GlobalTimers_UserToExerciseId",
                table: "GlobalTimers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserToExercises");

            migrationBuilder.DropColumn(
                name: "UserToExerciseId",
                table: "GlobalTimers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToExercises",
                table: "UserToExercises",
                columns: new[] { "UserId", "ExerciseId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserToExercises_TomatoTimerId",
                table: "UserToExercises",
                column: "TomatoTimerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserToExercises_GlobalTimers_TomatoTimerId",
                table: "UserToExercises",
                column: "TomatoTimerId",
                principalTable: "GlobalTimers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
