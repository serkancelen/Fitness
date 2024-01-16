using Helpers;

namespace Fitness.Entities.Dto
{
    public class ProgressLogDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LogDate { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal BodyFatPercentage { get; set; }
        public Enums.FitnessLevel FitnessLevel { get; set; }
    }
}
