using Fitness.Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fitness.Entities
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string ExerciseName { get; set; }

        public int DurationMinutes { get; set; }

        public DateTime EntryDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
