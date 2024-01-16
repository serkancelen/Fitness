using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Entities.Models
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId { get; set; }
        public string Name { get; set; }
        public decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbonhydrates { get; set; }
        public decimal Fat { get; set; }
        public decimal Quantity { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
