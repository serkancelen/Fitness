using Helpers;
using static Helpers.Enums;

namespace Fitness.Entities.Models
{
    public class ProgressLog
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime LogDate { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal BodyFatPercentage { get; set; }
        public FitnessLevel FitnessLevel { get; set; }
        public int UserId { get; set; }

    }
}
