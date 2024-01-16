using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Entities.Dto
{
    public class FoodItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbonhydrates { get; set; }
        public decimal Fat { get; set; }
        public decimal Quantity { get; set; }
        public int UserId { get; set; }
    }
}
