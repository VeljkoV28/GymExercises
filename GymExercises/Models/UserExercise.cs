using System;

public class UserExercise : Entity
{
    public int UserID { get; set; }
    public User User { get; set; }
    public int ExerciseID { get; set; }
    public Exercise Exercise { get; set; }
    public DateTime ExerciseDate { get; set; }
    public int Duration { get; set; }  // Duration in minutes
}

