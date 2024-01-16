using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Nutri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Users_UserId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Workouts_WorkoutId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Nutrition_NutritionId",
                table: "FoodItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Nutrition_Users_UserId",
                table: "Nutrition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nutrition",
                table: "Nutrition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "FitnessLevel",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Nutrition",
                newName: "Nutritions");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_Nutrition_UserId",
                table: "Nutritions",
                newName: "IX_Nutritions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercise_WorkoutId",
                table: "Exercises",
                newName: "IX_Exercises_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercise_UserId",
                table: "Exercises",
                newName: "IX_Exercises_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nutritions",
                table: "Nutritions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Users_UserId",
                table: "Exercises",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Nutritions_NutritionId",
                table: "FoodItems",
                column: "NutritionId",
                principalTable: "Nutritions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nutritions_Users_UserId",
                table: "Nutritions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Users_UserId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Nutritions_NutritionId",
                table: "FoodItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Nutritions_Users_UserId",
                table: "Nutritions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nutritions",
                table: "Nutritions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "Nutritions",
                newName: "Nutrition");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "Exercise");

            migrationBuilder.RenameIndex(
                name: "IX_Nutritions_UserId",
                table: "Nutrition",
                newName: "IX_Nutrition_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercise",
                newName: "IX_Exercise_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_UserId",
                table: "Exercise",
                newName: "IX_Exercise_UserId");

            migrationBuilder.AddColumn<int>(
                name: "FitnessLevel",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nutrition",
                table: "Nutrition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Users_UserId",
                table: "Exercise",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Workouts_WorkoutId",
                table: "Exercise",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Nutrition_NutritionId",
                table: "FoodItems",
                column: "NutritionId",
                principalTable: "Nutrition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrition_Users_UserId",
                table: "Nutrition",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
