using System;
using static Helpers.Enums;

namespace Fitness.Entities.Dto
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }
        public ExerciseType exerciseType { get; set; }
    }
}
