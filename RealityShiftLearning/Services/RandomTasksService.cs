using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityShiftLearning.Services
{
    public class RandomTasksService
    {
        private readonly List<string> names = new List<string>()
        {
            "English Literature: Translation",
            "Russian Literature: Essay",
            "Data Science: Practice",
            "UX/UI Design: Moodboard",
            "Python Programming"
        };

        private readonly Random random = new Random();
        public Exercise GetRandomExercise()
        {
            return new Exercise
            {
                Iterations = random.Next(5),
                TimeTechType = TimeTechType.Tomato,
                ResourceLink = "https://motiwade.rtuitlab.dev",
                Title = names[random.Next(names.Count)]
            };
        }
    }
}
