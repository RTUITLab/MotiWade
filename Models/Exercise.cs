using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Exercise
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ResourceLink { get; set; }
        [Required]
        public int Iterations { get; set; }
        public TimeTechType TimeTechType { get; set; }
        public DateTime Deadline { get; set; }
        public List<UserToExercise> UserToExercises { get; set; }
        public int Score { get; set; }
        public int WorkTime { get; set; } = 20;
        public int FreeTime { get; set; } = 5;
    }
}
