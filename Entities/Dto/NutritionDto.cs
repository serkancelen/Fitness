using static Helpers.Enums;

namespace Fitness.Entities.Dto
{
    public class NutritionDto
    {
        public int Id { get; set; } 
        public string NutritionName { get; set; }
        public DateTime LogDate { get; set; }
        public NutritionType NutritionType { get; set; }
        public decimal Calories { get; set; }
        public int UserId { get; set; }
        public List<FoodItemDto> FoodItems { get; set; }
    }
}