using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.SQLite.Migrations
{
    public partial class ExerciseModel_SQLite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    ResourceLink = table.Column<string>(nullable: true),
                    Interations = table.Column<int>(nullable: false),
                    TimeTechType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserToExercises",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    ExerciseProgress = table.Column<int>(nullable: false),
                    TomatoTimerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToExercises", x => new { x.UserId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_UserToExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToExercises_GlobalTimers_TomatoTimerId",
                        column: x => x.TomatoTimerId,
                        principalTable: "GlobalTimers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserToExercises_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToExercises_ExerciseId",
                table: "UserToExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToExercises_TomatoTimerId",
                table: "UserToExercises",
                column: "TomatoTimerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToExercises");

            migrationBuilder.DropTable(
                name: "Exercises");
        }
    }
}
