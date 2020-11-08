using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserToExercise
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public MotiWadeUser User { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public ExerciseProgress ExerciseProgress { get; set; }

        public int? TomatoTimerId { get; set; }
        public TomatoTimer TomatoTimer { get; set; }
    }
}
