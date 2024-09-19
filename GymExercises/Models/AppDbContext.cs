using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

public class AppDbContext: DbContext
{

    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserExercise> UserExercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Dodavanje početnih podataka
        modelBuilder.Entity<Exercise>().HasData(
            new Exercise { ID = 1, Name = "Bench Press", Description = "A compound exercise that targets the chest, shoulders, and triceps.", MuscleGroup = "Chest", Equipment = "Barbell", DifficultyLevel = "Intermediate" },
            new Exercise { ID = 2, Name = "Squat", Description = "A compound exercise that targets the legs and glutes.", MuscleGroup = "Legs", Equipment = "Barbell", DifficultyLevel = "Intermediate" }
        );

        modelBuilder.Entity<User>().HasData(
            new User { ID = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Password = "password123" },
            new User { ID = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Password = "password456" }
        );

        modelBuilder.Entity<UserExercise>().HasData(
            new UserExercise { ID = 1, UserID = 1, ExerciseID = 1, ExerciseDate = new DateTime(2024, 6, 20), Duration = 60 },
            new UserExercise { ID = 2, UserID = 2, ExerciseID = 2, ExerciseDate = new DateTime(2024, 6, 21), Duration = 45 }
        );
    }
}
