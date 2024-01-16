using Helpers;

namespace Fitness.Entities.Models
{
    public class Workout
    {
        public int WorkoutId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public List<Exercise> Exercises { get; set; }
        public string Intensity { get; set; }
        public string Notes { get; set; }
        public double CaloriesBurned { get; set; }
        public double Distance { get; set; }
        public int Duration { get; set; }
    }
}
