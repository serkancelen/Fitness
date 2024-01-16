using System;
using System.Collections.Generic;
using static Helpers.Enums;

namespace Fitness.Entities.Dto
{
    public class WorkoutDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<ExerciseDto> Exercises { get; set; }
        public string Intensity { get; set; }
        public string Notes { get; set; }
        public double CaloriesBurned { get; set; }
        public double Distance { get; set; }
        public int Duration { get; set; }
        public WorkoutStatus workoutStatus { get; set; }
    }
}
