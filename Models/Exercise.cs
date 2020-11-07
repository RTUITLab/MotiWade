using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ResourceLink { get; set; }
        public int Interations { get; set; }
        public TimeTechType TimeTechType { get; set; }

        public List<UserToExercise> UserToExercises { get; set; }

    }
}
