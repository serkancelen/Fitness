using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fooditem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutrition_FoodItems_FoodItemId",
                table: "Nutrition");

            migrationBuilder.DropIndex(
                name: "IX_Nutrition_FoodItemId",
                table: "Nutrition");

            migrationBuilder.DropColumn(
                name: "FoodItemId",
                table: "Nutrition");

            migrationBuilder.AddColumn<int>(
                name: "NutritionId",
                table: "FoodItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FoodItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_NutritionId",
                table: "FoodItems",
                column: "NutritionId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_UserId",
                table: "FoodItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Nutrition_NutritionId",
                table: "FoodItems",
                column: "NutritionId",
                principalTable: "Nutrition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Users_UserId",
                table: "FoodItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Nutrition_NutritionId",
                table: "FoodItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Users_UserId",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_NutritionId",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_UserId",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "NutritionId",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FoodItems");

            migrationBuilder.AddColumn<int>(
                name: "FoodItemId",
                table: "Nutrition",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nutrition_FoodItemId",
                table: "Nutrition",
                column: "FoodItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrition_FoodItems_FoodItemId",
                table: "Nutrition",
                column: "FoodItemId",
                principalTable: "FoodItems",
                principalColumn: "FoodItemId");
        }
    }
}
