using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;



namespace DataAccess
{
    public class FitnessDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Nutrition> Nutritions { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProgressLog> ProgressLogs { get; set; }

        public FitnessDbContext(DbContextOptions<FitnessDbContext> options) : base(options)
        {
        }
    }
}