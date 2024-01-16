using Fitness.Entities;
using Fitness.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.DataAccess
{
    public class FitnessDbContext : DbContext
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options) : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Nutrition> Nutritions { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProgressLog> ProgressLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.Property(e => e.Calories)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Carbonhydrates)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Fat)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Protein)
                    .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<ProgressLog>(entity =>
            {
                entity.Property(e => e.BodyFatPercentage)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Height)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Nutrition>(entity =>
            {
                entity.Property(e => e.Calories)
                    .HasColumnType("decimal(18,2)");
            });
        }

    }
}
