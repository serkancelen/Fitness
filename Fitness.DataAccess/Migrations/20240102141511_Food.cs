using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Food : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FoodItems",
                newName: "FoodItemId");

            migrationBuilder.AddColumn<int>(
                name: "FoodItemId",
                table: "NutritionEntries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NutritionEntries_FoodItemId",
                table: "NutritionEntries",
                column: "FoodItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionEntries_FoodItems_FoodItemId",
                table: "NutritionEntries",
                column: "FoodItemId",
                principalTable: "FoodItems",
                principalColumn: "FoodItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionEntries_FoodItems_FoodItemId",
                table: "NutritionEntries");

            migrationBuilder.DropIndex(
                name: "IX_NutritionEntries_FoodItemId",
                table: "NutritionEntries");

            migrationBuilder.DropColumn(
                name: "FoodItemId",
                table: "NutritionEntries");

            migrationBuilder.RenameColumn(
                name: "FoodItemId",
                table: "FoodItems",
                newName: "Id");
        }
    }
}
