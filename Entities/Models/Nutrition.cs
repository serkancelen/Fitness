using Fitness.Entities.Models;
using static Helpers.Enums;

namespace Fitness.Entities.Models
{
    public class Nutrition
    {
        public int Id { get; set; }
        public string NutritionName { get; set; }
        public decimal Calories { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public NutritionType NutritionType { get; set; }
        public List<FoodItem> FoodItems { get; set; }
    }
}